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

    public float lookRadius, attackRadius, tagRadius, wanderRadius;

    float nextAttack;
    float attackRate = 1f;
    bool agro, tagged, wandering, boss;
    public bool mb;
    Vector3 spawnLoc, wanderLoc;

    public GameObject gameManager;
    GameObject player;
    Transform targetPlayer;
    Type type;
    public SpawnPoint sp;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = PlayerManager.instance.ourPlayer;
        type = new Type();
        if (boss)
        {
            LoadStats(new Boss());
        }
        else
            LoadStats(new Normal());
        //xp = 50;
        //dmg = (4 + player.GetComponent<Player>().lvl) * (1.12f + player.GetComponent<Player>().lvl / 13);
        //mHealth = (56 + player.GetComponent<Player>().lvl * 5) * (1.12f + player.GetComponent<Player>().lvl / 13);
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
            navMeshAgent.speed = 2;
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
        if (boss)
        {
            Spawn.instance.nextBoss = true;
        }
    }

    public void LoadStats(EnemyType eType)
    {
        type.TypeChange(eType);
        type.Enemy(player.GetComponent<Player>().lvl);
        mHealth = type.mHealth;
        dmg = type.dmg;
        xp = type.xp;
        lookRadius = type.lookRadius;
        attackRadius = type.attackRadius;
        transform.name = type.name;
        transform.localScale = type.scale;
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
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = 3.5f;
        }
    }

    public void EnemyBoss()
    {
        boss = true;
        //xp = 5400;
        //dmg = 100;
        //mHealth = 5000;
        //health = mHealth;
        //lookRadius = 6;
        //attackRadius = 3.5f;
        //transform.name = "EnemyBoss";
        //transform.localScale = new Vector3(3, 3, 3);
    }
}

#region EnemyType
public abstract class EnemyType
{
    public float mHealth, dmg, xp, lookRadius, attackRadius;
    public string name;
    public Vector3 scale;

    public abstract void Stats(int lvl);
}

public class Normal : EnemyType
{
    public override void Stats(int lvl)
    {
        xp = 50 + 6 * lvl;
        dmg = (4 + lvl) * (1.12f + lvl / 13);
        mHealth = (56 + lvl * 5) * (1.12f + lvl / 13);
        lookRadius = 4;
        attackRadius = 1.5f;
        name = "Enemy";
        scale = new Vector3(1, 1, 1);
    }
}

public class Boss : EnemyType
{
    public override void Stats(int lvl)
    {
        xp = 5400;
        dmg = 100;
        mHealth = 5000;
        lookRadius = 6;
        attackRadius = 3.5f;
        name = "EnemyBoss";
        scale = new Vector3(3, 3, 3);
    }
}

public class Type
{
    private EnemyType _EnemyType;

    public float mHealth, dmg, xp, lookRadius, attackRadius;
    public string name;
    public Vector3 scale;

    public void TypeChange(EnemyType t)
    {
        _EnemyType = t;
    }

    public void Enemy(int lvl)
    {
        _EnemyType.Stats(lvl);
        mHealth = _EnemyType.mHealth;
        dmg = _EnemyType.dmg;
        xp = _EnemyType.xp;
        lookRadius = _EnemyType.lookRadius;
        attackRadius = _EnemyType.attackRadius;
        name = _EnemyType.name;
        scale = _EnemyType.scale;
    }
}
#endregion
