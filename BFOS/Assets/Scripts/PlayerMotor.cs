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
    public bool canParry;

    public Vector3 velocity;

    public Rigidbody rb;

    public float bufferCount;
    public float bufferAmount;
    public Action bufferedAction;
    public bool jumpTimed;

    public Walk walk;
    public Jump jump;
    public WallJump wallJump;
    public Parry parry;

    public Color playerColor;

    public enum Direction
    {
        Left,
        Right
    }
    public Direction facing;

    void FixedUpdate()
    {
        rb.useGravity = true;
        if (Input.GetAxis("Horizontal") > 0)
        {
            facing = Direction.Right;
            if (isWallLeft == false)
            {
                if (isStunned == false)
                {
                    walk.Use();
                }
            }
            else
            {
                if (isGrounded == false)
                {
                    if (jumpTimed == false)
                    {
                        Slide();
                    }
                }
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            facing = Direction.Left;
            if (isWallRight == false)
            {
                if (isStunned == false)
                {
                    walk.Use();
                }
            }
            else
            {
                if (isGrounded == false)
                {
                    if (jumpTimed == false)
                    {
                        Slide();
                    }
                   
                }
               
            }
        }
        
    }

    void Slide()
    {
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, -10, 0);
        Debug.Log("slidin");
    }


    void Update()
    {
        if (Input.GetKeyDown("space") || Input.GetKeyDown("w"))
        {
            bufferedAction = jump;
            bufferCount = bufferAmount;
            StartCoroutine(JumpBuffer());
        }
        else if (Input.GetKeyDown("left shift"))
        {
            bufferedAction = parry;
            bufferCount = bufferAmount;
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


    IEnumerator JumpBuffer()
    {
        jumpTimed = true;
        for (int i = 43; i > 0 ;i--)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            jumpTimed = true;
        }
        jumpTimed = false;
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

            if (motor.isGrounded && motor.isActioning == false)
            {
                motor.isActioning = true;
                motor.rb.velocity = new Vector3(motor.rb.velocity.x, 0, 0);
                motor.rb.AddForce(Vector3.up * motor.jumpHeight, ForceMode.Impulse);
                motor.isActioning = false;
                return true;
            }
            else if ((motor.isWallLeft || motor.isWallRight) && motor.isActioning == false)
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



    [System.Serializable]
    public class Parry : Action
    {
        public override bool Use()
        {
            if(motor.canParry == true)
            {
                if (motor.isActioning == false)
                {
                    if (motor.isGrounded == false)
                    {

                        motor.StartDash();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
    }

    public void StartDash()
    {
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        canParry = false;
        isActioning = true;
        isStunned = true;
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
        float vert = Input.GetAxisRaw("Vertical");
        float horiz;
        if (facing == Direction.Left)
        {
            horiz = -1;
        }
        else
        {
            horiz = 1;
        }
        Vector3 dashDirection = new Vector3(horiz, vert / 6, 0).normalized;
        rb.velocity = new Vector3(0, 0, 0);
        for (int i = 17; i > 0; i--)
        {
            rb.velocity = dashDirection * 40;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        rb.velocity = dashDirection * 5;
        isActioning = false;
        isStunned = false;
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", playerColor);
    }

}
