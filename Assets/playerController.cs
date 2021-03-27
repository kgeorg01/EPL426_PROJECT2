using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Animator anim;
    public Transform pos;
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Rotate(Vector3.up, 45 * Time.deltaTime);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Rotate(Vector3.up, -45 * Time.deltaTime);
        }
    }
}
