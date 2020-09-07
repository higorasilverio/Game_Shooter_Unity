using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;


public class AdsControl : MonoBehaviour
{

    [Tooltip("Advertisements control")]
    public static bool showAds = true;

    [Tooltip("Reference to the prefab obstacle")]
    public static ObstacleBehaviour obstacle;

    [Tooltip("Reference to the time we use to avoid multiple pay to recover")]
    public static DateTime? rewardCooldown = null;

    //Method used to create ads options
    public static void ShowAd()
    {
        ShowOptions options = new ShowOptions();
        // In some code I have inserted this pragma comment do avoid useless warnigs 
#pragma warning disable
        options.resultCallback = Unpause;
#pragma warning restore
        if (Advertisement.IsReady())
        {
            Advertisement.Show(options);
        }
        PauseMenu.paused = true;
        Time.timeScale = 0;
    }

    // Method used to unpause the game
    public static void Unpause(ShowResult result)
    {
        PauseMenu.paused = false;
        Time.timeScale = 1f;
    }

    // Method used to show ads with reward, waiting some time between then
    public static void ShowRewardAd()
    {
        rewardCooldown = DateTime.Now.AddSeconds(15);
        if (Advertisement.IsReady())
        {
            PauseMenu.paused = true;
            Time.timeScale = 0f;
            var options = new ShowOptions
            {
#pragma warning disable
                resultCallback = HandleShowResults
#pragma warning restore
            };
            Advertisement.Show(options);
        }
    }

    // Method used to handle the the ads choise. We only use one option 'ShowResult.Finished'
    public static void HandleShowResults(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                obstacle.Continue();
                break;
        }
        PauseMenu.paused = false;
        Time.timeScale = 1f;
    }

}
