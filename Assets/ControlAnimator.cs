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

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("block", playerVariables.blocking);
        anim.SetFloat("vertical", playerVariables.mV);
        anim.SetFloat("horizontal", playerVariables.mH);
    }
}
