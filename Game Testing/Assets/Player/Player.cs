using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Layer Mask For RAY")]
    public LayerMask layer;

    [Header("Projectile")]
    public GameObject projectile;
    public Transform projSpawnPoint;
    
    //BOOLS
    [HideInInspector]
    public bool canFunction = true;
    bool movingToNextRoom = false;
    bool canShoot = true;

    //private connections
    Vector3 forward;
    Vector3 right;

    public Animator anim;
    RoomPath rp;

    Ray cameraRay;
    RaycastHit cameraRayHit;

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
        anim.SetFloat("Firerate", 5);

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


            #region ROTATION
            // Cast a ray from the camera to the mouse cursor
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If the ray strikes an object...
            if (Physics.Raycast(cameraRay, out cameraRayHit, layer))
            {
                //get the position
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                //look at the position
                transform.LookAt(targetPosition);
            }
            #endregion


                #region MOVEMENT
                if (Input.GetAxis("VertiKey") != 0 || (Input.GetAxis("HoriKey") != 0))
            {
                //Vector3 direction = new Vector3(Input.GetAxis("HoriKey"), 0, Input.GetAxis("VertiKey"));
                Vector3 rightMovement = right * GameManager.instance.ps.speed * Time.deltaTime * Input.GetAxis("HoriKey");
                Vector3 upMovement = forward * GameManager.instance.ps.speed * Time.deltaTime * Input.GetAxis("VertiKey");

                transform.position += rightMovement;
                transform.position += -upMovement;

                anim.SetFloat("Speed", 1);
            }
            else
            {
                //idle
                anim.SetFloat("Speed", 0);
            }
            #endregion

            #region SHOOTING
            if (Input.GetMouseButton(0))
                anim.SetBool("Shooting", true);
            else
                anim.SetBool("Shooting", false);
            #endregion

        }
        //if not allowed to move
        else
        {
            //if in process of moving to next room
            if (movingToNextRoom)
            { 
                anim.SetFloat("Speed", 1);

                //automatically move the player
                Vector3 rightMovement = right * GameManager.instance.ps.speed * Time.deltaTime * rp.playerMovement[rp.direction].x;
                Vector3 upMovement = forward * GameManager.instance.ps.speed * Time.deltaTime * rp.playerMovement[rp.direction].y;
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

    public void Shoot()
    {
        Instantiate(projectile, projSpawnPoint.transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if collided with a room border
        if (other.tag == "Border")
        {
            print("HIT");
            //if can function (moving into the border)
            if (canFunction)
            {
                LevelManager lm = LevelManager.instance;
                GameManager gm = GameManager.instance;

                //stop the player being able to move
                canFunction = false;

                rp = other.GetComponentInParent<RoomPath>();

                //move the player
                movingToNextRoom = true;

                //set the active room position to the next room
                lm.activeRoomPosition = lm.activeRoomPosition + rp.nextPos[rp.direction];

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

                //start the room
                other.GetComponentInParent<Room>().RoomStart();

                //if the room hasnt been complepted yet
                if (!(other.GetComponentInParent<Room>().roomCompleted))
                {
                    //for each of the rooms in the level
                    foreach (GameObject room in lm.roomsInLevel)
                    {
                        //if the room is not the current room
                        if (!(room == other.transform.parent.parent.gameObject))
                        {
                            //turn room off
                            room.SetActive(false);
                        }
                    }  
                }  
            }

            //allow movement
            canFunction = true;
        }
    }
}



//FOR MOVEMENT ANIMATIONS
//get direction of player to mouse
//