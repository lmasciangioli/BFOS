using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public int count;
    public TMP_Text timer;
    public Image panel;
    public bool canswing = false;

    public Color red;
    public Color grey;

    public PlayerMotor player;
    void Start()
    {
        
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        if(count > 3)
        {
            panel.color = grey;
            timer.fontSize = 40;
            timer.text = count.ToString();
        }
        else
        {
            panel.color = red;
            timer.fontSize = 60;
            if (count < 0)
            {
                count = 0;
            }
            if (count == 3)
            {
                timer.text = "BIG";
            }
            else if (count == 2)
            {
                timer.text = "FUCK";
            }
            else if (count == 1)
            {
                timer.text = "OFF";
            }
            else if (count == 0)
            {
                timer.fontSize = 45;
                timer.text = "SWORD";
            }


        }
    }


    IEnumerator Countdown()
    {
        for(int i = 60; i > 0; --i)
        {
            yield return new WaitForSecondsRealtime(1);
            count -= 1;
        }
    }
}
