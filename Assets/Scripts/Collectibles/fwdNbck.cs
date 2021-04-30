using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script to make an object go forward and back
// Used in the moving platform
public class fwdNbck : MonoBehaviour
{
    private float min;
    private float max;

    void Start() {
        // the closest (min) and farthest (max) position object can go
        min = transform.position.z;
        max = transform.position.z + 8;

    }

    // Update the position of the object
    void Update() {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * 2, max - min) + min);
    }
}
