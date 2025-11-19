using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public float timeLimit = 6f;
    private float currentTime;
    private bool timerActive = false;
    public GameObject warning1;
    public GameObject warning2;
    public GameObject spearsBottom;
    public GameObject SpearsTop;
    public Spears move;


    private void Start()
    {
        warning1.GetComponent<MeshRenderer>().enabled = false;
        warning2.GetComponent<MeshRenderer>().enabled = false;

    }
    void Update()
    {
        
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 3)
            {
                warning1.GetComponent<MeshRenderer>().enabled = true;
                warning2.GetComponent<MeshRenderer>().enabled = true;
                Debug.Log("3");
                
            }
            if (currentTime <= 0)
            {
                timerActive = false;
                Debug.Log("0");
                warning1.GetComponent<MeshRenderer>().enabled = false;
                warning2.GetComponent<MeshRenderer>().enabled = false;
                move = GetComponent<Spears>();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentTime = timeLimit;
            timerActive = true;
            Debug.Log("enter");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timerActive = false;
            Debug.Log("exit");
        }
    }
}
