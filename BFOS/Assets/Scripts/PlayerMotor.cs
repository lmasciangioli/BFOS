using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool dashing;

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

    public Animator animator;
    public enum Direction
    {
        Left,
        Right
    }
    public Direction facing;

    public Meter meter;
    public BFOSAnimator bfosAnim;

    
    public AudioClip slap;
    public AudioClip dashie;
    #endregion


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            AudioSource.PlayClipAtPoint(slap, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            AudioSource.PlayClipAtPoint(dashie, transform.position);
        }
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
        

        void Slide()
        {
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, -10, 0);

            Debug.Log("slidin");

        }
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
        else if (Input.GetButtonDown("Dash"))
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

        if (dashing == false && Input.GetAxis("Horizontal") == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }
        else if (parrying == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
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
    }

    private void Awake()
    {
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
                //changed the y value from 8 to 1 to give more horizontal movement - blaire
                motor.rb.velocity = new Vector3(direction * motor.speed / 1, motor.rb.velocity.y, 0);

            }
            else
            {
                motor.rb.velocity = new Vector3((Input.GetAxis("Horizontal") * motor.speed), motor.rb.velocity.y, 0);
                
                //motor.animator.SetFloat("Velocity", motor.velocity.x);


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
        //changed wall jump duration to 6 from 12 - Blaire
        for (int i = 6; i > 0; --i)
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
        //is there a way to make the dash cut off early if the wall detectors are triggered? rn if a player dashes into a wall, they hang in air until the dash timer hits 0. Maybe an if statement checking of the wall detector was triggered and if so, instantly setting the i value to 0? - Blaire
        canDash = false;
        isActioning = true;
        isStunned = true;
        wallJumping = false;
        dashing = true;
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
            //changed the dash speed from 40 to 80, feels a bit better in my opinion - Blaire
            rb.velocity = dashDirection * 80;
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = dashDirection * 5;
        isActioning = false;
        isStunned = false;
        dashing = false;
        
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
