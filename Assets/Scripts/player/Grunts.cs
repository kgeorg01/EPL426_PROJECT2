using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Grunt Sounds
 */
public class Grunts : MonoBehaviour
{
    public AudioSource grunt1;
    public AudioSource grunt2;
    public AudioSource grunt3;

    void grunts()
    {
        int i = Random.Range(0, 2);
        if (i == 0) grunt1.Play();
        else if (i == 1) grunt2.Play();
        else grunt3.Play();
    }
}
