using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public bool triggered;
    public bool playIn;
    public bool live;
    public Vector3 position;
    void Start()
    {
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            gameObject.transform.position = position  + (new Vector3(1,1,1) * Random.Range(-0.3f, 0.3f));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(Spring());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playIn = false;
        }
    }


    IEnumerator Spring()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        live = true;
        triggered = false;
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 6, gameObject.transform.localScale.z);
        gameObject.transform.position = position + new Vector3(0, 2.75f, 0);
        BoxCollider box = gameObject.GetComponent<BoxCollider>();
        box.size = new Vector3(1, 1, 1);
        box.center = new Vector3(0,0,0);

        playIn = false;
        if ((box.bounds.Contains(GameObject.FindGameObjectWithTag("Player").transform.position)))
        {
            playIn = true;
        }
        if (playIn)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerManager>().ResetScene();
        }

        


        yield return new WaitForSecondsRealtime(0.2f);
        Destroy(gameObject);
    }
}
