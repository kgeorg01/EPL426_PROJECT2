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
    public AudioSource walk;
    public AudioSource jumpaudio;
    public AudioSource armouraudio;
    public int health = 100;
    public int armour = 50;
    public int gold = 0;

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
            if (!walk.isPlaying) walk.Play();
        }
        else if (mV < 0) {
            rb.MovePosition(movement);
            if (!walk.isPlaying) walk.Play();
        }
        else {
            if (walk.isPlaying) walk.Stop();
        }

        if (mH > 0) transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        else if (mH < 0) transform.Rotate(Vector3.up, -90 * Time.deltaTime);
        
        if (mH != 0 || mV != 0)
        {
            if (!armouraudio.isPlaying) armouraudio.Play();
        }
        else armouraudio.Pause();
        anim.SetFloat("vertical", mV);
        anim.SetFloat("horizontal", mH);
        if (Input.GetKeyDown("space")) { 
            if (grounded) { 
                anim.SetTrigger("jump");
                jumpaudio.Play();
                rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
                grounded = false;
            }
        }
        if (jumpaudio.isPlaying) walk.Stop(); 
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody.tag.Equals("Ground")) {
            grounded = true;
        }
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "HealthPotion") {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            health = health + 50;
        }
        if (col.tag == "Coin")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            gold = gold + 1;
        }
        if (col.tag == "Ingot")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            gold = gold + 5;
        }
        if (col.tag == "Shield")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            armour = armour + 25;
        }
    }
    

}
