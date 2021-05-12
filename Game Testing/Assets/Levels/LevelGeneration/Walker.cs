using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{ 
    NORTH = 0,
    EAST = 1,
    SOUTH = 2,
    WEST = 3
};


public class Walker : MonoBehaviour
{
    Vector2Int position;
    int counter = 0;

    GameManager gm = GameManager.instance;
    LevelManager lm = LevelManager.instance;

    //a dictionary that connects movement (NSEW to vector)
    private static readonly Dictionary<Direction, Vector2Int> moveTo = new Dictionary<Direction, Vector2Int>
    {
        { Direction.NORTH, Vector2Int.up },
        { Direction.SOUTH, Vector2Int.down },
        { Direction.EAST, Vector2Int.right },
        { Direction.WEST, Vector2Int.left },
    };

    private void Start()
    {
        //set position to start pos (center of grid)
        position = new Vector2Int(lm.grid.GetWidth() / 2, lm.grid.GetHeight() / 2);
        //start the moving function
        Move();
    }

    public void Move()
    {
        //check if current position does not have a room
        if (lm.grid.GetObjectOfFilledCell(position) == null)
            lm.grid.FillCell(position, lm.placeHolderRoom);

        //check if counter is less then specified numebr of iterations for active level
        if (counter < lm.levels[gm.Level - 1].numberOfIterations)
        {
            //move to next position
            position = position + moveTo[(Direction)Random.Range(0, moveTo.Count)];
            //increment the counter
            counter++;
            //re call this funtcion
            Move();
        }
        //if the counter is finished (walker is finished moving)
        else
            //destroy walker
            Destroy(gameObject);
    }
}