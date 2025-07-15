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
            transform.LookAt(player.transform.position);
            transform.position += transform.forward * Time.deltaTime * speed;

            //rb.Move(player.transform.position, player.transform.rotation);
        }
        
    }



    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            GameObject reset = GameObject.FindGameObjectWithTag("lMan");
            LevelManager lMan = reset.GetComponent<LevelManager>();
            lMan.changeScene();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
