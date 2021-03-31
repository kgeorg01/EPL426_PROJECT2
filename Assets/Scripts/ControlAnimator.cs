using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimator : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void checkAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy3") || anim.GetCurrentAnimatorStateInfo(0).IsName("Light1") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Light2") || anim.GetCurrentAnimatorStateInfo(0).IsName("Light3"))
        {
            playerVariables.attacking = true;
        }
        else
        {
            playerVariables.attacking = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkAttack();
        anim.SetBool("block", playerVariables.blocking);
        anim.SetFloat("vertical", playerVariables.mV);
        anim.SetFloat("horizontal", playerVariables.mH);
    }
}
