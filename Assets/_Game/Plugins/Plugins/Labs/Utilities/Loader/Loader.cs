
using System.Collections.Generic;
using UnityEngine;

public class Loader
{
    public static T Load<T>(string path, string fileName)
    {
        T result = default(T);
        TextAsset textAsset = Resources.Load<TextAsset>(path + "/" + fileName);
        result = Serializer.Deserialize<T>(textAsset.text);
        return result;
    }

    public static T Load<T>(string path)
    {
        T result = default(T);
        var jsonText = ES3.LoadRawString(path);
        result = Serializer.Deserialize<T>(jsonText);
        return result;
    }
    public static T LoadToScriptableObject<T>(string path, T obj) where T : ScriptableObject
    {
        T result = default(T);
        var jsonText = ES3.LoadRawString(path);
        result = Serializer.Override<T>(jsonText, obj);
        return result;
    }

    public static string LoadTexAsset(string path, string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path + "/" + fileName);
        return textAsset.text;
    }


    public static List<string> GetAllFileNameFrom(string folderName)
    {
        TextAsset[] listPath = Resources.LoadAll<TextAsset>(folderName);
        List<string> listName = new List<string>();
        foreach (var path in listPath)
        {
            listName.Add(path.name);
        }
        return listName;
    }

}

