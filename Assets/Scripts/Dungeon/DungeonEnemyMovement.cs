using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyMovement : Pathfinder
{
    private DungeonPlayerMovement player;
    public int sightRadius;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        transform.position = startingTile.gameObject.transform.position;
        currentTile = startingTile;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<DungeonPlayerMovement>();
        onTurn = CheckPlayerProximity;
        GetReachableTiles();
    }

    void CheckPlayerProximity()
    {
        if (currentTile.GetNeighbors(currentTile.position, sightRadius).Contains(player.currentTile.position) && !player.currentTile.isObstacle)
        {
            MoveTowardsTarget(player.currentTile);
        }
        else
        {
            turnManager.AdvanceTurn();
        }
    }

    void MoveTowardsTarget(DungeonTile endpoint)
    {
        currentTile.isObstacle = false;
        List<DungeonTile> path = AStar(currentTile, endpoint);
        if (path.Count > moveRadius + 1)
        {
            path.RemoveRange(moveRadius + 1, path.Count - (moveRadius + 1));
        }
        StartCoroutine(Walk(NextTurn, path));
    }

    void NextTurn()
    {
        currentTile.isObstacle = true;
        if (ReferenceEquals(currentTile, player.currentTile))
        {
            //Launch battle
            Debug.Log("Battle start!");
        }

        turnManager.AdvanceTurn();
    }
}
