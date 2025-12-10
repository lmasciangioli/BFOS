using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Transform[] waypoints;
    public int waypointIndex;
    public float speed;
    public GameObject thisEnemy;
    
    public Meter meterScript;

    public Material playerMat;
    public Color playerColor;

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
    private void Awake()
    {
        playerMat.SetColor("_BaseColor", playerColor);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerMat.SetColor("_Color", Color.red);
            meterScript.ChangeMeter(-25);
            meterScript.ActivateDamageSword();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerMat.SetColor("_Color", playerColor);

        }
    }



}
