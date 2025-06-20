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

    public Vector3 velocity;

    public Rigidbody rb;
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("a");
            if(isWallLeft == false)
            {
                Debug.Log("b");
                Move();
            }
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("c");
            if (isWallRight == false)
            {
                Debug.Log("a");
                Move();
            }
        }


    }



    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }
            else if (isWallLeft || isWallRight)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            }

        }
    }

    public void Move()
    {
       rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, 0);
    }




    public class Action
    {
        public float duration;
        public PlayerMotor motor;
        protected virtual void Use()
        {

        }
    }

    public class Walk : Action
    {
        protected override void Use()
        {
            base.Use();
            motor.rb.velocity = new Vector3(Input.GetAxis("Horizontal") * motor.speed, motor.rb.velocity.y, 0);

        }

    }

}
