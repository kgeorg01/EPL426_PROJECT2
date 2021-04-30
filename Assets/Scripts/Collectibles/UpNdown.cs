using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A script to make an object go up and down
// Used in the coins' movement
public class UpNdown : MonoBehaviour
{
    public AnimationCurve myCurve;
    private float height;
    private float rand;

    private void Start() {
        // Get the starting height of the coin
        height = transform.position.y;
        // A random number to make the coins start their movement in random positions
        rand = Random.Range(0f, 5f);
    }

    // Update position based on the height, the random number and the curve we defined in Unity
    void Update() {
        transform.position = new Vector3(transform.position.x, height + myCurve.Evaluate(((Time.time+rand) % myCurve.length)), transform.position.z);
    }
}
