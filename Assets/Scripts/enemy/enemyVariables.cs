using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The information about the enemy
public class enemyVariables : MonoBehaviour
{
    public int health;
    public int maxHP;
    public bool attacking = false;
    public bool dead = false;
    public int fistdamage = 1;
    public HUDHealthBar healthBar;
    // Start is called before the first frame update
    
   // Destroy dead enemies
    void death() {
        Destroy(gameObject);
    }


    public void TakeDamage(int damage) {
        // Takes damage and updates his healthbar
        health -= damage;
        if (health < 0) health = 0;
        healthBar.SetHealth(health);
        
        // Triggers death animation and destroys object after 10 seconds
        if (health == 0) {
            dead = true;
            Invoke("death", 10);
        }
    }

}
