using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public LevelManager levels;
    public PlayerMotor playerMotor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            ResetScene();
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            if (playerMotor.parrying)
            {
                Destroy(collision.gameObject);
                playerMotor.canJump = true;
                playerMotor.canParry = true;
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
