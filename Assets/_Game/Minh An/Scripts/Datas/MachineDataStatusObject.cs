using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDataStatusObject : DataStatusObject
{
    public static string nameLevel_Speed = "Level Speed";
    public static string nameLevel_Stack = "Level Stack";
    public static string nameCountItemInput = "CountItemInput";
    public static string nameCountItemOutput = "CountItemOutput";
    public static string PathGetData;
    private int Level_Speed = 1;
    private int Level_Stack = 1;
    private int countItemInput = 0;
    private int countItemOutput = 0;
    private BaseBuild baseBuild;
    private void Awake()
    {
        if(baseBuild == null) { baseBuild = GetComponent<BaseBuild>(); }
       
    }
    public override void LoadData()
    {
     //   Debug.Log(GetFileName());
        base.LoadData();
        Level_Speed = PlayerPrefs.GetInt(nameLevel_Speed + GetFileName());
        Level_Stack = PlayerPrefs.GetInt(nameLevel_Stack + GetFileName());
        countItemInput = PlayerPrefs.GetInt(nameCountItemInput + GetFileName());
        countItemOutput = PlayerPrefs.GetInt(nameCountItemOutput + GetFileName());
        //Debug.Log(Level_Speed);
        //Debug.Log(Level_Stack);
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(nameLevel_Speed + GetFileName(), Level_Speed);
        PlayerPrefs.SetInt(nameLevel_Stack + GetFileName(), Level_Stack);
        PlayerPrefs.SetInt(nameCountItemInput + GetFileName(), countItemInput);
        PlayerPrefs.SetInt(nameCountItemOutput + GetFileName(), countItemOutput);
    }
    public override void ResetData()
    {
        base.ResetData();
        Level_Speed = 1;
        Level_Stack = 1;
        countItemInput = 0;
        countItemOutput = 0;
    }
    public void SetLevel_Speed(int value)
    {
        Level_Speed = value;
        SaveData();
        LoadData();
    }
    public void NextLevel_Speed()
    {
        Level_Speed++;
      //  Debug.Log(Level_Speed);
        SaveData();
        LoadData();
        EnventManager.TriggerEvent(EventName.ReLoadDataUpgrade.ToString());
    }
    public int GetLevel_Speed()
    {
        LoadData();
        return Level_Speed;
    }
    public void SetLevel_Stack(int value)
    {
        Level_Stack = value;
        SaveData();
        LoadData();
    }
    public void NextLevel_Stack()
    {
        Level_Stack++;
       // Debug.Log(Level_Stack);
        SaveData();
        LoadData();
        EnventManager.TriggerEvent(EventName.ReLoadDataUpgrade.ToString());
    }
    public int GetLevel_Stack()
    {
        LoadData();
        return Level_Stack;
    }
    public InfoPirceObject GetInfoPirceObject_Speed()
    {
        InfoPirceObject infoPirceObject_Speed = GetPirceObject_InfoChild(GetStatus_All_Level_Object().nameObject_This, GetLevel_Speed(),
            baseBuild.ingredientType, nameLevel_Speed);
        if(infoPirceObject_Speed == null)
        {
            infoPirceObject_Speed = GetPirceObject_InfoChild(GetStatus_All_Level_Object().nameObject_This, GetLevel_Speed() - 1,
           baseBuild.ingredientType, nameLevel_Speed);
        }
     //   Debug.Log(infoPirceObject_Speed == null);
        return infoPirceObject_Speed;
    }
    public InfoPirceObject GetInfoPirceObject_Stack()
    {
        InfoPirceObject infoPirceObject_Stack = GetPirceObject_InfoChild(GetStatus_All_Level_Object().nameObject_This, GetLevel_Stack(),
            baseBuild.ingredientType, nameLevel_Stack);
        if(infoPirceObject_Stack == null)
        {
            infoPirceObject_Stack = GetPirceObject_InfoChild(GetStatus_All_Level_Object().nameObject_This, GetLevel_Stack() - 1,
            baseBuild.ingredientType, nameLevel_Stack);
        }
     //   Debug.Log(infoPirceObject_Stack == null);
        return infoPirceObject_Stack;
    }
    public InfoPirceObject GetInfoPirceObject_Speed(int Level)
    {
      //  Debug.Log(Level);
        InfoPirceObject infoPirceObject_Speed = GetPirceObject_InfoChild(GetStatus_All_Level_Object().nameObject_This, Level,
            baseBuild.ingredientType, nameLevel_Speed);
        return infoPirceObject_Speed;
    }
    public InfoPirceObject GetInfoPirceObject_Stack(int Level)
    {
        InfoPirceObject infoPirceObject_Stack = GetPirceObject_InfoChild(GetStatus_All_Level_Object().nameObject_This, Level,
            baseBuild.ingredientType, nameLevel_Stack);
        return infoPirceObject_Stack;
    }
    public bool isMaxLevelStack()
    {
        return GetInfoPirceObject_Stack(GetLevel_Stack() + 1) == null;
    }
    public bool isMaxLevelSpeed()
    {
        return GetInfoPirceObject_Speed(GetLevel_Speed() + 1) == null;
    }
    public InfoPirceObject GetPirceObject_InfoChild(NameObject_This nameObject_This, int Level, IngredientType ingredientType, string nameChild)
    {
        InfoPirceObject infoPirceObjectResource = null;
        PathGetData = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Pirce Object\\" + ingredientType.ToString().ToLower()
            + "\\" + nameObject_This.ToString() + "\\" + nameChild + "\\" + "Level " + Level.ToString();

        infoPirceObjectResource = (InfoPirceObject)Resources.Load(PathGetData, typeof(InfoPirceObject));
        // Debug.Log((infoPirceObjectResource == null) +  " "+ PathGetData);
        return infoPirceObjectResource;
    }
    public BaseBuild GetBaseBuild()
    {
        return baseBuild;
    }
    public void Set_CountItemInput(int value)
    {
        countItemInput = value;
        SaveData();
      //  LoadData();
    }
    public void Set_CountItemOutput(int value)
    {
        countItemOutput = value;
        SaveData();
      //  LoadData();
    }
    public int Get_CountItemInput()
    {
        return countItemInput;
    }
    public int Get_CountItemOutput()
    {
        return countItemOutput;
    }
}
