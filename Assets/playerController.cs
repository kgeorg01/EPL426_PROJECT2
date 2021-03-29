using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Animator anim;
    private bool grounded;
    private bool running;
    private Rigidbody rb;
    private float jump;
    private float speed;
    private Vector3 movement;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        grounded = true;
        jump = 5;
        speed = 5;
    }
   
    void FixedUpdate()
    {
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        movement.Set(mH, 0f, mV);
        movement = movement.normalized * speed * Time.deltaTime;
        movement = transform.position + (transform.forward * movement.z) + (transform.right * movement.x);

        if (mV > 0) {
            rb.MovePosition(movement);
        }
        else if (mV < 0)
        {
            rb.MovePosition(movement);
        }

        if (mH > 0) {
            transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        }
        else if (mH < 0) {
            transform.Rotate(Vector3.up, -90 * Time.deltaTime);
        }

        anim.SetFloat("vertical", mV);
        anim.SetFloat("horizontal", mH);
        if (Input.GetKeyDown("space")) { 
            if (grounded) { 
                anim.SetTrigger("jump");
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                grounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody.tag.Equals("Ground"))
        {
            grounded = true;
        }
    }
}
