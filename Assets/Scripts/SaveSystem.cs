using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public static class SaveSystem 
{
  
    public static void SavePlayer ( playerVariables player , int slot)
    {

        string saveName = "player"+slot+".save";

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, saveName);
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData pd = new PlayerData(player);

        formatter.Serialize(stream, pd);
        stream.Close();

       // SaveCollectibles(slot);

    }

    public static void SaveCollectibles(int slot)
    {

        GameObject[] collectP = GameObject.FindGameObjectsWithTag("HealthPotion");
        GameObject[] collectI = GameObject.FindGameObjectsWithTag("Ingot");
        GameObject[] collectC = GameObject.FindGameObjectsWithTag("Coin"); 
        GameObject[] collectS = GameObject.FindGameObjectsWithTag("Shield");

        int size = collectP.Length + collectI.Length + collectC.Length + collectS.Length;

        int[] collectID = new int[size];

        Debug.Log("SaveList");
       

        int i = 0;
        foreach (GameObject collect in collectP){
            collectID[i] = collect.GetInstanceID();
            Debug.Log(collectID[i]);
            i++;
        }
        foreach (GameObject collect in collectI)
        {
            collectID[i] = collect.GetInstanceID();
            Debug.Log(collectID[i]);
            i++;
        }
        foreach (GameObject collect in collectC)
        {
            collectID[i] = collect.GetInstanceID();
            Debug.Log(collectID[i]);
            i++;
        }
        foreach (GameObject collect in collectS)
        {
            collectID[i] = collect.GetInstanceID();
            Debug.Log(collectID[i]);
            i++;
        }

       


        string saveName = "collectibles" + slot + ".save";
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, saveName);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, collectID);
        stream.Close();

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

           // Time.timeScale = 1f;
           // SceneManager.LoadScene(pd.scenceIdx , LoadSceneMode.Single);
           // LoadCollectibles(slot);


            return pd;

        } else
        {
            Debug.LogError("File not found in" + path);
            return null;
        }

    }


    public static void LoadCollectibles (int slot)
    {


        string saveName = "collectibles" + slot + ".save";
        string path = Path.Combine(Application.persistentDataPath, saveName);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);


            int []  collectID = formatter.Deserialize(stream) as int[];
            List<int> loadCollect= new List<int>(collectID);
           
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
            

            foreach ( GameObject coll in allCollect)
            {
                int id = coll.GetInstanceID();
                Debug.Log(id);
                if (loadCollect.Contains(id))
                {
                    coll.SetActive(true);
                } else
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


}
