using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused;
#pragma warning disable 0649
    [SerializeField]
    private GameObject menuPause;
#pragma warning restore 0649

    // Start is called before the first frame update
    void Start()
    {
        //paused = false;
        Pause(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        EndTileBehaviour.distanceControl = 0;
        PlayerBehaviour.dashControl = true;
        PlayerBehaviour.indestructible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause(bool isPaused)
    {
        paused = isPaused;
        Time.timeScale = (paused) ? 0 : 1;
        menuPause.SetActive(paused);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
