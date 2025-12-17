using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public Animator trapArmAnim;

    public void playAnimation()
    {
        trapArmAnim.SetBool("play", true);
        Debug.Log("Animation is being played");
    }

    
}
