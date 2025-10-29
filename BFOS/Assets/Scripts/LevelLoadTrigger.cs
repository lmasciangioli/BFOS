using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoadTrigger : MonoBehaviour
{
    public string nextLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.levelManager.sceneName = nextLevel;
            LevelManager.levelManager.changeScene();
        }
    }
}
