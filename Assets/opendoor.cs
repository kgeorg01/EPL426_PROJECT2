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
        if (!opened && ChildCountActive(this.transform) == 0)
        {
            opened = true;
            gate.Play("Open");
        }
    }

    public int ChildCountActive(Transform t)
    {
        int k = 0;
        foreach (Transform c in t)
        {
            if (c.gameObject.activeSelf)
                k++;
        }
        return k;
    }

}
