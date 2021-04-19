using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    
    private Rigidbody rb;
    private Vector3 movement;
    public AudioSource walk;
    public AudioSource jumpaudio;
    public AudioSource armouraudio;
    private float lastclick = 0;
    private int counter = 0;
    private bool spikeEntered = false;
    private bool enemyEntered = false;
    private long soundDelay = 0;
    public playerVariables playervar;

    private float lavaPoolDamage = 0;
    public AudioSource lavaSound;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    private void checkBlock() {
        if (Input.GetMouseButton(1)){
            playerVariables.blocking = true;
        }
        else{
            playerVariables.blocking = false;
        }
    }

    private void PlayerMovemnt() {
        playerVariables.mH = Input.GetAxis("Horizontal");
        playerVariables.mV = Input.GetAxis("Vertical");
        movement.Set(playerVariables.mH, 0f, playerVariables.mV);
        movement = movement.normalized * playerVariables.speed * Time.deltaTime;
        movement = transform.position + (transform.forward * movement.z) + (transform.right * movement.x);

        if (playerVariables.mV > 0 && !playerVariables.blocking && !playerVariables.attacking) {
            rb.MovePosition(movement);
            if (!walk.isPlaying) walk.Play();
        }
        else if (playerVariables.mV < 0 && !playerVariables.blocking && !playerVariables.attacking) {
            rb.MovePosition(movement);
            if (!walk.isPlaying) walk.Play();
        }
        else {
            if (walk.isPlaying) walk.Stop();
        }

        if (playerVariables.mH != 0 || playerVariables.mV != 0) {
            if (!armouraudio.isPlaying && !playerVariables.blocking && !playerVariables.attacking) armouraudio.Play();
        }
        else armouraudio.Pause();
    }

    private void RotatePlayer() {
        if (playerVariables.mH > 0) transform.Rotate(Vector3.up, 180 * Time.deltaTime);
        else if (playerVariables.mH < 0) transform.Rotate(Vector3.up, -180 * Time.deltaTime);
    }

    private void Jump() {
        if (Input.GetKeyDown("space") && !playerVariables.blocking)
        {
            if (playerVariables.grounded)
            {
                gameObject.GetComponent<Animator>().SetTrigger("jump");
                jumpaudio.Play();
                rb.AddForce(Vector3.up * playerVariables.jump, ForceMode.Impulse);
                playerVariables.grounded = false;
            }
        }
        if (jumpaudio.isPlaying) walk.Stop();
    }

    private void Attack()
    {
       
       if (Time.time - lastclick > 2f) {
            playerVariables.clickslight = 0;
            playerVariables.clicksheavy = 0;

        }
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            lastclick = Time.time;
            playerVariables.clickslight = 0;
            playerVariables.clicksheavy++;
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("heavy1");
            }
            else if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Heavy1"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("heavy2");
            }
            else if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Heavy2"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("heavy3");
                playerVariables.clicksheavy = 0;
            }
            else if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Heavy3"))
            {
                gameObject.GetComponent<Animator>().ResetTrigger("heavy1");
                gameObject.GetComponent<Animator>().ResetTrigger("heavy2");
                gameObject.GetComponent<Animator>().ResetTrigger("heavy3");
            }
        }
        else if (Input.GetMouseButtonDown(0)) {
            lastclick = Time.time;
            playerVariables.clicksheavy = 0;
            playerVariables.clickslight++;
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("light1");
            }
            else if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Light1"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("light2");
            }
            else if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Light2"))
            {
                gameObject.GetComponent<Animator>().SetTrigger("light3");
                playerVariables.clickslight = 0;
            }
            else if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Light3"))
            {
                gameObject.GetComponent<Animator>().ResetTrigger("light1");
                gameObject.GetComponent<Animator>().ResetTrigger("light2");
                gameObject.GetComponent<Animator>().ResetTrigger("light3");
            }
        }
    }

    private void consumeHealthPotion()
    {
         playervar.DrinkPotion();
        
    }


    void FixedUpdate() {
        if (!playerVariables.dead)
        {
            checkBlock();
            PlayerMovemnt();
            RotatePlayer();
            Jump();
            Attack();
            
            if (transform.position.y < -8)
            {
                playerVariables.dead = true;
                playerVariables.falling = true;
            }

            if (lavaPoolDamage >= 1.5)
            {
                lavaPoolDamage = 0;
            }
            lavaPoolDamage += Time.fixedDeltaTime;

        }


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) consumeHealthPotion();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Ground")) {
            playerVariables.grounded = true;
        }
        if (collision.gameObject.tag.Equals("Arrow"))
        {
            playervar.TakeDamage(10);
        }
        if (collision.gameObject.tag.Equals("Arrow15"))
        {
            playervar.TakeDamage(15);
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "HealthPotion" && playervar.maxPotions>playervar.potions) {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            playervar.AddPotion(1);
        }
        if (col.tag == "Coin")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            playervar.AddGold(1);
        }
        if (col.tag == "Ingot")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            playervar.AddGold(5);
        }
        if (col.tag == "Shield")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            playervar.AddArmour(25);
        }
        if (col.tag == "Spike")
        {
            if (!spikeEntered)
            {
                spikeEntered = true;
                playervar.TakeDamage(50);

            }
        }
        if (col.tag == "EnemyAttack" && !col.gameObject.GetComponentInParent<enemyVariables>().dead)
        {
            if (!spikeEntered)
            {
                enemyEntered = true;
                int dmg = col.gameObject.GetComponentInParent<enemyVariables>().fistdamage;
                playervar.TakeDamage(dmg);

            }
        }
        if (col.tag == "Platform")
        {
            transform.parent = col.transform;
        }
        if (col.tag == "Lava")
        {
            playervar.TakeDamage(999);
        }
        if (col.tag == "fireStatue")
        {
            col.GetComponent<firestream>().enabled = true;
            col.transform.GetChild(1).gameObject.SetActive(true);
            
        }

    }

    private void OnTriggerStay(Collider col) {
        if (col.tag == "Poison") {
            counter++;
            if (counter == 15) {
                counter = 0;
                playervar.TakeDamage(1);
            }
        }
        if (col.tag == "Fire")
        {
            counter++;
            if (counter == 15)
            {
                counter = 0;
                playervar.TakeDamage(2);
            }
        }

        if (col.tag == "LavaPool")
        {
            playerVariables.speed = 3f;
            if (lavaPoolDamage >= 1.5)
            {
                playervar.TakeDamage(2);
                AudioSource.PlayClipAtPoint(lavaSound.clip, this.gameObject.transform.position);
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Spike"))
        {
            enemyEntered = false;
        }
        if (other.tag.Equals("EnemyAttack"))
        {
            enemyEntered = false;
        }
        if (other.tag == "Platform")
        {
            transform.parent = null;

        }

        if (other.tag == "fireStatue")
        {
            other.GetComponent<firestream>().enabled = false;
            other.transform.GetChild(1).gameObject.SetActive(false);
            
        }

        if (other.tag == "LavaPool")
        {
            
             playerVariables.speed = 5f;
            
        }


    }



}


