﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    public NavMeshAgent agent;
    private Animator anim;
    public Collider attackRange;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && !enemyVariables.attacking) anim.Play("walk");
            enemyVariables.attacking = false;
            attackRange.enabled = false;
            if (distance <= agent.stoppingDistance)
            {
                enemyVariables.attacking = true;
                faceTarget();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack2")
                    && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
                {
                    int i = Random.Range(1, 4);
                    anim.Play("attack" + i);
                    Invoke("attack", 5);
                    
                }
            }
        }
        else
        {
            enemyVariables.attacking = false;
            attackRange.enabled = false;
        }
    }

    void attack()
    {
        attackRange.enabled = true;
    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
}