using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyVariables : MonoBehaviour
{
    public int health = 100;
    public static bool attacking = false;
    public HUDHealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        //rest of the damage affects health
        health -= damage;
        if (health < 0) health = 0;
        healthBar.SetHealth(health);
        
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
