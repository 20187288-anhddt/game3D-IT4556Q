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
                return Is_Complete(IngredientType.COW, NameObject_This.CowHabitat);
            case EventName.CowClothMachine_Complete:
                return Is_Complete(IngredientType.COW, NameObject_This.CowClothMachine);
            case EventName.CowCloset_Complete:
                return Is_Complete(IngredientType.COW, NameObject_This.CowCloset);
            case EventName.CowCloset_1_Complete:
                return Is_Complete(IngredientType.COW, NameObject_This.CowCloset_1);
            case EventName.SheepHabitat_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepHabitat);
            case EventName.SheepClothMachine_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepClothMachine);
            case EventName.SheepCloset_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepCloset);
            case EventName.SheepCloset_1_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepCloset_1);
            case EventName.ChickenHabitat_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenHabitat);
            case EventName.ChickenClothMachine_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenClothMachine);
            case EventName.ChickenCloset_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenCloset);
            case EventName.ChickenCloset_1_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenCloset_1);
            case EventName.BearHabitat_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearHabitat);
            case EventName.BearClothMachine_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearClothMachine);
            case EventName.BearCloset_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearCloset);
            case EventName.BearCloset_1_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearCloset_1);

        }
        return false;
    }
    public bool Is_Complete(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        return BuildController.Instance.GetBuildIngredientController(ingredientType).IsBuild_Complete(nameObject_This);
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
                OnBuy(IngredientType.COW, NameObject_This.CowHabitat);
                break;
            case EventName.CowClothMachine_Complete:
                OnBuy(IngredientType.COW, NameObject_This.CowClothMachine);
                break;
            case EventName.CowCloset_Complete:
                OnBuy(IngredientType.COW, NameObject_This.CowCloset);
                break;
            case EventName.CowCloset_1_Complete:
                OnBuy(IngredientType.COW, NameObject_This.CowCloset_1);
                break;
            case EventName.SheepHabitat_Complete:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepHabitat);
                break;
            case EventName.SheepClothMachine_Complete:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepClothMachine);
                break;
            case EventName.SheepCloset_Complete:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepCloset);
                break;
            case EventName.SheepCloset_1_Complete:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepCloset_1);
                break;
            case EventName.ChickenHabitat_Complete:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenHabitat);
                break;
            case EventName.ChickenClothMachine_Complete:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenClothMachine);
                break;
            case EventName.ChickenCloset_Complete:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenCloset);
                break;
            case EventName.ChickenCloset_1_Complete:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenCloset_1);
                break;
            case EventName.BearHabitat_Complete:
                OnBuy(IngredientType.BEAR, NameObject_This.BearHabitat);
                break;
            case EventName.BearClothMachine_Complete:
                OnBuy(IngredientType.BEAR, NameObject_This.BearClothMachine);
                break;
            case EventName.BearCloset_Complete:
                OnBuy(IngredientType.BEAR, NameObject_This.BearCloset);
                break;
            case EventName.BearCloset_1_Complete:
                OnBuy(IngredientType.BEAR, NameObject_This.BearCloset_1);
                break;
        }
    }

    public void OnBuy(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        BuildController.Instance.GetBuildIngredientController(ingredientType).OnBuy(nameObject_This);
    }
   
}
