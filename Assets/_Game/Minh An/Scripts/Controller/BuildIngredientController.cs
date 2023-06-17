using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildIngredientController : MonoBehaviour
{
    public IngredientType ingredientType;
    [SerializeField] private Habitat Habitat;
    [SerializeField] private ClothMachine ClothMachine;
    [SerializeField] private Closet Closet;
    [SerializeField] private Closet Closet_1;

    [SerializeField] private DataStatusObject statusObject_Habitat;
    [SerializeField] private DataStatusObject statusObject_ClothMachine;
    [SerializeField] private DataStatusObject statusObject_Closet;
    [SerializeField] private DataStatusObject statusObjectCloset_1;

    public bool IsHabitat_Complete()
    {
        return false;
    }
    public bool IsClothMachine_Complete()
    {
        return false;
    }
    public bool IsCloset_Complete()
    {
        return false;
    }
    public bool IsCloset_1_Complete()
    {
        return false;
    }
    public DataStatusObject GetDataStatusObject_statusObject_Habitat()
    {
        return statusObject_Habitat;
    }
    public DataStatusObject GetDataStatusObject_statusObject_ClothMachine()
    {
        return statusObject_ClothMachine;
    }
    public DataStatusObject GetDataStatusObject_statusObject_Closet()
    {
        return statusObject_Closet;
    }
    public DataStatusObject GetDataStatusObject_statusObjectCloset_1()
    {
        return statusObjectCloset_1;
    }

    public void Habitat_OnBuy()
    {
        //neu Habitat da hoat dong thi k OnBuy
        GetDataStatusObject_statusObject_Habitat().OnBuy();
    }
    public void ClothMachine_OnBuy()
    {
        //tuong tu
        GetDataStatusObject_statusObject_ClothMachine().OnBuy();
    }
    public void Closet_OnBuy()
    {
        //tuong tu
        GetDataStatusObject_statusObject_Closet().OnBuy();
    }
    public void Closet_1_OnBuy()
    {
        //tuong tu
        GetDataStatusObject_statusObjectCloset_1().OnBuy();
    }
}
