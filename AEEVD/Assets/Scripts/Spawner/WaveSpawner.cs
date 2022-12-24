using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int numEnemies;
    public GameObject[] Enemies;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public GameObject player;
    private Wave currentWave;

    private int _currentWaveNum;
    public int CurrentWaveNum {get{return _currentWaveNum + 1;} set{;}}
    private float nextSpawnTime;
    private int amtEnemies;
    private float nextWaveTimer;
    private bool canSpawn = true;

    void Start()
    {
        nextWaveTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        currentWave = waves[_currentWaveNum];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length == 0 && !canSpawn && _currentWaveNum + 1 != waves.Length)
        {  
            nextWaveTimer += Time.deltaTime;
            if(nextWaveTimer >= 3f)
            {
                SpawnNextWave();
            }
        }
            
    }

    void SpawnNextWave()
    {   
        amtEnemies = 0;
        nextWaveTimer = 0f;
        _currentWaveNum++;
        canSpawn = true;
        player.GetComponent<PlayerHealth>().healEveryRound(_currentWaveNum);
    }
    

    void SpawnWave()
    {
        if(canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.Enemies[amtEnemies];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            amtEnemies++;
            currentWave.numEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if(currentWave.numEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }

    
}
