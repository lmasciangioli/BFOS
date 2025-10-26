using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressParticles : MonoBehaviour
{
    public ParticleSystem targetParticleSystem;

    [Range(0f, 1f)]

    public float currentProgress = 0f; 

    void Update()
    {
        var emission = targetParticleSystem.emission;
        emission.rateOverTime = currentProgress * 100f; 
    }
}
