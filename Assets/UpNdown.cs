using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpNdown : MonoBehaviour
{
    public AnimationCurve myCurve;
    private float height;
    private float rand;
    private void Start()
    {
        height = transform.position.y;
        rand = Random.Range(0f, 5f);
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, height + myCurve.Evaluate(((Time.time+rand) % myCurve.length)), transform.position.z);
    }
}
