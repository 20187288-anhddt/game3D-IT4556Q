using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCloset : ClosetBase
{
    public List<BagBase> listBags;
    public List<BagPos> listBagPos;
    [SerializeField]
    private BagBase outFitPrefab;
    public List<PlaceToBuyBag> listPlaceToBuyBag;
    public List<PlaceToBuyBag> listEmtyPlaceToBuyBag;

    void Start()
    {
        StartInGame();
    }
    public void SpawnOutfit()
    {
        BagPos o = GetEmtyPos();
        var curBag = AllPoolContainer.Instance.Spawn(outFitPrefab, o.transform.position, transform.rotation);
        (curBag as BagBase).ResetOutfit();
        if (!listBags.Contains(curBag as BagBase))
        {
            o.AddOutfit(curBag as BagBase);
            (curBag as BagBase).AddPos(o);
            listBags.Add(curBag as BagBase);
        }
        curBag.transform.parent = o.transform;
        curBag.transform.position = o.transform.position;
        curBag.transform.localRotation = Quaternion.identity;

    }
    public override void StartInGame()
    {
        base.StartInGame();
        foreach (PlaceToBuyBag p in listPlaceToBuyBag)
        {
            p.SetCloset(this);
        }
        foreach (BagPos o in listBagPos)
        {
            o.SetCloset(this);
        }
    }

    public BagBase GetAvailableOutfit()
    {
        BagBase outfit = null;
        for (int i = 0; i < listBags.Count; i++)
        {
            if (!listBags[i].isHaveCus)
            {
                outfit = listBags[i];
                break;
            }
        }
        return outfit;
    }
    public BagPos GetEmtyPos()
    {
        BagPos o = null;
        for (int i = 0; i < listBagPos.Count; i++)
        {
            if (!listBagPos[i].haveOutfit)
            {
                o = listBagPos[i];
                break;
            }
        }
        return o;
    }
    public void GetEmtyPlaceNum(int n)
    {
        listEmtyPlaceToBuyBag.Clear();
        for (int i = 0; i < listPlaceToBuyBag.Count; i++)
        {
            if (!listPlaceToBuyBag[i].isHaveCus)
            {
                if (!listEmtyPlaceToBuyBag.Contains(listPlaceToBuyBag[i]))
                {
                    listEmtyPlaceToBuyBag.Add(listPlaceToBuyBag[i]);
                }
            }
        }
        if(listEmtyPlaceToBuyBag.Count < n)
        {
            listEmtyPlaceToBuyBag.Clear();
        }
    }
}
