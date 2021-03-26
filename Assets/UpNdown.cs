using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpNdown : MonoBehaviour
{
     public AnimationCurve myCurve;
    private float height;
    private void Start()
    {
        height = transform.position.y;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, height + myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
    }
}
