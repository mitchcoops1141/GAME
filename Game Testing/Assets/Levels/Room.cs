using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] enemies;
    public RoomPath[] paths;

    public bool roomCompleted = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            RoomComplete();
        }
    }

    void RoomComplete()
    {
        roomCompleted = true;

        //reactivate the paths
        foreach (RoomPath path in paths)
        {
            path.Activate();
        }

        //reactivate all the rooms
        foreach (GameObject room in LevelManager.instance.roomsInLevel)
        {
            room.SetActive(true);
        }
    }
    
    public void RoomStart()
    {
        if (!roomCompleted)
        {
            //turn the paths off
            foreach (RoomPath path in paths)
            {
                path.Deactivate();
            }
        }
        

        //activate the enemies
    }
}
