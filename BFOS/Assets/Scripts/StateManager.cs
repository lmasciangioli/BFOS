using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        Cutscene,
        Intro,
        Menu
    }
    public GameState gameState;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
