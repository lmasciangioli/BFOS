using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CloseCalls : MonoBehaviour
{

    public List<Collider> log = new List<Collider>();
    public float logDuration;
    public float nearMissTimer;
    public Transform playerTrans;
    public Meter meter;
    public GameObject nearMissIndicator;


    void Start()
    {
<<<<<<< Updated upstream
        meter = FindAnyObjectByType<Meter>();       // AM: again, probs look into Singleton. Searching the scene is one of the slowest operations you can do in Unity, so you want to avoid it as much as you can!
=======
        meter = FindAnyObjectByType<Meter>();
        nearMissIndicator.SetActive(false);
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
>>>>>>> Stashed changes
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
            StartCoroutine(NearMissIndicator());
            Debug.Log("NearMiss!");
            meter.ChangeMeter(meter.nearMiss);




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
    IEnumerator NearMissIndicator()
    {
        float timer = nearMissTimer;
        nearMissIndicator.transform.position = playerTrans.position; 
        nearMissIndicator.SetActive(true);

        while (timer > 0)
        {
            yield return new WaitForFixedUpdate();
            timer -= 0.02f;
        }
        nearMissIndicator.SetActive(false);
    }
}
