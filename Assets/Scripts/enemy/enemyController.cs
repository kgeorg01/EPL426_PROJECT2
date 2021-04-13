using System.Collections;
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

    void attack()
    {
        attackRange.enabled = true;
    }

    void BanditControl()
    {
        if (enemyVariables.dead)
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
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && !enemyVariables.attacking) anim.Play("walk");
                enemyVariables.attacking = false;
                if (distance <= agent.stoppingDistance)
                {
                    enemyVariables.attacking = true;
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
                enemyVariables.attacking = false;
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
        arrowRB.AddForce((target.position - transform.position).normalized * 15, ForceMode.Impulse);
    }
    void ArcherControl()
    {
        if (enemyVariables.dead)
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
                enemyVariables.attacking = true;
                faceTarget();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack")){
                    anim.Play("attack");
                    Invoke("archeratt", 5);
                }
            }
            else
            {
                enemyVariables.attacking = false;
            }
        }
    }
}
