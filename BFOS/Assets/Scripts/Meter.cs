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
    public Material playerMat;
    public Color playerColor;

    public GameObject damageSword;

    public float score = 0;

    public AudioClip swing;
    public AudioSource swingReady;
    public static Meter meter;
    public AudioSource hit;
    public GameObject swordBurning;
    public GameObject swordTipper;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        damageSword.SetActive(false);
        score = 0;
    }
    public void Awake()
    {
        meter = this;
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
            swordBurning.SetActive(true);
            swordTipper.SetActive(true);
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

        if(amount > 0 && meterPercent < 100)
        {
            StartCoroutine(swordFlash());
        }

    }

    public void ActivateDamageSword()
    {
        damageSword.SetActive(true);
        CloseCalls.closeCalls.ActivateDelay();
        hit.Play(0);
        StartCoroutine(DamageSwordTimer());
    }

    private IEnumerator DamageSwordTimer()
    {
        yield return new WaitForSeconds(0.5f);
        damageSword.SetActive(false);
        playerMat.SetColor("_Color", playerColor);

    }

    
    IEnumerator swordFlash()
    {
        swordBurning.SetActive(true);
        float timer = 0.6f;

        while (timer > 0)
        {
            yield return new WaitForFixedUpdate();
            timer -= 0.02f;
        }
        swordBurning.SetActive(false);
    }


}
