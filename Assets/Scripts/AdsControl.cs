using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.Advertisements;


public class AdsControl : MonoBehaviour
{
    [Tooltip("Advertisements control")]
    public static bool showAds = true;

    public static ObstacleBehaviour obstacle;

    public static DateTime? rewardCooldown = null;

    public static void ShowAd()
    {

        ShowOptions options = new ShowOptions();

        options.resultCallback = Unpause;

        if (Advertisement.IsReady())
        {
            Advertisement.Show(options);
        }
        PauseMenu.paused = true;
        Time.timeScale = 0;
    }

    public static void Unpause(ShowResult result)
    {
        PauseMenu.paused = false;
        Time.timeScale = 1f;
    }

    public static void ShowRewardAd()
    {
        rewardCooldown = DateTime.Now.AddSeconds(15);
        if (Advertisement.IsReady())
        {
            PauseMenu.paused = true;
            Time.timeScale = 0f;
            var options = new ShowOptions
            {
                resultCallback = HandleShowResults
            };
            Advertisement.Show(options);
        }
    }

    public static void HandleShowResults(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                Debug.Log("Ad error. Do not handle");
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad skipped");
                break;
            case ShowResult.Finished:
                obstacle.Continue();
                break;
        }
        PauseMenu.paused = false;
        Time.timeScale = 1f;
    }

}
