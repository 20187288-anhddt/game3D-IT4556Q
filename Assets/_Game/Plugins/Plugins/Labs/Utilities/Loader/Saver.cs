
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class Saver
{
    public static void Save(string path, string defaulName, string content, string extension) {

        //Debug.Log("Saved at " + path);
        var fullPath = Path.Combine(path, defaulName + extension);
        if (fullPath.Length != 0)
        {
            Debug.Log(string.Format("<color=blue> Saved : file name = {0} at path = {1} </color>", defaulName, path));
            File.WriteAllText(fullPath, content, System.Text.Encoding.ASCII);
        }
    }

    public static void Save(string path, string content) {
        if (path.Length != 0)
        {
            Debug.Log(string.Format("<color=blue> Saved at path = {0} </color>", path));
            File.WriteAllText(path, content, System.Text.Encoding.ASCII);
        }
    }


    public static void Save<T>(string path, T t)
    {

        Debug.Log("Saved 2 at " + path);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Create);
        formatter.Serialize(fileStream, t);
        fileStream.Close();

    }
}
