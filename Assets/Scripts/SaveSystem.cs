﻿using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public static class SaveSystem
{

    public static void SavePlayer(playerVariables player, int slot)
    {

        string saveName = "player" + slot + ".save";

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, saveName);
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData pd = new PlayerData(player);

        formatter.Serialize(stream, pd);
        stream.Close();

        SaveCollectibles(slot);
        SaveEnemies(slot);

    }

    public static PlayerData LoadPlayer(int slot, bool allData = false)
    {

        string saveName = "player" + slot + ".save";
        string path = Path.Combine(Application.persistentDataPath, saveName);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData pd = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            //  Time.timeScale = 1f;
            //SceneManager.LoadScene(pd.scenceIdx , LoadSceneMode.Single);

            if (allData)
            {
                LoadCollectibles(slot);
                LoadEnemies(slot);
            }

            return pd;

        }
        else
        {
            Debug.LogError("File not found in" + path);
            return null;
        }

    }

    public static void SaveCollectibles(int slot)
    {

        GameObject[] collectP = GameObject.FindGameObjectsWithTag("HealthPotion");
        GameObject[] collectI = GameObject.FindGameObjectsWithTag("Ingot");
        GameObject[] collectC = GameObject.FindGameObjectsWithTag("Coin");
        GameObject[] collectS = GameObject.FindGameObjectsWithTag("Shield");

        int size = collectP.Length + collectI.Length + collectC.Length + collectS.Length;

        string[] collectID = new string[size];

        Debug.Log("SaveList");

        //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID"

        int i = 0;
        foreach (GameObject collect in collectP)
        {
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;

            i++;
        }
        foreach (GameObject collect in collectI)
        {
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;

            i++;
        }
        foreach (GameObject collect in collectC)
        {
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;

            i++;
        }
        foreach (GameObject collect in collectS)
        {
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;

            i++;
        }




        string saveName = "collectibles" + slot + ".save";
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, saveName);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, collectID);
        stream.Close();

    }




    public static void LoadCollectibles(int slot)
    {


        string saveName = "collectibles" + slot + ".save";
        string path = Path.Combine(Application.persistentDataPath, saveName);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            string[] collectID = formatter.Deserialize(stream) as string[];
            List<string> loadCollect = new List<string>(collectID);

            GameObject[] collectP = GameObject.FindGameObjectsWithTag("HealthPotion");
            GameObject[] collectI = GameObject.FindGameObjectsWithTag("Ingot");
            GameObject[] collectC = GameObject.FindGameObjectsWithTag("Coin");
            GameObject[] collectS = GameObject.FindGameObjectsWithTag("Shield");

            List<GameObject> allCollect = new List<GameObject>();
            allCollect.AddRange(collectP);
            allCollect.AddRange(collectI);
            allCollect.AddRange(collectC);
            allCollect.AddRange(collectS);

            Debug.Log("LoadList");

            //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID"
            foreach (GameObject coll in allCollect)
            {
                string id = coll.GetComponent<UniqueID>().uniqueId;

                if (loadCollect.Contains(id))
                {
                    coll.SetActive(true);
                }
                else
                {
                    coll.SetActive(false);
                }
            }

            stream.Close();


        }
        else
        {
            Debug.LogError("File not found in" + path);

        }


    }

    public static void SaveEnemies(int slot)
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        EnemyData[] enemyData = new EnemyData[enemies.Length];


        Debug.Log("SaveListE");

        //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID"

        int i = 0;
        foreach (GameObject enemy in enemies)
        {
            string id = enemy.GetComponent<UniqueID>().uniqueId;
            enemyVariables enemyVar = enemy.GetComponent<enemyVariables>();
            enemyData[i] = new EnemyData(enemyVar, id);
            i++;
        }


        string saveName = "enemies" + slot + ".save";
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, saveName);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, enemyData);
        stream.Close();

    }




    public static void LoadEnemies(int slot)
    {


        string saveName = "enemies" + slot + ".save";
        string path = Path.Combine(Application.persistentDataPath, saveName);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //Enemies in load file
            EnemyData[] enemyDat = formatter.Deserialize(stream) as  EnemyData[];
            List<EnemyData> loadData = new List<EnemyData>(enemyDat);


            //All the enemies in the scence
            GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Enemy");
            List<GameObject> enemiesList = new List<GameObject>(enemiesArray);
          

            Debug.Log("LoadListE");

            //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID"
            foreach (GameObject enem in enemiesList)
            {
                bool found = false;
                string id = enem.GetComponent<UniqueID>().uniqueId;
                foreach (EnemyData loadEnem in loadData)
                {
                    string LoadID = loadEnem.id;
                    if (LoadID == id)
                    {
                        found = true;
                        enem.SetActive(true);
                        enem.GetComponent<enemyVariables>().health = loadEnem.health;
                        Debug.Log(enem.GetComponent<enemyVariables>().health);
                        enem.GetComponent<enemyVariables>().healthBar.SetHealth(loadEnem.health);

                        Vector3 position;
                        position.x = loadEnem.position[0];
                        position.y = loadEnem.position[1];
                        position.z = loadEnem.position[2];

                        enem.transform.position = position;

                    } 


                }
                if (!found) enem.SetActive(false);
                
            }

            stream.Close();


        }
        else
        {
            Debug.LogError("File not found in" + path);

        }


    }

}