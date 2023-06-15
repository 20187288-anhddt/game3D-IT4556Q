using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : BuildObj
{
    public List<OutfitBase> listOutfits;
    public List<Customer> listCurCus;
    public List<OutfitPos> listOutfitPos;
    public IngredientType outfitType;
    public int maxObj;
    public List<PlaceToBuy> listPlaceToBuy;
    public List<PlaceToBuy> listEmtyPlaceToBuy;
    [SerializeField]
    private OutfitBase outFitPrefab;

    void Start()
    {
        StartInGame(); 
    }
    public void SpawnOutfit()
    {
        OutfitPos o = GetEmtyPos();
        var curOutfit = AllPoolContainer.Instance.Spawn(outFitPrefab, o.transform.position, transform.rotation);
        (curOutfit as OutfitBase).ResetOutfit();
        if (!listOutfits.Contains(curOutfit as OutfitBase))
        {
            o.AddOutfit(curOutfit as OutfitBase);
            (curOutfit as OutfitBase).AddPos(o);
            listOutfits.Add(curOutfit as OutfitBase);     
        }
        curOutfit.transform.parent = o.transform;
        curOutfit.transform.position = o.transform.position;
        curOutfit.transform.localRotation = Quaternion.identity;

    }
    public override void StartInGame()
    {
        base.StartInGame();
        foreach (PlaceToBuy p in listPlaceToBuy)
        {
            p.SetCloset(this);
        }
        foreach (OutfitPos o in listOutfitPos)
        {
            o.SetCloset(this);
        }
    }

    public OutfitBase GetAvailableOutfit()
    {
        OutfitBase outfit = null;
        for(int i = 0; i < listOutfits.Count; i++)
        {
            if (!listOutfits[i].isHaveCus)
            {
                outfit = listOutfits[i];
                break;
            }
        }
        return outfit;
    }
    public OutfitPos GetEmtyPos()
    {
        OutfitPos o = null;
        for (int i = 0; i < listOutfitPos.Count; i++)
        {
            if (!listOutfitPos[i].haveOutfit)
            {
                o = listOutfitPos[i];
                break;
            }
        }
        return o;
    }
    public void GetEmtyPlaceNum()
    {
        for(int i = 0; i < listPlaceToBuy.Count; i++)
        {
            if (!listPlaceToBuy[i].isHaveCus)
            {           
                if (!listEmtyPlaceToBuy.Contains(listPlaceToBuy[i]))
                {
                    listEmtyPlaceToBuy.Add(listPlaceToBuy[i]);
                }
            }      
        }
    }
}
