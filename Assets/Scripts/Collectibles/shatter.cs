using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatter : MonoBehaviour
{
    public GameObject broken;
    public GameObject gold;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player") &&  playerVariables.attacking)
        {
            if (broken!= null) Instantiate(broken, transform.position, transform.rotation);
            if (gold!= null) Instantiate(gold, transform.position + Vector3.up * 0.8f, gold.transform.rotation);
            Destroy(gameObject);
        }
    }
}
