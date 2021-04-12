using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignore : MonoBehaviour
{
    void test()
    {
        tag = "Finish";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Invoke("test", 1);
            
           
        }
        
    }
}
