using Ink.Parsed;
using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Rigidbody player;
    public float force = 20f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Rigidbody>();
            player.AddForce(transform.forward * force);
        }
    }
}
