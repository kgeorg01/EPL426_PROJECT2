using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerButton : MonoBehaviour
{
    // Used for the gate with the button to open
    public Animator gate;

    // when we step on the button play the animation to open the gate
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Animator>().Play("button"); // makes the button go down
        Invoke("openGate", 2); // opens the gates
    }

    void openGate()
    {
        gate.Play("gate");
    }
    
}
