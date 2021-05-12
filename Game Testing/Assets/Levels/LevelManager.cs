using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [Header("Levels")]
    public LevelCreationData[] levels;
    // public LevelCreationData level2;

    [Header("Place Holder Room")]
    public GameObject placeHolderRoom;

    [Header("Special Rooms")]
    public GameObject[] ItemRooms;
    public GameObject[] BossRooms;
    public GameObject[] ShopRooms;

    [Header("One Path Rooms")]
    public GameObject[] T_Rooms;
    public GameObject[] R_Rooms;
    public GameObject[] B_Rooms;
    public GameObject[] L_Rooms;

    [Header("Two Path Rooms")]
    public GameObject[] TR_Rooms;
    public GameObject[] TL_Rooms;
    public GameObject[] BL_Rooms;
    public GameObject[] RB_Rooms;
    public GameObject[] TB_Rooms;
    public GameObject[] RL_Rooms;

    [Header("Three Path Rooms")]
    public GameObject[] TRL_Rooms;
    public GameObject[] RBL_Rooms;
    public GameObject[] TBL_Rooms;
    public GameObject[] TRB_Rooms;

    [Header("Four Path Rooms")]
    public GameObject[] TRBL_Rooms;

    [HideInInspector]
    public Grid grid;

    [HideInInspector]
    public GameObject[] roomsInLevel;

    [HideInInspector]
    public Vector2Int activeRoomPosition;
    //a dictionary that connects movement (NSEW to vector)
    public Dictionary<string, GameObject[]> rooms;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        rooms = new Dictionary<string, GameObject[]>
        {
            { "T", T_Rooms },
            { "R", R_Rooms },
            { "B", B_Rooms },
            { "L", L_Rooms },
            { "TR", TR_Rooms },
            { "TL", TL_Rooms },
            { "BL", BL_Rooms },
            { "RB", RB_Rooms },
            { "TB", TB_Rooms },
            { "RL", RL_Rooms },
            { "TRL", TRL_Rooms },
            { "RBL", RBL_Rooms },
            { "TBL", TBL_Rooms },
            { "TRB", TRB_Rooms },
            { "TRBL", TRBL_Rooms },
        };
    }

    private void Start()
    {
        activeRoomPosition = new Vector2Int(levels[GameManager.instance.Level - 1].gridWidth / 2, levels[GameManager.instance.Level - 1].gridHeight / 2);
    }

    public void InstantiateGrid()
    {
        GameManager gm = GameManager.instance;
        grid = new Grid(levels[gm.Level - 1].gridWidth, levels[gm.Level - 1].gridHeight, levels[gm.Level - 1].gridCellSize);
    }
}