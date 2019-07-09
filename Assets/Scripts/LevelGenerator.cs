using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public Transform firstTile;
    public int visibleTileAmount;
    GameObject currentTile;
    int tileCounter;
    List<Transform> visibleTiles;

    void Awake()
    {
            visibleTiles = new List<Transform>();
            visibleTiles.Add(firstTile);
            tileCounter++;
            currentTile = firstTile.gameObject;

            AddInitialTiles();
    }

    void Update()
    {
        // just for testing
        if(Input.GetKeyDown(KeyCode.Space))
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
        int nextTileIndex = Random.Range(0, tiles.Length);
        GameObject nextTile = Instantiate(tiles[nextTileIndex]);
        visibleTiles.Add(nextTile.transform);

        Transform exitPoint = currentTile.transform.Find("ExitPoint");
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
        
        currentTile = nextTile;
    }
}
