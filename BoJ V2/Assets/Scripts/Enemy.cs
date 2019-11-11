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
    public float wanderRadius;

    float nextAttack;
    float attackRate = 1f;
    bool agro, tagged, wandering;
    public bool mb;
    Vector3 spawnLoc, wanderLoc;

    GameObject player;
    Transform targetPlayer;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = PlayerManager.instance.ourPlayer;
        xp = 50;
        dmg = (4 + player.GetComponent<Player>().lvl) * (1.12f + player.GetComponent<Player>().lvl / 13);
        mHealth = (56 + player.GetComponent<Player>().lvl * 5) * (1.12f + player.GetComponent<Player>().lvl / 13);
        health = mHealth;
        targetPlayer = player.transform;
        spawnLoc = transform.position;
        tagRadius = 4;
        wanderRadius = 3;
        wanderLoc = spawnLoc;
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
        if (distance <= lookRadius && !mb)
        {
            Agro();
        }
        if (agro)
        {
            wandering = false;
            MoveAndAttack();
        }
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= 0.1)
        {
            mb = false;
            wandering = true;
            //navMeshAgent.speed = 2;
        }
        if (wandering)
        {
            Wander();
        }
        if (Vector3.Distance(spawnLoc, transform.position) >= 10)
        {
            wandering = false;
            MoveBack();
        }
        if (targetPlayer.GetComponent<Player>().hp <= 0)
        {
            MoveBack();
        }
    }

    void OnDestroy()
    {
        if (PlayerManager.instance.ourPlayer != null)
        {
            if (tagged)
            {
                player.GetComponent<Player>().enemyCount--;
            }
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

    void Wander()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= 0.1)
        {
            float xWanderPos = spawnLoc.x + Random.Range(-wanderRadius, wanderRadius);
            float zWanderPos = spawnLoc.z + Random.Range(-wanderRadius, wanderRadius);

            wanderLoc = new Vector3(xWanderPos, 0, zWanderPos);
            transform.LookAt(wanderLoc);
            navMeshAgent.destination = wanderLoc;
        }
    }

    void MoveBack()
    {
        mb = true;
        agro = false;
        navMeshAgent.destination = spawnLoc;
        navMeshAgent.isStopped = false;
        health = mHealth;
    }

    public void Agro()
    {
        agro = true;
        //navMeshAgent.speed = 3.5f;
    }

    public void EnemyBoss()
    {
        xp = 5400;
        dmg = 100;
        mHealth = 10000;
        health = mHealth;
        lookRadius = 6;
        attackRadius = 3.5f;
    }
}
