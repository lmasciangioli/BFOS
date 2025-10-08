using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject gobloomba;
    public GameObject tweeter;
    public GameObject trapper;
    public List<EnemyPreset> enemies = new List<EnemyPreset>();


    [System.Serializable]
    public class EnemyPreset
    {
        public enum Type
        {
            Gobloomba,
            Tweeter,
            Trapper
        }

        public Type type;
        public Vector3 spawnPos;
        
        [Header("Gobloomba Variables")]
        public Transform[] waypoints;
        public int waypointIndex;

        [Header("Tweeter Variables")]
        public bool homing;
        public PlayerMotor.Direction facing;

        [Header("Trapper Variables")]
        public float[] xBounds = new float[2];
    }



    public void SpawnEnemy(EnemyPreset target)
    {
        GameObject enemy;
        if (target.type == EnemyPreset.Type.Gobloomba)
        {
            enemy = Instantiate(gobloomba);
            Goomba enemyScript = enemy.GetComponent<Goomba>();
            enemyScript.waypoints = target.waypoints;
            enemyScript.waypointIndex = target.waypointIndex;
        }
        else if (target.type == EnemyPreset.Type.Tweeter)
        {
            enemy = Instantiate(tweeter);
            Tweeter enemyScript = enemy.GetComponent<Tweeter>();
            enemyScript.facing = target.facing;
            enemyScript.homing = target.homing;
        }
        else
        {
            enemy = Instantiate(trapper);
            Trapper enemyScript = enemy.GetComponent<Trapper>();
            enemyScript.xBounds = target.xBounds;
        }
        enemy.transform.position = target.spawnPos;
    }

    public void Start()
    {
        //SpawnEnemy(enemies[0]);
    }


}
