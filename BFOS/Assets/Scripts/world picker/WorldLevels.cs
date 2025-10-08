using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLevels : MonoBehaviour
{
    public string levelName;
    public bool locked;
    public WorldLevels worldLeft;
    public List<Vector3> pathLeft = new List<Vector3>();
    public WorldLevels worldRight;
    public List<Vector3> pathRight = new List<Vector3>();



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
