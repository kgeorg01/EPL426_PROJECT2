using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cameera;

    public void Start ()
    {
        GameObject[] camera = GameObject.FindGameObjectsWithTag("MainCamera");
        cameera= camera[0].transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cameera.forward);
    }
}
