using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enterShop : MonoBehaviour
{
    // Used to interact with the player
    private bool active = false;
    public GameObject shop;

    // When player comes close to shop, enables the canvas of the shop
    private void OnTriggerEnter(Collider other)
    {
        if (!active && other.tag.Equals("Player"))
        {
            active = true;
            shop.SetActive(true);
            Time.timeScale = 0;    // freezes game world
        }
    }

    public void Continue()
    {
        Time.timeScale = 1; // resume game world
        Invoke("setFalse", 5f); // we use invoke because when he exits the shop, we dont want the shop to reopen instantly until he leaves and reenters
    }

    void setFalse()
    {
        active = false;
    }
}
