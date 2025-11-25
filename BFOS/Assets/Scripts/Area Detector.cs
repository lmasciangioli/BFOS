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
    public Spears spearsScript;
    public SpearRptation SpearRptation;

    public bool isMiddleSpears = false;


    private void Start()
    {
        warning1.SetActive(false);
        warning2.SetActive(false);

    }
    void Update()
    {
        
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 3)
            {
                warning1.SetActive(true);
                warning2.SetActive(true);
                Debug.Log("3");
                
            }
            if (currentTime <= 0)
            {
                timerActive = false;
                Debug.Log("0");
                warning1.SetActive(false);
                warning2.SetActive(false);
                if (isMiddleSpears == true)
                {
                    SpearRptation.move = true;
                }
                else
                {
                    spearsScript.move = true;
                }
                
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
            warning1.SetActive(false);
            warning2.SetActive(false);
            Debug.Log("exit");
        }
    }
}
