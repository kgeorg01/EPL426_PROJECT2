using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firestream : MonoBehaviour
{
    public ParticleSystem fire;
    private float duration = 0;
    private float change = 0;
    private float rotationtime = 3;
    // Update is called once per frame
    void Update()
    {
        duration = Time.time - change;
        if (duration > rotationtime)
        {
            change = Time.time;
            if (fire.isPlaying) {
                fire.Stop();
                rotationtime = 5;
            }
            else {
                fire.Play();
                rotationtime = 3;
             }
        }
    }
}
