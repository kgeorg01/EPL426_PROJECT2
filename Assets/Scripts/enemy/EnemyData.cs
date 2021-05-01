using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The object to save enemy's data
 * 
 */
[System.Serializable]
public class EnemyData
{
    public string id;
    public int health;
    public int maxHP;
    public float[] position;

    public EnemyData (enemyVariables enemy , string id) 
    {
        this.id = id;
        health = enemy.health;
        maxHP = enemy.maxHP;
        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
    }
}
