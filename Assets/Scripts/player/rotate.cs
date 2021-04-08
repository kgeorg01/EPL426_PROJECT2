using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
   
    void Update() {
        transform.Rotate(new Vector3(0, 150 * Time.deltaTime, 0), Space.World);
    }
}
