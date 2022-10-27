using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

//Adapted from https://forum.unity.com/threads/hexagonal-tilemap-movement-issue-using-new-tilemap-system.657373/ and
//https://github.com/Unity-Technologies/2d-extras/issues/69
public class DungeonMovement : MonoBehaviour
{
    public Tilemap hexagon;
    public DungeonTile startingTile;
    private DungeonTile currentTile;
    private DungeonTile endPoint;
    public int moveRadius;
    public Tilemap dungeonLayout;
    private List<Vector3Int> reachableTiles = new List<Vector3Int>();
    public Tile movableIndicator;
    private bool walking;
    void Start()
    {
        transform.position = startingTile.gameObject.transform.position;
        currentTile = startingTile;
        GetReachableTiles();
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
                walking = true;

                RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask.GetMask("Dungeon"));
                endPoint = raycastHit.transform.gameObject.GetComponent<DungeonTile>();

                StartCoroutine(Walk(AStar(currentTile, endPoint)));
            }
        }

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
        reachableTiles = currentTile.GetNeighbors(currentTile.position, moveRadius);
        foreach (Vector3Int tile in reachableTiles)
        {
            hexagon.SetTile(tile, movableIndicator);
        }
    }

    //Code from https://blog.theknightsofunity.com/pathfinding-on-a-hexagonal-grid-a-algorithm/
    /// <summary>
    /// Finds path from given start point to end point. Returns an empty list if the path couldn't be found.
    /// </summary>
    List<DungeonTile> AStar(DungeonTile start, DungeonTile goal)
    {
        List<DungeonTile> openPathTiles = new List<DungeonTile>();
        List<DungeonTile> closedPathTiles = new List<DungeonTile>();

        // Prepare the start tile.
        DungeonTile curTile = start;

        curTile.g = 0;
        curTile.h = GetEstimatedPathCost(start.position, goal.position);

        // Add the start tile to the open list.
        openPathTiles.Add(curTile);

        while (openPathTiles.Count != 0)
        {
            // Sorting the open list to get the tile with the lowest F.
            openPathTiles = openPathTiles.OrderBy(x => x.F).ThenByDescending(x => x.g).ToList();
            curTile = openPathTiles[0];

            // Removing the current tile from the open list and adding it to the closed list.
            openPathTiles.Remove(curTile);
            closedPathTiles.Add(curTile);

            int g = curTile.g + 1;

            // If there is a target tile in the closed list, we have found a path.
            if (closedPathTiles.Contains(goal))
            {
                break;
            }

            // Investigating each adjacent tile of the current tile.
            foreach (DungeonTile adjacentTile in curTile.adjacentTiles)
            {
                // Ignore not walkable adjacent tiles.
                if (adjacentTile.isObstacle)
                {
                    continue;
                }

                // Ignore the tile if it's already in the closed list.
                if (closedPathTiles.Contains(adjacentTile))
                {
                    continue;
                }

                // If it's not in the open list - add it and compute G and H.
                if (!(openPathTiles.Contains(adjacentTile)))
                {
                    adjacentTile.g = g;
                    adjacentTile.h = GetEstimatedPathCost(adjacentTile.position, goal.position);
                    openPathTiles.Add(adjacentTile);
                }
                // Otherwise check if using current G we can get a lower value of F, if so update it's value.
                else if (adjacentTile.F > g + adjacentTile.h)
                {
                    adjacentTile.g = g;
                }
            }
        }

        List<DungeonTile> finalPathTiles = new List<DungeonTile>();

        // Backtracking - setting the final path.
        if (closedPathTiles.Contains(goal))
        {
            curTile = goal;
            finalPathTiles.Add(curTile);

            for (int i = goal.g - 1; i >= 0; i--)
            {
                curTile = closedPathTiles.Find(x => x.g == i && curTile.adjacentTiles.Contains(x));
                finalPathTiles.Add(curTile);
            }

            finalPathTiles.Reverse();
        }

        return finalPathTiles;
    }

    /// <summary>
    /// Returns estimated path cost from given start position to target position of hex tile using Manhattan distance.
    /// </summary>
    /// <param name="startPosition">Start position.</param>
    /// <param name="targetPosition">Destination position.</param>
    protected static int GetEstimatedPathCost(Vector3Int startPosition, Vector3Int targetPosition)
    {
        return Mathf.Max(Mathf.Abs(startPosition.z - targetPosition.z), Mathf.Max(Mathf.Abs(startPosition.x - targetPosition.x), Mathf.Abs(startPosition.y - targetPosition.y)));
    }

    IEnumerator Walk(List<DungeonTile> path)
    {
        for (int i = 0; i < path.Count; i++)
        {
            transform.position = path[i].gameObject.transform.position;
            yield return new WaitForSeconds(1.0f);
        }

        currentTile = endPoint;
        GetReachableTiles();
        walking = false;
    }
}
