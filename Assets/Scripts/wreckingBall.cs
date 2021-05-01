using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wreckingBall : MonoBehaviour
{
    // used for our wrecking ball trap
    public Rigidbody wb;

    // when player steps on trigger, we disable is kinematic to make the wrecking ball move
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            wb.isKinematic = false;
        }
    }
}
