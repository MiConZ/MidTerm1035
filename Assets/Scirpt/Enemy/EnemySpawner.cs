using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private int waveNumber = 1;
    public float minimumTimeBetweenWaves = 1.5f; 
    public float difficultyScale = 0.1f;        
    private float currentTimeBetweenWaves;
   


    void Start()
    {
        currentTimeBetweenWaves = timeBetweenWaves;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            bool isBossWave = (waveNumber % 5 == 0);

            if (isBossWave)
            {
                SpawnBoss();
                
                yield return new WaitForSeconds(1.5f);
            }

            int enemiesToSpawn = waveNumber * 2;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();

                
                float spawnDelay = Mathf.Max(0.2f, 1f - (waveNumber * 0.05f));
                yield return new WaitForSeconds(spawnDelay);
            }

          
            waveNumber++;

            currentTimeBetweenWaves = Mathf.Max(minimumTimeBetweenWaves, currentTimeBetweenWaves - difficultyScale);

            yield return new WaitForSeconds(currentTimeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        int randEnemy = Random.Range(0, enemyPrefabs.Length);
        int randPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefabs[randEnemy], spawnPoints[randPoint].position, Quaternion.identity);
    }
    void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            int randPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(bossPrefab, spawnPoints[randPoint].position, Quaternion.identity);
            Debug.Log("⚠️ Boss Spawned at Wave: " + waveNumber);
        }
        else
        {
            Debug.LogWarning("ยังไม่ได้กำหนด Boss Prefab ใน Inspector!");
        }
    }
}