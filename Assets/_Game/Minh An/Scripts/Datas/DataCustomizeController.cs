using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataCustomizeController : MonoBehaviour
{
    [SerializeField] private List<InfoSkinPlayer> infoSkinPlayers_Head;
    private DataCustomize_Head dataCustomize_Head = new DataCustomize_Head();
    private bool isInItData = false;
    private void Awake()
    {
        InItData();
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.ClearData.ToString(), () =>
        {
            dataCustomize_Head.ClearData();
        });

    }
    private void InItData()
    {
        dataCustomize_Head.InItData();
        isInItData = true;
    }
    public DataCustomize_Head GetDataCustomize_Head()
    {
        if (!isInItData)
        {
            InItData();
        }
        return dataCustomize_Head;
    }
    public List<InfoSkinPlayer> GetInfoSkinPlayers()
    {
        return infoSkinPlayers_Head;
    }
}
[System.Serializable]
public class DataCustomize_Head : DataBase
{
    public Data_Head data_Head = new Data_Head();
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
    public void ClearData()
    {
        ResetData();
        SaveData();
        LoadData();
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
        EnventManager.TriggerEvent(EventName.NewID_Customize.ToString());
    }
    public int GetID()
    {
        return GetData_Head().GetID();
    }
    public List<int> GetID_Onboughts()
    {
        return GetData_Head().GetID_Onboughts();
    }
    public void AddID_Onboughts(int ID)
    {
        ID--;
        GetData_Head().AddID_Onboughts(ID);
        SaveData();
        LoadData();
    }
    public void SetValueWatchVideo(int ID, int value)
    {
        GetData_Head().GetInfoID_WtachVideo(ID).SetValueWatchVideo(value);
        SaveData();
        LoadData();
    }
    public int GetValueWatchVideo(int ID)
    {
        LoadData();
        return GetData_Head().GetInfoID_WtachVideo(ID).GetValueWatchVideo();
    }
    public void AddValueWatchVideo(int ID, int value)
    {
        GetData_Head().GetInfoID_WtachVideo(ID).AddValueWatchVideo(value);
        SaveData();
        LoadData();
    }
    public InfoSkinPlayer GetInfoSkinPlayer_Current()
    {
        LoadData();
        return GetData_Head().GetInfoSkinPlayer_Current();
    }
}
[System.Serializable]
public class Data_Head
{
    public int ID;
    public List<int> ID_OnBought;
    public List<InfoID_WtachVideo> infoID_WtachVideos = new List<InfoID_WtachVideo>();
    public void SetID(int value)
    {
        ID = value;
    }
    public int GetID()
    {
        return ID;
    }
    public void AddID_Onboughts(int ID)
    {
        if (!ID_OnBought.Contains(ID))
        {
            ID_OnBought.Add(ID);
        }
    }
    public List<int> GetID_Onboughts()
    {
        return ID_OnBought;
    }
    public void ResetData()
    {
      //  Debug.Log("Reset");
        ID = 1;
        ID_OnBought = new List<int>();
        foreach (InfoID_WtachVideo infoID_WtachVideo in infoID_WtachVideos)
        {
            infoID_WtachVideo.ResetData();
        }
    }
    public InfoID_WtachVideo GetInfoID_WtachVideo(int ID)
    {
        foreach (InfoID_WtachVideo infoID_WtachVideo in infoID_WtachVideos)
        {
            if (infoID_WtachVideo.ID == ID)
            {
                return infoID_WtachVideo;
            }
        }
        InfoID_WtachVideo infoID_WtachVideo_ = new InfoID_WtachVideo();
        infoID_WtachVideo_.ID = ID;
        infoID_WtachVideo_.ResetData();
        infoID_WtachVideos.Add(infoID_WtachVideo_);
        return infoID_WtachVideo_;
    }
    public InfoSkinPlayer GetInfoSkinPlayer_Current()
    {
        InfoSkinPlayer infoSkinPlayer = (InfoSkinPlayer)Resources.Load("Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Skin\\Head\\Skin " + (ID), typeof(InfoSkinPlayer));
        return infoSkinPlayer;
    }
}
[System.Serializable]
public class InfoID_WtachVideo
{
    public int ID;
    public int valueWatchVideo;

    public void SetValueWatchVideo(int value)
    {
        valueWatchVideo = value;
    }
    public int GetValueWatchVideo()
    {
        return valueWatchVideo;
    }
    public void AddValueWatchVideo(int value)
    {
        valueWatchVideo += value;
    }
    public void ResetData()
    {
        valueWatchVideo = 0;
    }
}
