using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweeter : MonoBehaviour
{
    public GameObject bird;
    public PlayerMotor.Direction facing;
    public float facingOffset;
    public bool homing;

    void Start()
    {
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        if(facing == PlayerMotor.Direction.Left)
        {
            facingOffset = -1.6f;
        }
        else
        {
            facingOffset = 1.6f;
        }


        GameObject shot = Instantiate(bird);
        shot.transform.position = new Vector3(facingOffset + transform.position.x, transform.position.y, 6.6f);
        //shot.transform.lossyScale = new Vector3(1, 1, 1);
        Projectile proj = shot.GetComponent<Projectile>();
        proj.facing = facing;
        proj.Homing(homing);
        


        yield return new WaitForSecondsRealtime(1.2f);
        StartCoroutine(Wander());
    }
}
