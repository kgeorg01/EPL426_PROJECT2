using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for the spike attack. When player steps on them, it start an animation which bring the spikes up
// Spikes have a box collider which cause dmg to the player 
public class trapAttack : MonoBehaviour
{
    private Animator animtrap;
    public AudioSource trapsound;

    
    void Start(){
        animtrap = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) {
            animtrap.SetTrigger("trigger");
        }
    }

    void trapaudio()
    {
        trapsound.Play();
    }
}
