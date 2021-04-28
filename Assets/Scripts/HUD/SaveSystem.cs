using System.IO;
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

        if (slot > 0) { 
            SaveCollectibles(slot);
            SaveEnemies(slot);
        }
    }

    public static PlayerData LoadPlayer(int slot)
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

            if (slot >0)
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
        GameObject[] collectB = GameObject.FindGameObjectsWithTag("Crate");

        int size = collectP.Length + collectI.Length + collectC.Length + collectS.Length + collectB.Length;

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
            try
            {
                collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
            }catch (System.Exception e)
            {
                Debug.LogError("Ingot Missing");
            }
            i++;
        }
        foreach (GameObject collect in collectC)
        {
            try
            {
                collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Coin Missing");
            }
                i++;
        }
        foreach (GameObject collect in collectS)
        {
            try
            {
                collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
            }
            catch (System.Exception e)
            {
                Debug.LogError("ShieldMissing");
            }
                i++;
        }

        foreach (GameObject collect in collectB)
        {
            try
            {
                collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
            }
            catch (System.Exception e)
            {
                Debug.LogError("Crate Missing");

            }
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
            GameObject[] collectB = GameObject.FindGameObjectsWithTag("Crate");

            List<GameObject> allCollect = new List<GameObject>();
            allCollect.AddRange(collectP);
            allCollect.AddRange(collectI);
            allCollect.AddRange(collectC);
            allCollect.AddRange(collectS);
            allCollect.AddRange(collectB);

          

            //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID"
            foreach (GameObject coll in allCollect)
            {
                string id = "0";
                try
                {
                     id = coll.GetComponent<UniqueID>().uniqueId;
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Missing Object");
                }

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
          

         

            //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID"
            foreach (GameObject enem in enemiesList)
            {
                bool found = false;
                string id = enem.GetComponent<UniqueID>().uniqueId;
                foreach (EnemyData loadEnem in loadData)
                {
                    string LoadID = loadEnem.id;
                    if (loadEnem.health>0 && LoadID == id)
                    {
                        found = true;
                        enem.SetActive(true);
                      

                        enem.GetComponent<enemyVariables>().maxHP = loadEnem.maxHP;
                        enem.GetComponent<enemyVariables>().healthBar.SetMaxHealth(loadEnem.maxHP);

                        enem.GetComponent<enemyVariables>().health = loadEnem.health;
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