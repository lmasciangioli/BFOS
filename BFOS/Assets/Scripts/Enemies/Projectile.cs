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
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (homing == true)
        {
            transform.LookAt(player.transform.position + new Vector3(0,1,0));
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else
        {
            if (facing == PlayerMotor.Direction.Left)
            {
                transform.position -= transform.forward * Time.deltaTime * speed;
            }
            else
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }
        }
        

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

    public void Homing(bool home)
    {
        if (home == true)
        {
            homing = true;
            speed = 15;
        }
        else
        {
            homing = false;
            speed = 25;
        }
        
    }

}
