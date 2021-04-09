using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fwdNbck : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    // Use this for initialization
    void Start()
    {

        min = transform.position.z;
        max = transform.position.z + 10;

    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);

    }
}
