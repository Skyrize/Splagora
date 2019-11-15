using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
   
    public static void SaveSettings (SettingsUI settingslight)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.customlights"; //créer un fichier sauvegarde sur le système
        FileStream stream = new FileStream(path, FileMode.Create);

        SettingsData data = new SettingsData(settingslight);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SettingsData LoadData()
    {
        string path = Application.persistentDataPath + "/settings.customlights";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;

            Debug.Log(data.ToString());

            stream.Close();

            return data;
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            SettingsData data = new SettingsData();

            formatter.Serialize(stream, data);
            stream.Close();
            return data;
        }
    }
}
