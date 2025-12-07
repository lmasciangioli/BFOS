using Ink.Parsed;
using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float force = 20;
    public Transform gustAim;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null && gustAim != null)
            {
                Vector3 direction = gustAim.forward.normalized;
                rb.AddForce(direction * force, ForceMode.Force);
            }
        }
    }
}
