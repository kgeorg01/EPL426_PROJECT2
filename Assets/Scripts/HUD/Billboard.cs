using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for the healthbar of the enemies
public class Billboard : MonoBehaviour
{
    public Transform cameera;

    // find the postion of the main camera
    public void Start ()
    {
        GameObject[] camera = GameObject.FindGameObjectsWithTag("MainCamera");
        cameera= camera[0].transform;
    }

    // Position healthbar towards camera
    void LateUpdate()
    {
        transform.LookAt(transform.position + cameera.forward);
    }
}
