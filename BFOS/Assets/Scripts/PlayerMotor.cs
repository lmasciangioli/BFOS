using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float fallSpeed;
    public bool isGrounded;
    public bool isWallLeft;
    public bool isWallRight;
    public bool isActioning;
    public bool isStunned;

    public Vector3 velocity;

    public Rigidbody rb;

    public float bufferCount;
    public Action bufferedAction;

    public Walk walk;
    public Jump jump;
    public WallJump wallJump;
        
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            if(isWallLeft == false)
            {
                if(isStunned == false)
                {
                    walk.Use();
                }
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (isWallRight == false)
            {
                if(isStunned == false)
                {
                    walk.Use();
                }
            }
        }


    }



    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            bufferedAction = jump;
            bufferCount = jump.duration;
        }
    }


    IEnumerator bufferCountdown()
    {
        if (bufferCount > 0)
        {
            bufferCount -= 0.01f;
        }
        else
        {
            bufferCount = 0;
        }
        yield return new WaitForSecondsRealtime(0.01f);
        if(bufferCount > 0)
        {
            if (bufferedAction != null)
            {
                if (bufferedAction.Use())
                {
                    bufferCount = 0;
                    bufferedAction = null;
                }
                
            }
        }
        else
        {
            bufferedAction = null;
        }
        StartCoroutine(bufferCountdown());
    }

    void Start()
    {
        StartCoroutine(bufferCountdown());
    }











    [System.Serializable]
    public class Action
    {
        public float duration;
        public PlayerMotor motor;
        public virtual bool Use()
        {
            return true;
        }
    }
    [System.Serializable]
    public class Walk : Action
    {
        public override bool Use()
        {
            motor.rb.velocity = new Vector3(Input.GetAxis("Horizontal") * motor.speed, motor.rb.velocity.y, 0);
            return true;

        }

    }

    [System.Serializable]
    public class Jump : Action
    {
        public override bool Use()
        {

            if (motor.isGrounded)
            {
                motor.isActioning = true;
                motor.rb.velocity = new Vector3(motor.rb.velocity.x, 0, 0);
                motor.rb.AddForce(Vector3.up * motor.jumpHeight, ForceMode.Impulse);
                motor.isActioning = false;
                return true;
            }
            else if (motor.isWallLeft || motor.isWallRight)
            {
                motor.wallJump.Use();
                return true;
            }
            else
            {
                return false;
            }

        }

    }

    [System.Serializable]
    public class WallJump : Action
    {
        public override bool Use()
        {
            motor.isActioning = true;
            motor.rb.velocity = new Vector3(motor.rb.velocity.x, 0, 0);
            motor.rb.AddForce(Vector3.up * motor.jumpHeight, ForceMode.Impulse);
            motor.isActioning = false;
            return true;
        }

    }

}
