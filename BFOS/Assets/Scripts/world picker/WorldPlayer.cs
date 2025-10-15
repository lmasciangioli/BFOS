using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPlayer : MonoBehaviour
{
    public LevelManager levelManager;
    public WorldLevels currentWorld;
    public PlayerMotor.Direction movingTo;
    public bool movingAttempt;
    public bool moving;
    public float speed;
    public Vector3 heightOffset;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 && currentWorld.worldRight is not null)
        {
            movingAttempt = true;
            movingTo = PlayerMotor.Direction.Right;
        }
        else if (Input.GetAxis("Horizontal") < 0 && currentWorld.worldLeft is not null)
        {
            movingAttempt = true;
            movingTo = PlayerMotor.Direction.Left;
        }
        else
        {
            movingAttempt = false;
        }

        if (movingAttempt && moving == false)
        {
            if (movingTo == PlayerMotor.Direction.Right)
            {
                StartCoroutine(MoveWorlds(currentWorld.worldRight));
            }
            else if (movingTo == PlayerMotor.Direction.Left)
            {
                StartCoroutine(MoveWorlds(currentWorld.worldLeft));
            }
        }

        if (Input.GetKeyDown("space") && moving == false)
        {
            levelManager.sceneName = currentWorld.levelName;
            levelManager.changeScene();
        }
    }



    IEnumerator MoveWorlds(WorldLevels target)
    {
        moving = true;
        while (gameObject.transform.position != target.transform.position + heightOffset)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position + heightOffset, speed);
            yield return new WaitForFixedUpdate();
        }
        currentWorld = target;
        moving = false;
        yield return null;
    }
}
