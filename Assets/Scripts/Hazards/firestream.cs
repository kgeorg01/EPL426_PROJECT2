using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for the fire statues
public class firestream : MonoBehaviour
{
    public ParticleSystem fire;
    public Collider col;
    private float duration = 0;
    private float change = 0;
    private float rotationtime = 3;
    public AudioSource firesound;
    // Update is called once per frame
    void Update() {
        // when time passes we check in which state we are
        duration = Time.time - change;
        if (duration > rotationtime)
        {
            // if the firestream is active disable it and wait 5 seconds to enable it again
            change = Time.time;
            if (fire.isPlaying) {
                fire.Stop();
                firesound.Stop();
                col.enabled = false;
                rotationtime = 5;
            }
            // if the firestream is inactive enable it and wait 2 seconds to disable it again
            else
            {
                fire.Play();
                firesound.Play();
                col.enabled = true;
                rotationtime = 2;
             }
        }
    }
}
