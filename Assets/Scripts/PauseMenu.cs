using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Tooltip ("Variable which state if the game is paused or not")]
    public static bool paused;
#pragma warning disable
    /// <summary>
    /// Reference to the Menu Pause shown after Button Pause click
    /// </summary>
    [SerializeField]
    private GameObject menuPause;
#pragma warning restore

    // Start is called before the first frame update
    void Start()
    {
        Pause(false);
    }

    /// <summary>
    /// Function used to actually restart the game
    /// </summary>
    public void Restart()
    {
        EndTileBehaviour.distanceControl = 0;
        PlayerBehaviour.dashControl = true;
        PlayerBehaviour.indestructible = false;
        PlayerBehaviour.speed = 5.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Function used to actually pause the game
    /// </summary>
    /// <param name="isPaused"> Variable used to verify if the game is pause or not</param>
    public void Pause(bool isPaused)
    {
        paused = isPaused;
        Time.timeScale = (paused) ? 0 : 1;
        menuPause.SetActive(paused);
    }

    /// <summary>
    /// Fucntion used to load the Initial Scene
    /// </summary>
    /// <param name="sceneName"> Reference to the Initial Scene</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
