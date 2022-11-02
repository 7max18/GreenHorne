using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

//Adapted from https://forum.unity.com/threads/hexagonal-tilemap-movement-issue-using-new-tilemap-system.657373/ and
//https://github.com/Unity-Technologies/2d-extras/issues/69
public class DungeonPlayerMovement : Pathfinder
{
    public Tile movableIndicator;
    
    void Start()
    {
        transform.position = startingTile.gameObject.transform.position;
        currentTile = startingTile;
        onTurn = SetReachableTiles;
        SetReachableTiles();
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !walking)
        {
            Vector3 clicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clicked.z = 0;

            if (hexagon.HasTile(hexagon.WorldToCell(clicked)))
            {
                ClearTiles();

                RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask.GetMask("Dungeon"));
                endPoint = raycastHit.transform.gameObject.GetComponent<DungeonTile>();

                StartCoroutine(Walk(NextTurn, AStar(currentTile, endPoint)));
            }
        }
    }

    void SetReachableTiles()
    {
        GetReachableTiles();
        foreach (Vector3Int tile in reachableTiles)
        {
            hexagon.SetTile(tile, movableIndicator);
        }
    }

    void ClearTiles()
    {
        foreach (Vector3Int tile in reachableTiles)
        {
            hexagon.SetTile(tile, null);
        }

        reachableTiles = null;
    }

    void NextTurn()
    {
        turnManager.AdvanceTurn();
    }
}
