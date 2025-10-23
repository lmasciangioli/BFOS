using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{

    public GameObject meter;
    public float meterPercent = 50f;
    public float decay;
    public float rapidDecay;
    public float nearMiss;
    public float parry;

    public Vector3 origin;
    public Vector3 originScale;
    public GameObject player;
    public Vector3 playerPos;

    public float score = 0; 

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        originScale = meter.gameObject.transform.localScale;
        score = 0;
    }

    void FixedUpdate()
    {
        if (meterPercent < 100)
        {
            if (player.transform.position == playerPos && player.GetComponent<PlayerMotor>().parrying == false)
            {
                ChangeMeter(-rapidDecay); 
            }
            else
            {
                ChangeMeter(-decay);
            }
            playerPos = player.transform.position;
            score += 0.1f;
        }
        else
        {
            Debug.Log("Player's score for this level was: " + (1000 - score));
        }
        meter.gameObject.transform.localScale = new Vector3(originScale.x * (meterPercent / 100) , originScale.y, originScale.z);
        
    }

    public void ChangeMeter(float amount)
    {
        meterPercent += amount;
        if(meterPercent > 100)
        {
            meterPercent = 100f;
        }
        else if(meterPercent < 0 )
        {
            meterPercent = 0;
        }
    }






}
