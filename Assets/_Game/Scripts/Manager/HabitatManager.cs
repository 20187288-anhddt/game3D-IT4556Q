using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabitatManager : MonoBehaviour
{
    public List<Habitat> allActiveHabitats;
    public List<Habitat> listChickenHabitatsActive;
    public List<Habitat> listCowHabitatsActive;
    public List<Habitat> listSheepHabitatsActive;
    public List<Habitat> listBearHabitatsActive;
    public List<Habitat> listLionHabitatsActive;
    public List<Habitat> listCrocHabitatsActive;
    public List<Habitat> listEleHabitatsActive;
    public List<Habitat> listZebraHabitatsActive;

    public Habitat GetHabitatWithTypeForStaff(IngredientType type)
    {
        Habitat curHabitat = null;
        switch (type)
        {
            case IngredientType.SHEEP:
                for(int i = 0; i < listSheepHabitatsActive.Count; i++)
                {
                    if (listSheepHabitatsActive[i].animalsIsReady.Count == listSheepHabitatsActive[i].allAnimals.Count && !listSheepHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listSheepHabitatsActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.COW:
                for (int i = 0; i < listCowHabitatsActive.Count; i++)
                {
                    if (listCowHabitatsActive[i].animalsIsReady.Count == listCowHabitatsActive[i].allAnimals.Count && !listCowHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listCowHabitatsActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.CHICKEN:
                for (int i = 0; i < listChickenHabitatsActive.Count; i++)
                {
                    if (listChickenHabitatsActive[i].animalsIsReady.Count == listChickenHabitatsActive[i].allAnimals.Count && !listChickenHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listChickenHabitatsActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.BEAR:
                for (int i = 0; i < listBearHabitatsActive.Count; i++)
                {
                    if (listBearHabitatsActive[i].animalsIsReady.Count == listBearHabitatsActive[i].allAnimals.Count && !listBearHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listBearHabitatsActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.LION:
                for (int i = 0; i < listLionHabitatsActive.Count; i++)
                {
                    if (listLionHabitatsActive[i].animalsIsReady.Count == listLionHabitatsActive[i].allAnimals.Count && !listLionHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listLionHabitatsActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.ELE:
                for (int i = 0; i < listEleHabitatsActive.Count; i++)
                {
                    if (listEleHabitatsActive[i].animalsIsReady.Count == listEleHabitatsActive[i].allAnimals.Count && !listEleHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listEleHabitatsActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.CROC:
                for (int i = 0; i < listCrocHabitatsActive.Count; i++)
                {
                    if (listCrocHabitatsActive[i].animalsIsReady.Count == listCrocHabitatsActive[i].allAnimals.Count && !listCrocHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listCrocHabitatsActive[i];
                        break;
                    }
                }
                break;
            case IngredientType.ZEBRA:
                for (int i = 0; i < listZebraHabitatsActive.Count; i++)
                {
                    if (listZebraHabitatsActive[i].animalsIsReady.Count == listZebraHabitatsActive[i].allAnimals.Count && !listZebraHabitatsActive[i].isHaveStaff)
                    {
                        curHabitat = listZebraHabitatsActive[i];
                        break;
                    }
                }
                break;
        }
        return curHabitat;
    }
    public bool CheckHabitatWithTypeForCus(IngredientType type)
    {
        bool isHaveHabitat = false;
        if (allActiveHabitats.Count <= 0)
            isHaveHabitat = false;
        switch (type)
        {
            case IngredientType.SHEEP:
                if (listSheepHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
            case IngredientType.COW:
                if (listCowHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
            case IngredientType.CHICKEN:
                if (listChickenHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
            case IngredientType.BEAR:
                if (listBearHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
            case IngredientType.LION:
                if (listLionHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
            case IngredientType.CROC:
                if (listCrocHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
            case IngredientType.ELE:
                if (listEleHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
            case IngredientType.ZEBRA:
                if (listZebraHabitatsActive.Count > 0)
                {
                    isHaveHabitat = true;
                }
                break;
        }
        return isHaveHabitat;
    }
    public Habitat GetHabitatWithTypeForAnimal(IngredientType type)
    {
        Habitat curHabitat = null;
        switch (type)
        {
            case IngredientType.SHEEP:
                curHabitat = listSheepHabitatsActive[0];
                break;
            case IngredientType.COW:
                curHabitat = listCowHabitatsActive[0];
                break;
            case IngredientType.CHICKEN:
                curHabitat = listChickenHabitatsActive[0];
                break;
            case IngredientType.BEAR:
                curHabitat = listBearHabitatsActive[0];
                break;
            case IngredientType.LION:
                curHabitat = listLionHabitatsActive[0];
                break;
            case IngredientType.CROC:
                curHabitat = listCrocHabitatsActive[0];
                break;
            case IngredientType.ELE:
                curHabitat = listEleHabitatsActive[0];
                break;
            case IngredientType.ZEBRA:
                curHabitat = listZebraHabitatsActive[0];
                break;
        }
        return curHabitat;
    }
}
