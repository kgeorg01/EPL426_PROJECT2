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


    private void Start()
    {
        grounded = false;
        blocking = false;
        attacking = false;
        mH = 0;
        mV = 0;
        maxHealth = 100;
        maxArmour = 50;
        maxPotions = 9;
        health = 60;
        armour = 50;
        gold = 0;
        potions = 0;
        speed = 5f;
        jump = 5f;
        clickslight = 0;
        clicksheavy = 0;
    }
}
