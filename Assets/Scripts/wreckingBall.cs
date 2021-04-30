using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wreckingBall : MonoBehaviour
{
    public Rigidbody wb;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            wb.isKinematic = false;
        }
    }
}
