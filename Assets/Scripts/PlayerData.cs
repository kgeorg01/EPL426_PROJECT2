using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{

    // Variables for stats
    public int maxHealth;
    public int maxPotions;
    public int maxArmour;
    public int health;
    public int armour;
    public int gold;
    public int potions;
    public float attackDamage;

    public float[] position;
    public string scenceIdx;

    public PlayerData ( playerVariables player) {
        // Variables for stats
        maxHealth = player.maxHealth;
        maxPotions = player.maxPotions;
        maxArmour = player.maxArmour;
        health = player.health;
        armour = player.armour;
        gold = player.gold;
        potions =  player.potions;
        attackDamage =  player.attackDamage;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        scenceIdx = SceneManager.GetActiveScene().name;

    }

}
