using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    public static LevelManager levelManager;
    public bool changeSceneOnClear;

    public void Awake()
    {
        levelManager = this;

    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
