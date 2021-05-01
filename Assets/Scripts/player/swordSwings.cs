using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Choose a random swing audio
 */
public class swordSwings : MonoBehaviour
{
    public AudioSource swing1;
    public AudioSource swing2;
    public AudioSource swing3;
    public AudioSource swing4;

    void playswingaudio()
    {
        int i = Random.Range(0, 3);
        if (i == 0) swing1.Play();
        else if (i == 1) swing2.Play();
        else if (i == 2) swing3.Play();
        else swing4.Play(); 
    }
}
