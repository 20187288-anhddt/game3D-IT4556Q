using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : Singleton<BuildController>
{
    [SerializeField] private List<BuildIngredientController> buildIngredientControllers;

    public BuildIngredientController GetBuildIngredientController(IngredientType ingredientType)
    {
        foreach (BuildIngredientController buildIngredientController in buildIngredientControllers)
        {
            if(buildIngredientController.ingredientType == ingredientType)
            {
                return buildIngredientController;
            }
        }
        return null;
    }
}
