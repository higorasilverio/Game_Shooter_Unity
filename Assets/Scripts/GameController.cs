using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("Basic Tile reference")]
    public Transform tile;

    [Tooltip("Obstacle reference")]
    public Transform obstacle;

    [Tooltip("Initial point to insert the first Basic Tile")]
    public Vector3 initialpoint = new Vector3(0, 0, -5);

    [Tooltip("Initial tiles quantity")]
    [Range(1, 20)]
    public int initialTilesNumber;

    [Tooltip("Quantity of initial tiles without obstacles")]
    [Range(1, 4)]
    public int initialTilesWithoutObs;

    /// <summary>
    /// Location for the next tile spawn
    /// </summary>
    private Vector3 nextTilePosition;

    /// <summary>
    /// Next tile rotation
    /// </summary>
    private Quaternion nextTileRotation;

    // Start is called before the first frame update
    void Start()
    {
        //Preparing the initial point
        nextTilePosition = initialpoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initialTilesNumber; i++)
        {
            SpawnNextTile(i >= initialTilesWithoutObs);
        }

    }

    public void SpawnNextTile()
    {
        var newTile = Instantiate(tile, nextTilePosition, nextTileRotation);

        //Detects the spawn location for the next tile
        var nextTile = newTile.Find("Spawn Point");
        nextTilePosition = nextTile.position;
        nextTileRotation = nextTile.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
