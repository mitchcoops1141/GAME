using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPath : MonoBehaviour
{
    public Transform[] pathModules;
    public static float intervalSpeed = 0.06f;

    public void Deactivate()
    {
        StartCoroutine("DeactivateIE");
    }

    public void Activate()
    {
        StartCoroutine("ActivateIE");
    }

    IEnumerator DeactivateIE()
    {
        foreach(Transform pm in pathModules)
        {
            pm.gameObject.SetActive(false);
            yield return new WaitForSeconds(intervalSpeed);
        }
    }

    IEnumerator ActivateIE()
    {
        foreach (Transform pm in pathModules)
        {
            pm.gameObject.SetActive(true);
            yield return new WaitForSeconds(intervalSpeed);
        }
    }
}
