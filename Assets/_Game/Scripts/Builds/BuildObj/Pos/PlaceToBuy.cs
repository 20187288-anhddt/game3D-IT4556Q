using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceToBuy : PlaceBase
{
    public Transform myTransform;
    public OutfitBase curOutfit;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }

    void Update()
    {
        CheckStatus();
    }
    public override void AddCus(Customer customer)
    {
        curCus = customer;
        closet.listCurCus.Add(curCus);
        curCus.placeToBuy = this;
        curCus.outfitType = this.type;
        curCus.transCloset = transform.position;
        isHaveCus = true;
        cusMoving = false;
    }

    public void AddOutfit(OutfitBase outfit)
    {
        curOutfit = outfit;
    }

    public void CheckStatus()
    {
        if (isHaveCus)
        {
            if (!cusMoving && !curCus.onPlacePos)
            {
                cusMoving = true;
                if (curCus.isLeader)
                {
                    curCus.UpdateState(BaseCustomer.MOVE_TO_CLOSET_STATE);
                }
                else
                {
                    curCus.UpdateState(BaseCustomer.FOLLOW_LEADER_STATE);
                }
            }
            if (curCus.onPlacePos)
            {
                if (!haveOutFit && !curCus.gotOutfit)
                {
                    OutfitBase o = (closet as Closet).GetAvailableOutfit();
                    if (o != null)
                    {
                        haveOutFit = true;
                        AddOutfit(o);
                        o.AddPlace(this);
                        curOutfit.transform.DOMove(curCus.transform.position, 0.25f).OnComplete(() =>
                        {
                            Buy();
                        });

                    }
                }
            }
            if (readyGo)
            {
                if (curCus.isLeader)
                {
                    if (curCus.grCus.CheckGotOutfit())
                    {
                        readyGo = false;
                        CustomerManager cusManager = closet.levelManager.customerManager;
                        cusManager.listGroupsHaveOutfit.Add(curCus.grCus);
                        //closetManager.CheckBagClosetEmptyWithNum(curCus.grCus.grNum);
                        //if (closetManager.listAvailableBagClosets.Count > 0)
                        //{

                        //    int r = Random.Range(0, closetManager.listAvailableBagClosets.Count);
                        //    PlaceToBuyBag curBagPlace = closetManager.listAvailableBagClosets[r].listEmtyPlaceToBuyBag[0];
                        //    if (curBagPlace != null)
                        //    {
                        //        readyGo = false;
                        //        curCus.grCus.AddCloset(closetManager.listAvailableBagClosets[r]);
                        //        curCus.grCus.typeBag = closetManager.listAvailableBagClosets[r].type;
                        //        curBagPlace.AddCus(curCus);
                        //        for (int i = 0; i < curCus.grCus.teammates.Count; i++)
                        //        {
                        //            PlaceToBuyBag nextPlace = closetManager.listAvailableBagClosets[r].listEmtyPlaceToBuyBag[i + 1];
                        //            nextPlace.AddCus(curCus.grCus.teammates[i]);
                        //        }
                        //        closetManager.listAvailableBagClosets[r].listEmtyPlaceToBuyBag.Clear();
                        //        closetManager.listAvailableBagClosets.Clear();
                        //        for (int i = 0; i < curCus.grCus.listCus.Count; i++)
                        //        {
                        //            curCus.grCus.listCus[i].onPlacePos = false;
                        //            closet.listCurCus.Remove(curCus.grCus.listCus[i]);
                        //        }
                        //        isHaveCus = false;  
                        //    }
                        //}
                    } 
                }
            }
        }
    }
    public void SetCloset(Closet closet)
    {
        this.closet = closet;
        this.type = closet.ingredientType;
    }
    public void Buy()
    {
        curOutfit.outfitPos.haveOutfit = false;
        curCus.gotOutfit = true;
        (closet as Closet).listOutfits.Remove(curOutfit);
        AllPoolContainer.Instance.Release(curOutfit);
        curCus.ChangeOutfit(this.type);
        haveOutFit = false;
        readyGo = true;
    }
    public void StartInGame()
    {
        isHaveCus = false;
        cusMoving = false;
        readyGo = false;
        haveOutFit = false;
    }
}
