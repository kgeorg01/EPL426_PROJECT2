﻿using System.Collections;
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

    public playerVariables playervar;

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
        if (playerVariables.mH > 0) transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        else if (playerVariables.mH < 0) transform.Rotate(Vector3.up, -90 * Time.deltaTime);
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            playervar.DrinkPotion();
        }
    }


    void FixedUpdate() {
        checkBlock();
        PlayerMovemnt();
        RotatePlayer();
        Jump();
        Attack();
        consumeHealthPotion();

        //FOR TESTING HEALTH
        if (Input.GetKeyDown(KeyCode.J))
        {
            playervar.TakeDamage(20);
            
        }



    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Ground")) {
            playerVariables.grounded = true;
        }
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "HealthPotion") {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            // playerVariables.potions++; 
            //if (playerVariables.potions > playerVariables.maxPotions) playerVariables.potions = playerVariables.maxPotions;
            playervar.AddPotion(1);
        }
        if (col.tag == "Coin")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            //playerVariables.gold = playerVariables.gold + 1;
            playervar.AddGold(1);
        }
        if (col.tag == "Ingot")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            //playerVariables.gold = playerVariables.gold + 5;
            playervar.AddGold(5);
        }
        if (col.tag == "Shield")
        {
            AudioSource audio = col.gameObject.GetComponent<AudioSource>();
            AudioSource.PlayClipAtPoint(audio.clip, this.gameObject.transform.position);
            Destroy(col.gameObject);
            playervar.AddArmour(25);
           // playerVariables.armour = playerVariables.armour + 25;
           //if (playerVariables.armour > playerVariables.maxArmour) playerVariables.armour = playerVariables.maxArmour;
        }
    }
    

}
