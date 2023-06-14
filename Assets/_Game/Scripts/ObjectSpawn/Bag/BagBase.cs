using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagBase : IngredientBase
{
    public PlaceToBuyBag placeToBuyBag;
    public BagPos bagPos;
    public bool isHaveCus;
    public Customer curCus;
    void Start()
    {
        isHaveCus = false;
    }
    public void AddPlace(PlaceToBuyBag p)
    {
        isHaveCus = true;
        this.placeToBuyBag = p;
        this.curCus = p.curCus;
    }
    public void AddPos(BagPos o)
    {
        this.bagPos = o;
    }
    public void ResetOutfit()
    {
        isHaveCus = false;
        placeToBuyBag = null;
        curCus = null;
    }
}
