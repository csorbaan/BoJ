using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    public GameObject enemy;
    public GameObject enemyBoss;

    float nextBossSpawn, bossSpawn;
    public bool nextBoss;

    // Start is called before the first frame update
    void Start()
    {
        //enemyBoss.GetComponent<Enemy>().EnemyBoss();
        bossSpawn = 120;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    Instantiate(enemy, new Vector3(180, 0, 150), Quaternion.identity);
        //}
        if (nextBoss)
        {
            nextBossSpawn = Time.time + bossSpawn;
            nextBoss = false;
        }
        if (enemyBoss == null && Time.time > nextBossSpawn)
        {
            enemyBoss = Instantiate(enemy, new Vector3(183, 0, 147), Quaternion.identity);
            enemyBoss.GetComponent<Enemy>().EnemyBoss();
        }
    }

    void Awake()
    {
        instance = this;
    }
}
