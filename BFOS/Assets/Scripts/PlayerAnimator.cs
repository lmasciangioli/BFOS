using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public enum PlayerState
    {
        idle,
        run,
        jump,
        walljump,
        wallslide,
        parry,
        dash
    }
    public PlayerState state;
    public Animator animator;



    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
