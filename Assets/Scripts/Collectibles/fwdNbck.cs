using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fwdNbck : MonoBehaviour
{
    private float min;
    private float max;
    // Use this for initialization
    void Start()
    {

        min = transform.position.z;
        max = transform.position.z + 8;

    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * 2, max - min) + min);

    }
}
