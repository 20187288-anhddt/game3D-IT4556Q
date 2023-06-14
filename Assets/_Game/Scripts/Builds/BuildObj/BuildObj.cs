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
    #region Level_1
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
    Car,
    HireAnimal_Chicken,
    HireAnimal_Cow,
    HireAnimal_Bear,
    HireStaff_4,
    HireStaff_5,
    NextLevel_2,
    NextLevel_3,
    #endregion
    #region Level_2
    CrocHabitat,
    EleHabitat,
    LionHabitat,
    ZebraHabitat,

    CrocClothMachine,
    CrocBagMachine,

    EleClothMachine,
    EleBagMachine,

    LionClothMachine,
    LionBagMachine,

    ZebraClothMachine,
    ZebraBagMachine,

    CrocCloset,
    CrocBagCloset,

    EleCloset,
    EleBagCloset,

    LionCloset,
    LionBagCloset,

    ZebraCloset,
    ZebraBagCloset,

    HireAnimal_Croc,
    HireAnimal_Ele,
    HireAnimal_Lion,
    HireAnimal_Zebra,
    NextLevel_4,
    BuildStage_2,
    #endregion
}

