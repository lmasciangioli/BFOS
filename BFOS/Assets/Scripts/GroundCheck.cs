using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMotor playerMotor;

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Ground") || other.CompareTag("Wall"))
    //    {
    //        playerMotor.isGrounded = true;
    //    }
    //}
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            playerMotor.isGrounded = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            playerMotor.isGrounded = true;
        }
    }
}
