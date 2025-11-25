using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerMotor;

public class Meter : MonoBehaviour
{

    public GameObject meterFg;
    public float meterPercent = 50f;
    public float decay;
    public float rapidDecay;
    public float nearMiss;
    public float parry;

    public GameObject player;
    public Vector3 playerPos;

    public float score = 0;

    public AudioClip swing;
    public AudioSource swingReady;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        meterFg.GetComponent<Image>().fillAmount = meterPercent / 100;
    }

    public void ChangeMeter(float amount)
    {
        meterPercent += amount;
        if(meterPercent > 100)
        {
            meterPercent = 100f;
            swingReady.PlayOneShot(swing);
        }
        else if(meterPercent < 0 )
        {
            meterPercent = 0;
        }
    }






}
