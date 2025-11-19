using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpearRptation : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float retractDelay = 1f;
    public bool move = true;
    private float moveTimer = 0f;
    private Vector3 startPosition;

    public float smooth;
    private void Start()
    {
        move = false;

    }
    private void Update()
    {

        if (move)
        {
            Quaternion newRotation = Quaternion.Euler(180, 0, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * smooth);
            Debug.Log(transform.rotation);

            //if (transform.position == targetPosition.position)
            //{
            //    move = false;
            //    moveTimer = retractDelay;
            //}
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

        }
    }
}

