using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Adapted from https://forum.unity.com/threads/hexagonal-tilemap-movement-issue-using-new-tilemap-system.657373/ and
//https://github.com/Unity-Technologies/2d-extras/issues/69
public class DungeonMovement : MonoBehaviour
{
    public Tilemap hexagon;
    public Vector3Int startingTile;
    private Vector3Int currentTile;
    public int moveRadius;
    public Tilemap dungeonLayout;
    private List<Vector3Int> reachableTiles = new List<Vector3Int>();
    public Tile movableIndicator;
    void Start()
    {
        transform.position = hexagon.CellToWorld(startingTile);
        currentTile = startingTile;
        GetReachableTiles();
    }
    void Update()
    {
        // Pathfinding();

        if (Input.GetMouseButtonUp(0))
        {
            //Where I clicked
            Vector3 clicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clicked.z = 0;

            if (hexagon.HasTile(hexagon.WorldToCell(clicked)))
            {
                //Where I want to go
                currentTile = hexagon.WorldToCell(clicked);

                transform.position = hexagon.CellToWorld(currentTile);
                ClearTiles();
                GetReachableTiles();
            } 
        }

    }

    private List<Vector3Int> GetNeighbors(Vector3Int unityCell, int range)
    {
        var centerCubePos = UnityCellToCube(unityCell);

        var result = new List<Vector3Int>();

        int min = -range, max = range;

        for (int x = min; x <= max; x++)
        {
            for (int y = min; y <= max; y++)
            {
                var z = -x - y;
                if (z < min || z > max)
                {
                    continue;
                }

                var cubePosOffset = new Vector3Int(x, y, z);
                result.Add(CubeToUnityCell(centerCubePos + cubePosOffset));
            }

        }

        result.Remove(CubeToUnityCell(centerCubePos));

        return result;
    }
    private Vector3Int UnityCellToCube(Vector3Int cell)
    {
        var yCell = cell.x;
        var xCell = cell.y;
        var x = yCell - (xCell - (xCell & 1)) / 2;
        var z = xCell;
        var y = -x - z;
        return new Vector3Int(x, y, z);
    }
    private Vector3Int CubeToUnityCell(Vector3Int cube)
    {
        var x = cube.x;
        var z = cube.z;
        var col = x + (z - (z & 1)) / 2;
        var row = z;

        return new Vector3Int(col, row, 0);
    }
    void ClearTiles()
    {
        foreach (Vector3Int tile in reachableTiles)
        {
            hexagon.SetTile(tile, null);
        }
    }
    void GetReachableTiles()
    {
        reachableTiles = GetNeighbors(currentTile, moveRadius);
        foreach (Vector3Int tile in reachableTiles)
        {
            if(dungeonLayout.HasTile(tile))
            {
                hexagon.SetTile(tile, movableIndicator);
            }
        }
    }
}
