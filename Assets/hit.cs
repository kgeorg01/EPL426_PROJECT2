using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    private string lastanim = "";
    public Animator anim;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy") && playerVariables.attacking) {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(lastanim))
            {
                if (playerVariables.attackingheavy)
                {
                    other.gameObject.GetComponent<enemyVariables>().TakeDamage(20);
                }
                else if (playerVariables.attackinglight)
                {
                    other.gameObject.GetComponent<enemyVariables>().TakeDamage(10);
                }
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light1")) lastanim = "Light1";
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light2")) lastanim = "Light2";
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light3")) lastanim = "Light3";
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy1")) lastanim = "Heavy1";
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy2")) lastanim = "Heavy2";
                else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy3")) lastanim = "Heavy3";
                else lastanim = "";
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) lastanim = "";
    }
}
