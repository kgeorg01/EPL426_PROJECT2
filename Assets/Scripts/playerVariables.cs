using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVariables : MonoBehaviour
{
    public static bool grounded;
    public static bool blocking;
    public static bool attacking;
    public static float mH;
    public static float mV;
    public static int health;
    public static int armour;
    public static int gold;
    public static float speed;
    public static float jump;
    public static int clickslight;
    public static int clicksheavy;


    private void Start()
    {
        grounded = false;
        blocking = false;
        attacking = false;
        mH = 0;
        mV = 0;
        health = 150;
        armour = 50;
        gold = 0;
        speed = 5f;
        jump = 5f;
        clickslight = 0;
        clicksheavy = 0;
    }
}
