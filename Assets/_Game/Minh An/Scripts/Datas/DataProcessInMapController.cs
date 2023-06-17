using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataProcessInMapController : DataBase
{
    public DataProcess dataProcess;
    private void Start()
    {
        InItData();
    }
    public void InItData()
    {
        SetFileName(nameof(DataProcess));
        AddEvent();
        LoadData();
        dataProcess.GetDataApparatusProcess().InItData();
    }
    public override void SaveData()
    {
        base.SaveData();
        string json = JsonUtility.ToJson(dataProcess);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
    }
    public override void LoadData()
    {
        base.LoadData();
        string json = File.ReadAllText(Application.persistentDataPath + "/" + GetFileName());
        DataProcess dataSave = JsonUtility.FromJson<DataProcess>(json);
        dataProcess = dataSave;
    }
    public void CheckAndLoadRewardMissionComplete()
    {
        dataProcess.GetDataApparatusProcess().CheckAndLoadRewardMissionComplete();
    }
    public override void ResetData()
    {
        base.ResetData();
        dataProcess.GetDataApparatusProcess().InItData();
    }

    public void AddEvent()
    {
        #region Bear
        EnventManager.AddListener(EventName.BearCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearBagMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.BearBagCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
        #region Cow
        EnventManager.AddListener(EventName.CowCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowBagMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.CowBagCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
        #region Sheep
        EnventManager.AddListener(EventName.SheepCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepBagMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.SheepBagCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
        #region Chicken
        EnventManager.AddListener(EventName.ChickenCloset_1_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenClothMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenBagMachine_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenHabitat_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        EnventManager.AddListener(EventName.ChickenBagCloset_Complete.ToString(), CheckAndLoadRewardMissionComplete);
        #endregion
    }
    
}
[System.Serializable]
public class DataProcess
{
    public DataApparatusProcess dataApparatusProcess;
    public DataApparatusProcess GetDataApparatusProcess()
    {
        return dataApparatusProcess;
    }
}
[System.Serializable]
public class DataApparatusProcess
{
    public DataApparatusProcessCurrent dataApparatusProcessCurrent;
    private ApparatusProcess apparatusProcessCurrent;
    private string fileName = " ";
    public void InItData()
    {
        SetFileName(nameof(DataProcessInMapController) +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
        LoadData();
    }
    public void CheckAndLoadRewardMissionComplete()
    {
        if (apparatusProcessCurrent == null) { LoadData(); }
        if (apparatusProcessCurrent.missionProcess.isCompleteMission())
        {
            apparatusProcessCurrent.rewardProcessCompleteMission.OnLoadReward();
            NextProcess();
        }
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
        string json = JsonUtility.ToJson(dataApparatusProcessCurrent);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
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
    public void NextProcess()
    {
        dataApparatusProcessCurrent.NextProcess();
        if (GetApparatusProcess_Current() == null)
        {

            switch (dataApparatusProcessCurrent.ingredientType)
            {
                case IngredientType.SHEEP:
                    dataApparatusProcessCurrent.ingredientType = IngredientType.COW;
                    break;
                case IngredientType.COW:
                    dataApparatusProcessCurrent.ingredientType = IngredientType.BEAR;
                    break;
                case IngredientType.BEAR:
                    dataApparatusProcessCurrent.ingredientType = IngredientType.BEAR;
                    break;
                case IngredientType.CHICKEN:
                    dataApparatusProcessCurrent.ingredientType = IngredientType.SHEEP;
                    break;
            }
            dataApparatusProcessCurrent.LevelCurrent = 1;
        }
        SaveData();
        LoadData();
    }
    public void SetDataApparatusProcessCurrent(IngredientType ingredientType, int LevelCurrent)
    {
        dataApparatusProcessCurrent.ingredientType = ingredientType;
        dataApparatusProcessCurrent.LevelCurrent = LevelCurrent;
        SaveData();
        LoadData();
    }
    public void ResetData()
    {
        dataApparatusProcessCurrent.ResetData();
    }

}
[System.Serializable]
public class DataApparatusProcessCurrent
{
    public IngredientType ingredientType;
    public int LevelCurrent;
    public void NextProcess()
    {
        LevelCurrent++;
    }
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
            case EventName.CowBagMachine_Complete:
                return Is_Complete(IngredientType.COW, NameObject_This.CowBagMachine);
            case EventName.CowBagCloset_Complete:
                return Is_Complete(IngredientType.COW, NameObject_This.CowBagCloset);
            case EventName.CowCloset_Complete:
                return Is_Complete(IngredientType.COW, NameObject_This.CowCloset);
            case EventName.CowCloset_1_Complete:
                return Is_Complete(IngredientType.COW, NameObject_This.CowCloset_1);
            case EventName.SheepHabitat_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepHabitat);
            case EventName.SheepClothMachine_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepClothMachine);
            case EventName.SheepBagMachine_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepBagMachine);
            case EventName.SheepBagCloset_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepBagCloset);
            case EventName.SheepCloset_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepCloset);
            case EventName.SheepCloset_1_Complete:
                return Is_Complete(IngredientType.SHEEP, NameObject_This.SheepCloset_1);
            case EventName.ChickenHabitat_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenHabitat);
            case EventName.ChickenClothMachine_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenClothMachine);
            case EventName.ChickenBagMachine_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenBagMachine);
            case EventName.ChickenBagCloset_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenBagCloset);
            case EventName.ChickenCloset_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenCloset);
            case EventName.ChickenCloset_1_Complete:
                return Is_Complete(IngredientType.CHICKEN, NameObject_This.ChickenCloset_1);
            case EventName.BearHabitat_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearHabitat);
            case EventName.BearClothMachine_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearClothMachine);
            case EventName.BearBagMachine_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearBagMachine);
            case EventName.BearBagCloset_Complete:
                return Is_Complete(IngredientType.BEAR, NameObject_This.BearBagCloset);
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
            case EventName.CowHabitat_OnBuy:
                OnBuy(IngredientType.COW, NameObject_This.CowHabitat);
                break;
            case EventName.CowClothMachine_OnBuy:
                OnBuy(IngredientType.COW, NameObject_This.CowClothMachine);
                break;
            case EventName.CowBagMachine_OnBuy:
                OnBuy(IngredientType.COW, NameObject_This.CowBagMachine);
                break;
            case EventName.CowBagCloset_OnBuy:
                OnBuy(IngredientType.COW, NameObject_This.CowBagCloset);
                break;
            case EventName.CowCloset_OnBuy:
                OnBuy(IngredientType.COW, NameObject_This.CowCloset);
                break;
            case EventName.CowCloset_1_OnBuy:
                OnBuy(IngredientType.COW, NameObject_This.CowCloset_1);
                break;
            case EventName.SheepHabitat_OnBuy:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepHabitat);
                break;
            case EventName.SheepClothMachine_OnBuy:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepClothMachine);
                break;
            case EventName.SheepBagMachine_OnBuy:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepBagMachine);
                break;
            case EventName.SheepBagCloset_OnBuy:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepBagCloset);
                break;
            case EventName.SheepCloset_OnBuy:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepCloset);
                break;
            case EventName.SheepCloset_1_OnBuy:
                OnBuy(IngredientType.SHEEP, NameObject_This.SheepCloset_1);
                break;
            case EventName.ChickenHabitat_OnBuy:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenHabitat);
                break;
            case EventName.ChickenClothMachine_OnBuy:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenClothMachine);
                break;
            case EventName.ChickenBagMachine_OnBuy:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenBagMachine);
                break;
            case EventName.ChickenBagCloset_OnBuy:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenBagCloset);
                break;
            case EventName.ChickenCloset_OnBuy:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenCloset);
                break;
            case EventName.ChickenCloset_1_OnBuy:
                OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenCloset_1);
                break;
            case EventName.BearHabitat_OnBuy:
                OnBuy(IngredientType.BEAR, NameObject_This.BearHabitat);
                break;
            case EventName.BearClothMachine_OnBuy:
                OnBuy(IngredientType.BEAR, NameObject_This.BearClothMachine);
                break;
            case EventName.BearBagMachine_OnBuy:
                OnBuy(IngredientType.BEAR, NameObject_This.BearBagMachine);
                break;
            case EventName.BearBagCloset_OnBuy:
                OnBuy(IngredientType.BEAR, NameObject_This.BearBagCloset);
                break;
            case EventName.BearCloset_OnBuy:
                OnBuy(IngredientType.BEAR, NameObject_This.BearCloset);
                break;
            case EventName.BearCloset_1_OnBuy:
                OnBuy(IngredientType.BEAR, NameObject_This.BearCloset_1);
                break;
            case EventName.CheckOutTable_OnBought:
                OnBought(IngredientType.CHECKOUT, NameObject_This.CheckOutTable);
                break;
            case EventName.CheckOutTable_1_OnBought:
                OnBought(IngredientType.CHECKOUT, NameObject_This.CheckOutTable_1);
                break;
        }
    }

    public void OnBuy(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        BuildController.Instance.GetBuildIngredientController(ingredientType).OnBuy(nameObject_This);
    }
    public void OnBought(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        BuildController.Instance.GetBuildIngredientController(ingredientType).OnBought(nameObject_This);
    }
}
