using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Tooltip("Basic Tile reference")]
    public Transform tile;

    [Tooltip("Obstacle reference")]
    public Transform obstacle;

    [Tooltip("Coin reference")]
    public Transform coin;

    [Tooltip("Ofset for coin elevation")]
    public Vector3 coinOfset = new Vector3(0, 4, 0);

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

    // Update is called once per frame
    void Update()
    {

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

        // Search for all posible locations for coins
        var coinPoints = new List<GameObject>();

        foreach (Transform son in newTile)
        {
            // Verify if there is a tag ObstacleSpawn
            if (son.CompareTag("ObstacleSpawn"))
            {
                // Create a list as possible spawn points for obstacles
                obstaclePoints.Add(son.gameObject);

            }

            // Verify if there is a tag CoinSpawn
            if (son.CompareTag("CoinSpawn"))
            {
                // Create a list as possible spawn points for coins
                coinPoints.Add(son.gameObject);

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

            if (coinPoints.Count > 0)
            {
                // Get a random point
                var spawnCoinPoint = coinPoints[Random.Range(0, coinPoints.Count)];

                if (spawnCoinPoint.transform.position.x == spawnPoint.transform.position.x
                    && EndTileBehaviour.distanceControl%2 != 0)
                {
                    // Keep the spawn position
                    var positionSpawnCoinPoint = spawnCoinPoint.transform.position;

                    // Create a new coin
                    var newCoin = Instantiate(coin, positionSpawnCoinPoint, Quaternion.identity);

                    // Makes this coin son of its basic tile
                    newCoin.SetParent(spawnPoint.transform);
                }

            }
        }
    }

}
