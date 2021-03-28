using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Animator anim;


    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
        
        if (Input.GetAxisRaw("Horizontal") > 0){
            transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.Rotate(Vector3.up, -90 * Time.deltaTime);
        }
       
    }

   
}
