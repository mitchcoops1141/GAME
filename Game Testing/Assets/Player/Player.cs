using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;

    Vector3 forward;
    Vector3 right;

    Animator anim;

    [HideInInspector]
    public bool canFunction = true;

    bool movingToNextRoom = false;
    RoomBorder rb;

    // Start is called before the first frame update
    void Start()
    {
        //setting players rotations
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        //get the animator
        anim = GetComponentInChildren<Animator>();

        //setting position
        GameManager gm = GameManager.instance;
        LevelManager lm = LevelManager.instance;
        float x = lm.levels[gm.Level - 1].gridWidth / 2 * lm.levels[gm.Level - 1].gridCellSize;
        float z = lm.levels[gm.Level - 1].gridHeight / 2 * lm.levels[gm.Level - 1].gridCellSize;
        transform.position = new Vector3(x, 1.7f, z);
    }

    // Update is called once per frame
    void Update()
    {
        //if allowed to move
        if (canFunction)
        {
            //if input
            if (Input.anyKey)
                Move();
            else
                anim.SetFloat("Speed", 0);
        }
        //if not allowed to move
        else
        {
            //if in process of moving to next room
            if (movingToNextRoom)
            { 
                anim.SetFloat("Speed", 1);

                //automatically move the player
                Vector3 rightMovement = right * speed * Time.deltaTime * rb.playerMovement[rb.direction].x;
                Vector3 upMovement = forward * speed * Time.deltaTime * rb.playerMovement[rb.direction].y;
                Vector3 heading = Vector3.Normalize(rightMovement + -upMovement);
                transform.forward = heading;
                transform.position += rightMovement;
                transform.position += -upMovement;
            }
            else
            {
                //remove animations
                anim.SetFloat("Speed", 0);
            }
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("HoriKey"), 0, Input.GetAxis("VertiKey"));
        Vector3 rightMovement = right * speed * Time.deltaTime * Input.GetAxis("HoriKey");
        Vector3 upMovement = forward * speed * Time.deltaTime * Input.GetAxis("VertiKey");

        Vector3 heading = Vector3.Normalize(rightMovement + -upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += -upMovement;

        if (Input.GetAxis("VertiKey") != 0 || (Input.GetAxis("HoriKey") != 0))
            anim.SetFloat("Speed", 1);
    }
    private void OnTriggerEnter(Collider other)
    {
        //if collided with a room border
        if (other.tag == "Border")
        {
            //if can function (moving into the border)
            if (canFunction)
            {
                LevelManager lm = LevelManager.instance;
                GameManager gm = GameManager.instance;

                //stop the player being able to move
                canFunction = false;

                rb = other.GetComponent<RoomBorder>();

                //move the player
                movingToNextRoom = true;

                //set the active room position to the next room
                lm.activeRoomPosition = lm.activeRoomPosition + rb.nextPos[rb.direction];

                //update the camera position
                Vector3 pos = new Vector3(lm.activeRoomPosition.x * lm.levels[gm.Level - 1].gridCellSize, 0f, lm.activeRoomPosition.y * lm.levels[gm.Level - 1].gridCellSize);
                Camera.main.transform.parent.GetComponent<CameraController>().UpdateCameraPosition(pos);
            }     
        }

        //if collide with the collider for the next room
        if (other.tag == "NextRoomStart")
        {
            //if cant function (coming from other room)
            if (!canFunction)
            {
                LevelManager lm = LevelManager.instance;
               
                //if the room hasnt been complepted yet
                if (!(other.GetComponentInParent<Room>().roomCompleted))
                {
                    //for each of the rooms in the level
                    foreach (GameObject room in lm.roomsInLevel)
                    {
                        //if the room is not the current room
                        if (!(room == other.transform.parent.gameObject))
                        {
                            //turn room off
                            room.SetActive(false);
                        }
                    }

                    //start the room
                    other.GetComponentInParent<Room>().RoomStart();
                }
                
            }

            //allow movement
            canFunction = true;
        }
    }
}

/*TO DO LIST
 * paths have to incrementally come up
 */