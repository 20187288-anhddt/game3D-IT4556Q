using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum EventName {
    PlayJoystick,
    StopJoyStick,

    #region Map 1
    CowHabitat_Complete,
    CowClothMachine_Complete,
    CowCloset_Complete,
    CowCloset_1_Complete,

    SheepHabitat_Complete,
    SheepClothMachine_Complete,
    SheepCloset_Complete,
    SheepCloset_1_Complete,

    ChickenHabitat_Complete,
    ChickenClothMachine_Complete,
    ChickenCloset_Complete,
    ChickenCloset_1_Complete,

    BearHabitat_Complete,
    BearClothMachine_Complete,
    BearCloset_Complete,
    BearCloset_1_Complete,

    CowHabitat_OnBuy,
    CowClothMachine_OnBuy,
    CowCloset_OnBuy,
    CowCloset_1_OnBuy,

    SheepHabitat_OnBuy,
    SheepClothMachine_OnBuy,
    SheepCloset_OnBuy,
    SheepCloset_1_OnBuy,

    ChickenHabitat_OnBuy,
    ChickenClothMachine_OnBuy,
    ChickenCloset_OnBuy,
    ChickenCloset_1_OnBuy,

    BearHabitat_OnBuy,
    BearClothMachine_OnBuy,
    BearCloset_OnBuy,
    BearCloset_1_OnBuy,

    BearBagMachine_OnBuy,
    CowBagMachine_OnBuy,
    SheepBagMachine_OnBuy,
    ChickenBagMachine_OnBuy,

    CowBagCloset_OnBuy,
    SheepBagCloset_OnBuy,
    ChickenBagCloset_OnBuy,
    BearBagCloset_OnBuy,

    CheckOutTable_OnBuy,
    CheckOutTable_1_OnBuy,

    BearBagMachine_Complete,
    CowBagMachine_Complete,
    SheepBagMachine_Complete,
    ChickenBagMachine_Complete,

    CowBagCloset_Complete,
    SheepBagCloset_Complete,
    ChickenBagCloset_Complete,
    BearBagCloset_Complete,

    CheckOutTable_Complete,
    CheckOutTable_1_Complete,

    StatusData_OnLoad,

    CheckOutTable_OnBought,
    CheckOutTable_1_OnBought,

    ReLoadMoney,

    OpenLevelMap_1,
    OpenLevelMap_2,
    OpenLevelMap_3,

    ReLoadNavMesh,

    Camera_Follow_CowHabitat,
    Camera_Follow_CowClothMachine,
    Camera_Follow_CowCloset,
    Camera_Follow_CowCloset_1,

    Camera_Follow_SheepHabitat,
    Camera_Follow_SheepClothMachine,
    Camera_Follow_SheepCloset,
    Camera_Follow_SheepCloset_1,

    Camera_Follow_ChickenHabitat,
    Camera_Follow_ChickenClothMachine,
    Camera_Follow_ChickenCloset,
    Camera_Follow_ChickenCloset_1,

    Camera_Follow_BearHabitat,
    Camera_Follow_BearClothMachine,
    Camera_Follow_BearCloset,
    Camera_Follow_BearCloset_1,

    CameraFollow_Full_Info_Room_Map_1,
    Camera_Follow_ChickenBagMachine,
    Camera_Follow_ChickenBagCloset,

    BuildStage_OnBuy,
    BuildStage_OnComplete,
    BuildStage_1_OnBuy,
    BuildStage_1_OnComplete,

    Camera_Follow_Checkout,
    Camera_Follow_Checkout_1,

    HireStaff_OnBuy,
    HireStaff_1_OnBuy,
    HireStaff_2_OnBuy,
    HireStaff_3_OnBuy,

    HireStaff_OnComplete,
    HireStaff_1_OnComplete,
    HireStaff_2_OnComplete,
    HireStaff_3_OnComplete,

    Camera_Follow_HireStaff,
    Camera_Follow_HireStaff_1,
    Camera_Follow_HireStaff_2,
    Camera_Follow_HireStaff_3,
    Car_OnBought,
    Camera_Follow_PosCar,
    ReLoadDistanceCamera,
    ReLoadDataUpgrade,
    NewID_Customize,
    OpenUIHome,
    Show_BtnUpgrade,
    QuitGame,

    HireAnimal_Chicken_OnBuy,
    HireAnimal_Cow_OnBuy,
    HireAnimal_Bear_OnBuy,

    ReLoadUpgrade,
    Machine_Double_Speed_Play,
    Machine_Double_Speed_Stop,
    OnEventBonus,
    OnEventDoubleMoney,
    OffEventDoubleMoney,
    Player_Double_Speed_Play,
    Player_Double_Speed_Stop,
    NextDataProcessBonusMoneyBuff,
    NoShit_Play,
    NoShit_Stop,
    OnBonus_NoShit,
    CheckTickInCustomize,
    OpenUIBonus,
    DoneAllTuT,
    ClearData,

    Camera_Follow_CowBagMachine,
    Camera_Follow_CowBagCloset,
    Camera_Follow_BearBagMachine,
    Camera_Follow_BearBagCloset,

    HireStaff_4_OnBuy,
    HireStaff_5_OnBuy,
    HireStaff_4_OnComplete,
    HireStaff_5_OnComplete,
    #endregion
    NextLevel_2_OnBuy,
    NextLevel_2_Complete,
    NextLevel_3_OnBuy,
    NextLevel_3_Complete,

    Call_Car_Mission,
    #region Map 2
    CrocHabitat_OnBuy,
    CrocHabitat_OnComplete,

    EleHabitat_OnBuy,
    EleHabitat_OnComplete,

    LionHabitat_OnBuy,
    LionHabitat_OnComplete,

    ZebraHabitat_OnBuy,
    ZebraHabitat_OnComplete,

    CrocClothMachine_OnBuy,
    CrocClothMachine_OnComplete,

    EleClothMachine_OnBuy,
    EleClothMachine_OnComplete,

    LionClothMachine_OnBuy,
    LionClothMachine_OnComplete,

    ZebraClothMachine_OnBuy,
    ZebraClothMachine_OnComplete,

    CrocBagMachine_OnBuy,
    CrocBagMachine_OnComplete,

    EleBagMachine_OnBuy,
    EleBagMachine_OnComplete,

    LionBagMachine_OnBuy,
    LionBagMachine_OnComplete,

    ZebraBagMachine_OnBuy,
    ZebraBagMachine_OnComplete,

    CrocCloset_OnBuy,
    CrocCloset_OnComplete,

    EleCloset_OnBuy,
    EleCloset_OnComplete,

    LionCloset_OnBuy,
    LionCloset_OnComplete,

    ZebraCloset_OnBuy,
    ZebraCloset_OnComplete,

    CrocBagCloset_OnBuy,
    CrocBagCloset_OnComplete,

    EleBagCloset_OnBuy,
    EleBagCloset_OnComplete,

    LionBagCloset_OnBuy,
    LionBagCloset_OnComplete,

    ZebraBagCloset_OnBuy,
    ZebraBagCloset_OnComplete,

    HireAnimal_Croc_OnBuy,
    HireAnimal_Croc_OnComplete,

    HireAnimal_Ele_OnBuy,
    HireAnimal_Ele_OnComplete,

    HireAnimal_Lion_OnBuy,
    HireAnimal_Lion_OnComplete,

    HireAnimal_Zebra_OnBuy,
    HireAnimal_Zebra_OnComplete,
    #endregion
    ReLoadBonusMoneyBuff,
    Open_Canvas_Tutorial,
    Close_Canvas_Tutorial,
    On_NextMap,
    On_BackMap,
    Complete_NextMap,
    Complete_BackMap,
    LoadMap_Complete,
    UpdateDataCountUpgradeCurrent,

    CompleteTut_Upgrade,
    NextLevel_4_Complete,
    BuildStage_2_OnComplete,
    OpenLevelMap_4,
    BuildStage_2_OnBuy,

    Camera_Follow_EleHabitat,
    Camera_Follow_EleClothMachine,
    Camera_Follow_EleCloset,
    Camera_Follow_EleCloset_1,

    Camera_Follow_LionHabitat,
    Camera_Follow_LionClothMachine,
    Camera_Follow_LionCloset,
    Camera_Follow_LionCloset_1,

    Camera_Follow_ZebraHabitat,
    Camera_Follow_ZebraClothMachine,
    Camera_Follow_ZebraCloset,
    Camera_Follow_ZebraCloset_1,

    Camera_Follow_CrocHabitat,
    Camera_Follow_CrocClothMachine,
    Camera_Follow_CrocCloset,
    Camera_Follow_CrocCloset_1,

    Camera_Follow_EleBagMachine,
    Camera_Follow_EleBagCloset,

    Camera_Follow_LionBagMachine,
    Camera_Follow_LionBagCloset,

    Camera_Follow_ZebraBagMachine,
    Camera_Follow_ZebraBagCloset,

    Camera_Follow_CrocBagMachine,
    Camera_Follow_CrocBagCloset,
}
public class EnventManager : GenerticSingleton<EnventManager>
{
    //public static EnventManager instance;
    //private void Awake()
    //{
    //    if (instance != null)
    //        Destroy(gameObject);
    //    else instance = this;
    //}

    private Dictionary<string, UnityEvent> eventDic = new Dictionary<string, UnityEvent>();

    public static void AddListener(string eventName, UnityAction action) {
        UnityEvent thisEvent = null;
        if (Instance.eventDic.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(action);
            Instance.eventDic.Add(eventName, thisEvent);
        }
    }
    public static void TriggerEvent(string name)
    {
        UnityEvent thisEvent = null;
        if (Instance)
        {
            if (Instance.eventDic.TryGetValue(name, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }

}
