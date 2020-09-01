using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleBehaviour : MonoBehaviour
{
    [Tooltip("How mach time before restart the game?")]
    public float waitTime = 2.0f;

    [Tooltip("Variable that count the number of obstacles destroyed")]
    public static int obstaclesDestroiedCount = 0;

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

    /// <summary>
    /// Method called to destroy the obstacle and count this destruction
    /// </summary>
    public void DestroyObject()
    {
        obstaclesDestroiedCount++;
        Destroy(gameObject);
    }
}
