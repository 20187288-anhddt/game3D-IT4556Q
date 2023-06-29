using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataCustomizeController : MonoBehaviour
{
    [SerializeField] private List<InfoSkinPlayer> infoSkinPlayers_Head;
    private DataCustomize_Head dataCustomize_Head = new DataCustomize_Head();

    private void Awake()
    {
        InItData();
    }
    private void InItData()
    {
        dataCustomize_Head.InItData();
    }
}
[System.Serializable]
public class DataCustomize_Head : DataBase
{
    public Data_Head data_Head;
    public void InItData()
    {
        SetFileName(nameof(DataCustomize_Head));
    }
    public override void SaveData()
    {
        base.SaveData();
        string json = JsonUtility.ToJson(data_Head);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
    }
    public override void LoadData()
    {
        base.LoadData();
        string json = File.ReadAllText(Application.persistentDataPath + "/" + GetFileName());
        Data_Head dataSave = JsonUtility.FromJson<Data_Head>(json);
        data_Head = dataSave;
    }
    public override void ResetData()
    {
        data_Head.ResetData();
    }
    public Data_Head GetData_Head()
    {
        LoadData();
        return data_Head;
    }
    public void SetID(int value)
    {
        GetData_Head().SetID(value);
        SaveData();
        LoadData();
    }
    public int GetID()
    {
        return GetData_Head().GetID();
    }
    public List<int> GetID_Onboughts()
    {
        return GetData_Head().GetID_Onboughts();
    }
}
[System.Serializable] 
public class Data_Head
{
    public int ID;
    public List<int> ID_OnBought;
    public void SetID(int value)
    {
        ID = value;
    }
    public int GetID()
    {
        return ID;
    }
    public List<int> GetID_Onboughts()
    {
        return ID_OnBought;
    }
    public void ResetData()
    {
        ID = 0;
        ID_OnBought = new List<int>();
    }
}