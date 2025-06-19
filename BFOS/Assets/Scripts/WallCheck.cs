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
        if (other.CompareTag("Wall"))
        {
            if(side == wallSide.Left)
            {
                playerMotor.isWallLeft = true;
            }
            else
            {
                playerMotor.isWallRight = true;
            }
            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
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
