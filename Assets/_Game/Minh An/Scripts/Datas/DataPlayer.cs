using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class DataPlayer
{
    public DataBoss DataBoss = new DataBoss();
    public DataStaff DataStaff_Worker = new DataStaff();
    public DataStaff DataStaff_Farmer = new DataStaff();
    public void ResetData()
    {
        DataBoss = DataBoss.ResetData();
        DataStaff_Worker = DataStaff_Worker.ResetData();
        DataStaff_Farmer = DataStaff_Farmer.ResetData();
    }
    public DataBoss GetDataBoss()
    {
        return DataBoss;
    }
    public DataStaff GetDataStaff(StaffType staffType)
    {
        switch (staffType)
        {
            case StaffType.FARMER:
                return DataStaff_Farmer;
            case StaffType.WORKER:
                return DataStaff_Worker;
        }
        return null;
    }
}

[System.Serializable]
public class DataBoss
{
    public static string pathLoadData_Capacity = "";
    public static string pathLoadData_Price = "";
    public static string pathLoadData_Speed = "";
    public int level_Capacity_Taget;
    public int level_Price_Taget;
    public int level_Speed_Taget;
   // public static bool isInItData = false;
    public infoSpeed GetInfoSpeedTaget()
    {
        pathLoadData_Speed = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Speeds\\Level ";
        infoSpeed infoSpeed_ = (infoSpeed)Resources.Load(pathLoadData_Speed + level_Speed_Taget, typeof(infoSpeed));
        if(infoSpeed_ == null)
        {
            level_Speed_Taget--;
            pathLoadData_Speed = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Speeds\\Level ";
            infoSpeed_ = (infoSpeed)Resources.Load(pathLoadData_Speed + level_Speed_Taget, typeof(infoSpeed));
        }
        return infoSpeed_;
    }
    public infoCapacity GetInfoCapacityTaget()
    {
        pathLoadData_Capacity = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Capacitys\\Level ";
        infoCapacity infoCapacity_ = (infoCapacity)Resources.Load(pathLoadData_Capacity + level_Capacity_Taget, typeof(infoCapacity));
        if(infoCapacity_ == null)
        {
            level_Capacity_Taget--;
            pathLoadData_Capacity = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Capacitys\\Level ";
            infoCapacity_ = (infoCapacity)Resources.Load(pathLoadData_Capacity + level_Capacity_Taget, typeof(infoCapacity));
        }
        return infoCapacity_;
    }
    public infoPrice GetInfoPriceTaget()
    {
        pathLoadData_Price = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Prices\\Level ";
        infoPrice infoPrice_ = (infoPrice)Resources.Load(pathLoadData_Price + level_Price_Taget, typeof(infoPrice));
        if(infoPrice_ == null)
        {
            level_Price_Taget--;
            pathLoadData_Price = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Prices\\Level ";
            infoPrice_ = (infoPrice)Resources.Load(pathLoadData_Price + level_Price_Taget, typeof(infoPrice));
        }
        return infoPrice_;
    }
    public infoSpeed GetInfoSpeedTaget(int Level)
    {
        pathLoadData_Speed = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Speeds\\Level ";
        infoSpeed infoSpeed_ = (infoSpeed)Resources.Load(pathLoadData_Speed + Level, typeof(infoSpeed));
        return infoSpeed_;
    }
    public infoCapacity GetInfoCapacityTaget(int Level)
    {
        pathLoadData_Capacity = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Capacitys\\Level ";
        infoCapacity infoCapacity_ = (infoCapacity)Resources.Load(pathLoadData_Capacity + Level, typeof(infoCapacity));
        Debug.Log(infoCapacity_ == null);
        Debug.Log(Level);
        return infoCapacity_;
    }
    public infoPrice GetInfoPriceTaget(int Level)
    {
        pathLoadData_Price = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Boss\\Prices\\Level ";
        infoPrice infoPrice_ = (infoPrice)Resources.Load(pathLoadData_Price + Level, typeof(infoPrice));
        return infoPrice_;
    }
    //public void InItData()
    //{
    //    if (isInItData)
    //    {
    //        return;
    //    }
    //    isInItData = true;
    //}
    public DataBoss ResetData()
    {
        level_Capacity_Taget = 1;
        level_Price_Taget = 1;
        level_Speed_Taget = 1;
       // InItData();
        return this;
    }
    public void SetLevel_Capacity(int value)
    {
        level_Capacity_Taget = value;
    }
    public void SetLevel_Price(int value)
    {
        level_Price_Taget = value;
    }
    public void SetLevel_Speed(int value)
    {
        level_Speed_Taget = value;
    }
    public void NextLevel_Capacity()
    {
        level_Capacity_Taget ++;
    }
    public void NextLevel_Price()
    {
        level_Price_Taget ++;
    }
    public void NextLevel_Speed()
    {
        level_Speed_Taget ++;
    }
    public int GetLevel_Capacity()
    {
        return level_Capacity_Taget;
    }
    public int GetLevel_Price()
    {
        return level_Price_Taget;
    }
    public int GetLevel_Speed()
    {
        return level_Speed_Taget;
    }
    public bool IsMaxLevel_Capacity(int Level)
    {
        return GetInfoCapacityTaget(Level) == null;
    }
    public bool IsMaxLevel_Price(int Level)
    {
        return GetInfoPriceTaget(Level) == null;
    }
    public bool IsMaxLevel_Speed(int Level)
    {
        return GetInfoSpeedTaget(Level) == null;
    }
}
[System.Serializable]
public class DataStaff
{
    public static string pathLoadData_Capacity = "";
    public static string pathLoadData_Hire = "";
    public static string pathLoadData_Speed = "";
    public int level_Capacity_Taget;
    public int level_Hire_Taget;
    public int level_Speed_Taget;

   // public static bool isInItData = false;
    public infoSpeed GetInfoSpeedTaget(StaffType staffType)
    {
        pathLoadData_Speed = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Speeds\\Level ";
        infoSpeed infoSpeed_ = (infoSpeed)Resources.Load(pathLoadData_Speed + level_Speed_Taget, typeof(infoSpeed));
        if(infoSpeed_ == null)
        {
            level_Speed_Taget--;
            pathLoadData_Speed = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Speeds\\Level ";
            infoSpeed_ = (infoSpeed)Resources.Load(pathLoadData_Speed + level_Speed_Taget, typeof(infoSpeed));
        }
        return infoSpeed_;
    }
    public infoCapacity GetInfoCapacityTaget(StaffType staffType)
    {
        pathLoadData_Capacity = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Capacitys\\Level ";
        infoCapacity infoCapacity_ = (infoCapacity)Resources.Load(pathLoadData_Capacity + level_Capacity_Taget, typeof(infoCapacity));
        if(infoCapacity_ == null)
        {
            level_Capacity_Taget--;
            pathLoadData_Capacity = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Capacitys\\Level ";
             infoCapacity_ = (infoCapacity)Resources.Load(pathLoadData_Capacity + level_Capacity_Taget, typeof(infoCapacity));
        }
        return infoCapacity_;
    }
    public infoHire GetInfoHireTaget(StaffType staffType)
    {
        pathLoadData_Hire = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Hires\\Level ";
        infoHire infoHire_ = (infoHire)Resources.Load(pathLoadData_Hire + level_Hire_Taget, typeof(infoHire));
        if(infoHire_ == null)
        {
            level_Hire_Taget--;
            pathLoadData_Hire = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Hires\\Level ";
            infoHire_ = (infoHire)Resources.Load(pathLoadData_Hire + level_Hire_Taget, typeof(infoHire));
        }
        return infoHire_;
    }
    public infoSpeed GetInfoSpeedTaget(StaffType staffType, int Level)
    {
        pathLoadData_Speed = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Speeds\\Level ";
        infoSpeed infoSpeed_ = (infoSpeed)Resources.Load(pathLoadData_Speed + Level, typeof(infoSpeed));
        return infoSpeed_;
    }
    public infoCapacity GetInfoCapacityTaget(StaffType staffType, int Level)
    {
        pathLoadData_Capacity = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Capacitys\\Level ";
        infoCapacity infoCapacity_ = (infoCapacity)Resources.Load(pathLoadData_Capacity + Level, typeof(infoCapacity));
        return infoCapacity_;
    }
    public infoHire GetInfoHireTaget(StaffType staffType, int Level)
    {
        pathLoadData_Hire = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Staff\\" + staffType.ToString().ToLower() + "\\Hires\\Level ";
        infoHire infoHire_ = (infoHire)Resources.Load(pathLoadData_Hire + Level, typeof(infoHire));
        return infoHire_;
    }
    //public void InItData()
    //{
    //    if (isInItData)
    //    {
    //        return;
    //    }
    //    isInItData = true;
    //}
    public DataStaff ResetData()
    {
        level_Capacity_Taget = 1;
        level_Hire_Taget = 1;
        level_Speed_Taget = 1;
       // InItData();
        return this;
    }
    public void NextLevel_Capacity()
    {
        level_Capacity_Taget++;
    }
    public void NextLevel_Hire()
    {
        level_Hire_Taget++;
    }
    public void NextLevel_Speed()
    {
        level_Speed_Taget++;
    }
    public void SetLevel_Capacity(int value)
    {
        level_Capacity_Taget = value;
    }
    public void SetLevel_Hire(int value)
    {
        level_Hire_Taget = value;
    }
    public void SetLevel_Speed(int value)
    {
        level_Speed_Taget = value;
    }
    public int GetLevel_Speed()
    {
       return level_Speed_Taget;
    }
    public int GetLevel_Capacity()
    {
        return level_Capacity_Taget;
    }
    public int GetLevel_Hire()
    {
        return level_Hire_Taget;
    }
    public bool IsMaxLevel_Speed(StaffType staffType, int Level)
    {
        return GetInfoSpeedTaget(staffType, Level) == null;
    }
    public bool IsMaxLevel_Capacity(StaffType staffType, int Level)
    {
        return GetInfoCapacityTaget(staffType, Level) == null;
    }
    public bool IsMaxLevel_Hire(StaffType staffType, int Level)
    {
        return GetInfoHireTaget(staffType, Level) == null;
    }
}