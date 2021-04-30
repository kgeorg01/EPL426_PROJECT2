using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerButton : MonoBehaviour
{
    public Animator gate;
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Animator>().Play("button");
        Invoke("openGate", 2);
    }

    void openGate()
    {
        gate.Play("gate");
    }
    
}
