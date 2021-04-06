using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapAttack : MonoBehaviour
{
    private Animator animtrap;
    public AudioSource trapsound;
    // Start is called before the first frame update
    void Start()
    {
        animtrap = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            animtrap.SetTrigger("trigger");
        }
    }

    void trapaudio()
    {
        trapsound.Play();
    }
}
