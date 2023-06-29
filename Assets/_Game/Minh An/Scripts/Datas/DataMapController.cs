using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataMapController : DataBase
{
    public DataMap dataMap;
    public MapCurrent mapCurrent;
    private bool isInItData = false;

    private void Awake()
    {
        CheckInItData();
    }
    public void InItData()
    {
        dataMap.InItData();
        mapCurrent.InItData();
        SelectDataMap(mapCurrent.GetDataMapCurrent().GetLevelCurrent());
    }
    public DataMap GetDataMap()
    {
        CheckInItData();
        return dataMap;
    }
    public MapCurrent GetMapCurrent()
    {
        CheckInItData();
        return mapCurrent;
    }
    public void CheckInItData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public void SelectDataMap(int levelCurrent)
    {
        mapCurrent.SetLevelCurrent(levelCurrent);
        dataMap.SelectDataMap(levelCurrent);
    }
}
[System.Serializable]
public class MapCurrent //luu data
{
    public DataMapCurrent dataMapCurrent;
    private string fileName = " ";
    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }
    public string GetFileName()
    {
        return fileName;
    }

    public void InItData()
    {
        SetFileName(nameof(MapCurrent));
        LoadData();
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(dataMapCurrent);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
    }

    public void LoadData()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + GetFileName()))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/" + GetFileName(), FileMode.Create);
            ResetData();
            file.Dispose();
            SaveData();
        }
        string json = File.ReadAllText(Application.persistentDataPath + "/" + GetFileName());
        DataMapCurrent dataSave = JsonUtility.FromJson<DataMapCurrent>(json);
        dataMapCurrent = dataSave;
    }
    public void SetLevelCurrent(int value)
    {
        dataMapCurrent.SetLevelCurrent(value);
        SaveData();
        LoadData();
    }
    public void SetLevelInMapCurrent(MiniMapController.TypeLevel value)
    {
        dataMapCurrent.SetLevelInMapCurrent(value);
        SaveData();
        LoadData();
    }
    public DataMapCurrent GetDataMapCurrent()
    {
        return dataMapCurrent;
    }
    public void ResetData()
    {
        dataMapCurrent.ResetData();
    }
}
[System.Serializable]
public class DataMapCurrent //dong goi de quan li
{
    public int LevelCurrent;
    public MiniMapController.TypeLevel LevelInMapCurrent;
    public void ResetData()
    {
        LevelCurrent = 1;
        LevelInMapCurrent = MiniMapController.TypeLevel.Level_1;
    }
    public int GetLevelCurrent()
    {
        if (LevelCurrent < 1) { LevelCurrent = 1; }
        return LevelCurrent;
    }
    public MiniMapController.TypeLevel GetLevelInMapCurrent()
    {
        if ((int)LevelInMapCurrent < 1) { LevelInMapCurrent = MiniMapController.TypeLevel.Level_1; }
        return LevelInMapCurrent;
    }
    public void SetLevelCurrent(int value)
    {
        LevelCurrent = value;
    }
    public void SetLevelInMapCurrent(MiniMapController.TypeLevel value)
    {
        LevelInMapCurrent = value;
    }
}
[System.Serializable]
public class DataMap //luu data
{
    public Data_Map data_Map = new Data_Map();
    private string fileName = " ";
    private bool isInItData = false;
    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }
    public string GetFileName()
    {
        return fileName;
    }
   
    public void InItData()
    {
        SetFileName(nameof(Data_Map) + data_Map.GetLevelMap());
        LoadData();
        isInItData = true;
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(data_Map);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
    }

    public void LoadData()
    {

        if (!File.Exists(Application.persistentDataPath + "/" + GetFileName()))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/" + GetFileName(), FileMode.Create);
            ResetData();
            file.Dispose();
            SaveData();
        }
        string json = File.ReadAllText(Application.persistentDataPath + "/" + GetFileName());
        Data_Map dataSave = JsonUtility.FromJson<Data_Map>(json);
        data_Map = dataSave;
    }
    public void ResetData()
    {
        data_Map.ResetData();
    }
    public Data_Map GetData_Map()
    {
        if (!isInItData)
        {
            InItData();
        }
        LoadData();
        return data_Map;
    }
    public void SelectDataMap(int levelCurrent)
    {
        if(data_Map.LevelMap == levelCurrent) { return; }
        data_Map = new Data_Map();
        data_Map.LevelMap = levelCurrent;
        InItData();
    }
    #region Boss
    public void SetLevel_Capacity_Boss(int value)
    {
        GetData_Map().GetDataPlayer().GetDataBoss().SetLevel_Capacity(value);
        SaveData();
        LoadData();
    }
    public void SetLevel_Price_Boss(int value)
    {
        GetData_Map().GetDataPlayer().GetDataBoss().SetLevel_Price(value);
        SaveData();
        LoadData();
    }
    public void SetLevel_Speed_Boss(int value)
    {
        GetData_Map().GetDataPlayer().GetDataBoss().SetLevel_Speed(value);
        SaveData();
        LoadData();
    }
    public void NextLevel_Capacity_Boss()
    {
        GetData_Map().GetDataPlayer().GetDataBoss().NextLevel_Capacity();
        SaveData();
        LoadData();
    }
    public void NextLevel_Price_Boss()
    {
        GetData_Map().GetDataPlayer().GetDataBoss().NextLevel_Price();
        SaveData();
        LoadData();
    }
    public void NextLevel_Speed_Boss()
    {
        GetData_Map().GetDataPlayer().GetDataBoss().NextLevel_Speed();
        SaveData();
        LoadData();
    }
    #endregion
    #region Staff
    public void SetLevel_Capacity_Staff(int value, StaffType staffType)
    {
        GetData_Map().GetDataPlayer().GetDataStaff(staffType).SetLevel_Capacity(value);
        SaveData();
        LoadData();
    }
    public void SetLevel_Hire_Staff(int value, StaffType staffType)
    {
        GetData_Map().GetDataPlayer().GetDataStaff(staffType).SetLevel_Hire(value);
        SaveData();
        LoadData();
    }
    public void SetLevel_Speed_Staff(int value, StaffType staffType)
    {
        GetData_Map().GetDataPlayer().GetDataStaff(staffType).SetLevel_Speed(value);
        SaveData();
        LoadData();
    }
    public void NextLevel_Capacity_Staff( StaffType staffType)
    {
        GetData_Map().GetDataPlayer().GetDataStaff(staffType).NextLevel_Capacity();
        SaveData();
        LoadData();
    }
    public void NextLevel_Hire_Staff(StaffType staffType)
    {
        GetData_Map().GetDataPlayer().GetDataStaff(staffType).NextLevel_Hire();
        SaveData();
        LoadData();
    }
    public void NextLevel_Speed_Staff(StaffType staffType)
    {
        GetData_Map().GetDataPlayer().GetDataStaff(staffType).NextLevel_Speed();
        SaveData();
        LoadData();
    }
    #endregion
    //#region Checkout
    //public void SetData_ActiveStaff_Checkout(NameObject_This nameObject_This, bool value)
    //{
    //    GetData_Map().GetData_InCheckout().SetData_ActiveStaff_Checkout(nameObject_This, value);
    //    SaveData();
    //    LoadData();
    //}
    //#endregion
}
[System.Serializable]
public class Data_Map //dong goi de quan li
{
    public DataPlayer dataPlayer;
    public Data_InCheckout data_InCheckout;
    public int LevelMap;

    public Data_Map ResetData()
    {
        if(LevelMap < 1) { LevelMap = 1; }
        dataPlayer.ResetData();
        data_InCheckout.ResetData();
        return this;
    }
    public Data_InCheckout GetData_InCheckout()
    {
        return data_InCheckout;
    }
    public DataPlayer GetDataPlayer()
    {
        return dataPlayer;
    }
    public int GetLevelMap()
    {
        if (LevelMap < 1) { LevelMap = 1; }
        return LevelMap;
    }

}
[System.Serializable]
public class Data_InCheckout
{
    public List<Data_Checkout> data_Staff_InCheckouts;

    public Data_Checkout GetData_Checkout(NameObject_This nameObject_This)
    {
        foreach (Data_Checkout data_Checkout in data_Staff_InCheckouts)
        {
            if(data_Checkout.NameObject_This == nameObject_This)
            {
                return data_Checkout;
            }
        }
        return null;
    }
    public void SetData_ActiveStaff_Checkout(NameObject_This nameObject_This, bool value)
    {
        foreach (Data_Checkout data_Checkout in data_Staff_InCheckouts)
        {
            if (data_Checkout.NameObject_This == nameObject_This)
            {
                data_Checkout.isActiveStaff = value;
            }
        }
    }

    public void ResetData()
    {
        foreach(Data_Checkout data_Checkout in data_Staff_InCheckouts)
        {
            data_Checkout.ResetData();
        }
    }
}
[System.Serializable]
public class Data_Checkout
{
    public NameObject_This NameObject_This;
    public bool isActiveStaff;

    public void ResetData()
    {
        isActiveStaff = false;
    }
}
