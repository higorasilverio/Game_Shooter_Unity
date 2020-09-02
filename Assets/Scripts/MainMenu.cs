using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Method used to load the game scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadGameScene (string sceneName)
    {
        if (AdsControl.showAds)
        {
            AdsControl.ShowAd();
        }
        SceneManager.LoadScene(sceneName);
    }

    public void LoadInstructionsScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadUpgradesScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
