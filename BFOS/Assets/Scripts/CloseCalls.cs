using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCalls : MonoBehaviour
{

    public List<Collider> log = new List<Collider>();
    public float logDuration;



    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Projectile"))
        {
            StartCoroutine(CloseCall(other));
        }
        else if (other.CompareTag("Trap"))
        {
            if (other.GetComponent<Trap>().live)
            {
                StartCoroutine(CloseCall(other));
            }
        }
    }


    IEnumerator CloseCall(Collider hit)
    {
        bool isIn = false;
        foreach(Collider logged in log)
        {
            if(logged == hit)
            {
                isIn = true;
            }
        }
        if(isIn == false)
        {
            //NEAR MISS CODE HERE
            Debug.Log("Miss!");





            log.Add(hit);
            float timer = logDuration;

            while(timer > 0)
            {
                yield return new WaitForFixedUpdate();
                timer -= 0.02f;
            }
            log.Remove(hit);
        }
        else
        {
            yield return null;
        }
    }
}
