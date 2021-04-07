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

       SaveCollectibles(slot);

    }

    public static void SaveCollectibles(int slot)
    {

        GameObject[] collectP = GameObject.FindGameObjectsWithTag("HealthPotion");
        GameObject[] collectI = GameObject.FindGameObjectsWithTag("Ingot");
        GameObject[] collectC = GameObject.FindGameObjectsWithTag("Coin"); 
        GameObject[] collectS = GameObject.FindGameObjectsWithTag("Shield");

        int size = collectP.Length + collectI.Length + collectC.Length + collectS.Length;

        string [] collectID = new string[size];

        Debug.Log("SaveList");

        //THE GAME OBJECTS MUST HAVE A COMPONENT WITH THE SCRIPT "UniqueID"

        int i = 0;
        foreach (GameObject collect in collectP){
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
            Debug.Log(collectID[i]);
            i++;
        }
        foreach (GameObject collect in collectI)
        {
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
            Debug.Log(collectID[i]);
            i++;
        }
        foreach (GameObject collect in collectC)
        {
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
            Debug.Log(collectID[i]);
            i++;
        }
        foreach (GameObject collect in collectS)
        {
            collectID[i] = collect.GetComponent<UniqueID>().uniqueId;
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

    public static PlayerData LoadPlayer(int slot , bool allData = false)
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

           if (allData) LoadCollectibles(slot);


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


            string []  collectID = formatter.Deserialize(stream) as string[];
            List<string> loadCollect= new List<string>(collectID);
           
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
            foreach ( GameObject coll in allCollect)
            {
                string id = coll.GetComponent<UniqueID>().uniqueId;
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
