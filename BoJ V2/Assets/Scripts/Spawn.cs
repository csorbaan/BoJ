using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemyBoss;

    // Start is called before the first frame update
    void Start()
    {
        enemyBoss.GetComponent<Enemy>().EnemyBoss();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(enemy, new Vector3(180, 0, 150), Quaternion.identity);
        }
    }
}
