using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCheckOutTable : DataStatusObject
{
    private int isHireStaff = -1;
    private int countMoney_NotCollect = 0;
    public override void LoadData()
    {
        base.LoadData();
        isHireStaff = PlayerPrefs.GetInt(nameof(isHireStaff) + GetStatus_All_Level_Object().nameObject_This.ToString() +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
        countMoney_NotCollect = PlayerPrefs.GetInt(nameof(countMoney_NotCollect) + GetStatus_All_Level_Object().nameObject_This.ToString() +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(nameof(isHireStaff) + GetStatus_All_Level_Object().nameObject_This.ToString() +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString(), isHireStaff);
        PlayerPrefs.SetInt(nameof(countMoney_NotCollect) + GetStatus_All_Level_Object().nameObject_This.ToString() +
           "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString(), countMoney_NotCollect);
    }
    public override void ResetData()
    {
        base.ResetData();
        isHireStaff = -1;
        countMoney_NotCollect = 0;
    }
    public void SetData_IsHireStaff(bool Value)
    {
        if(Value)
        {
            isHireStaff = 1;
        }
        else
        {
            isHireStaff = -1;
        }
      
        SaveData();
        LoadData();
    }
    public bool GetData_IsHireStaff()
    {
        LoadData();
        return (isHireStaff == -1) ? false : true;
    }
    public int GetCount_Money_Not_Collect()
    {
        LoadData();
        return countMoney_NotCollect;
    }
    public void SetCount_Money_Not_Collect(int value)
    {
        countMoney_NotCollect = value;
        SaveData();
        LoadData();
    }
}
