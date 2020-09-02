using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How mach time before restart the game?")]
    public float waitTime = 2.0f;

    [Tooltip("Variable that count the number of obstacles destroyed")]
    public static int obstaclesDestroiedCount = 0;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
    private void OnCollisionEnter(Collision collision)
    {
        // Check if it is the player
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            // If its player is using dash power, it is indestructible
            if (PlayerBehaviour.indestructible)
            {
                DestroyObject();
            }

            // If it is not using dash, the player is killed
            else
            {
                collision.gameObject.SetActive(false);
                player = collision.gameObject;
                Invoke("GameReset", waitTime);
            }
        }
    }

    /// <summary>
    /// Method called to restart the level
    /// </summary>
    void GameReset()
    {

        var menuGameOver = GetMenuGameOver();
        menuGameOver.SetActive(true);
        var buttons = menuGameOver.transform.GetComponentsInChildren<Button>();
        Button continueButton = null;

        foreach (var button in buttons)
        {
            if (button.gameObject.name.Equals(""))
            {
                continueButton = button;
                break;
            }
        }

        if (continueButton)
        {
            StartCoroutine(ShowContinue(continueButton));

            //continueButton.onClick.AddListener(AdsControl.ShowRewardAd);
            //AdsControl.obstacle = this;
        }

        // Actually restart the level and its global variables
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //EndTileBehaviour.distanceControl = 0;
        //PlayerBehaviour.dashControl = true;
        //PlayerBehaviour.indestructible = false;
    }

    public void Continue()
    {
        var go = GetMenuGameOver();
        go.SetActive(false);
        player.SetActive(true);
        DestroyObject();
        obstaclesDestroiedCount--;
    }

    GameObject GetMenuGameOver()
    {
        return GameObject.Find("Canvas").transform.Find("Game Over Menu").gameObject;
    }

    /// <summary>
    /// Method called to destroy the obstacle and count this destruction
    /// </summary>
    public void DestroyObject()
    {
        obstaclesDestroiedCount++;
        Destroy(gameObject);
    }

    public IEnumerator ShowContinue(Button continueButton)
    {
        var btnText = continueButton.GetComponentInChildren<Text>();
        while (true)
        {
            if (AdsControl.rewardCooldown.HasValue && 
                (DateTime.Now < AdsControl.rewardCooldown.Value))
            {
                continueButton.interactable = false;
                TimeSpan missing = AdsControl.rewardCooldown.Value - DateTime.Now;
                var countdown = string.Format("{0:D2}:{1:D2}", missing.Minutes, missing.Seconds);
                btnText.text = countdown;
                yield return new WaitForSeconds(1f);
            }
            else
            {
                continueButton.interactable = true;
                continueButton.onClick.AddListener(AdsControl.ShowRewardAd);
                AdsControl.obstacle = this;
                btnText.text = "Continue (See Ad)";
                break;
            }
        }
    }
}
