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
    public float settingTime;
    public float speed;

    public AudioSource trapperWalk;
    public AudioSource trapPlace;
    bool trapper_ToggleChange;

    [SerializeField]
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        // AM: Player.Instance
        StartCoroutine(Wander());
        trapperWalk = GetComponent<AudioSource>();
        trapPlace = GetComponent<AudioSource>();

    }

    IEnumerator Wander()
    {
        target = Random.Range(xBounds[0], xBounds[1]);
        moving = true;
        if (moving == true && trapper_ToggleChange == true)
        {
            trapperWalk.Play();
            trapper_ToggleChange = false;
        }
        if (moving == false && trapper_ToggleChange == true)
        {
            trapperWalk.Stop();
            trapper_ToggleChange = false;
        }
        yield return new WaitUntil(() => moving == false);
        yield return new WaitForSecondsRealtime(settingTime);
        StartCoroutine(Trap());
    }
    IEnumerator Trap()
    {
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 5, 6.6f);


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
