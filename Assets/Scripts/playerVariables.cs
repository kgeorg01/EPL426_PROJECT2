using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVariables : MonoBehaviour
{
    // Variables about the state of our player
    public static bool grounded;
    public static bool blocking;
    public static bool attacking;

    // Variables about movement
    public static float mH;
    public static float mV;
    public static float speed;
    public static float jump;

    // Variables for stats
    public static int maxHealth;
    public static int maxPotions;
    public static int maxArmour;
    public static int health;
    public static int armour;
    public static int gold;
    public static int potions;
    
    // Variables for attack
    public static int clickslight;
    public static int clicksheavy;

    public  HUDHealthBar healthBar;
    public  HUDShieldBar shieldbar;
    public  HUDGold goldInd;
    public  HUDPotion potionInd;

    private void Start()
    {
        grounded = false;
        blocking = false;
        attacking = false;
        mH = 0;
        mV = 0;
        speed = 5f;
        jump = 5f;
        clickslight = 0;
        clicksheavy = 0;

        //Starting values
        maxHealth = 100;
        maxArmour = 50;
        maxPotions = 9;
        gold = 0;
        potions = 0;

        health = maxHealth;
        armour = maxArmour;
        

        healthBar.SetMaxHealth(maxHealth);
        shieldbar.SetMaxShield(maxArmour);
        goldInd.SetGold(gold);
        potionInd.SetPotion(potions);

    }

    public  void TakeDamage(int damage)
    {
        if (armour > damage)
        {
            armour -= damage;
            shieldbar.SetShield(armour);

        } else if (armour >0)
        {
            //damage>armor
            damage -= armour;
            armour = 0; //damage >=armor so armor becomes 0
            shieldbar.SetShield(armour);

            //rest of the damage affects health
            health -= damage;
            if (health < 0) health = 0;
            healthBar.SetHealth(health);

        } else
        {
            //no shield left
            health -= damage;
            if (health < 0) health = 0;
            healthBar.SetHealth(health);
        }
    }

    public void AddGold(int gold2)
    {

        gold += gold2;
        goldInd.SetGold(gold);
    }

    public void SubtractGold(int gold2)
    {
        gold -= gold2;
        
        goldInd.SetGold(gold);
    }

    public  void AddHealth (int hp)
    {
        health += hp;
        if (health > maxHealth) health = maxHealth;
        healthBar.SetHealth(health);
    }

    public void AddArmour(int shield)
    {
        armour += shield;
        if (armour> maxArmour) armour= maxArmour;
        shieldbar.SetShield(armour);
    }

    public  void AddPotion(int pot)
    {
        potions += pot;
        if (potions > maxPotions) potions = maxPotions;
        potionInd.SetPotion(potions);
    }

    public  void RemovePotion(int pot)
    {
        potions -= pot;
        if (potions >= 0) potionInd.SetPotion(potions);
    }

    public void DrinkPotion()
    {
        if (health < maxHealth && potions>0)
        {
            RemovePotion(1);
            AddHealth(25);
        }

    }
}
