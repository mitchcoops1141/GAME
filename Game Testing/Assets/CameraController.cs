using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;

    Vector3 newPos;
    // Update is called once per frame
    void Start()
    {
        GameManager gm = GameManager.instance;
        LevelManager lm = LevelManager.instance;
        transform.position = new Vector3(lm.levels[gm.Level - 1].gridWidth / 2 * LevelManager.instance.levels[GameManager.instance.Level - 1].gridCellSize, 0f, lm.levels[gm.Level - 1].gridHeight / 2 * lm.levels[gm.Level - 1].gridCellSize);

        transform.position = transform.position + offset;
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * 5f); ;
    }

    public void UpdateCameraPosition(Vector3 pos)
    {
        newPos = pos + offset;
    }
}
