using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    public List<MachineBase> allActiveMachine;
    public List<ClothMachine> allActiveClothMachine;
    public List<BagMachine> allActiveBagMachine;

    public List<ClothMachine> listBearClothMachineActive;
    public List<ClothMachine> listCowClothMachineActive;
    public List<ClothMachine> listSheepClothMachineActive;
    public List<ClothMachine> listChickenClothMachineActive;
    public List<ClothMachine> listLionClothMachineActive;
    public List<ClothMachine> listCrocClothMachineActive;
    public List<ClothMachine> listEleClothMachineActive;

    public List<ClothMachine> listZebraClothMachineActive;
    public List<BagMachine> listChickenBagMachineActive;
    public List<BagMachine> listBearBagMachineActive;
    public List<BagMachine> listCowBagMachineActive;
    public List<BagMachine> listSheepBagMachineActive;
    public List<BagMachine> listLionBagMachineActive;
    public List<BagMachine> listCrocBagMachineActive;
    public List<BagMachine> listEleBagMachineActive;
    public List<BagMachine> listZebraBagMachineActive;

    public List<Habitat> listSheepHabitatActive;
    public List<Habitat> listCowHabitatActive;
    public List<Habitat> listChickenHabitatActive;
    public List<Habitat> listBearHabitatActive;
    public List<Habitat> listLionHabitatActive;
    public List<Habitat> listCrocHabitatActive;
    public List<Habitat> listEleHabitatActive;
    public List<Habitat> listZebraHabitatActive;

    public MachineBase CheckMachineInputEmtyWithType()
    {
        MachineBase curMachine = null;
        if (allActiveMachine.Count <= 0)
        {
            curMachine = null;
        }
        else
        {
            int r = Random.Range(0, allActiveMachine.Count);
            if (!allActiveMachine[r].isHaveInStaff && allActiveMachine[r].ingredients.Count < allActiveMachine[r].maxObjInput / 2)
                curMachine = allActiveMachine[r];
        }
        
        //else
        //{
        //    bool tmp = false;
        //    //List<MachineBase> listAvailableMachine = new List<MachineBase>();
        //    do
        //    {
        //        int r = Random.Range(0, allActiveMachine.Count);
        //        curMachine = allActiveMachine[r];
        //        if (!allActiveMachine[r].isHaveInStaff && allActiveMachine[r].ingredients.Count < allActiveMachine[r].maxObjInput / 3)
        //        {
        //            tmp = true;
        //        }
        //    } while (!tmp);
        //}
        //while(curMachine = null)
        //{
        //    int r = Random.Range(0, allActiveMachine.Count);
        //    if(!allActiveMachine[r].isHaveInStaff && allActiveMachine[r].ingredients.Count < allActiveMachine[r].maxObjInput / 3)
        //    {
        //        curMachine = allActiveMachine[r];
        //    }
        //}
        //for (int i = 0; i < allActiveMachine.Count; i++)
        //{
        //    if (!allActiveMachine[i].isHaveInStaff && allActiveMachine[i].ingredients.Count < allActiveMachine[i].maxObjInput / 3)
        //    {
        //        //if (!listAvailableMachine.Contains(allActiveMachine[i]))
        //        //{
        //        //    listAvailableMachine.Add(allActiveMachine[i]);
        //        //}
        //        curMachine = allActiveMachine[i];
        //        break;
        //    }
        //}
        return curMachine;
        //int r = Random.Range(0, listAvailableMachine.Count);
        //curMachine = listAvailableMachine[r];
        //switch (type)
        //{
        //    case IngredientType.BEAR:
        //        for (int i = 0; i < listBearClothMachineActive.Count; i++)
        //        {
        //            if (listBearClothMachineActive[i].ingredients.Count <= listBearClothMachineActive[i].maxObjInput / 2 && !listBearClothMachineActive[i].isHaveInStaff)
        //            {
        //                curMachine = listBearClothMachineActive[i];
        //                break;
        //            }
        //        }
        //        break;
        //            case IngredientType.COW:
        //        for (int i = 0; i < listCowClothMachineActive.Count; i++)
        //        {
        //            if (listCowClothMachineActive[i].ingredients.Count <= listCowClothMachineActive[i].maxObjInput / 2 && !listCowClothMachineActive[i].isHaveInStaff)
        //            {
        //                curMachine = listCowClothMachineActive[i];
        //                break;
        //            }
        //        }
        //        break;
        //    case IngredientType.CHICKEN:
        //        for (int i = 0; i < listChickenClothMachineActive.Count; i++)
        //        {
        //            if (listChickenClothMachineActive[i].ingredients.Count <= listChickenClothMachineActive[i].maxObjInput / 2 && !listChickenClothMachineActive[i].isHaveInStaff)
        //            {
        //                curMachine = listChickenClothMachineActive[i];
        //                break;
        //            }
        //        }
        //        break;
        //    case IngredientType.SHEEP:
        //        for (int i = 0; i < listSheepClothMachineActive.Count; i++)
        //        {
        //            if (listSheepClothMachineActive[i].ingredients.Count <= listSheepClothMachineActive[i].maxObjInput / 2 && !listSheepClothMachineActive[i].isHaveInStaff)
        //            {
        //                curMachine = listSheepClothMachineActive[i];
        //                break;
        //            }
        //        }
        //        break;
        //}
    }
    public ClothMachine GetClothMachineWithType(IngredientType type)
    {
        ClothMachine curClothMachine = null;
        if (allActiveMachine.Count <= 0 || allActiveClothMachine.Count <=0)
            curClothMachine = null;
        switch (type)
        {
            case IngredientType.SHEEP:
                for (int i = 0; i < listSheepClothMachineActive.Count; i++)
                {
                    if (listSheepClothMachineActive[i].outCloths.Count > 3 || !listSheepClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listSheepClothMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.COW:
                for (int i = 0; i < listCowClothMachineActive.Count; i++)
                {
                    if (listCowClothMachineActive[i].outCloths.Count > 3 || !listCowClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listCowClothMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.CHICKEN:
                for (int i = 0; i < listChickenClothMachineActive.Count; i++)
                {
                    if (listChickenClothMachineActive[i].outCloths.Count > 3 || !listChickenClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listChickenClothMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.BEAR:
                for (int i = 0; i < listBearClothMachineActive.Count; i++)
                {
                    if (listBearClothMachineActive[i].outCloths.Count > 3 || !listBearClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listBearClothMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.LION:
                for (int i = 0; i < listLionClothMachineActive.Count; i++)
                {
                    if (listLionClothMachineActive[i].outCloths.Count > 3 || !listLionClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listLionClothMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.CROC:
                for (int i = 0; i < listCrocClothMachineActive.Count; i++)
                {
                    if (listCrocClothMachineActive[i].outCloths.Count > 3 || !listCrocClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listCrocClothMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.ELE:
                for (int i = 0; i < listEleClothMachineActive.Count; i++)
                {
                    if (listEleClothMachineActive[i].outCloths.Count > 3 || !listEleClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listEleClothMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.ZEBRA:
                for (int i = 0; i < listZebraClothMachineActive.Count; i++)
                {
                    if (listZebraClothMachineActive[i].outCloths.Count > 3 || !listZebraClothMachineActive[i].isHaveOutStaff)
                    {
                        curClothMachine = listZebraClothMachineActive[i];
                        break;
                    }
                }
                break;
        }
        return curClothMachine;
    }
    public BagMachine GetBagMachineWithType(IngredientType type)
    {
        BagMachine curBagMachine = null;
        if (allActiveMachine.Count <= 0 || allActiveBagMachine.Count <= 0)
            curBagMachine = null;
        switch (type)
        {
            case IngredientType.SHEEP:
                for (int i = 0; i < listSheepBagMachineActive.Count; i++)
                {
                    if (listSheepBagMachineActive[i].outCloths.Count > 3 || !listSheepBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listSheepBagMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.COW:
                for (int i = 0; i < listCowBagMachineActive.Count; i++)
                {
                    if (listCowBagMachineActive[i].outCloths.Count > 3 || !listCowBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listCowBagMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.CHICKEN:
                for (int i = 0; i < listChickenBagMachineActive.Count; i++)
                {
                    if (listChickenBagMachineActive[i].outCloths.Count > 3 || !listChickenBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listChickenBagMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.BEAR:
                for (int i = 0; i < listBearBagMachineActive.Count; i++)
                {
                    if (listBearBagMachineActive[i].outCloths.Count > 3 || !listBearBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listBearBagMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.LION:
                for (int i = 0; i < listLionBagMachineActive.Count; i++)
                {
                    if (listLionBagMachineActive[i].outCloths.Count > 3 || !listLionBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listLionBagMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.CROC:
                for (int i = 0; i < listCrocBagMachineActive.Count; i++)
                {
                    if (listCrocBagMachineActive[i].outCloths.Count > 3 || !listCrocBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listCrocBagMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.ELE:
                for (int i = 0; i < listEleBagMachineActive.Count; i++)
                {
                    if (listEleBagMachineActive[i].outCloths.Count > 3 || !listEleBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listEleBagMachineActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.ZEBRA:
                for (int i = 0; i < listZebraBagMachineActive.Count; i++)
                {
                    if (listZebraBagMachineActive[i].outCloths.Count > 3 || !listZebraBagMachineActive[i].isHaveOutStaff)
                    {
                        curBagMachine = listZebraBagMachineActive[i];
                        break;
                    }
                }
                break;
        }
        return curBagMachine;
    }
    public ClothMachine GetRandomTypeClothMachine()
    {
        ClothMachine curMachine = null ;
        while(curMachine == null)
        {
            int r = Random.Range(0, allActiveClothMachine.Count);
            if (allActiveClothMachine[r].levelManager.habitatManager.CheckHabitatWithTypeForCus(allActiveClothMachine[r].ingredientType))
            {
                curMachine = allActiveClothMachine[r];
            }
        }
        return curMachine;
    }
    public BagMachine GetRandomTypeBagMachine()
    {
        BagMachine curMachine = null;
        int r = Random.Range(0, allActiveBagMachine.Count);
        curMachine = allActiveBagMachine[r];
        return curMachine;
    }
    public bool CheckAvailableBagMachineWithType(IngredientType type)
    {
        bool isHaveBagMachine = false;
        if (allActiveMachine.Count <= 0 || allActiveBagMachine.Count <= 0)
            isHaveBagMachine = false;
        switch (type)
        {
            case IngredientType.SHEEP:
                if(listSheepBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
            case IngredientType.COW:
                if (listCowBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
            case IngredientType.CHICKEN:
                if (listChickenBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
            case IngredientType.BEAR:
                if (listBearBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
            case IngredientType.LION:
                if (listLionBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
            case IngredientType.CROC:
                if (listCrocBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
            case IngredientType.ELE:
                if (listEleBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
            case IngredientType.ZEBRA:
                if (listZebraBagMachineActive.Count > 0)
                {
                    isHaveBagMachine = true;
                }
                break;
        }
        return isHaveBagMachine;
    }
}
