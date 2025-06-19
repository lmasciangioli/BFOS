using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMotor playerMotor;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            playerMotor.isGrounded = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            playerMotor.isGrounded = false;
        }
    }


}
