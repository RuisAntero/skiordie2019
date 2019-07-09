using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject[] tiles;
    [SerializeField] Transform firstTile;
    [SerializeField] int visibleTileAmount;
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
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     AddNextTile();
        // }

        // Add a new tile to the right when right side of first tile in the visibleTiles list exits screen
        // TODO: Optimize this
        Vector3 firstVisiblePos = playerCamera.WorldToViewportPoint(visibleTiles[0].Find("exit").position);
        bool isFirstVisibleOnScreen = firstVisiblePos.x > 0 && firstVisiblePos.x < 1;
        Debug.Log(isFirstVisibleOnScreen);
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
        int nextTileIndex = Random.Range(0, tiles.Length);
        GameObject nextTile = Instantiate(tiles[nextTileIndex]);
        visibleTiles.Add(nextTile.transform);

        Transform exitPoint = currentTile.transform.Find("exit");
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
