using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public string startScene;
    public GameObject menuCanvas;
    public GameObject optionsCanavs;
    public GameObject creditsCanvas;

    private void Start()
    {
        menuCanvas.SetActive(true);
        optionsCanavs.SetActive(false);
        creditsCanvas.SetActive(false);
    }
    public void PressStart()
    {
        SceneManager.LoadScene(startScene);

    }
    public void PressOptionsMENU()
    {
        optionsCanavs.SetActive(true);
        menuCanvas.SetActive(false);
    }

    //there is almopst certainly a way to just have a "back button"function, but I cant figure it out rn.

    // AM: UI devs usually use a Stack container to track what screen they're on, and what came before it..and what came before that etc.
    // your soln is fine for just a few screens though.
   
    public void PressBackOPTIONS()
    {
        optionsCanavs.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void PressCredits()
    {
        menuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
    }
    public void PressBackCREDITS()
    {
        creditsCanvas.SetActive(false );
        menuCanvas.SetActive(true);
    }
    public void PressQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
