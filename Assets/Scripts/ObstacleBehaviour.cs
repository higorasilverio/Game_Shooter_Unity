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

    /// <summary>
    /// Reference to the Player Game Object
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Reference to the sound used when the Player Game Object hit the Obstacle Game Object
    /// </summary>
    public AudioClip clip;
            
    private void OnCollisionEnter(Collision collision)
    {
        // Check if it is the Player that has collided with the Obstacle
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            // Calls the AudioClip of the hit
            AudioSource.PlayClipAtPoint(clip, collision.transform.position, 1.0f);
            // If its player is using dash power, it is indestructible and the Obstacle is destroyed
            if (PlayerBehaviour.indestructible)
            {
                DestroyObject();
            }

            // If it is not using dash, the player is hidden, to show some options 
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
        // Selec the button pressed when stopped by a obstacle, shown the Game Over menu
        var menuGameOver = GetMenuGameOver();
        menuGameOver.SetActive(true);
        var buttons = menuGameOver.transform.GetComponentsInChildren<Button>();
        Button continueButton = null;

        foreach (var button in buttons)
        {
            if (button.gameObject.name.Equals("Continue Ad Button"))
            {
                continueButton = button;
                break;
            }
        }

        if (continueButton)
        {
            PlayerBehaviour.dashControl = true;
            PlayerBehaviour.indestructible = false;
            StartCoroutine(ShowContinue(continueButton));
        }
    }

    // If the player hits Continue, it is shown and the game continues
    public void Continue()
    {
        var go = GetMenuGameOver();
        go.SetActive(false);
        player.SetActive(true);
        DestroyObject();
        obstaclesDestroiedCount--;
    }

    /// <summary>
    /// Function used to get the Game Over Menu into the Canvas object
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Coroutine used to set some time between the uses of the Ad with reward
    /// and show the Player its cooldown time
    /// </summary>
    /// <param name="continueButton"> Reference to the Button Continue show in the Game Over Menu </param>
    /// <returns></returns>
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
