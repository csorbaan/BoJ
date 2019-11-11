using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public float dmg;
    public float xp;
    public float mHealth;

    NavMeshAgent navMeshAgent;
    Animator anim;
    EnemyHPBar ehp;

    public float lookRadius;
    public float attackRadius;
    public float tagRadius;

    float nextAttack;
    float attackRate = 1f;
    bool agro, tagged;
    Vector3 spawnLoc;

    GameObject player;
    Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        xp = 250;
        dmg = 2;
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = 100;
        mHealth = 100;
        player = PlayerManager.instance.ourPlayer;
        targetPlayer = player.transform;
        spawnLoc = transform.position;
        tagRadius = 5;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        if (distance <= tagRadius && !tagged)
        {
            player.GetComponent<Player>().enemyCount++;
            tagged = true;
        }
        if (distance >= tagRadius && tagged)
        {
            player.GetComponent<Player>().enemyCount--;
            tagged = false;
        }
        if (distance <= lookRadius || agro)
        {
            MoveAndAttack();
        }
        if (Vector3.Distance(spawnLoc, transform.position) >= 10)
        {
            MoveBack();
        }
    }

    void OnDestroy()
    {
        if (tagged)
        {
            player.GetComponent<Player>().enemyCount--;
        }
        if (PlayerManager.instance.ourPlayer != null)
        {
            PlayerManager.instance.ourPlayer.GetComponent<Player>().GetXP(xp);
        }
    }

    public void Hit(float dmg)
    {
        health -= dmg;
        if (health <= 0) Destroy(gameObject);
    }

    void MoveAndAttack()
    {
        navMeshAgent.destination = targetPlayer.position;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance > attackRadius)
        {
            navMeshAgent.isStopped = false;
        }
        else if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= attackRadius)
        {
            transform.LookAt(targetPlayer);

            if (Time.time > nextAttack)
            {
                PlayerManager.instance.ourPlayer.GetComponent<Player>().Hit(dmg);
                nextAttack = Time.time + attackRate;
            }

            navMeshAgent.isStopped = true;
        }
    }

    void MoveBack()
    {
        agro = false;
        navMeshAgent.destination = spawnLoc;
    }

    public void Agro()
    {
        agro = true;
    }
}
