using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : Singleton<MachineManager>
{
    public List<MachineBase> allActiveMachine;
    public List<ClothMachine> allActiveClothMachine;
    public List<BagMachine> allActiveBagMachine;
    public List<ClothMachine> listBearClothMachineActive;
    public List<ClothMachine> listCowClothMachineActive;
    public List<ClothMachine> listSheepClothMachineActive;
    public List<ClothMachine> listChickenClothMachineActive;
    public List<BagMachine> listChickenBagMachineActive;
    public List<BagMachine> listBearBagMachineActive;
    public List<BagMachine> listCowBagMachineActive;
    public List<BagMachine> listSheepBagMachineActive;
    public List<Habitat> listSheepHabitatActive;
    public List<Habitat> listCowHabitatActive;
    public List<Habitat> listChickenHabitatActive;
    public List<Habitat> listBearHabitatActive;

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
        }
        return curBagMachine;
    }
    public ClothMachine GetRandomTypeClothMachine()
    {
        ClothMachine curMachine = null ;
        int r = Random.Range(0, allActiveClothMachine.Count);
        curMachine = allActiveClothMachine[r];
        return curMachine;
    }
    public BagMachine GetRandomTypeBagMachine()
    {
        BagMachine curMachine = null;
        int r = Random.Range(0, allActiveBagMachine.Count);
        curMachine = allActiveBagMachine[r];
        return curMachine;
    }
}
