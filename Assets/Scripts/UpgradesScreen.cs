using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradesScreen : MonoBehaviour
{
    /// <summary>
    /// Cost, in coins, to reduce the time between dashes
    /// </summary>
    private static int dashCountdownCost = 15;

    /// <summary>
    /// Cost, in coins, to increase the number of obstacles destroyed in a single dash
    /// </summary>
    private static int dashDestructionCost = 15;

    [Tooltip("Button to upgrade time between dashes")]
    public Button dashCount;

    [Tooltip("Button to upgrade number of obstacles destroyed in a single dash")]
    public Button destroyCount;

    /// <summary>
    /// Function to load the Game Scene
    /// </summary>
    /// <param name="sceneName"> Game Scene call</param>
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Function to load the Initial Screen
    /// </summary>
    /// <param name="sceneName"> Initial Screeen call</param>
    public void LoadInitialScreen(string sceneName)
    {
        PlayerBehaviour.speed = 5.0f;
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        // Convert and show the number of coins own
        var totalCoins = CoinBehaviour.coinsCatch.ToString();
        GameObject.Find("Canvas").transform.Find("Coins Panel").transform.Find("Text").GetComponentInChildren<Text>().text = totalCoins;

        // Conver and show the seconds between dashes already in use
        var countdownTime = PlayerBehaviour.dashTimerCountdown.ToString(); 
        GameObject.Find("Canvas").transform.Find("Status").transform.Find("Countdown Text").GetComponentInChildren<Text>().text = countdownTime;

        // Conver and show the number of obstacles destroyed in a single dash already in use
        var destroyCount = PlayerBehaviour.dashDestroyControl.ToString();
        GameObject.Find("Canvas").transform.Find("Status").transform.Find("Destruction Text").GetComponentInChildren<Text>().text = destroyCount;

        // Conver and show the cost to upgrade dash countdown
        var countdownCost = dashCountdownCost.ToString();
        GameObject.Find("Canvas").transform.Find("Coins Cost Countdown Panel").transform.Find("Text").GetComponentInChildren<Text>().text = countdownCost;
        
        // Conver and show the cost to upgrade dash destruction
        var destroyCost = dashDestructionCost.ToString();
        GameObject.Find("Canvas").transform.Find("Coins Cost Destruction Panel").transform.Find("Text").GetComponentInChildren<Text>().text = destroyCost;

        // With three upgrades there is no more cost, because the button is disabla
        if (PlayerBehaviour.dashTimerCountdown <= 2)
        {
            GameObject.Find("Canvas").transform.Find("Coins Cost Countdown Panel").transform.Find("Text").GetComponentInChildren<Text>().text = "-";
        }

        // With three upgrades there is no more cost, because the button is disabla
        if (PlayerBehaviour.dashDestroyControl >= 5)
        {
            GameObject.Find("Canvas").transform.Find("Coins Cost Destruction Panel").transform.Find("Text").GetComponentInChildren<Text>().text = "-";
        }

    }

    /// <summary>
    /// Function to performe the upgrade in dash count down
    /// </summary>
    public void UpdateDashCountdown()
    {
        if (CoinBehaviour.coinsCatch >= dashCountdownCost && PlayerBehaviour.dashTimerCountdown > 2)
        {
            CoinBehaviour.coinsCatch = CoinBehaviour.coinsCatch - dashCountdownCost;
            PlayerBehaviour.dashTimerCountdown--;
            dashCountdownCost = dashCountdownCost * 2;
        }

        // With three upgrades, or less than 2 seconds between dashes, the button is disable
        if (PlayerBehaviour.dashTimerCountdown <= 2)
        {
            dashCount.interactable = false;
        }
    }

    /// <summary>
    /// Function to performe the upgrade in dash destruction count
    /// </summary>
    public void UpdateDashDestructionCount()
    {
        if (CoinBehaviour.coinsCatch >= dashDestructionCost && PlayerBehaviour.dashDestroyControl < 5)
        {
            CoinBehaviour.coinsCatch = CoinBehaviour.coinsCatch - dashDestructionCost;
            PlayerBehaviour.dashDestroyControl++;
            dashDestructionCost = dashDestructionCost * 2;
        }

        // With three upgrades, or more than 4 obstacles per dash, the button is disable
        if (PlayerBehaviour.dashDestroyControl >= 5)
        {
            destroyCount.interactable = false;
        }

    }
}
