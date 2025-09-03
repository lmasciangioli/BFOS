using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BFOSAnimator : MonoBehaviour
{
    public List<Sprite> sequence = new List<Sprite>();
    public Image image;
    public int fps;

    IEnumerator RenderSequence()
    {
        foreach(Sprite sprite in sequence)
        {
            image.sprite = sprite;
            yield return new WaitForSecondsRealtime(1 / fps);
        }
    }
    public void Play()
    {
        StartCoroutine(RenderSequence());
    }


}
