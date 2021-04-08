using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlAnimator : MonoBehaviour
{
    private Animator anim;
    public AudioSource death;
    public AudioSource death2;
    public CanvasGroup GameOver;
    private float Duration = 4f;
    private bool over = false;
    private float counter = 0;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    private void checkAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy2") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Heavy3"))
        {
            playerVariables.attacking = true;
            playerVariables.attackingheavy = true;
            playerVariables.attackinglight = false;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Light1") || anim.GetCurrentAnimatorStateInfo(0).IsName("Light2") 
            || anim.GetCurrentAnimatorStateInfo(0).IsName("Light3"))
        {
            playerVariables.attacking = true;
            playerVariables.attackingheavy = false;
            playerVariables.attackinglight = true;
        }
        else
        {
            playerVariables.attacking = false;
            playerVariables.attackingheavy = false;
            playerVariables.attackinglight = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkAttack();
        anim.SetBool("block", playerVariables.blocking);
        anim.SetFloat("vertical", playerVariables.mV);
        anim.SetFloat("horizontal", playerVariables.mH);
        if (playerVariables.dead)
        {
            if (!playerVariables.falling)
            {
                anim.Play("Death");
                if (!over)
                {
                    over = true;
                    death.Play();
                }
            }
            else
            {
                anim.Play("Falling");
                if (!over)
                {
                    over = true;
                    death2.Play();
                    death.Play();
                }
            }
            counter += Time.deltaTime;
            GameOver.alpha = Mathf.Lerp(0, 1, counter / Duration);
            if (counter > 7f) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
