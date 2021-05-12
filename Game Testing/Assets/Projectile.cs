using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ParticleSystem effect;
    public ParticleSystem hit;
    public ParticleSystem flash;

    Rigidbody rb;
    Vector3 flashPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //play effect
        effect.Play();

        //play flash animation
        flash.Play();
        flashPos = flash.transform.position;

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += transform.forward * GameManager.instance.ps.projectileSpeed * Time.deltaTime;
        flash.transform.position = flashPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //instantiate hit
        //play hit animation
        //destroy hit
    }
}
