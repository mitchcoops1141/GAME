using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public GameObject walker;

    private void Start()
    {
        //instantiae grid on levelmanager
        LevelManager.instance.InstantiateGrid();

        StartCoroutine("waitforwalker");         
    }

    IEnumerator waitforwalker()
    {
        LevelManager lm = LevelManager.instance;
        GameManager gm = GameManager.instance;

        int i = lm.levels[gm.Level - 1].numberOfWalkers;
        while (i > 0)
        {
            Instantiate(walker);

            yield return new WaitForSeconds(0.1f);

            i--;
        }

        lm.grid.UpdatePaths();  
        

        

        //find all rooms and add to the levelmanager array
        lm.roomsInLevel = GameObject.FindGameObjectsWithTag("Room");

    }
}