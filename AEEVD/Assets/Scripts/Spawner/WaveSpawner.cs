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
    public GameObject[] Enemies;

    private Wave currentWave;

    public int CurrentWaveNum {get{return _currentWaveNum + 1;} set{;}}
    public float randomWaveSpawnInterval;

    private int _currentWaveNum;
    private float nextSpawnTime = 0;
    private int amtEnemies;
    private float nextWaveTimer;
    private bool canSpawn = true;
    private bool randomizeWaves;
    private int randomWaveNumEnemies;

    void Start()
    {
        _currentWaveNum = 0;
        nextWaveTimer = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    { 
        if(waves.Length <= _currentWaveNum)
        {
            SpawnRandomizedWaves();
        }
        else
        {
            currentWave = waves[_currentWaveNum];
            SpawnWave();
        }
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length == 0 && !canSpawn)
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
        randomWaveNumEnemies = _currentWaveNum * 3 / 2;
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

    void SpawnRandomizedWaves()
    {
        if(canSpawn && nextSpawnTime < Time.time)
        {
            for(int i = 0; i < _currentWaveNum/4; i++)
            {
                GameObject randomEnemy = Enemies[Random.Range(0, Enemies.Length)];
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
                randomWaveNumEnemies--;
                amtEnemies++;
            }
            if(_currentWaveNum/4 % 4 != 0 && (_currentWaveNum * 3 / 2 - amtEnemies) < 4)
            {
                for(int i = 0; i < _currentWaveNum * 3 / 2 - amtEnemies; i++)
                {
                    GameObject randomEnemy = Enemies[Random.Range(0, Enemies.Length)];
                    Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    Instantiate((Enemies[UnityEngine.Random.Range(0, Enemies.Length - 1)]), randomPoint.position, Quaternion.identity);
                    randomWaveNumEnemies--;
                    amtEnemies++;
                }
            }
            nextSpawnTime = Time.time + randomWaveSpawnInterval;
            if(randomWaveNumEnemies <= 0)
            {
                canSpawn = false;
            }
            Debug.Log(canSpawn + " " + randomWaveNumEnemies + " " + _currentWaveNum);
        }
    }
    
}
