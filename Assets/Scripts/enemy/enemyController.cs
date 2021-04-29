﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    public NavMeshAgent agent;
    public Animator anim;
    public Collider attackRange;
    public int enemyType = 0;
    public GameObject healthbar;
    public GameObject arrow;
    public enemyVariables enemyVar;

    private int counter = 0;
    private float lavaPoolDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == 1)
        {
            BanditControl();
        }
        else if (enemyType == 2)
        {
            ArcherControl();
        }
        
    }
    void FixedUpdate()
    {
        if (lavaPoolDamage >= 1.5)
        {
            lavaPoolDamage = 0;
        }
        lavaPoolDamage += Time.fixedDeltaTime;
    }

    void attack()
    {
        attackRange.enabled = true;
    }

    void BanditControl()
    {
        if (enemyVar.dead)
        {
            attackRange.enabled = false;
            anim.Play("Dead");
            gameObject.GetComponent<MeshCollider>().enabled = false;
            Destroy(healthbar);
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && !enemyVar.attacking) anim.Play("walk");
                enemyVar.attacking = false;
                if (distance <= agent.stoppingDistance)
                {
                    enemyVar.attacking = true;
                    faceTarget();
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack2")
                        && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack3"))
                    {
                        Invoke("attack", 2f);
                        int i = Random.Range(1, 4);
                        anim.Play("attack" + i);
                    }
                }
            }
            else
            {
                enemyVar.attacking = false;
                attackRange.enabled = false;
            }
        }
    }

    void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }

    void archeratt()
    {
        GameObject instarrow = Instantiate(arrow, transform.position + Vector3.up + (target.position - transform.position).normalized, transform.rotation);
        Rigidbody arrowRB = instarrow.GetComponent<Rigidbody>();
        arrowRB.AddForce((target.position - transform.position).normalized * 25, ForceMode.Impulse);
    }
    void ArcherControl()
    {
        if (enemyVar.dead)
        {
            anim.Play("Dead");
            gameObject.GetComponent<MeshCollider>().enabled = false;
            Destroy(healthbar);
        }
        else
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                enemyVar.attacking = true;
                faceTarget();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                    anim.Play("attack");
                    Invoke("archeratt", 5);
                }
            }
            else
            {
                enemyVar.attacking = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag.Equals("Arrow"))
        {
            enemyVar.TakeDamage(7);
        }
        if (collision.gameObject.tag.Equals("Arrow15"))
        {
            enemyVar.TakeDamage(10);
        }
        if (collision.gameObject.tag.Equals("SpearTrap"))
        {
            enemyVar.TakeDamage(20);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Lava")
        {
            enemyVar.TakeDamage(999);
        }

        if (col.gameObject.tag.Equals("TrapBlades"))
        {
        
            enemyVar.TakeDamage(5);
        }

        if (col.gameObject.tag.Equals("GenTrap"))
        {
            enemyVar.TakeDamage(5);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.tag == "Poison")
        {
            counter++;
            if (counter == 20)
            {
                counter = 0;
                enemyVar.TakeDamage(1);
            }
        }
        if (col.tag == "Fire")
        {
            counter++;
            if (counter == 20)
            {
                counter = 0;
                enemyVar.TakeDamage(1);
            }
        }

        if (col.tag == "LavaPool")
        {
            if (lavaPoolDamage >= 1.5)
            {
                enemyVar.TakeDamage(1);
            }
        }

        if (col.gameObject.tag.Equals("Saw"))
        {
            enemyVar.TakeDamage(2);
        }
    }

      

    

}
