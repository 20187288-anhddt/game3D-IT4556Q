using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitBase : IngredientBase
{
    public PlaceToBuy placeToBuy;
    public bool isHaveCus;
    public Customer curCus;

    void Start()
    {
        isHaveCus = false;
    }
    public void AddPlace(PlaceToBuy p)
    {
        isHaveCus = true;
        this.placeToBuy = p;
        this.curCus = p.curCus;
    }
    public void ResetOutfit()
    {
        isHaveCus = false;
        placeToBuy = null;
        curCus = null;
    }
}
