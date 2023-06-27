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
        StartCoroutine(IE_DelayAction(() => 
        {
            CheckAndLoadRewardMissionComplete(EventName.ChickenHabitat_OnBuy);
        }, 0.5f));
    }
    public void InItData()
    {
        SetFileName(nameof(DataProcess));
        AddEvent();
        LoadData();
        dataProcess.GetDataApparatusProcess().InItData();
        dataProcess.GetDataEventProcess().InItData();
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
    public void CheckAndLoadRewardMissionComplete(EventName eventName)
    {
        dataProcess.GetDataApparatusProcess().CheckAndLoadRewardMissionComplete(eventName);
        dataProcess.GetDataEventProcess().CheckAndLoadRewardMissionComplete(eventName);
    }
    public override void ResetData()
    {
        base.ResetData();
        dataProcess.GetDataApparatusProcess().InItData();
        dataProcess.GetDataEventProcess().InItData();
    }

    public void AddEvent()
    {
        #region Bear
        EnventManager.AddListener(EventName.BearCloset_1_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BearCloset_1_Complete); });
        EnventManager.AddListener(EventName.BearClothMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BearClothMachine_Complete); });
        EnventManager.AddListener(EventName.BearBagMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BearBagMachine_Complete); });
        EnventManager.AddListener(EventName.BearCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BearCloset_Complete); });
        EnventManager.AddListener(EventName.BearHabitat_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BearHabitat_Complete); });
        EnventManager.AddListener(EventName.BearBagCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BearBagCloset_Complete); });
        #endregion
        #region Cow
        EnventManager.AddListener(EventName.CowCloset_1_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CowCloset_1_Complete); });
        EnventManager.AddListener(EventName.CowCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CowCloset_Complete); });
        EnventManager.AddListener(EventName.CowClothMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CowClothMachine_Complete); });
        EnventManager.AddListener(EventName.CowBagMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CowBagMachine_Complete); });
        EnventManager.AddListener(EventName.CowHabitat_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CowHabitat_Complete); });
        EnventManager.AddListener(EventName.CowBagCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CowBagCloset_Complete); });
        #endregion
        #region Sheep
        EnventManager.AddListener(EventName.SheepCloset_1_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.SheepCloset_1_Complete); });
        EnventManager.AddListener(EventName.SheepCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.SheepCloset_Complete); });
        EnventManager.AddListener(EventName.SheepClothMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.SheepClothMachine_Complete); });
        EnventManager.AddListener(EventName.SheepBagMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.SheepBagMachine_Complete); });
        EnventManager.AddListener(EventName.SheepHabitat_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.SheepHabitat_Complete); });
        EnventManager.AddListener(EventName.SheepBagCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.SheepBagCloset_Complete); });
        #endregion
        #region Chicken
        EnventManager.AddListener(EventName.ChickenCloset_1_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ChickenCloset_1_Complete); });
        EnventManager.AddListener(EventName.ChickenCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ChickenCloset_Complete); });
        EnventManager.AddListener(EventName.ChickenClothMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ChickenClothMachine_Complete); });
        EnventManager.AddListener(EventName.ChickenBagMachine_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ChickenBagMachine_Complete); });
        EnventManager.AddListener(EventName.ChickenHabitat_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ChickenHabitat_Complete); });
        EnventManager.AddListener(EventName.ChickenBagCloset_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ChickenBagCloset_Complete); });
        #endregion
        #region Build Stage
        EnventManager.AddListener(EventName.BuildStage_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BuildStage_OnComplete); });
        EnventManager.AddListener(EventName.BuildStage_1_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BuildStage_1_OnComplete); });
        #endregion
        #region CheckOut
        EnventManager.AddListener(EventName.CheckOutTable_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CheckOutTable_Complete); });
        EnventManager.AddListener(EventName.CheckOutTable_1_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CheckOutTable_1_Complete); });
        #endregion

    }
    IEnumerator IE_DelayAction(System.Action action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action?.Invoke();
    }
}
[System.Serializable]
public class DataProcess
{
    public DataApparatusProcess dataApparatusProcess;
    public DataEventProcess dataEventProcess;
    public DataEventProcess GetDataEventProcess()
    {
        return dataEventProcess;
    }
    public DataApparatusProcess GetDataApparatusProcess()
    {
        return dataApparatusProcess;
    }
}
[System.Serializable]
public class DataEventProcess
{
    public DataEventProcessCurrent dataEventProcessCurrent;
    private EventInMapProcess eventInMapProcessCurrent;
    private string fileName = " ";
    public void InItData()
    {
        SetFileName(nameof(DataEventProcess) +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
        LoadData();
    }
    public void CheckAndLoadRewardMissionComplete(EventName eventName)
    {
        if (eventInMapProcessCurrent == null) { LoadData(); }
        foreach(EventInMap eventInMap in eventInMapProcessCurrent.eventInMaps)
        {
            if (eventInMap.missionProcess.isCompleteMission() && eventInMap.missionProcess.nameMissions.Contains(eventName))
            {
                eventInMap.rewardProcessCompleteMission.OnLoadReward();
            }
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
        string json = JsonUtility.ToJson(dataEventProcessCurrent);
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
        DataEventProcessCurrent dataSave = JsonUtility.FromJson<DataEventProcessCurrent>(json);
        dataEventProcessCurrent = dataSave;
        GetEventInMapProcess_Current();
    }
    public EventInMapProcess GetEventInMapProcess_Current()
    {
        string PathResource = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap +
            "\\EventInMapProcess\\Mission " + dataEventProcessCurrent.LevelCurrent;
        eventInMapProcessCurrent = (EventInMapProcess)Resources.Load(PathResource, typeof(EventInMapProcess));
        return eventInMapProcessCurrent;
    }
    public void NextProcess()
    {
        dataEventProcessCurrent.LevelCurrent++;
        if (GetEventInMapProcess_Current() == null)
        {
            dataEventProcessCurrent.LevelCurrent--;
        }
        SaveData();
        LoadData();
    }
    public void SetDataEventInMapProcessCurrent(int LevelCurrent)
    {
        dataEventProcessCurrent.LevelCurrent = LevelCurrent;
        SaveData();
        LoadData();
    }
    public void ResetData()
    {
        dataEventProcessCurrent.ResetData();
    }

}
[System.Serializable]
public class DataEventProcessCurrent
{
    public int LevelCurrent;
    public void ResetData()
    {
        LevelCurrent = 1;
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
    public void CheckAndLoadRewardMissionComplete(EventName eventName)
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
             + "Mission " + dataApparatusProcessCurrent.LevelCurrent;
        apparatusProcessCurrent = (ApparatusProcess)Resources.Load(PathResource, typeof(ApparatusProcess));
        return apparatusProcessCurrent;
    }
    public void NextProcess()
    {
        dataApparatusProcessCurrent.NextProcess();
        if (GetApparatusProcess_Current() == null)
        {
            //switch (dataApparatusProcessCurrent.ingredientType)
            //{
            //    //case IngredientType.SHEEP:
            //    //    dataApparatusProcessCurrent.ingredientType = IngredientType.COW;
            //    //    break;
            //    case IngredientType.COW:
            //        dataApparatusProcessCurrent.ingredientType = IngredientType.BEAR;
            //        break;
            //    case IngredientType.BEAR:
            //        dataApparatusProcessCurrent.ingredientType = IngredientType.BEAR;
            //        break;
            //    case IngredientType.CHICKEN:
            //        dataApparatusProcessCurrent.ingredientType = IngredientType.COW;
            //        break;
            //}
            dataApparatusProcessCurrent.LevelCurrent -= 1;
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
            case EventName.ChickenBagCloset_OnBuy:
                return Is_OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenBagCloset);
            case EventName.ChickenBagMachine_OnBuy:
                return Is_OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenBagMachine);
            case EventName.ChickenCloset_1_OnBuy:
                return Is_OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenCloset_1);
            case EventName.ChickenCloset_OnBuy:
                return Is_OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenCloset);
            case EventName.ChickenClothMachine_OnBuy:
                return Is_OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenClothMachine);
            case EventName.ChickenHabitat_OnBuy:
                return Is_OnBuy(IngredientType.CHICKEN, NameObject_This.ChickenHabitat);
            case EventName.BuildStage_OnComplete:
                return Is_Complete(IngredientType.BUILDSTAGE, NameObject_This.BuildStage);
            case EventName.BuildStage_1_OnComplete:
                return Is_Complete(IngredientType.BUILDSTAGE, NameObject_This.BuildStage_1);
            case EventName.CheckOutTable_Complete:
                return Is_Complete(IngredientType.CHECKOUT, NameObject_This.CheckOutTable);
            case EventName.CheckOutTable_1_Complete:
                return Is_Complete(IngredientType.CHECKOUT, NameObject_This.CheckOutTable_1);
            case EventName.HireStaff_OnComplete:
                return Is_Complete(IngredientType.HIRESTAFF, NameObject_This.HireStaff);
            case EventName.HireStaff_1_OnComplete:
                return Is_Complete(IngredientType.HIRESTAFF, NameObject_This.HireStaff_1);
            case EventName.HireStaff_2_OnComplete:
                return Is_Complete(IngredientType.HIRESTAFF, NameObject_This.HireStaff_2);
            case EventName.HireStaff_3_OnComplete:
                return Is_Complete(IngredientType.HIRESTAFF, NameObject_This.HireStaff_3);

        }
        return false;
    }
    public bool Is_Complete(IngredientType ingredientType, NameObject_This nameObject_This)
    {

        return BuildController.Instance.GetBuildIngredientController(ingredientType).GetDataStatusObject(nameObject_This).isStatus_Bought();
    }
    public bool Is_OnBuy(IngredientType ingredientType, NameObject_This nameObject_This)
    {
       
        return BuildController.Instance.GetBuildIngredientController(ingredientType).GetDataStatusObject(nameObject_This).isStaus_OnBuy();
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
            case EventName.CheckOutTable_OnBuy:
                OnBuy(IngredientType.CHECKOUT, NameObject_This.CheckOutTable);
                break;
            case EventName.CheckOutTable_1_OnBuy:
                OnBuy(IngredientType.CHECKOUT, NameObject_This.CheckOutTable_1);
                break;
            case EventName.OpenLevelMap_1:
                OpenLevelMap(MiniMapController.TypeLevel.Level_1);
                break;
            case EventName.OpenLevelMap_2:
                OpenLevelMap(MiniMapController.TypeLevel.Level_2);
                break;
            case EventName.OpenLevelMap_3:
                OpenLevelMap(MiniMapController.TypeLevel.Level_3);
                break;
            case EventName.Camera_Follow_BearCloset:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearCloset, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearCloset_1:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearCloset_1, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearHabitat:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearHabitat, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearClothMachine:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearClothMachine, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenCloset:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenCloset, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenBagCloset:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenBagCloset, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenBagMachine:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenBagMachine, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenCloset_1:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenCloset_1, true, 0.5f, 5, 5, 5);
                break;
            case EventName.Camera_Follow_ChickenClothMachine:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenClothMachine, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenHabitat:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenHabitat, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepCloset:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepCloset, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepCloset_1:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepCloset_1, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepClothMachine:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepClothMachine, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepHabitat:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepHabitat, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowCloset:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowCloset, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowCloset_1:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowCloset_1, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowClothMachine:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowClothMachine, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowHabitat:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowHabitat, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_Checkout:
                CameraFollowObject(IngredientType.CHECKOUT, NameObject_This.CheckOutTable, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_Checkout_1:
                CameraFollowObject(IngredientType.CHECKOUT, NameObject_This.CheckOutTable_1, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.CameraFollow_Full_Info_Room_Map_1:
                Transform point_Follow = MapController.Instance.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPointFollow_Farm();
                CameraController.Instance.SetFollowAndLookAt(point_Follow, point_Follow, false, 0.5f, 5, 5, 5
                  , () => 
                  {
                      PlayerStopMove();
                  }, 
                  () => 
                  {
                      point_Follow = MapController.Instance.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPointFollow_Machine();
                      CameraController.Instance.SetFollowAndLookAt(point_Follow, point_Follow, false, 0.5f, 5, 5, 5
                 , () =>
                 {
                     CameraController.Instance.MoveDistance(55, 5);
                 },
                 () =>
                 {
                     point_Follow = MapController.Instance.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPointFollow_Shop();
                     CameraController.Instance.SetFollowAndLookAt(point_Follow, point_Follow, true, 0.5f, 5, 5, 5
                 , () =>
                 {

                 },
                 () =>
                 {
                      CameraController.Instance.MoveDistance(32, 5);
                 });
                 });
                  });
                break;
            case EventName.BuildStage_1_OnBuy:
                OnBuy(IngredientType.BUILDSTAGE, NameObject_This.BuildStage_1);
                break;
            case EventName.BuildStage_OnBuy:
                OnBuy(IngredientType.BUILDSTAGE, NameObject_This.BuildStage);
                break;
            case EventName.HireStaff_OnBuy:
                OnBuy(IngredientType.HIRESTAFF, NameObject_This.HireStaff);
                break;
            case EventName.HireStaff_1_OnBuy:
                OnBuy(IngredientType.HIRESTAFF, NameObject_This.HireStaff_1);
                break;
            case EventName.HireStaff_2_OnBuy:
                OnBuy(IngredientType.HIRESTAFF, NameObject_This.HireStaff_2);
                break;
            case EventName.HireStaff_3_OnBuy:
                OnBuy(IngredientType.HIRESTAFF, NameObject_This.HireStaff_3);
                break;
            case EventName.Camera_Follow_HireStaff:
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff, true, 0.5f, 5, 5, 5
                     , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_HireStaff_1:
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff_1, true, 0.5f, 5, 5, 5
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_HireStaff_2:
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff_2, true, 0.5f, 5, 5, 5
                     , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_HireStaff_3:
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff_3, true, 0.5f, 5, 5, 5
                     , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Car_OnBought:
                OnBought(IngredientType.CAR, NameObject_This.Car);
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
    public void OpenLevelMap(MiniMapController.TypeLevel value)
    {
        MapController.Instance.OpenMap(value);
    }
    public void CameraFollowObject(IngredientType ingredientType, NameObject_This nameObject_This, bool isResetFollowPlayer = false, 
        float timeDelayFollow = 0, float XDamping = 1, float YDamping = 1, float ZDamping = 1, 
        System.Action actionStartFollow = null, System.Action actionEndFollow = null)
    {
        BuildIngredientController buildIngredientController = BuildController.Instance.GetBuildIngredientController(ingredientType);
        CameraController.Instance.SetFollowAndLookAt(buildIngredientController.GetBaseBuild(nameObject_This).myTransform, buildIngredientController.GetBaseBuild(nameObject_This).myTransform, 
            isResetFollowPlayer, timeDelayFollow, XDamping, YDamping, ZDamping, actionStartFollow, actionEndFollow);
    }
    private void PlayerStopMove()
    {
        Player.Instance.isStopMove = true;
        Canvas_Joystick.Instance.isStopJoysick = true;
        EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
    }
    private void PlayerContinueMove()
    {
        Player.Instance.isStopMove = false;
        Canvas_Joystick.Instance.isStopJoysick = false;
    }
}
