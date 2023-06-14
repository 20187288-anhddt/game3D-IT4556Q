using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer
{
    public static string Serialize<T>(T obj)
    {
        string text = string.Empty;
        text = JsonUtility.ToJson(obj);
        return text;
    }

    public static T Deserialize<T>(string text)
    {
        T obj = default(T);
        obj = JsonUtility.FromJson<T>(text);
        return obj;
    }


    public static T Override<T>(string text, T obj) where T: ScriptableObject {
        JsonUtility.FromJsonOverwrite(text, obj);
        return obj;
    }

}

