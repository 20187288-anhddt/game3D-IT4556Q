using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatDataStatusObject : DataStatusObject
{
    private static string nameData_ChickenCountShit = "DataCountShit";
    private static string nameData_ChickenCountAnimal = "DataCountAnimal";
    private int countShit = 0;
    private int countAnimal = 0;

    public override void LoadData()
    {
        base.LoadData();
        countShit = PlayerPrefs.GetInt(nameData_ChickenCountShit + GetFileName());
        countAnimal = PlayerPrefs.GetInt(nameData_ChickenCountAnimal + GetFileName());
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(nameData_ChickenCountShit + GetFileName(), countShit);
        PlayerPrefs.SetInt(nameData_ChickenCountAnimal + GetFileName(), countAnimal);
    }
    public override void ResetData()
    {
        base.ResetData();
        countShit = 0;
        countAnimal = 3;
    }
    public int GetCountShit()
    {
        LoadData();
        return countShit;
    }
    public int GetCountAnimal()
    {
        LoadData();
        return countAnimal;
    }
    public void SetCountShit(int value)
    {
        countShit = value;
        SaveData();
        LoadData();
    }
    public void SetCountAnimal(int value)
    {
        countAnimal = value;
        SaveData();
        LoadData();
    }
}
