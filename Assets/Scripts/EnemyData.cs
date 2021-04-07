﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public string id;
    public int health;
    public float[] position;

    public EnemyData (enemyVariables enemy , string id) 
    {
        this.id = id;
        health = enemy.health;
        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
    }
}