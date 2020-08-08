using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTileBehaviour : MonoBehaviour
{
    [Tooltip("Time to destruct the Basic Tile")]
    public float destructTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if it was the ball/player that pass through the Basic Tile ending
        if (other.GetComponent<PlayerBehaviour>())
        {
            //As it was the ball/player, lets create a Basic Tile at the next point
            //But this next point is after the last Basic Tile present on the scene
            GameObject.FindObjectOfType<GameController>().SpawnNextTile();

            //And now the Basic Tile is destroyed
            Destroy(transform.parent.gameObject, destructTime);
        }


    }

}
