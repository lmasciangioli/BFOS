using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Transform[] waypoints;
    public int waypointIndex;
    public float speed;
    public GameObject thisEnemy;
    void Start()
    {
        thisEnemy.transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            thisEnemy.transform.position = Vector3.MoveTowards(thisEnemy.transform.position, waypoints[waypointIndex].transform.position, speed * Time.deltaTime);
            
            
            if (thisEnemy.transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<PlayerManager>().ResetScene();
        }
    }



}
