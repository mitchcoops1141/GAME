using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int   health;
    public float speed;
    public float fireRate; // x = x time per second (1 = 1 per second) //min 2, max 0.15
    public float damage;
    public float projectileSpeed;
    public int   range; // x * 35 = 1 tile (1 = 1 tile)  //min 1(35), max 12(420)
    public float statusChance;
    public float statusDamage;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PlayerStats ps;

    public int Level = 1;

    int rangeMultiplier = 35;

    Player player;

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
    }

    public void Start()
    {
        ps.range = ps.range * rangeMultiplier;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void UpdateFirerate(float firerate)
    {
        //update the float to whatever was passed into the function
        ps.fireRate = firerate;
        player.anim.SetFloat("Firerate", ps.fireRate);
    }

    public void UpdateRange(int range)
    {
        ps.range = range * rangeMultiplier;
    }
}