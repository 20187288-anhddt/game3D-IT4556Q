using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataBase : MonoBehaviour
{
    private string fileName = " ";
    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }
    public string GetFileName()
    {
       return fileName;
    }
    public virtual void SaveData()
    {
       
    }
    public virtual void LoadData()
    {
      //  Debug.Log("Load Data Base");
        if(!File.Exists(Application.persistentDataPath + "/" + GetFileName()))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/" + GetFileName(), FileMode.Create);
            ResetData();
            file.Dispose();
            SaveData();
        }
    }
    public virtual void ResetData()
    {

    }
}
