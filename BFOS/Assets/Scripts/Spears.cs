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

    }
    private void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            if (transform.position == targetPosition.position)
            {
                move = false;
                moveTimer = retractDelay;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            }
        }
    }
}