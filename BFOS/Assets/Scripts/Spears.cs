using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spears : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float retractDelay = 1f;
    public bool move = true;
    private float moveTimer = 0f;
    private Vector3 startPosition;
    public Transform targetPosition;
    private void Start()
    {
        move = false;
        startPosition = transform.position;
<<<<<<< HEAD

=======
>>>>>>> 383b840 (spears and spear rotation in 1-3 'prince the goat frfr')
    }
    private void Update()
    {
        //if (move)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        //    Debug.Log("Getting called Now");
        //    if (transform.position == targetPosition)
        //    {
        //        move = false;
        //        moveTimer = retractDelay;
        //    }
        //}
        //else
        //{
        //    if (moveTimer > 0)
        //    {
        //        moveTimer -= Time.deltaTime;
        //    }
        //    else
        //    {
        //      transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

        //      if (transform.position == startPosition)
        //      {
        //            move = true;
        //      }
        //    }
        //}



        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition.position)
            {
                move = false;
                moveTimer = retractDelay;
            }
<<<<<<< HEAD
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            }
=======
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            //if (moveTimer > 0)
            //{
            //    moveTimer -= Time.deltaTime;
            //}
            //else
            //{
            //    transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            //    if (transform.position == startPosition)
            //    {
            //        move = true;
            //    }
            //}
>>>>>>> 383b840 (spears and spear rotation in 1-3 'prince the goat frfr')
        }
    }
}