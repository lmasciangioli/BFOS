using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BFOSAnimator : MonoBehaviour
{
    public List<Sprite> sequence = new List<Sprite>();
    public Image image;
    public int fps;
    public string queuedScene;

    IEnumerator RenderSequence()
    {
        foreach (Sprite sprite in sequence)
        {
            image.sprite = sprite;

            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
        }
        if(LevelManager.levelManager.changeSceneOnClear)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            LevelManager.levelManager.sceneName = queuedScene;
            LevelManager.levelManager.changeScene();
        }
        
    }
    public void Play()
    {
        StartCoroutine(RenderSequence());
    }
    

}
