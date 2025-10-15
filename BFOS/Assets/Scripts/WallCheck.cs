using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public PlayerMotor playerMotor;
    public enum wallSide
    {
        Left,
        Right
    }
    public wallSide side;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            if(side == wallSide.Left)
            {
                playerMotor.isWallLeft = true;  // AM: this would prob be nicer to work with if it was playerMotor.WallDirection (left/right/none) instead of separate bools. Wouldn't need separate code paths.
            }
            else
            {
                playerMotor.isWallRight = true;
            }
            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            if (side == wallSide.Left)
            {
                playerMotor.isWallLeft = false;
            }
            else
            {
                playerMotor.isWallRight = false;
            }
        }
    }
}
