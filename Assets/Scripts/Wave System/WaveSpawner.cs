using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class WaveSpawner : MonoBehaviour
{
   
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
 
    public SpawnPoint[] spawnPoints;
    public int spawnIndex;
 
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
 
    [Header("Enemy Initial Movement")]
    [SerializeField] private Vector2 initialMovementDirection = Vector2.down;
    [SerializeField] private float initialMovementDistance = 3f;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTimer <=0)
        {
            //spawn an enemy
            if(enemiesToSpawn.Count >0)
            {
                spawnPoints[spawnIndex].SpawnEnemy(enemiesToSpawn[0]);
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;

                if(spawnIndex + 1 <= spawnPoints.Length-1)
                {
                    spawnIndex++;
                }
                else
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
 
        if(waveTimer<=0 && spawnedEnemies.Count <=0)
        {
            currWave++;
            GenerateWave();
        }
    }
 
    public void GenerateWave()
    {
        waveValue = currWave * 10;
        GenerateEnemies();
 
        spawnInterval = waveDuration / enemiesToSpawn.Count; // gives a fixed time between each enemies
        waveTimer = waveDuration; // wave duration is read only
    }
 
    public void GenerateEnemies()
    {
        // Create a temporary list of enemies to generate
        // 
        // in a loop grab a random enemy 
        // see if we can afford it
        // if we can, add it to our list, and deduct the cost.
 
        // repeat... 
 
        //  -> if we have no points left, leave the loop
 
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue>0 || generatedEnemies.Count <50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
 
            if(waveValue-randEnemyCost>=0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue<=0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }
  
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}