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
    public List<BagMachine> listChickenBagMachineActive;
    public List<BagMachine> listBearBagMachineActive;
    public List<BagMachine> listCowBagMachineActive;
    public List<BagMachine> listSheepBagMachineActive;
    public List<Habitat> listSheepHabitatActive;
    public List<Habitat> listCowHabitatActive;
    public List<Habitat> listChickenHabitatActive;
    public List<Habitat> listBearHabitatActive;

    public MachineBase CheckMachineInputEmty()
    {
        MachineBase curMachine = null;
        if (allActiveMachine.Count <= 0)
            curMachine = null;
        //int r = Random.Range(0, allActiveMachine.Count);
        for(int i = 0; i < allActiveMachine.Count; i++)
        {
            if(allActiveMachine[i].ingredients.Count <= allActiveMachine[i].maxObjInput/2 && !allActiveMachine[i].isHaveInStaff)
            {
                curMachine = allActiveMachine[i];
                break;
            }
        }
        return curMachine;
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
}
