using Ink.Parsed;
using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float force = 20;
    public GameObject player;
    public Rigidbody rb;
    public Vector3 direction;

    public void Awake()
    {
        player = FindObjectOfType<PlayerManager>().gameObject;
        rb = player.GetComponent<Rigidbody>();
        direction = gameObject.transform.forward;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.AddForce(direction * force, ForceMode.VelocityChange);
        }
    }
}
