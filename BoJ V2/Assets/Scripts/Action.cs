using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Action : MonoBehaviour
{
    public GameObject EnemyHealth;
    Text EHText;
    public float CHD;
    public float CHC;

    [Header("Stats")]
    public float attackDistance;
    public float attackRate;
    float nextAttack;
    public float dmg;

    NavMeshAgent navMeshAgent;
    Animator anim;
    Enemy enemy;
    EnemyHPBar ehpb;

    Transform targetedEnemy;
    bool enemyClicked;
    bool walking;

    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        EHText = EnemyHealth.GetComponentInChildren<Text>();
        EnemyHealth.SetActive(false);
        ehpb = EnemyHealth.GetComponent<EnemyHPBar>();
    }

    void Update()
    {
        dmg = PlayerManager.instance.ourPlayer.GetComponent<Player>().dmg;
        CHC = PlayerManager.instance.ourPlayer.GetComponent<Player>().CHC;
        CHD = PlayerManager.instance.ourPlayer.GetComponent<Player>().CHD;
        attackRate = PlayerManager.instance.ourPlayer.GetComponent<Player>().AS;
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Physics.Raycast(ray, out hit, 1000))
            {
                navMeshAgent.isStopped = true;
                walking = false;

                if (hit.collider.tag == "Enemy")
                {
                    enemy = hit.collider.gameObject.GetComponent<Enemy>();
                    
                    targetedEnemy = hit.transform;
                    enemyClicked = true;
                    EnemyHealth.SetActive(true);
                    EHText.text = enemy.health.ToString("0");
                    
                }
                else
                {
                    walking = true;
                    enemyClicked = false;
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = hit.point;
                    EnemyHealth.SetActive(false);
                }
            }
        }

        if (enemyClicked)
        {
            MoveAndAttack();
            ehpb.barUpload(enemy.mHealth, enemy.health);
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            walking = false;
        }
        else
        {
            walking = true;
        }
    }

    void MoveAndAttack()
    {
        if (targetedEnemy == null)
        {
            return;
        }

        navMeshAgent.destination = targetedEnemy.position;

        if (navMeshAgent.remainingDistance > attackDistance)
        {
            navMeshAgent.isStopped = false;
            walking = true;
        }
        else
        {
            transform.LookAt(targetedEnemy);
            Vector3 dirToAttack = targetedEnemy.transform.position - transform.position;

            if (Time.time > nextAttack)
            {
                if (Random.Range(0, 100) <= CHC)
                {
                    dmg = dmg * (1 + CHD / 100);
                }
                enemy.Hit(dmg);
                if (!enemy || enemy.health <= 0) EnemyHealth.SetActive(false);
                else EHText.text = enemy.health.ToString();
                nextAttack = Time.time + attackRate;
            }
            navMeshAgent.isStopped = true;
            walking = false;
        }
    }
}
