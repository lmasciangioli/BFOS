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
    public Action actionQ;
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
            if (isGrounded)
            {
                jump.Use();
            }
            else if (isWallLeft || isWallRight)
            {
                wallJump.Use();
            }

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
        public virtual void Use()
        {

        }
    }
    [System.Serializable]
    public class Walk : Action
    {
        public override void Use()
        {
            base.Use();
            motor.rb.velocity = new Vector3(Input.GetAxis("Horizontal") * motor.speed, motor.rb.velocity.y, 0);

        }

    }

    [System.Serializable]
    public class Jump : Action
    {
        public override void Use()
        {
            base.Use();
            motor.rb.velocity = new Vector3(motor.rb.velocity.x, 0, 0);
            motor.rb.AddForce(Vector3.up * motor.jumpHeight, ForceMode.Impulse);
        }

    }

    [System.Serializable]
    public class WallJump : Action
    {
        public override void Use()
        {
            base.Use();
            motor.rb.velocity = new Vector3(motor.rb.velocity.x, 0, 0);
            motor.rb.AddForce(Vector3.up * motor.jumpHeight, ForceMode.Impulse);
        }

    }

}
