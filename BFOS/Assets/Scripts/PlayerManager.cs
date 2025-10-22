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
        levels = FindAnyObjectByType<LevelManager>();       // AM: look into Singleton pattern if you need access to something there's only ever going to be one instance of.
                                                            // eg. so you can just do LevelManager.Instance,  instead of searching for it.
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
                playerMotor.canDash = true;
                meter.ChangeMeter(meter.parry);
            }
            else
            {
                ResetScene();
            }
        }
    }

    /*
     *  AM: better form to just see if you can get the component out of the collision, and use that.
     *  eg. var trap = collision.gameObject.GetComponent<Trap>();
     *  if (trap != null && trap.live)
     *  {
     *      ResetScene();
     *  }
     *  Relying on tags is prob gonna be a bad time.
     * 
     */


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
        levels.changeScene();
    }
}
