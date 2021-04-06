using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


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

            return pd;

        } else
        {
            Debug.LogError("File not found in" + path);
            return null;
        }

    }

}
