using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatter : MonoBehaviour
{
    public GameObject broken;
    public GameObject gold;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Instantiate(broken, transform.position, transform.rotation);
            Instantiate(gold, transform.position + Vector3.up * 0.8f, gold.transform.rotation);
            Destroy(gameObject);
        }
    }
}
