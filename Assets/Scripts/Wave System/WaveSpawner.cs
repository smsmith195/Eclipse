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

    [Header("Wave Settings")]
    [SerializeField] private int baseWaveValue = 10;
    [SerializeField] private float waveValueMultiplier = 1.2f;
    [SerializeField] private int maxEnemiesPerWave = 20;

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
            if(enemiesToSpawn.Count >0)
            {
                GameObject enemyPrefab = enemiesToSpawn[0];
                GameObject spawnedEnemy = spawnPoints[spawnIndex].SpawnEnemy(enemyPrefab);
                if (spawnedEnemy != null)
                {
                    spawnedEnemies.Add(spawnedEnemy);
                }
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
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
 
        // Remove any destroyed enemies from the list
        spawnedEnemies.RemoveAll(enemy => enemy == null);
 
        if(waveTimer<=0 && spawnedEnemies.Count <=0)
        {
            currWave++;
            GenerateWave();
        }
    }
 
    public void GenerateWave()
    {
        // Calculate wave value with diminishing returns
        waveValue = Mathf.RoundToInt(baseWaveValue * Mathf.Pow(waveValueMultiplier, currWave));
        GenerateEnemies();
 
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }
 
    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        int remainingValue = waveValue;
        
        while(remainingValue > 0 && generatedEnemies.Count < maxEnemiesPerWave)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
 
            if(remainingValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);
                remainingValue -= randEnemyCost;
            }
            else
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