using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum EventName { 
    PlayJoystick,
    StopJoyStick,


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
