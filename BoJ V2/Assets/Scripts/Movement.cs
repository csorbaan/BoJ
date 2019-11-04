using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject e_HP;
    Text e_HP_text;

    [Header("Stats")]
    public float attackDistance;
    public float attackRate;
    float nextAttack;
    public float dmg;
    
    NavMeshAgent navMeshAgent;
    Animator anim;
    Enemy enemy;

    Transform targetedEnemy;
    bool enemyClicked;
    bool walking;

    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        e_HP_text = e_HP.GetComponentInChildren<Text>();
        e_HP.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Input.GetMouseButton(0))
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
                    e_HP.SetActive(true);
                    e_HP_text.text = enemy.health.ToString();
                }
                else
                {
                    walking = true;
                    enemyClicked = false;
                    navMeshAgent.isStopped = false;
                    navMeshAgent.destination = hit.point;
                    e_HP.SetActive(false);
                }
            }
        }

        if (enemyClicked)
        {
            MoveAndAttack();
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
                enemy.Hit(dmg);
                if (!enemy) e_HP.SetActive(false);
                else e_HP_text.text = enemy.health.ToString();
                nextAttack = Time.time + attackRate;
            }
            navMeshAgent.isStopped = true;
            walking = false;
        }
    }
}
