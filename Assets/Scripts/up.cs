using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class up : MonoBehaviour
{
    // used on the lava on stage 3
    // continusly increase its position to look like it rises
    private void Update()
    {
        this.transform.position += Vector3.up*0.014f;
    }
}
