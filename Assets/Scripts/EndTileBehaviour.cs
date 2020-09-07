using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTileBehaviour : MonoBehaviour
{
    [Tooltip("Time to destruct the Basic Tile")]
    public float destructTime = 2.0f;

    [Tooltip("Variable in charge of monitoring the travelled distance")]
    public static int distanceControl = 0;        

    void Update()
    {

        // Convert and show the distance traveled
        var distance = distanceControl.ToString();
        GameObject.Find("Canvas").transform.Find("HUD").transform.Find("Panel Distance").transform.Find("Text").GetComponentInChildren<Text>().text = distance;
    }

        private void OnTriggerEnter(Collider other)
    {
        // Check if it was the Player that pass through the Basic Tile ending
        if (other.GetComponent<PlayerBehaviour>())
        {
            // As it was the Player, lets create a Basic Tile at the next point
            // But this next point is after the last Basic Tile present on the scene
            GameObject.FindObjectOfType<GameController>().SpawnNextTile();

            // And now one of the Basic Tile is destroyed
            Destroy(transform.parent.gameObject, destructTime);

            // Increments the distance already travelled
            distanceControl++;
        }


    }
}
