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
    public int initialTilesNumber = 15;

    [Tooltip("Quantity of initial tiles without obstacles")]
    [Range(1, 4)]
    public int initialTilesWithoutObs = 4;

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
        // Preparing the initial point
        nextTilePosition = initialpoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initialTilesNumber; i++)
        {
            SpawnNextTile(i >= initialTilesWithoutObs);
        }

    }

    public void SpawnNextTile(bool spawnObstacles = true)
    {
        var newTile = Instantiate(tile, nextTilePosition, nextTileRotation);

        var nextTile = newTile.Find("Spawn Point");
        nextTilePosition = nextTile.position;
        nextTileRotation = nextTile.rotation;

        // Verify if we can create the next obstacle
        if (!spawnObstacles)
            return;

        // Search for all posible locations for obstacles
        var obstaclePoints = new List<GameObject>();

        foreach (Transform son in newTile)
        {
            // Verify if there is a tag ObstacleSpawn
            if (son.CompareTag("ObstacleSpawn"))
            {
                // Create a list as possible spawn points
                obstaclePoints.Add(son.gameObject);

            }
        }

        if (obstaclePoints.Count > 0)
        {
            // Get a random point
            var spawnPoint = obstaclePoints[Random.Range(0, obstaclePoints.Count)];

            // Keep the spawn position
            var positionSpawnPoint = spawnPoint.transform.position;

            // Create a new obstacle
            var newObstacle = Instantiate(obstacle, positionSpawnPoint, Quaternion.identity);

            // Makes this obstacle son of its basic tile
            newObstacle.SetParent(spawnPoint.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
