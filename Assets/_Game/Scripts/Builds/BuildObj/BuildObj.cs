using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObj : BaseBuild
{

    public override void Start()
    {
        base.Start();
       
    }

   
    public override void OnBought()
    {
        base.OnBought();
        //if(pirceObject != null)
        //{
        //    DataManager.Instance.GetDataMoneyController().RemoveMoney(Money.TypeMoney.USD, pirceObject.Get_Pirce());
        //}
    }
}
public enum NameObject_This
{
    ChickenHabitat,
    ChickenClothMachine,
    ChickenCloset,
    ChickenCloset_1,

    SheepHabitat,
    SheepClothMachine,
    SheepCloset,
    SheepCloset_1,

    CowHabitat,
    CowClothMachine,
    CowCloset,
    CowCloset_1,

    BearHabitat,
    BearClothMachine,
    BearCloset,
    BearCloset_1,

    BearBagMachine,
    CowBagMachine,
    SheepBagMachine,
    ChickenBagMachine,

    CowBagCloset,
    SheepBagCloset,
    ChickenBagCloset,
    BearBagCloset,

    CheckOutTable,
    CheckOutTable_1,

    BuildStage,
    BuildStage_1,

    HireStaff,
    HireStaff_1,
    HireStaff_2,
    HireStaff_3,
    Car
}

