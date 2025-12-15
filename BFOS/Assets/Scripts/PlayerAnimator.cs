using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerMotor motor;
    public Animator animator;
    public GameObject playerMesh;
    void Start()
    { 

    }

    void Update()
    {
        animator.SetBool("isGrounded", motor.isGrounded);
        // walking
        if (motor.facing == PlayerMotor.Direction.Right && motor.isWalking && motor.isGrounded)
        {
            animator.SetBool("isWalking", motor.isWalking);
            playerMesh.transform.eulerAngles = new Vector3(playerMesh.transform.eulerAngles.x, 90, playerMesh.transform.eulerAngles.z);
            playerMesh.transform.localPosition = new Vector3(playerMesh.transform.localPosition.x, -2.08f, playerMesh.transform.localPosition.z);
        }
        else if (motor.facing == PlayerMotor.Direction.Left && motor.isWalking && motor.isGrounded)
        {
            animator.SetBool("isWalking", motor.isWalking);
            playerMesh.transform.eulerAngles = new Vector3(playerMesh.transform.eulerAngles.x, -90, playerMesh.transform.eulerAngles.z);
            playerMesh.transform.localPosition = new Vector3(playerMesh.transform.localPosition.x, -2.08f, playerMesh.transform.localPosition.z);
        }
        else
        {
            animator.SetBool("isWalking", false);
            playerMesh.transform.localPosition = new Vector3(playerMesh.transform.localPosition.x, -1.08f, playerMesh.transform.localPosition.z);
        }

        // jumping

        if (Input.GetButtonDown("Jump") && motor.isGrounded)
        {
            animator.SetTrigger("trJumpingUp");
            playerMesh.transform.localPosition = new Vector3(playerMesh.transform.localPosition.x, -2.08f, playerMesh.transform.localPosition.z);
        }
    }
}
