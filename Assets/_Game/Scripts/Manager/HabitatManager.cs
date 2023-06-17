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

    public Habitat GetHabitatWithType(IngredientType type)
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
        }
        return curHabitat;
    }
}
