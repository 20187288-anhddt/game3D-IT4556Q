using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCheckOutTable : DataStatusObject
{
    private int isHireStaff = -1;

    public override void LoadData()
    {
        base.LoadData();
        isHireStaff = PlayerPrefs.GetInt(nameof(isHireStaff));
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(nameof(isHireStaff), isHireStaff);
    }
    public override void ResetData()
    {
        base.ResetData();
        isHireStaff = -1;
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
}
