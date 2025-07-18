using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool homing = false;

    public float speed;
    public PlayerMotor.Direction facing;
    public Rigidbody rb;

    [SerializeField]
    public GameObject player;

    void Start()
    {
        Homing(true);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (homing == true)
        {
            transform.LookAt(player.transform.position);
        }
        transform.position += transform.forward * Time.deltaTime * speed;

    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") == false)
        {
            Destroy(gameObject);
        }
        else
        {

            
        }
    }

    void Homing(bool home)
    {
        homing = home;
        if (homing)
        {
            speed = 15;
        }
        else
        {
            speed = 25;
        }
    }

}
