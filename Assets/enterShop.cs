using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterShop : MonoBehaviour
{
    private bool active = false;
    public GameObject shop;
    private void OnTriggerEnter(Collider other)
    {
        if (!false && other.tag.Equals("Player"))
        {
            active = true;
            shop.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        Time.timeScale = 1;
        Invoke("setFalse", 5f);
    }

    void setFalse()
    {
        active = false;
    }
}
