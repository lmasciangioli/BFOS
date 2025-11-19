using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spears : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 10f;
    public float retractDelay = 1f;
    private bool move = true;
    private float moveTimer = 0f;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private void Start()
    {
        move = false;
        startPosition = transform.position;
        targetPosition = startPosition + transform.up * moveDistance;
    }
    private void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                move = false;
                moveTimer = retractDelay;
            }
        }
        else
        {
            if (moveTimer > 0)
            {
                moveTimer -= Time.deltaTime;
            }
            else
            {
              transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

              if (transform.position == startPosition)
              {
                    move = true;
              }
            }
        }
    }
}