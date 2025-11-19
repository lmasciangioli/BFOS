using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public float timeLimit = 6f;
    private float currentTime;
    private bool timerActive = false;
    public MeshRenderer warning1;
    public MeshRenderer warning2;
    public Spears spearsScript;
    public SpearRptation SpearRptation;

    public bool isMiddleSpears = false;


    private void Start()
    {
        warning1.enabled = false;
        warning2.enabled = false;

    }
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 3)
        {
<<<<<<< HEAD
            warning1.enabled = true;
            warning2.enabled = true;
            Debug.Log("3");
=======
            currentTime -= Time.deltaTime;
            if (currentTime <= 3)
            {
                warning1.enabled = true;
                warning2.enabled = true;
                Debug.Log("3");
                
            }
            if (currentTime <= 0)
            {
                timerActive = false;
                Debug.Log("0");
                warning1.enabled = false;
                warning2.enabled = false;
                if (isMiddleSpears == true)
                {
                    SpearRptation.move = true;
                }
                else
                {
                    spearsScript.move = true;
                }
                
            }
>>>>>>> 383b840 (spears and spear rotation in 1-3 'prince the goat frfr')
        }
        else
        {
            timerActive = false;
<<<<<<< HEAD
            Debug.Log("0");
            warning1.enabled = false;
            warning2.enabled = false;
            if (isMiddleSpears == true)
            {
                SpearRptation.move = true;
            }
            else
            {
                spearsScript.move = true;
            }
            if (CompareTag("Player"))
            {
                timerActive = false;
                warning1.enabled = false;
                warning2.enabled = false;
                Debug.Log("exit");
            }
=======
            warning1.enabled = false;
            warning2.enabled = false;
            Debug.Log("exit");
>>>>>>> 383b840 (spears and spear rotation in 1-3 'prince the goat frfr')
        }
    }
}
   