using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class FileManager : MonoBehaviour
{
    public static void SaveText(string filePath, string content)
    {
        File.WriteAllText(filePath, content);

    }
    public static string LoadText(string filePath)
    {
        return File.ReadAllText(filePath);
    }
    public static void SaveBinary<T>(string filePath, T data)
    {
        using (FileStream fs = File.Create(filePath))
        {
            BinaryFormatter format = new BinaryFormatter();
            format.Serialize(fs, data);
            fs.Close();
        }
    }

    public static T LoadBinary<T>(string filePath)
    {
        T data = default;
        if (File.Exists(filePath)) 
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                BinaryFormatter format = new BinaryFormatter();
                data = (T)format.Deserialize(fs);
                fs.Close();
            }
        }
        return data;
    }
}
