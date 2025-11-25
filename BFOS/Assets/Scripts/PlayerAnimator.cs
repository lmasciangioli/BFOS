using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public static PlayerAnimator playerAnimator;
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
    public string currentAnimation;
    public float crossfade;

    

    public void Awake()
    {
        playerAnimator = this;
    }

    public void ChangeAnimation(string animation)
    {
        if (currentAnimation != animation)
        {
            animator.CrossFade(animation, crossfade);
        }
    }

    void Start()
    {
        //currentAnimation = animator.name;
    }

    
    void Update()
    {
        
    }
}
