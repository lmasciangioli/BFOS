using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public LevelManager levels;
    public PlayerMotor playerMotor;
    public Meter meter;
    void Start()
    {
        meter = FindAnyObjectByType<Meter>();
        levels = FindAnyObjectByType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Ting");
            ResetScene();
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            if (playerMotor.parrying)
            {
                Destroy(collision.gameObject);
                playerMotor.canJump = true;
                playerMotor.canParry = true;
                meter.ChangeMeter(meter.parry);
            }
            else
            {
                ResetScene();
            }
        }
    }


    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            if (collision.gameObject.GetComponent<Trap>().live == true)
            {
                ResetScene();
            }
        }
    }


    public void ResetScene()
    {
        Debug.Log("BONG");
        levels.sceneName = "SampleScene";
        levels.changeScene();
    }
}
