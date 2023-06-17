using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataProcessInMapController : DataBase
{
    public DataApparatusProcessCurrent dataApparatusProcessCurrent;
    public ApparatusProcess apparatusProcessCurrent;
    private void Awake()
    {
        InItData();
    }
    public void InItData()
    {
        SetFileName(nameof(DataProcessInMapController) +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
        LoadData();
        AddEvent();
    }
    public void AddEvent()
    {
        #region Bear
        EnventManager.AddListener(EventName.BearCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
        #region Cow
        EnventManager.AddListener(EventName.CowCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
        #region Sheep
        EnventManager.AddListener(EventName.SheepCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
        #region Chicken
        EnventManager.AddListener(EventName.ChickenCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
    }
    public void CheckAndLoadRewardMissionComplete()
    {
        if(apparatusProcessCurrent == null) { LoadData(); }
        if (apparatusProcessCurrent.missionProcess.isCompleteMission())
        {
            apparatusProcessCurrent.rewardProcessCompleteMission.OnLoadReward();
        }
    }
    public override void SaveData()
    {
        base.SaveData();
        string json = JsonUtility.ToJson(dataApparatusProcessCurrent);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
    }
    public override void LoadData()
    {
        base.LoadData();
        string json = File.ReadAllText(Application.persistentDataPath + "/" + GetFileName());
        DataApparatusProcessCurrent dataSave = JsonUtility.FromJson<DataApparatusProcessCurrent>(json);
        dataApparatusProcessCurrent = dataSave;
        GetApparatusProcess_Current();
    }
    public ApparatusProcess GetApparatusProcess_Current()
    {
        string PathResource = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\ApparatusProcess\\"
            + dataApparatusProcessCurrent.ingredientType.ToString().ToLower() + "\\Mission " + dataApparatusProcessCurrent.LevelCurrent;
        apparatusProcessCurrent = (ApparatusProcess)Resources.Load(PathResource, typeof(ApparatusProcess));
        return apparatusProcessCurrent;
    }
    public void SetDataApparatusProcessCurrent(IngredientType ingredientType, int LevelCurrent)
    {
        dataApparatusProcessCurrent.ingredientType = ingredientType;
        dataApparatusProcessCurrent.LevelCurrent = LevelCurrent;
        SaveData();
        LoadData();
    }
    public override void ResetData()
    {
        base.ResetData();
        dataApparatusProcessCurrent.ResetData();
    }
}
[System.Serializable]
public class DataApparatusProcessCurrent
{
    public IngredientType ingredientType;
    public int LevelCurrent;

    public void ResetData()
    {
        LevelCurrent = 1;
        ingredientType = IngredientType.CHICKEN;
    }
}
[System.Serializable]
public class MissionProcess
{
    public List<EventName> nameMissions;
    public bool isCompleteMission()
    {
        foreach(EventName eventName in nameMissions)
        {
            if (!CheckEventComplete(eventName))
            {
                return false;
            }
        }
        return true;
    }
    public bool CheckEventComplete(EventName eventName)
    {
        switch (eventName)
        {
            case EventName.CowHabitat_Complete:
                return IsHabitat_Complete(IngredientType.COW);
            case EventName.CowClothMachine_Complete:
                return IsClothMachine_Complete(IngredientType.COW);
            case EventName.CowCloset_Complete:
                return IsCloset_Complete(IngredientType.COW);
            case EventName.CowCloset_1_Complete:
                return IsCloset_1_Complete(IngredientType.COW);
            case EventName.SheepHabitat_Complete:
                return IsHabitat_Complete(IngredientType.SHEEP);
            case EventName.SheepClothMachine_Complete:
                return IsClothMachine_Complete(IngredientType.SHEEP);
            case EventName.SheepCloset_Complete:
                return IsCloset_Complete(IngredientType.SHEEP);
            case EventName.SheepCloset_1_Complete:
                return IsCloset_1_Complete(IngredientType.SHEEP);
            case EventName.ChickenHabitat_Complete:
                return IsHabitat_Complete(IngredientType.CHICKEN);
            case EventName.ChickenClothMachine_Complete:
                return IsClothMachine_Complete(IngredientType.CHICKEN);
            case EventName.ChickenCloset_Complete:
                return IsCloset_Complete(IngredientType.CHICKEN);
            case EventName.ChickenCloset_1_Complete:
                return IsCloset_1_Complete(IngredientType.CHICKEN);
            case EventName.BearHabitat_Complete:
                return IsHabitat_Complete(IngredientType.BEAR);
            case EventName.BearClothMachine_Complete:
                return IsClothMachine_Complete(IngredientType.BEAR);
            case EventName.BearCloset_Complete:
                return IsCloset_Complete(IngredientType.BEAR);
            case EventName.BearCloset_1_Complete:
                return IsCloset_1_Complete(IngredientType.BEAR);

        }
        return false;
    }
    public bool IsHabitat_Complete(IngredientType ingredientType)
    {
        return BuildController.Instance.GetBuildIngredientController(ingredientType).IsHabitat_Complete();
    }
    public bool IsClothMachine_Complete(IngredientType ingredientType)
    {
        return BuildController.Instance.GetBuildIngredientController(ingredientType).IsClothMachine_Complete();
    }
    public bool IsCloset_Complete(IngredientType ingredientType)
    {
        return BuildController.Instance.GetBuildIngredientController(ingredientType).IsCloset_Complete();
    }
    public bool IsCloset_1_Complete(IngredientType ingredientType)
    {
        return BuildController.Instance.GetBuildIngredientController(ingredientType).IsCloset_1_Complete();
    }
}
[System.Serializable]
public class RewardProcessCompleteMission
{
    public List<EventName> nameMissions;
    public void OnLoadReward()
    {
        foreach(EventName eventName in nameMissions)
        {
            LoadReward(eventName);
        }
    }
    public void LoadReward(EventName eventName)
    {
        switch (eventName)
        {
            case EventName.CowHabitat_Complete:
                Habitat_OnBuy(IngredientType.COW);
                break;
            case EventName.CowClothMachine_Complete:
                ClothMachine_OnBuy(IngredientType.COW);
                break;
            case EventName.CowCloset_Complete:
                Closet_OnBuy(IngredientType.COW);
                break;
            case EventName.CowCloset_1_Complete:
                Closet_1_OnBuy(IngredientType.COW);
                break;
            case EventName.SheepHabitat_Complete:
                Habitat_OnBuy(IngredientType.SHEEP);
                break;
            case EventName.SheepClothMachine_Complete:
                ClothMachine_OnBuy(IngredientType.SHEEP);
                break;
            case EventName.SheepCloset_Complete:
                Closet_OnBuy(IngredientType.SHEEP);
                break;
            case EventName.SheepCloset_1_Complete:
                Closet_1_OnBuy(IngredientType.SHEEP);
                break;
            case EventName.ChickenHabitat_Complete:
                Habitat_OnBuy(IngredientType.CHICKEN);
                break;
            case EventName.ChickenClothMachine_Complete:
                ClothMachine_OnBuy(IngredientType.CHICKEN);
                break;
            case EventName.ChickenCloset_Complete:
                Closet_OnBuy(IngredientType.CHICKEN);
                break;
            case EventName.ChickenCloset_1_Complete:
                Closet_1_OnBuy(IngredientType.CHICKEN);
                break;
            case EventName.BearHabitat_Complete:
                Habitat_OnBuy(IngredientType.BEAR);
                break;
            case EventName.BearClothMachine_Complete:
                ClothMachine_OnBuy(IngredientType.BEAR);
                break;
            case EventName.BearCloset_Complete:
                Closet_OnBuy(IngredientType.BEAR);
                break;
            case EventName.BearCloset_1_Complete:
                Closet_1_OnBuy(IngredientType.BEAR);
                break;
        }
    }

    public void Habitat_OnBuy(IngredientType ingredientType)
    {
        BuildController.Instance.GetBuildIngredientController(ingredientType).Habitat_OnBuy();
    }
    public void ClothMachine_OnBuy(IngredientType ingredientType)
    {
        BuildController.Instance.GetBuildIngredientController(ingredientType).ClothMachine_OnBuy();
    }
    public void Closet_OnBuy(IngredientType ingredientType)
    {
        BuildController.Instance.GetBuildIngredientController(ingredientType).Closet_OnBuy();
    }
    public void Closet_1_OnBuy(IngredientType ingredientType)
    {
        BuildController.Instance.GetBuildIngredientController(ingredientType).Closet_1_OnBuy();
    }

}
