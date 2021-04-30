using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class up : MonoBehaviour
{
    private void Update()
    {
        this.transform.position += Vector3.up*0.014f;
    }
}
