using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    string EXIT_POINT_NAME = "exit";
    [SerializeField] GameObject[] tiles;
    [SerializeField] Transform firstTile;
    [SerializeField] int visibleTileAmount;
    GameObject currentTile;
    int tileCounter;
    List<Transform> visibleTiles;
    Camera playerCamera;

    [SerializeField] GameObject[] obstacles;
    //[SerializeField] int maxObstaclesPerTile;
    int maxObstaclesPerTile = 1; // tiles are so small that one obstacle per tile is enough
    [SerializeField] int minTileGap;
    int tileGapsPassed;
    [SerializeField] int obstaclePropabilityPercent;

    void Awake()
    {
            visibleTiles = new List<Transform>();
            visibleTiles.Add(firstTile);
            tileCounter++;
            currentTile = firstTile.gameObject;

            AddInitialTiles();
    }

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Add a new tile to the right when right side of first tile in the visibleTiles list exits screen
        Vector3 firstVisiblePos = playerCamera.WorldToViewportPoint(visibleTiles[0].Find(EXIT_POINT_NAME).position);
        bool isFirstVisibleOnScreen = firstVisiblePos.x > 0 && firstVisiblePos.x < 1;
        //Debug.Log(isFirstVisibleOnScreen);
        if (!isFirstVisibleOnScreen)
        {
            AddNextTile();
        }
    }

    void AddInitialTiles()
    {
        for (int i = 0; i < visibleTileAmount - 1; i++)
        {
            AddNextTile();
        }
    }

    void AddNextTile()
    {
        int typeIndex = Random.Range(0, tiles.Length);
        GameObject nextTile = Instantiate(tiles[typeIndex]);
        visibleTiles.Add(nextTile.transform);

        Transform exitPoint = currentTile.transform.Find(EXIT_POINT_NAME);
        nextTile.transform.position = new Vector2(exitPoint.position.x, exitPoint.position.y);
        
        if (tileCounter < visibleTileAmount)
        {
            tileCounter++;
        }
        else
        {
            Destroy(visibleTiles[0].gameObject);
            visibleTiles.RemoveAt(0);
        }

        PopulateWithObstacles(nextTile);
        
        currentTile = nextTile;
    }

    void PopulateWithObstacles(GameObject tile)
    {
        int obstacleAmount = Random.Range(1, maxObstaclesPerTile + 1);
        bool insertObstacles = false;
        if ( obstaclePropabilityPercent >= Random.Range(0, 100+1) )
        {
            insertObstacles = true;
        }
        if (insertObstacles && tileGapsPassed >= minTileGap)
        {
            for (int i = 0; i < obstacleAmount; i++)
            {
                AddObstacle(tile);
            }
            tileGapsPassed = 0;
        } 
        else
        {
            tileGapsPassed++;
        }
    }

    void AddObstacle(GameObject tile)
    {
        //Debug.Log(tile.transform.position + " " + tile.transform.Find(EXIT_POINT_NAME).position);
        int typeIndex = Random.Range(0, obstacles.Length);
        float fracJourney = 0.5f; // Random.Range(0f, 1f);
        Vector3 pos = Vector3.Lerp(tile.transform.position, tile.transform.Find(EXIT_POINT_NAME).position, fracJourney);
        //Debug.Log(pos);
        GameObject obstacle = Instantiate(obstacles[typeIndex]);
        obstacle.transform.position = pos;

        Destroy(obstacle, 10f);
    }
}
