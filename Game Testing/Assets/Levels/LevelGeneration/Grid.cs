using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilledCells
{
    public float x;
    public float y;
    public GameObject room;
}


public class Grid : MonoBehaviour
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private List<FilledCells> filledCells;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        filledCells = new List<FilledCells>();

        gridArray = new int[width, height];
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
    public void FillCell(Vector2 pos, GameObject room)
    {
        GameObject.Instantiate(room, new Vector3(pos[0] * cellSize, 0, pos[1] * cellSize), Quaternion.Euler(0, 0, 0));
        AddFilledCells(pos[0] * cellSize, pos[1] * cellSize, room);
    }

    public void UpdatePaths()
    {
        foreach(GameObject placeholder in GameObject.FindGameObjectsWithTag("Placeholder"))
        {
            string str = "";
            //get pos
            Vector2 pos = new Vector2(placeholder.transform.position.x / cellSize, placeholder.transform.position.z / cellSize);

            //check pos up
            if (GetObjectOfFilledCell(new Vector2(pos.x, pos.y + 1)) != null)
                str = str.Insert(str.Length, "T");
            //check pos right
            if (GetObjectOfFilledCell(new Vector2(pos.x + 1, pos.y)) != null)
                str = str.Insert(str.Length, "R");

            //check pos down
            if (GetObjectOfFilledCell(new Vector2(pos.x, pos.y - 1)) != null)
                str = str.Insert(str.Length, "B");

            //check pos left
            if (GetObjectOfFilledCell(new Vector2(pos.x - 1, pos.y)) != null)
                str = str.Insert(str.Length, "L");

            //fill cell with pos and level manager room dictionary at the first index //CHANGE THIS WHEN ADDING RANDOM ROOMS
            FillCell(pos, LevelManager.instance.rooms[str][0]);
            Destroy(placeholder);
        }

        //remove all from filledcells where room.tag == Placeholder
        filledCells.RemoveAll(i => i.room.tag == "Placeholder");
    }

    public void AddFilledCells(float x, float y, GameObject level)
    {
        FilledCells toInsert = new FilledCells();
        toInsert.x = x;
        toInsert.y = y;
        toInsert.room = level;

        filledCells.Add(toInsert);
    }

    public List<FilledCells> GetAllFilledCells()
    {
        return filledCells;
    }

    public Vector2 GetLocationOfFilledCell(string name)
    {
        Vector2 pos = new Vector2(0,0);
        //loop through all filled cells to find the start room
        foreach (FilledCells fc in GetAllFilledCells())
        {
            if (fc.room.name == name)
            {
                pos.x = fc.x / cellSize;
                pos.y = fc.y / cellSize; 
            }
        }

        return pos;
    }

    public GameObject GetObjectOfFilledCell(Vector2 pos)
    {
        GameObject obj = null;
        //loop through all filled cells to find the start room
        foreach (FilledCells fc in GetAllFilledCells())
        {
            if (fc.x / cellSize == pos[0] && fc.y / cellSize == pos[1])
            {
                obj = fc.room;
            }
        }

        return obj;
    }
}