using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to open the gates when all enemies of the current battle die (stage 4)
public class opendoor : MonoBehaviour
{
    private bool opened = false;
    public Animator gate;
    
    void Update()
    {
        // play the animation to open the gates
        if (!opened && ChildCountActive(this.transform) == 0)
        {
            opened = true;
            gate.Play("Open");
        }
    }

    // we putted as childs the enemies
    // when all enemies are dead, then the gate will open
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
