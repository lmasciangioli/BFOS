using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapper : MonoBehaviour
{
    public GameObject trap;
    public PlayerMotor.Direction facing;

    public bool moving;
    public float[] xBounds = new float[2];
    public float target;
    public float speed;



    [SerializeField]
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        target = Random.Range(xBounds[0], xBounds[1]);
        moving = true;
        yield return new WaitUntil(() => moving == false);
        yield return new WaitForSecondsRealtime(2.5f);
        StartCoroutine(Trap());
    }
    IEnumerator Trap()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 2, 6.6f);


        GameObject newTrap = Instantiate(trap, pos, Quaternion.Euler(0,0,0));

        yield return new WaitUntil(() => newTrap.GetComponent<Trap>().live);
        yield return new WaitForSecondsRealtime(1.2f);

        StartCoroutine(Wander());

    }

    public void Update()
    {
        if(gameObject.transform.position.x != target)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(target, gameObject.transform.position.y, gameObject.transform.position.z), speed * Time.deltaTime);
        }
        else
        {
            moving = false;
        }
    }


}
