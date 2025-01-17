using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; // this is required to use IAstarAI class

// DOCUMENTATION
// https://docs.unity3d.com/Manual/layers-and-layermasks.html
// https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
// https://www.youtube.com/watch?v=dmQyfWxUNPw

public class LineOfSight : MonoBehaviour
{

    public float rotationSpeed;
    public float visionDistance;
    public GameObject enemy;
    public bool isSpawner = false;

    private Transform player;
    private IAstarAI enemyChase;
    private RangedEnemyController rangedEnemy;
    //private Enemy en;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public float Timer;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyChase = enemy.GetComponent<IAstarAI>();
        rangedEnemy = enemy.GetComponent<RangedEnemyController>();
    }
  
    // Update is called once per frame
    void Update()
    {    

        Timer -= Time.deltaTime;
        // rotate looking for player
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // will only hit objects on the obstacle layer
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, visionDistance, 8);
        
        if(hitInfo.collider != null) 
        {
            // debug stuff
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            // melee enemy targetting
            if(hitInfo.collider.tag == "Player" && enemyChase != null && !isSpawner)
            {
                // chase player
                //Timer -= Time.deltaTime;
                enemyChase.destination = player.position;
                //Debug.Log("spawn");
                
            }
            else if(hitInfo.collider.tag == "Player" && enemyChase != null && isSpawner)
            {
                if(Timer <=0f)
                {
                    for(int i = 0;i<5;i++)
                    {
                        int randEnemy = Random.Range(0, enemyPrefabs.Length);
                        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
                
                        Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, Quaternion.identity);
                        
                    }
                    Timer = 20;
                }
                enemyChase.destination = player.position;
            }
            // ranged enemy targeting
            else if(hitInfo.collider.tag == "Player" && rangedEnemy != null) 
            {
                rangedEnemy.setRange(true);
            }
        }
        else {
            Debug.DrawLine(transform.position, transform.position + transform.right * visionDistance, Color.green);
        }
    }
}
