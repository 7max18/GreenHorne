using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonTile : MonoBehaviour
{
    public Tilemap layout;
    /// <summary>
    /// Sum of G and H.
    /// </summary>
    public int F => g + h;

    /// <summary>
    /// Cost from start tile to this tile.
    /// </summary>
    public int g;

    /// <summary>
    /// Estimated cost from this tile to destination tile.
    /// </summary>
    public int h;

    /// <summary>
    /// Tile's coordinates.
    /// </summary>
    public Vector3Int position;

    /// <summary>
    /// References to all adjacent tiles.
    /// </summary>
    public List<DungeonTile> adjacentTiles = new List<DungeonTile>();

    /// <summary>
    /// If true - Tile is an obstacle impossible to pass.
    /// </summary>
    public bool isObstacle;

    private void Awake()
    {
        layout = transform.parent.GetComponent<Tilemap>();
        position = layout.WorldToCell(transform.position);
        foreach (Vector3Int cell in GetNeighbors(position, 1))
        {
            adjacentTiles.Add(Physics2D.OverlapCircle(layout.CellToWorld(cell), 0.01f, LayerMask.GetMask("Dungeon")).gameObject.GetComponent<DungeonTile>());
        }
    }

    //Adapted from https://forum.unity.com/threads/hexagonal-tilemap-movement-issue-using-new-tilemap-system.657373/ and
    //https://github.com/Unity-Technologies/2d-extras/issues/69
    public List<Vector3Int> GetNeighbors(Vector3Int unityCell, int range)
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

                Vector3Int cell = CubeToUnityCell(centerCubePos + cubePosOffset);

                if (Physics2D.OverlapCircle(layout.CellToWorld(cell), 0.01f, LayerMask.GetMask("Dungeon")))
                {
                    result.Add(cell);
                }
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
}
