using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool homing = false;
    public bool lazering = false;
    public List<Transform> lazerList;

    public float speed;
    public PlayerMotor.Direction facing;
    public Rigidbody rb;

    [SerializeField]
    public GameObject player;
    public Tweeter tweeter;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tweeter = GameObject.FindAnyObjectByType<Tweeter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (homing == true && lazering == false)
        {
            transform.LookAt(player.transform.position + new Vector3(0,1,0));
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else if (homing == false && lazering == false)
        {
            if (facing == PlayerMotor.Direction.Left)
            {
                transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
                transform.position += transform.forward * Time.deltaTime * speed;
            }
            else
            {
                transform.position += transform.forward * Time.deltaTime * speed;
            }
        }
        else if(homing == false && lazering == true)
        {
            transform.LookAt(lazerList[tweeter.wayPointTracker].position);
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.CompareTag("Indicator") == true && other.transform.position != lazerList[tweeter.wayPointTracker - 1].position)
        {
            if (tweeter.wayPointTracker != (tweeter.targetWPs))
            {
                tweeter.wayPointTracker++;
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") == false && lazering == false)
        {
            Destroy(gameObject);
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

    public void Lazer(List<Transform> GetLazerList)
    {
        lazering = true;
        speed = 100;
        lazerList.AddRange(GetLazerList);
    }

}
