using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool hasSpawned;
    int spawnAmount;
    private Transform player;
    float respawnTime, nextRespawnTime;

    public GameObject[] enemySpawn;
    public GameObject[] enemies;

    public int maxSpawn;
    public float spawnRange;
    public float detectionRange;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.ourPlayer.transform;
        spawnRange = 3;
        detectionRange = 7;
        maxSpawn = 4;
        respawnTime = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            if (!hasSpawned)
            {
                SpawnEnemy();
                nextRespawnTime = Time.time + respawnTime;
            }
            else
            {
                for (int i = 0; i < spawnAmount; i++)
                {
                    if (enemies[i] != null)
                    {
                        if (!enemies[i].GetComponent<Enemy>().mb)
                        {
                            enemies[i].GetComponent<Enemy>().Agro();
                        }
                    }
                }
            }
        }
        if (Time.time > nextRespawnTime && hasSpawned)
        {
            hasSpawned = false;
        }
    }

    void SpawnEnemy()
    {
        hasSpawned = true;
        spawnAmount = Random.Range(1, maxSpawn + 1);
        enemies = new GameObject[spawnAmount];
        for (int i = 0; i < spawnAmount; i++)
        {
            float xSpawnPos = transform.position.x + Random.Range(-spawnRange, spawnRange);
            float zSpawnPos = transform.position.z + Random.Range(-spawnRange, spawnRange);

            float rot = 360f + Random.value;
            Quaternion spawnRot = Quaternion.Euler(0, rot, 0);

            Vector3 spawnPoint = new Vector3(xSpawnPos, 0, zSpawnPos);
            GameObject newEnemy = (GameObject)Instantiate(enemySpawn[Random.Range(0, enemySpawn.Length)], spawnPoint, spawnRot);
            newEnemy.GetComponent<Enemy>().Agro();
            newEnemy.GetComponent<Enemy>().sp = this;
            enemies[i] = newEnemy;
        }
    }
}
