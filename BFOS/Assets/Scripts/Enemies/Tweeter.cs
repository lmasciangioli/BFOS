using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tweeter : MonoBehaviour
{
    public GameObject bird;
    public CapsuleCollider tweeter;
    public PlayerMotor.Direction facing;
    public float facingOffset;
    public bool homing;
    public float projectileStartDelay;
    public float projectileInterval;
    public List<Transform> tweetWayPoints;
    public List<Transform> usableWayPoints;
    public bool lazer;
    public int targetWPs;
    public GameObject wpObjects;
    public LineRenderer lr;
    void Start()
    {
        
        if (lazer == true)
        {
            StartCoroutine(ImmaFiringMahLazor());
        }
        StartCoroutine(Wander());
        tweeter = this.GetComponent<CapsuleCollider>();
        facingOffset = 5;
        if (facing == PlayerMotor.Direction.Left)
        {
            facingOffset = -facingOffset;
        }
    }

    IEnumerator Wander()
    {
        yield return new WaitForSecondsRealtime(projectileStartDelay);
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        GameObject shot = Instantiate(bird);
        shot.transform.position = new Vector3(facingOffset + transform.position.x, transform.position.y + (tweeter.height / 2), 6.6f);
        //shot.transform.lossyScale = new Vector3(1, 1, 1);
        Projectile proj = shot.GetComponent<Projectile>();
        proj.facing = facing;
        proj.Homing(homing);
        


        yield return new WaitForSecondsRealtime(projectileInterval);
        StartCoroutine(Wander());
    }

    IEnumerator ImmaFiringMahLazor()
    {
        usableWayPoints.Clear();
        usableWayPoints.AddRange(tweetWayPoints);
        while (usableWayPoints.Count > targetWPs)
        {
            usableWayPoints.Remove(usableWayPoints[Random.Range(0, usableWayPoints.Count)]);
        }

        if(usableWayPoints.Count != 0)
        {
            List<Vector3> temp = new List<Vector3>();
            
            lr.positionCount = targetWPs;
            for (int i = targetWPs; i > 0; i--)
            {
                Instantiate(wpObjects).transform.position = usableWayPoints[i- 1].position;
                temp.Add(usableWayPoints[i - 1].position);
                
                
            }
            lr.SetPositions(temp.ToArray());
        }



        yield return null;
    }
}
