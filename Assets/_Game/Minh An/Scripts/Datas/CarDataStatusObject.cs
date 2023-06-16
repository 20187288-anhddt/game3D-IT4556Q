using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDataStatusObject : DataStatusObject
{
    private static string Name_Check_IsOpenCar_OneShot = "Car_Open_OneShot";
    private bool isOpenOneShot = false;
    public override void LoadData()
    {
        //   Debug.Log(GetFileName());
        base.LoadData();
        isOpenOneShot = PlayerPrefs.GetInt(Name_Check_IsOpenCar_OneShot + GetFileName()) == 1 ? true : false;
        //Debug.Log(Level_Speed);
        //Debug.Log(Level_Stack);
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(Name_Check_IsOpenCar_OneShot + GetFileName(), isOpenOneShot == true ? 1 : -1);
    }
    public override void ResetData()
    {
        base.ResetData();
        isOpenOneShot = false;
    }
    public bool IsOpenOneShot()
    {
        LoadData();
        return isOpenOneShot;
    }
    public void SetIsOpenOneShot(bool value)
    {
        isOpenOneShot = value;
        SaveData();
        LoadData();
    }
}
