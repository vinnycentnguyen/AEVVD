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

    private Wave currentWave;
    private int currentWaveNum;
    private float nextSpawnTime;
    private int amtEnemies;

    private bool canSpawn = true;

    private void Update()
    {
        currentWave = waves[currentWaveNum];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length == 0 && !canSpawn && currentWaveNum+1 != waves.Length)
        {
            SpawnNextWave();
        }
            
    }

    void SpawnNextWave()
    {   
        amtEnemies = 0;
        currentWaveNum++;
        canSpawn = true;
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
