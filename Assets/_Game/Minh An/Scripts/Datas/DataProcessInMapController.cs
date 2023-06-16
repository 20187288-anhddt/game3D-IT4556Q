using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataProcessInMapController : DataBase
{
    public DataProcess dataProcess;
    private bool isAddEvent = false;
    private void Start()
    {
        OnInIt();
       
        EnventManager.AddListener(EventName.ClearData.ToString(), ClearData);
        EnventManager.AddListener(EventName.LoadMap_Complete.ToString(), OnInIt);
    }
    private void OnInIt()
    {
        InItData();
        StartCoroutine(IE_DelayAction(() =>
        {
            CheckAndLoadRewardMissionComplete(EventName.ChickenHabitat_OnBuy);
            CheckAndLoadRewardMissionComplete(EventName.CrocHabitat_OnBuy);
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
    public void ClearData()
    {
        dataProcess.GetDataApparatusProcess().ClearData();
        dataProcess.GetDataEventProcess().ClearData();
        SaveData();
        LoadData();
    }
    public void AddEvent()
    {
        if (isAddEvent)
        {
            return;
        }
        isAddEvent = true;
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
        EnventManager.AddListener(EventName.BuildStage_2_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.BuildStage_2_OnComplete); });
        #endregion
        #region CheckOut
        EnventManager.AddListener(EventName.CheckOutTable_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CheckOutTable_Complete); });
        EnventManager.AddListener(EventName.CheckOutTable_1_Complete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CheckOutTable_1_Complete); });
        #endregion
        #region Car
        EnventManager.AddListener(EventName.Camera_Follow_PosCar.ToString(), 
        ()
        =>
        {
            BuildController buildController = null;
            switch (GameManager.Instance.curLevel)
            {
                case 0:
                    buildController = GameManager.Instance.GetBuildController(NameMap.Map_1);
                    break;
                case 1:
                    buildController = GameManager.Instance.GetBuildController(NameMap.Map_2);
                    break;
            }
            BuildIngredientController buildIngredientController = buildController.GetBuildIngredientController(IngredientType.CAR);
            CameraController.Instance.SetFollowAndLookAt(buildIngredientController.GetBaseBuild(NameObject_This.Car).myTransform, buildIngredientController.GetBaseBuild(NameObject_This.Car).myTransform,
                true, 0.5f, 3.8f, 2.5f, 2.5f, 2.5f, () => {
                    Player.Instance.isStopMove = true;
                    Canvas_Joystick.Instance.isStopJoysick = true;
                    EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
                }, () => {
                    Player.Instance.isStopMove = false;
                    Canvas_Joystick.Instance.isStopJoysick = false;
                });
        });
        #endregion
        #region TuT
        EnventManager.AddListener(EventName.DoneAllTuT.ToString(), () =>
        {
            DataManager.Instance.GetDataUIController().Set_IsOpenCanvasHome(true);
            DataManager.Instance.GetDataUIController().Set_IsOpenCanvasBonus(true);
            EventBounsController.Instance.GetDataBonus().Set_OnShowBouns(true);
        });
        #endregion
        #region Croc
        EnventManager.AddListener(EventName.CrocCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CrocCloset_OnComplete); });
        EnventManager.AddListener(EventName.CrocClothMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CrocClothMachine_OnComplete); });
        EnventManager.AddListener(EventName.CrocBagMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CrocBagMachine_OnComplete); });
        EnventManager.AddListener(EventName.CrocHabitat_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CrocHabitat_OnComplete); });
        EnventManager.AddListener(EventName.CrocBagCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.CrocBagCloset_OnComplete); });
        #endregion
        #region Ele
        EnventManager.AddListener(EventName.EleCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.EleCloset_OnComplete); });
        EnventManager.AddListener(EventName.EleClothMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.EleClothMachine_OnComplete); });
        EnventManager.AddListener(EventName.EleBagMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.EleBagMachine_OnComplete); });
        EnventManager.AddListener(EventName.EleHabitat_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.EleHabitat_OnComplete); });
        EnventManager.AddListener(EventName.EleBagCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.EleBagCloset_OnComplete); });
        #endregion
        #region Lion
        EnventManager.AddListener(EventName.LionCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.LionCloset_OnComplete); });
        EnventManager.AddListener(EventName.LionClothMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.LionClothMachine_OnComplete); });
        EnventManager.AddListener(EventName.LionBagMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.LionBagMachine_OnComplete); });
        EnventManager.AddListener(EventName.LionHabitat_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.LionHabitat_OnComplete); });
        EnventManager.AddListener(EventName.LionBagCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.LionBagCloset_OnComplete); });
        #endregion
        #region Zebra
        EnventManager.AddListener(EventName.ZebraCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ZebraCloset_OnComplete); });
        EnventManager.AddListener(EventName.ZebraClothMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ZebraClothMachine_OnComplete); });
        EnventManager.AddListener(EventName.ZebraBagMachine_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ZebraBagMachine_OnComplete); });
        EnventManager.AddListener(EventName.ZebraHabitat_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ZebraHabitat_OnComplete); });
        EnventManager.AddListener(EventName.ZebraBagCloset_OnComplete.ToString(), () => { CheckAndLoadRewardMissionComplete(EventName.ZebraBagCloset_OnComplete); });
        #endregion
        #region Tutorial
        EnventManager.AddListener(EventName.CompleteTut_Upgrade.ToString(),
            () => 
            {
                BuildController buildController = null;
                switch (GameManager.Instance.curLevel)
                {
                    case 0:
                        buildController = GameManager.Instance.GetBuildController(NameMap.Map_1);
                        break;
                    case 1:
                        buildController = GameManager.Instance.GetBuildController(NameMap.Map_2);
                        break;
                }
                BuildIngredientController buildIngredientController = buildController.GetBuildIngredientController(IngredientType.CHICKEN);
                CameraController.Instance.SetFollowAndLookAt(buildIngredientController.GetBaseBuild(NameObject_This.ChickenBagCloset).myTransform, buildIngredientController.GetBaseBuild(NameObject_This.ChickenBagCloset).myTransform,
                    true, 0.5f, 3.8f, 2.5f, 2.5f, 2.5f, () => {
                        Player.Instance.isStopMove = true;
                        Canvas_Joystick.Instance.isStopJoysick = true;
                        EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
                    }, () => {
                        Player.Instance.isStopMove = false;
                        Canvas_Joystick.Instance.isStopJoysick = false;
                    });
            });
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
    private bool isInItData;
    public void CheckInInData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public void InItData()
    {
        SetFileName(nameof(DataEventProcess) +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
        LoadData();
        isInItData = true;
    }
    public void CheckAndLoadRewardMissionComplete(EventName eventName)
    {
        CheckInInData();
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
    public void ClearData()
    {
        CheckInInData();
        ResetData();
        SaveData();
        LoadData();
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
    public ApparatusProcess apparatusProcessCurrent;
    private string fileName = " ";
    private bool isInItData;
    public void CheckInInData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public void InItData()
    {
        SetFileName(nameof(DataProcessInMapController) +
            "_Map " + DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelCurrent().ToString());
       // Debug.Log(GetFileName());
        LoadData();
        isInItData = true;
    }
    public void CheckAndLoadRewardMissionComplete(EventName eventName)
    {
        CheckInInData();
        if (apparatusProcessCurrent == null) { InItData(); }
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
      //  CheckInInData();
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
        //CheckInInData();
        string PathResource = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\ApparatusProcess\\"
             + "Mission " + dataApparatusProcessCurrent.LevelCurrent;
        apparatusProcessCurrent = (ApparatusProcess)Resources.Load(PathResource, typeof(ApparatusProcess));
        return apparatusProcessCurrent;
    }
    public void NextProcess()
    {
        CheckInInData();
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
        CheckInInData();
        dataApparatusProcessCurrent.ingredientType = ingredientType;
        dataApparatusProcessCurrent.LevelCurrent = LevelCurrent;
        SaveData();
        LoadData();
    }
    public void ResetData()
    {
        dataApparatusProcessCurrent.ResetData();
    }
    public void ClearData()
    {
        CheckInInData();
        ResetData();
        SaveData();
        LoadData();
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
        switch(DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap - 1)
        {
            case 0:
                ingredientType = IngredientType.CHICKEN;
                break;
            case 1:
                ingredientType = IngredientType.ZEBRA;
                break;
        }
      
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
            #region Map 1
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
            case EventName.BuildStage_2_OnComplete:
                return Is_Complete(IngredientType.BUILDSTAGE, NameObject_This.BuildStage_2);
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
            case EventName.HireStaff_4_OnComplete:
                return Is_Complete(IngredientType.HIRESTAFF, NameObject_This.HireStaff_4);
            case EventName.HireStaff_5_OnComplete:
                return Is_Complete(IngredientType.HIRESTAFF, NameObject_This.HireStaff_5);
            case EventName.NextLevel_2_Complete:
                return Is_Complete(IngredientType.NEXTLEVEL, NameObject_This.NextLevel_2);
            case EventName.NextLevel_3_Complete:
                return Is_Complete(IngredientType.NEXTLEVEL, NameObject_This.NextLevel_3);
            case EventName.NextLevel_4_Complete:
                return Is_Complete(IngredientType.NEXTLEVEL, NameObject_This.NextLevel_4);
            #endregion
            #region Map 2
            case EventName.CrocHabitat_OnBuy:
                return Is_OnBuy(IngredientType.CROC, NameObject_This.CrocHabitat);
            case EventName.CrocHabitat_OnComplete:
                return Is_Complete(IngredientType.CROC, NameObject_This.CrocHabitat);
            case EventName.EleHabitat_OnBuy:
                return Is_OnBuy(IngredientType.ELE, NameObject_This.EleHabitat);
            case EventName.EleHabitat_OnComplete:
                return Is_Complete(IngredientType.ELE, NameObject_This.EleHabitat);
            case EventName.LionHabitat_OnBuy:
                return Is_OnBuy(IngredientType.LION, NameObject_This.LionHabitat);
            case EventName.LionHabitat_OnComplete:
                return Is_Complete(IngredientType.LION, NameObject_This.LionHabitat);
            case EventName.ZebraHabitat_OnBuy:
                return Is_OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraHabitat);
            case EventName.ZebraHabitat_OnComplete:
                return Is_Complete(IngredientType.ZEBRA, NameObject_This.ZebraHabitat);
            case EventName.CrocClothMachine_OnBuy:
                return Is_OnBuy(IngredientType.CROC, NameObject_This.CrocClothMachine);
            case EventName.CrocClothMachine_OnComplete:
                return Is_Complete(IngredientType.CROC, NameObject_This.CrocClothMachine);
            case EventName.EleClothMachine_OnBuy:
                return Is_OnBuy(IngredientType.ELE, NameObject_This.EleClothMachine);
            case EventName.EleClothMachine_OnComplete:
                return Is_Complete(IngredientType.ELE, NameObject_This.EleClothMachine);
            case EventName.LionClothMachine_OnBuy:
                return Is_OnBuy(IngredientType.LION, NameObject_This.LionClothMachine);
            case EventName.LionClothMachine_OnComplete:
                return Is_Complete(IngredientType.LION, NameObject_This.LionClothMachine);
            case EventName.ZebraClothMachine_OnBuy:
                return Is_OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraClothMachine);
            case EventName.ZebraClothMachine_OnComplete:
                return Is_Complete(IngredientType.ZEBRA, NameObject_This.ZebraClothMachine);
            case EventName.CrocBagMachine_OnBuy:
                return Is_OnBuy(IngredientType.CROC, NameObject_This.CrocBagMachine);
            case EventName.CrocBagMachine_OnComplete:
                return Is_Complete(IngredientType.CROC, NameObject_This.CrocBagMachine);
            case EventName.EleBagMachine_OnBuy:
                return Is_OnBuy(IngredientType.ELE, NameObject_This.EleBagMachine);
            case EventName.EleBagMachine_OnComplete:
                return Is_Complete(IngredientType.ELE, NameObject_This.EleBagMachine);
            case EventName.LionBagMachine_OnBuy:
                return Is_OnBuy(IngredientType.LION, NameObject_This.LionBagMachine);
            case EventName.LionBagMachine_OnComplete:
                return Is_Complete(IngredientType.LION, NameObject_This.LionBagMachine);
            case EventName.ZebraBagMachine_OnBuy:
                return Is_OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraBagMachine);
            case EventName.ZebraBagMachine_OnComplete:
                return Is_Complete(IngredientType.ZEBRA, NameObject_This.ZebraBagMachine);
            case EventName.CrocCloset_OnBuy:
                return Is_OnBuy(IngredientType.CROC, NameObject_This.CrocCloset);
            case EventName.CrocCloset_OnComplete:
                return Is_Complete(IngredientType.CROC, NameObject_This.CrocCloset);
            case EventName.EleCloset_OnBuy:
                return Is_OnBuy(IngredientType.ELE, NameObject_This.EleCloset);
            case EventName.EleCloset_OnComplete:
                return Is_Complete(IngredientType.ELE, NameObject_This.EleCloset);
            case EventName.LionCloset_OnBuy:
                return Is_OnBuy(IngredientType.LION, NameObject_This.LionCloset);
            case EventName.LionCloset_OnComplete:
                return Is_Complete(IngredientType.LION, NameObject_This.LionCloset);
            case EventName.ZebraCloset_OnBuy:
                return Is_OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraCloset);
            case EventName.ZebraCloset_OnComplete:
                return Is_Complete(IngredientType.ZEBRA, NameObject_This.ZebraCloset);
            case EventName.CrocBagCloset_OnBuy:
                return Is_OnBuy(IngredientType.CROC, NameObject_This.CrocBagCloset);
            case EventName.CrocBagCloset_OnComplete:
                return Is_Complete(IngredientType.CROC, NameObject_This.CrocBagCloset);
            case EventName.EleBagCloset_OnBuy:
                return Is_OnBuy(IngredientType.ELE, NameObject_This.EleBagCloset);
            case EventName.EleBagCloset_OnComplete:
                return Is_Complete(IngredientType.ELE, NameObject_This.EleBagCloset);
            case EventName.LionBagCloset_OnBuy:
                return Is_OnBuy(IngredientType.LION, NameObject_This.LionBagCloset);
            case EventName.LionBagCloset_OnComplete:
                return Is_Complete(IngredientType.LION, NameObject_This.LionBagCloset);
            case EventName.ZebraBagCloset_OnBuy:
                return Is_OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraBagCloset);
            case EventName.ZebraBagCloset_OnComplete:
                return Is_Complete(IngredientType.ZEBRA, NameObject_This.ZebraBagCloset);
            case EventName.HireAnimal_Croc_OnBuy:
                return Is_OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Croc);
            case EventName.HireAnimal_Croc_OnComplete:
                return Is_Complete(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Croc);
            case EventName.HireAnimal_Ele_OnBuy:
                return Is_OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Ele);
            case EventName.HireAnimal_Ele_OnComplete:
                return Is_Complete(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Ele);
            case EventName.HireAnimal_Lion_OnBuy:
                return Is_OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Lion);
            case EventName.HireAnimal_Lion_OnComplete:
                return Is_Complete(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Lion);
            case EventName.HireAnimal_Zebra_OnBuy:
                return Is_OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Zebra);
            case EventName.HireAnimal_Zebra_OnComplete:
                return Is_Complete(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Zebra);
                #endregion
        }
        return false;
    }
    public bool Is_Complete(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        BuildController buildController = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_1);
                break;
            case 1:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_2);
                break;
        }
        return buildController.GetBuildIngredientController(ingredientType).GetDataStatusObject(nameObject_This).isStatus_Bought();
    }
    public bool Is_OnBuy(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        BuildController buildController = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_1);
                break;
            case 1:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_2);
                break;
        }
        return buildController.GetBuildIngredientController(ingredientType).GetDataStatusObject(nameObject_This).isStaus_OnBuy();
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
            #region Map 1
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
            case EventName.OpenLevelMap_4:
                OpenLevelMap(MiniMapController.TypeLevel.Level_4);
                break;
            case EventName.Camera_Follow_BearCloset:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearCloset_1:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearCloset_1, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearHabitat:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearClothMachine:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenCloset:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenBagCloset:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenBagCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenBagMachine:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenBagMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenCloset_1:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenCloset_1, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f);
                break;
            case EventName.Camera_Follow_ChickenClothMachine:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ChickenHabitat:
                CameraFollowObject(IngredientType.CHICKEN, NameObject_This.ChickenHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepCloset:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepCloset_1:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepCloset_1, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepClothMachine:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_SheepHabitat:
                CameraFollowObject(IngredientType.SHEEP, NameObject_This.SheepHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowCloset:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowCloset_1:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowCloset_1, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowClothMachine:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowHabitat:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_Checkout:
                CameraFollowObject(IngredientType.CHECKOUT, NameObject_This.CheckOutTable, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_Checkout_1:
                CameraFollowObject(IngredientType.CHECKOUT, NameObject_This.CheckOutTable_1, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.CameraFollow_Full_Info_Room_Map_1:
                MapController mapController = null;
                switch (GameManager.Instance.curLevel)
                {
                    case 0:
                        mapController = GameManager.Instance.GetMapController(NameMap.Map_1);
                        break;
                    case 1:
                        mapController = GameManager.Instance.GetMapController(NameMap.Map_2);
                        break;
                }
                Transform point_Follow = mapController.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPointFollow_Farm();
                CameraController.Instance.SetFollowAndLookAt(point_Follow, point_Follow, false, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                  , () => 
                  {
                      PlayerStopMove();
                  }, 
                  () => 
                  {
                      point_Follow = mapController.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPointFollow_Machine();
                      CameraController.Instance.SetFollowAndLookAt(point_Follow, point_Follow, false, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                 , () =>
                 {
                     //int levelMap = 0;
                     //switch (DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent())
                     //{
                     //    case MiniMapController.TypeLevel.Level_1:
                     //        levelMap = 1;
                     //        break;
                     //    case MiniMapController.TypeLevel.Level_2:
                     //        levelMap = 2;
                     //        break;
                     //    case MiniMapController.TypeLevel.Level_3:
                     //        levelMap = 3;
                     //        break;
                     //}
                     //CameraController.Instance.MoveDistance(30 + levelMap * 5, 5);
                 },
                 () =>
                 {
                     point_Follow = mapController.GetMiniMapController(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent()).GetPointFollow_Shop();
                     CameraController.Instance.SetFollowAndLookAt(point_Follow, point_Follow, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                 , () =>
                 {

                 },
                 () =>
                 {
                     //int levelMap = 0;
                     //switch (DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent())
                     //{
                     //    case MiniMapController.TypeLevel.Level_1:
                     //        levelMap = 1;
                     //        break;
                     //    case MiniMapController.TypeLevel.Level_2:
                     //        levelMap = 2;
                     //        break;
                     //    case MiniMapController.TypeLevel.Level_3:
                     //        levelMap = 3;
                     //        break;
                     //}
                     //CameraController.Instance.MoveDistance(30 + levelMap * 5, 5);
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
            case EventName.BuildStage_2_OnBuy:
                OnBuy(IngredientType.BUILDSTAGE, NameObject_This.BuildStage_2);
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
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                     , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_HireStaff_1:
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff_1, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_HireStaff_2:
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff_2, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                     , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_HireStaff_3:
                CameraFollowObject(IngredientType.HIRESTAFF, NameObject_This.HireStaff_3, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                     , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Car_OnBought:
                OnBought(IngredientType.CAR, NameObject_This.Car);
                break;
            case EventName.Camera_Follow_PosCar:
                CameraFollowObject(IngredientType.CAR, NameObject_This.Car, true, 0.5f, 3.8f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.OpenUIHome:
                DataManager.Instance.GetDataUIController().Set_IsOpenCanvasHome(true);
                break;
            case EventName.Show_BtnUpgrade:
                DataManager.Instance.GetDataUIController().Set_IsOpenUIUpgrade(true);
                break;
            case EventName.HireAnimal_Chicken_OnBuy:
                OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Chicken);
                break;
            case EventName.HireAnimal_Cow_OnBuy:
                OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Cow);
                break;
            case EventName.HireAnimal_Bear_OnBuy:
                OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Bear);
                break;
            case EventName.OnEventBonus:
                DataManager.Instance.GetDataUIController().Set_IsOpenCanvasBonus(true);
                EventBounsController.Instance.GetDataBonus().Set_OnShowBouns(true);
                break;
            case EventName.NextDataProcessBonusMoneyBuff:
                DataManager.Instance.GetData_Bonus_BuffMoney().NextProcessMoneyBuff();
                break;
            case EventName.OnBonus_NoShit:
                (EventBounsController.Instance.GetUIBonus(TypeBonus.NoShit) as UI_Bonus_NoShit).SetDataOnBonus(true);
                break;
            case EventName.Camera_Follow_CowBagMachine:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowBagMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CowBagCloset:
                CameraFollowObject(IngredientType.COW, NameObject_This.CowBagCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                   , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearBagMachine:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearBagMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                  , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_BearBagCloset:
                CameraFollowObject(IngredientType.BEAR, NameObject_This.BearBagCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                  , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.HireStaff_4_OnBuy:
                OnBuy(IngredientType.HIRESTAFF, NameObject_This.HireStaff_4);
                break;
            case EventName.HireStaff_5_OnBuy:
                OnBuy(IngredientType.HIRESTAFF, NameObject_This.HireStaff_5);
                break;
            case EventName.NextLevel_2_OnBuy:
                OnBuy(IngredientType.NEXTLEVEL, NameObject_This.NextLevel_2);
                break;
            case EventName.NextLevel_3_OnBuy:
                OnBuy(IngredientType.NEXTLEVEL, NameObject_This.NextLevel_3);
                break;
            case EventName.Call_Car_Mission:
                (LoadMapController.Instance.GetMap_().GetBuildController().
                    GetBuildIngredientController(IngredientType.CAR).
                    GetBaseBuild(NameObject_This.Car) as CarMission).CallCar();
                //CarMission.Instance?.CallCar();
                break;
            #endregion
            #region Map 2
            case EventName.CrocHabitat_OnBuy:
                OnBuy(IngredientType.CROC, NameObject_This.CrocHabitat);
                break;
            case EventName.EleHabitat_OnBuy:
                OnBuy(IngredientType.ELE, NameObject_This.EleHabitat);
                break;
            case EventName.LionHabitat_OnBuy:
                OnBuy(IngredientType.LION, NameObject_This.LionHabitat);
                break;
            case EventName.ZebraHabitat_OnBuy:
                OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraHabitat);
                break;
            case EventName.CrocClothMachine_OnBuy:
                OnBuy(IngredientType.CROC, NameObject_This.CrocClothMachine);
                break;
            case EventName.LionClothMachine_OnBuy:
                OnBuy(IngredientType.LION, NameObject_This.LionClothMachine);
                break;
            case EventName.EleClothMachine_OnBuy:
                OnBuy(IngredientType.ELE, NameObject_This.EleClothMachine);
                break;
            case EventName.ZebraClothMachine_OnBuy:
                OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraClothMachine);
                break;
            case EventName.CrocBagMachine_OnBuy:
                OnBuy(IngredientType.CROC, NameObject_This.CrocBagMachine);
                break;
            case EventName.EleBagMachine_OnBuy:
                OnBuy(IngredientType.ELE, NameObject_This.EleBagMachine);
                break;
            case EventName.LionBagMachine_OnBuy:
                OnBuy(IngredientType.LION, NameObject_This.LionBagMachine);
                break;
            case EventName.ZebraBagMachine_OnBuy:
                OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraBagMachine);
                break;
            case EventName.CrocCloset_OnBuy:
                OnBuy(IngredientType.CROC, NameObject_This.CrocCloset);
                break;
            case EventName.EleCloset_OnBuy:
                OnBuy(IngredientType.ELE, NameObject_This.EleCloset);
                break;
            case EventName.LionCloset_OnBuy:
                OnBuy(IngredientType.LION, NameObject_This.LionCloset);
                break;
            case EventName.ZebraCloset_OnBuy:
                OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraCloset);
                break;
            case EventName.CrocBagCloset_OnBuy:
                OnBuy(IngredientType.CROC, NameObject_This.CrocBagCloset);
                break;
            case EventName.EleBagCloset_OnBuy:
                OnBuy(IngredientType.ELE, NameObject_This.EleBagCloset);
                break;
            case EventName.LionBagCloset_OnBuy:
                OnBuy(IngredientType.LION, NameObject_This.LionBagCloset);
                break;
            case EventName.ZebraBagCloset_OnBuy:
                OnBuy(IngredientType.ZEBRA, NameObject_This.ZebraBagCloset);
                break;
            case EventName.HireAnimal_Croc_OnBuy:
                OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Croc);
                break;
            case EventName.HireAnimal_Ele_OnBuy:
                OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Ele);
                break;
            case EventName.HireAnimal_Lion_OnBuy:
                OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Lion);
                break;
            case EventName.HireAnimal_Zebra_OnBuy:
                OnBuy(IngredientType.HIRE_ANIMAL, NameObject_This.HireAnimal_Zebra);
                break;
            case EventName.Camera_Follow_EleHabitat:
                CameraFollowObject(IngredientType.ELE, NameObject_This.EleHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_EleClothMachine:
                CameraFollowObject(IngredientType.ELE, NameObject_This.EleClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_EleCloset:
                CameraFollowObject(IngredientType.ELE, NameObject_This.EleCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_LionHabitat:
                CameraFollowObject(IngredientType.LION, NameObject_This.LionHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_LionClothMachine:
                CameraFollowObject(IngredientType.LION, NameObject_This.LionClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_LionCloset:
                CameraFollowObject(IngredientType.LION, NameObject_This.LionCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ZebraHabitat:
                CameraFollowObject(IngredientType.ZEBRA, NameObject_This.ZebraHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ZebraClothMachine:
                CameraFollowObject(IngredientType.ZEBRA, NameObject_This.ZebraClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ZebraCloset:
                CameraFollowObject(IngredientType.ZEBRA, NameObject_This.ZebraCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CrocHabitat:
                CameraFollowObject(IngredientType.CROC, NameObject_This.CrocHabitat, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CrocClothMachine:
                CameraFollowObject(IngredientType.CROC, NameObject_This.CrocClothMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CrocCloset:
                CameraFollowObject(IngredientType.CROC, NameObject_This.CrocCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_EleBagMachine:
                CameraFollowObject(IngredientType.ELE, NameObject_This.EleBagMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_EleBagCloset:
                CameraFollowObject(IngredientType.ELE, NameObject_This.EleBagCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_LionBagMachine:
                CameraFollowObject(IngredientType.LION, NameObject_This.LionBagMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_LionBagCloset:
                CameraFollowObject(IngredientType.LION, NameObject_This.LionBagCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ZebraBagMachine:
                CameraFollowObject(IngredientType.ZEBRA, NameObject_This.ZebraBagMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_ZebraBagCloset:
                CameraFollowObject(IngredientType.ZEBRA, NameObject_This.ZebraBagCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CrocBagMachine:
                CameraFollowObject(IngredientType.CROC, NameObject_This.CrocBagMachine, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
            case EventName.Camera_Follow_CrocBagCloset:
                CameraFollowObject(IngredientType.CROC, NameObject_This.CrocBagCloset, true, 0.5f, 2.5f, 2.5f, 2.5f, 2.5f
                    , PlayerStopMove, PlayerContinueMove);
                break;
                #endregion
        }
    }

    public void OnBuy(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        BuildController buildController = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_1);
                break;
            case 1:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_2);
                break;
        }
        buildController.GetBuildIngredientController(ingredientType).OnBuy(nameObject_This);
    }
    public void OnBought(IngredientType ingredientType, NameObject_This nameObject_This)
    {
        BuildController buildController = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_1);
                break;
            case 1:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_2);
                break;
        }
        buildController.GetBuildIngredientController(ingredientType).OnBought(nameObject_This);
    }
    public void OpenLevelMap(MiniMapController.TypeLevel value)
    {
        MapController mapController = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                mapController = GameManager.Instance.GetMapController(NameMap.Map_1);
                break;
            case 1:
                mapController = GameManager.Instance.GetMapController(NameMap.Map_2);
                break;
        }
        mapController.OpenMap(value);
    }
    public void CameraFollowObject(IngredientType ingredientType, NameObject_This nameObject_This, bool isResetFollowPlayer = false, 
        float timeDelayFollow = 0, float timeDelayResetFollowPlayer = 2.5f, float XDamping = 1, float YDamping = 1, float ZDamping = 1, 
        System.Action actionStartFollow = null, System.Action actionEndFollow = null)
    {
        BuildController buildController = null;
        switch (GameManager.Instance.curLevel)
        {
            case 0:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_1);
                break;
            case 1:
                buildController = GameManager.Instance.GetBuildController(NameMap.Map_2);
                break;
        }
        BuildIngredientController buildIngredientController = buildController.GetBuildIngredientController(ingredientType);
        CameraController.Instance.SetFollowAndLookAt(buildIngredientController.GetBaseBuild(nameObject_This).myTransform, buildIngredientController.GetBaseBuild(nameObject_This).myTransform, 
            isResetFollowPlayer, timeDelayFollow, timeDelayResetFollowPlayer, XDamping, YDamping, ZDamping, actionStartFollow, actionEndFollow);
    }
    private void PlayerStopMove()
    {
        Player.Instance.PlayerStopMove();
    }
    private void PlayerContinueMove()
    {
        Player.Instance.PlayerContinueMove();
    }
}
