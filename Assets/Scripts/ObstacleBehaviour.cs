using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleBehaviour : MonoBehaviour
{

    [Tooltip("How mach time before restart the game?")]
    public float waitTime = 2.0f;

    public static int obstaclesDestroiedCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if it is the player
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {

            if (PlayerBehaviour.indestructible)
            {
                DestroyObject();
            }
            else
            {
                //Destroy the player object
                Destroy(collision.gameObject);
                Invoke("GameReset", waitTime);
            }
        }
    }

    /// <summary>
    /// Method called to restart the level
    /// </summary>
    void GameReset()
    {
        // Actually restart the level and its global variables
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        EndTileBehaviour.distanceControl = 0;
        PlayerBehaviour.dashControl = true;
        PlayerBehaviour.indestructible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyObject()
    {
        obstaclesDestroiedCount++;
        Destroy(gameObject);
    }
}
