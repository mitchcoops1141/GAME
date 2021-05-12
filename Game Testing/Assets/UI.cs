using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    Transform player;
    public Transform crosshair;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        #region CROSSHAIR

        //crosshair.position = Input.mousePosition;



        Vector3 cursorPos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(player.transform.position);
        Vector3 playerToCursor = cursorPos - playerPos;
        Vector3 dir = playerToCursor.normalized;
        Vector3 cursorVector = dir * GameManager.instance.ps.range;

        if (playerToCursor.magnitude < cursorVector.magnitude) // detect if mouse is in inner radius
        {
            cursorVector = playerToCursor;
        }       

        crosshair.position = playerPos + cursorVector;

        #endregion
    }
}
