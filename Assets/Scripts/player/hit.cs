using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * The attack animations and attack damage on enemies
 */
public class hit : MonoBehaviour
{
    private string lastanim = "";
    public Animator anim;
	public playerVariables playervar;
    
    private void OnTriggerEnter(Collider other)
    {
        // if sword game in contact with enemy he will take dmg
        if (other.tag.Equals("Enemy") && playerVariables.attacking) {
            // chech if we are still in the current animation
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName(lastanim))
            {
                int damage = (int)Math.Round(playervar.attackDamage);
                // Heavy attack dmg
                if (playerVariables.attackingheavy)
                {
					
                    other.gameObject.GetComponent<enemyVariables>().TakeDamage(damage*2);
                   
                }
                // Light attack dmg
                else if (playerVariables.attackinglight)
                {
                    other.gameObject.GetComponent<enemyVariables>().TakeDamage(damage);
                    
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
