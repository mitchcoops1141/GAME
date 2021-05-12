using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimListener : MonoBehaviour
{
    public void Shoot()
    {
        //run the parent (Player) shoot function
        GetComponentInParent<Player>().Shoot();
    }
}
