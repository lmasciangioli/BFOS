using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region Variables
    [Header("Tunables")]
    public float speed;
    public float jumpHeight;
    public float wallJumpforce;
    public float fallSpeed;

    [Header("States")]
    public bool isGrounded;
    public bool isWallLeft;
    public bool isWallRight;
    public bool isActioning;
    public bool isStunned;
    public bool canDash;
    public bool canParry;
    public bool canJump;
    public bool parrying;

    public Vector3 velocity;

    public Rigidbody rb;
    public Collider collider;

    public float bufferCount;
    public float bufferAmount;
    public Action bufferedAction;
    public bool jumpTimed;
    public bool wallJumping;
    public Direction wallJumpDirection;

    public Walk walk;
    public Jump jump;
    public WallJump wallJump;
    public Parry parry;
    public Dash dash;
    public BFOS bfos;

    public Color playerColor;
    public Material playerMat;

    public enum Direction
    {
        Left,
        Right
    }
    public Direction facing;

    public Meter meter;
    public BFOSAnimator bfosAnim;


    #endregion


    void FixedUpdate()
    {

        /* AM: Generally when you see code like this, where it's double-handling code paths with very slight differences...
         * it should be sign that your structure isn't right.
         * 
         * eg. you could try something like
         *   float horiz = Input.GetAxis("Horizontal");
         *   facing = horiz < 0 ? Direction.Left : Direction.Right;
         *   if (isStunned == false)
         *   {
         *      // your other movement code here.
         *   }
         * 
         * */

        rb.useGravity = true;

        //float horiz = Input.GetAxis("Horizontal");
        //facing = horiz < 0 ? Direction.Left : Direction.Right;


        if (Input.GetAxis("Horizontal") > 0)
        {
            facing = Direction.Right;
            if (isWallLeft == false)
            {
                if (isStunned == false && wallJumping == false)
                {
                    walk.Use();
                }
            }
            else
            {
                if (isGrounded == false)
                {
                    if (isStunned == false && jumpTimed == false && wallJumping == false)
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
                if (isStunned == false && wallJumping == false)
                {
                    walk.Use();
                }
            }
            else
            {
                if (isGrounded == false)
                {
                    if (isStunned == false && jumpTimed == false && wallJumping == false)
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
        //Debug.Log("slidin");
    }

    public KeyCode jumpy;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            bufferedAction = jump;
            bufferCount = bufferAmount;
            StartCoroutine(JumpBuffer());
        }
        else if (Input.GetKeyDown("left shift"))
        {
            bufferedAction = dash;
            bufferCount = bufferAmount;
        }
        else if(Input.GetAxis("Fire1") > 0)
        {
            bufferedAction = bfos;
            bufferCount = bufferAmount;
        }
        else if (Input.GetAxis("Fire2") > 0)
        {
            bufferedAction = parry;
            bufferCount = bufferAmount;
        }
    }


    IEnumerator bufferCountdown()
    {
        if (bufferCount > 0)
        {
            bufferCount -= 0.01f;   // AM: this probably wants to be Time.deltaTime instead of 0.01f.
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
        for (int i = 43; i > 0 ;i--)    // AM: wtf is this?
        {
            yield return new WaitForSecondsRealtime(0.01f);
            jumpTimed = true;
        }
        jumpTimed = false;
    }
    void Start()
    {
        meter = FindAnyObjectByType<Meter>();
        bfosAnim = FindAnyObjectByType<BFOSAnimator>();
        StartCoroutine(bufferCountdown());
        playerMat.SetColor("_BaseColor", playerColor);
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
            if (motor.wallJumping)
            {
                // AM: separate code paths like this is bad form.  Just do something like motor.rb.velocity = new Vector3(direction * motor.speed / 8, motor.rb.velocity.y, 0);  (where direction is either -1 or 1)
                int direction = motor.wallJumpDirection == Direction.Left ? -1 : 1;

                motor.rb.velocity = new Vector3(direction * motor.speed / 8, motor.rb.velocity.y, 0);

            }
            else
            {
                motor.rb.velocity = new Vector3((Input.GetAxis("Horizontal") * motor.speed), motor.rb.velocity.y, 0);
            }

            
            return true;

        }

    }

    [System.Serializable]
    public class Jump : Action
    {
        public override bool Use()
        {

            if (motor.isGrounded && motor.isActioning == false || motor.canJump)
            {
                motor.isActioning = true;
                motor.rb.velocity = new Vector3(motor.rb.velocity.x, 0, 0);
                motor.rb.AddForce(Vector3.up * motor.jumpHeight, ForceMode.Impulse);
                motor.isActioning = false;
                motor.canJump = false;
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
            if (motor.isWallLeft)
            {
                motor.wallJumpDirection = Direction.Left;
            }
            else
            {
                motor.wallJumpDirection = Direction.Right;
            }
            motor.isActioning = true;
            motor.rb.velocity = new Vector3(motor.rb.velocity.x, 0, 0);
            motor.rb.AddForce(Vector3.up * motor.jumpHeight, ForceMode.Impulse);
            motor.runWJOffeset();
            //motor.rb.AddForce(new Vector3(jumpOffset,0,0), ForceMode.Force);
            motor.isActioning = false;
            motor.canJump = false;
            return true;
        }

    }


    public void runWJOffeset()
    {
        StopCoroutine(WalljumpOffset());
        StartCoroutine(WalljumpOffset());
    }


    IEnumerator WalljumpOffset()
    {
        wallJumping = true;
        jumpTimed = true;
        for (int i = 12; i > 0; --i)
        {
            walk.Use();
            yield return new WaitForFixedUpdate();
        }
        wallJumping = false;
        for (int i = 18; i > 0; --i)
        {
            yield return new WaitForFixedUpdate();
        }
        jumpTimed = false;
        
    }



    [System.Serializable]
    public class Parry : Action
    {
        public override bool Use()
        {
            if(motor.isActioning == false && motor.isStunned == false && motor.canParry)
            {
                motor.StartParry();
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }

    public void StartParry()
    {
        StartCoroutine(ParryCo());
    }

    IEnumerator ParryCo()
    {
        playerMat.SetColor("_Color", Color.red);
        canParry = false;
        parrying = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        for (int i = 12; i > 0; i--)
        {
            yield return new WaitForFixedUpdate();
        }
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        playerMat.SetColor("_Color", playerColor);
        parrying = false;
        for (int i = 18; i > 0; i--)
        {
            yield return new WaitForFixedUpdate();
        }
        canParry = true;
    }
    

    [System.Serializable]
    public class Dash : Action
    {
        public override bool Use()
        {
            if (!motor.canDash || motor.isActioning || motor.isGrounded)
                return false;

            motor.StartDash();
            return true;

        }
    }

    public void StartDash()
    {
        StartCoroutine(DashCo());
    }
    IEnumerator DashCo()
    {
        canDash = false;
        isActioning = true;
        isStunned = true;
        wallJumping = false;
        
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
        for (int i = 10; i > 0; i--)
        {
            rb.velocity = dashDirection * 40;
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = dashDirection * 5;
        isActioning = false;
        isStunned = false;
        
    }
     













    [System.Serializable]
    public class BFOS : Action
    {
        public override bool Use()
        {
            if(motor.isStunned == false && motor.isActioning == false && motor.meter.meterPercent == 100)
            {
                Debug.Log("Shawing");
                motor.bfosAnim.Play();
                motor.collider.gameObject.SetActive(false);



                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(enemy);
                }
                
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}
