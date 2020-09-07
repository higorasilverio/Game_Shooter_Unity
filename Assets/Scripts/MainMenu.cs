using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Method used to load the game scene
    /// </summary>
    /// <param name="sceneName"> Game Scene call</param>
    public void LoadGameScene (string sceneName)
    {
        if (AdsControl.showAds)
        {
            AdsControl.ShowAd();
        }
        // Restart the inicial parameters of the game
        EndTileBehaviour.distanceControl = 0;
        PlayerBehaviour.dashControl = true;
        PlayerBehaviour.indestructible = false;
        PlayerBehaviour.speed = 5.0f;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Method used to load the Instructions Screen
    /// </summary>
    /// <param name="sceneName">Instructions Screen call </param>
    public void LoadInstructionsScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Method used to load the Upgrades Screen
    /// </summary>
    /// <param name="sceneName"> Upgrades Screen call </param>
    public void LoadUpgradesScreen(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
