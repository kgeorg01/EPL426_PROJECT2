using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : MonoBehaviour
{
    private bool opened = false;
    public Animator gate;
    // Update is called once per frame
    void Update()
    {
        if (!opened && transform.childCount == 0)
        {
            opened = true;
            gate.Play("Open");
        }
    }
}
