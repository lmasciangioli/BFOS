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
    public float lazerChargeTime;
    public float lazerFireRate;
    public int wayPointTracker;
    public bool canContinue;
    public AudioClip windup;
    public AudioClip Throw;

    void Start()
    {
        canContinue = false;
        wayPointTracker = 1;
        if (lazer == true)
        {
            StartCoroutine(ImmaFiringMahLazor());
        }
        else
        {
            StartCoroutine(Wander());
        }
        tweeter = this.GetComponent<CapsuleCollider>();
        facingOffset = 5;
        if (facing == PlayerMotor.Direction.Left)
        {
            facingOffset = -facingOffset;
        }
    }
    private void Update()
    {
        Debug.Log(wayPointTracker);
        if (wayPointTracker == targetWPs) 
        {
            wayPointTracker = 1;

            foreach (var gameObj in GameObject.FindGameObjectsWithTag("Indicator"))
            {
               Destroy(gameObj);
            }
            canContinue = false;

            lr.positionCount = 0;

            StartCoroutine(ImmaFiringMahLazor());
        } 
        else if (canContinue)
        {
            StartCoroutine(Shoot());
            canContinue = false;
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

        if (lazer == false)
        {
            AudioSource.PlayClipAtPoint(Throw,transform.position);
            shot.transform.position = new Vector3(facingOffset + transform.position.x, transform.position.y + (tweeter.height / 2), 6.6f);
            //shot.transform.lossyScale = new Vector3(1, 1, 1);
            Projectile proj = shot.GetComponent<Projectile>();
            proj.facing = facing;
            proj.Homing(homing);
            StartCoroutine(Wander());
        }
        else if (lazer == true)
        {
            if (wayPointTracker == 1)
            {
                shot.transform.position = new Vector3(transform.position.x, transform.position.y + (tweeter.height / 2), 6.6f);
            }
            else
            {
                shot.transform.position = usableWayPoints[wayPointTracker - 1].position;
            }
            Projectile proj = shot.GetComponent<Projectile>();
            proj.Lazer(usableWayPoints);
            yield return null;
        }

    }
    IEnumerator ImmaFiringMahLazor()
    {
        yield return new WaitForSecondsRealtime(lazerFireRate);
        usableWayPoints.Clear();
        usableWayPoints.Add(this.transform);
        usableWayPoints.AddRange(tweetWayPoints);
        while (usableWayPoints.Count > targetWPs)
        {
            usableWayPoints.Remove(usableWayPoints[Random.Range(1, usableWayPoints.Count)]);
        }

        if (usableWayPoints.Count != 0)
        {
            List<Vector3> temp = new List<Vector3>();

            lr.positionCount = targetWPs;
            for (int i = targetWPs; i > 0; i--)
            {
                Instantiate(wpObjects).transform.position = usableWayPoints[i - 1].position;
                temp.Add(usableWayPoints[i - 1].position);
            }
            lr.SetPositions(temp.ToArray());
        }

        yield return new WaitForSecondsRealtime(lazerChargeTime);
        StartCoroutine(Shoot());
    }
}
