using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public CustomerManager customerManager;
    public CheckOutManager checkOutManager;
    public MachineManager machineManager;
    public ClosetManager closetManager;
    public HabitatManager habitatManager;
    public StaffManager staffManager;
    public DataLevelManager dataLevelManager;
    public bool isDoneMachineTUT;
    public bool isDoneClosetTUT;
    public bool isDoneBagClosetTUT;
    public bool isDoneCarTUT;
    //public PlaceManager placeManager;

    public void StartInGame()
    {
        if (dataLevelManager.Get_isDoneAllTUT())
        {
            isDoneMachineTUT = true;
            isDoneClosetTUT = true;
            isDoneBagClosetTUT = true;
            isDoneCarTUT = true;
        }
        else
        {
            isDoneMachineTUT = false;
            isDoneClosetTUT = false;
            isDoneBagClosetTUT = false;
            isDoneCarTUT = false;
        }
      
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.DoneAllTuT.ToString(), () =>
        {
            Set_isDoneAllTUT(true);
        });
        EnventManager.AddListener(EventName.ClearData.ToString(), () =>
        {
            dataLevelManager.ClearData();
        });
    }
    public void ResetLevel()
    {
        
    }
    public void Set_isDoneAllTUT(bool value)
    {
        isDoneMachineTUT = value;
        dataLevelManager.Set_isDoneAllTUT(value);
    }
    public void Set_isDoneMachineTUT(bool value)
    {
        isDoneMachineTUT = value;
       // dataLevelManager.Set_isDoneMachineTUT(value);
        if (value)
        {
            GameManager.Instance.tutManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].CheckDoneAllTuT();
        }
    }
    public void Set_isDoneClosetTUT(bool value)
    {
        isDoneClosetTUT = value;
       // dataLevelManager.Set_isDoneClosetTUT(value);
        if (value)
        {
            GameManager.Instance.tutManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].CheckDoneAllTuT();
        }

    }
    public void Set_isDoneBagClosetTUT(bool value)
    {
        isDoneBagClosetTUT = value;
       // dataLevelManager.Set_isDoneBagClosetTUT(value);
        if (value)
        {
            GameManager.Instance.tutManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].CheckDoneAllTuT();
        }
    }
    public void Set_isDoneCarTUT(bool value)
    {
        isDoneCarTUT = value;
      //  dataLevelManager.Set_isDoneCarTUT(value);
        if (value)
        {
            GameManager.Instance.tutManagers[DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1].CheckDoneAllTuT();
        }
    }

}
[System.Serializable]
public class DataLevelManager
{
    public DataLevelMap dataLevelMap;
    private bool isInItData = false;
    private string fileName = " ";
    public void InItData()
    {
        SetFileName(nameof(DataLevelManager) + "Map_" + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap);
        LoadData();
        isInItData = true;
    }
    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }
    public string GetFileName()
    {
        return fileName;
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(dataLevelMap);
        File.WriteAllText(Application.persistentDataPath + GetFileName(), json);
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
        string json = File.ReadAllText(Application.persistentDataPath + GetFileName());
        dataLevelMap = JsonUtility.FromJson<DataLevelMap>(json);
    }
    public void ResetData()
    {
        dataLevelMap.ResetData();
    }
    public void ClearData()
    {
        ResetData();
        SaveData();
        LoadData();
    }
    private void CheckInItData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public void Set_isDoneAllTUT(bool value)
    {
        CheckInItData();
        dataLevelMap.isDoneAllTuT = value;
        SaveData();
    }
    public bool Get_isDoneAllTUT()
    {
        CheckInItData();
        return dataLevelMap.isDoneAllTuT;
    }
    //public void Set_isDoneMachineTUT(bool value)
    //{
    //    CheckInItData();
    //    dataLevelMap.isDoneMachineTUT = value;
    //    SaveData();
    //}
    //public void Set_isDoneClosetTUT(bool value)
    //{
    //    CheckInItData();
    //    dataLevelMap.isDoneClosetTUT = value;
    //    SaveData();
    //}
    //public void Set_isDoneBagClosetTUT(bool value)
    //{
    //    CheckInItData();
    //    dataLevelMap.isDoneBagClosetTUT = value;
    //    SaveData();
    //}
    //public void Set_isDoneCarTUT(bool value)
    //{
    //    CheckInItData();
    //    dataLevelMap.isDoneCarTUT = value;
    //    SaveData();
    //}
    //public bool Get_isDoneMachineTUT()
    //{
    //    CheckInItData();
    //    return dataLevelMap.isDoneMachineTUT;
    //}
    //public bool Get_isDoneClosetTUT()
    //{
    //    CheckInItData();
    //    return dataLevelMap.isDoneClosetTUT;
    //}
    //public bool Get_isDoneBagClosetTUT()
    //{
    //    CheckInItData();
    //    return dataLevelMap.isDoneBagClosetTUT;
    //}
    //public bool Get_isDoneCarTUT()
    //{
    //    CheckInItData();
    //    return dataLevelMap.isDoneCarTUT;
    //}
}
[System.Serializable]
public class DataLevelMap
{
    //public bool isDoneMachineTUT;
    //public bool isDoneClosetTUT;
    //public bool isDoneBagClosetTUT;
    //public bool isDoneCarTUT;
    public bool isDoneAllTuT;
    public void ResetData()
    {
        isDoneAllTuT = false;
        //isDoneMachineTUT = false;
        //isDoneClosetTUT = false;
        //isDoneBagClosetTUT = false;
        //isDoneCarTUT = false;
    }
}