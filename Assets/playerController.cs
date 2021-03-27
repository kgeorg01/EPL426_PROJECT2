using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Animator anim;
    public float speed = 5;
   
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Horizontal") > 0){
            transform.Rotate(Vector3.up, 45 * Time.deltaTime);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.Rotate(Vector3.up, -45 * Time.deltaTime);
        }
        else if(Input.GetAxisRaw("Vertical") > 0)
        {
            transform.position += Vector3.forward * 5 * Time.deltaTime;

        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            transform.position += -Vector3.forward * 2 * Time.deltaTime;

        }
    }

   
}
