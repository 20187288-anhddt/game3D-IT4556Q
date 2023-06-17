using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataStatusObject : DataBase
{
    public Status_All_Level_Object status_All_Level_Object;

    public void Start()
    {
        InItData();
    }
    public void InItData()
    {
        SetFileName(nameof(Status_All_Level_Object) + status_All_Level_Object.nameObject_This +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
        LoadData();
        // OnBuy();
       // OnBought();
    }
    public override void SaveData()
    {
        base.SaveData();
        string json = JsonUtility.ToJson(status_All_Level_Object);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
    }
    public override void LoadData()
    {
        base.LoadData();
        string json = File.ReadAllText(Application.persistentDataPath + "/" + GetFileName());
        Status_All_Level_Object dataSave = JsonUtility.FromJson<Status_All_Level_Object>(json);
        status_All_Level_Object = dataSave;
        status_All_Level_Object.LoadStatusObjectCurrent();
    }
    public override void ResetData()
    {
        base.ResetData();
        status_All_Level_Object.ResetData();
    }
    public Status_All_Level_Object GetStatus_All_Level_Object()
    {
        return status_All_Level_Object;
    }
    public bool isStatusActive(StatusObject statusObjectCheck = null)
    {
        if(statusObjectCheck == null)
        {
            statusObjectCheck = GetStatusObject_Current();
        }
        foreach(StatusObject statusObject in GetStatus_All_Level_Object().GetStatusObjects())
        {
            if(statusObject.typeStatus == StatusObject.TypeStatus_IsActive.Active && statusObjectCheck == statusObject)
            {
                return true;
            }
        }
        return false;
    }
    public bool isStatusDeActive(StatusObject statusObjectCheck = null)
    {
        if (statusObjectCheck == null)
        {
            statusObjectCheck = GetStatusObject_Current();
        }
        return !isStatusActive(statusObjectCheck);
    }
    public bool isStaus_OnBuy(StatusObject statusObjectCheck = null)
    {
        if (statusObjectCheck == null)
        {
            statusObjectCheck = GetStatusObject_Current();
        }
        foreach (StatusObject statusObject in GetStatus_All_Level_Object().GetStatusObjects())
        {
            if (statusObject.typeStatus_IsBought == StatusObject.TypeStatus_IsBought.Buy && statusObject == statusObjectCheck)
            {
                return true;
            }
        }
        return false;
    }
    public bool isStaus_NotBuy(StatusObject statusObjectCheck = null)
    {
        if (statusObjectCheck == null)
        {
            statusObjectCheck = GetStatusObject_Current();
        }
        foreach (StatusObject statusObject in GetStatus_All_Level_Object().GetStatusObjects())
        {
            if (statusObject.typeStatus_IsBought == StatusObject.TypeStatus_IsBought.NotBuy && statusObject == statusObjectCheck)
            {
                return true;
            }
        }
        return false;
    }
    public bool isStatus_Bought(StatusObject statusObjectCheck = null)
    {
        if (statusObjectCheck == null)
        {
            statusObjectCheck = GetStatusObject_Current();
        }
        return !isStaus_NotBuy(statusObjectCheck) && !isStaus_OnBuy(statusObjectCheck);
    }
    public void OnBought(StatusObject statusObjectCheck = null)//chuyen sang trang thai da mua
    {
        if (statusObjectCheck == null)
        {
            statusObjectCheck = GetStatusObject_Current();
        }
        foreach (StatusObject statusObject in GetStatus_All_Level_Object().GetStatusObjects())
        {
            if (statusObject == statusObjectCheck)
            {
                statusObject.typeStatus = StatusObject.TypeStatus_IsActive.Active;
                statusObject.typeStatus_IsBought = StatusObject.TypeStatus_IsBought.Bought;
            }
        }
        if (GetStatus_All_Level_Object().GetStatusObjects().Contains(statusObjectCheck))
        {
            if(GetStatus_All_Level_Object().GetStatusObjects().Count > GetStatus_All_Level_Object().GetStatusObjects().IndexOf(statusObjectCheck) + 1)
            {
                OnBuy(GetStatus_All_Level_Object().GetStatusObjects()[GetStatus_All_Level_Object().GetStatusObjects().IndexOf(statusObjectCheck) + 1]);
            }
        }
        SaveData();
        LoadData();
    }
    public void OnBuy(StatusObject statusObjectCheck = null)
    {
        if (statusObjectCheck == null)
        {
            statusObjectCheck = GetStatusObject_Current();
        }
        else
        {
            SetStatusObject_Current(statusObjectCheck);
        }
        foreach (StatusObject statusObject in GetStatus_All_Level_Object().GetStatusObjects())
        {
            if (statusObject.levelThis == statusObjectCheck.levelThis)
            {
                statusObject.typeStatus = StatusObject.TypeStatus_IsActive.DeActive;
                statusObject.typeStatus_IsBought = StatusObject.TypeStatus_IsBought.Buy;
            }
        }
     
        SaveData();
        LoadData();
      
    }// chuyen sang trang thai co the mua
    public StatusObject GetStatusObject_Current()
    {
        return status_All_Level_Object.GetStatusObject_Current();
    }
    public void SetStatusObject_Current(StatusObject statusObject)
    {
        status_All_Level_Object.SetStatusObject_Current(statusObject);
        SaveData();
        LoadData();
    }
}
[System.Serializable]
public class Status_All_Level_Object //1 doi tuong co nhieu level
{
    public List<StatusObject> statusObjects;
    public StatusObject statusObjectCurrent;
    public NameObject_This nameObject_This;
    public StatusObject GetStatusObject_Current()
    {
        return statusObjectCurrent;
    }
    public void SetStatusObject_Current(StatusObject statusObject)
    {
        statusObjectCurrent = statusObject;
    }
    public List<StatusObject> GetStatusObjects()
    {
        return statusObjects;
    }
    public void LoadStatusObjectCurrent()
    {
        foreach (StatusObject statusObject in GetStatusObjects())
        {
            if (statusObject.levelThis == statusObjectCurrent.levelThis)
            {
                statusObjectCurrent = statusObject;
            }
        }
    }
    public void ResetData()
    {
        statusObjectCurrent = statusObjects[0];
    }
}

