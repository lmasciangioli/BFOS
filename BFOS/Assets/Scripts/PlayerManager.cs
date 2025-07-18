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
        void ResetScene()
        {
            Debug.Log("BONG");
            levels.sceneName = "SampleScene";
            levels.changeScene();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            ResetScene();
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            if (playerMotor.parrying)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                ResetScene();
            }
        }
    }

}
