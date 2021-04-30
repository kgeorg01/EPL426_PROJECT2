using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for the breakable crates. when they come into contact with the player of with an attack of him they break and reveal their content
public class shatter : MonoBehaviour
{
    public GameObject broken;
    public GameObject gold;
    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag.Equals("Player") &&  playerVariables.attacking) {
            //Spawn the broken crate
            if (broken!= null) Instantiate(broken, transform.position, transform.rotation);
            //Spawn the content of the crate
            if (gold!= null) Instantiate(gold, transform.position + Vector3.up * 0.8f, gold.transform.rotation);
            //Destroy the unbreakable crate
            Destroy(gameObject);
        }
    }
}
