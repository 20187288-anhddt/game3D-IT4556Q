using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlaceToBuyBag : PlaceBase
{
    public bool readyCheckout;
    public BagBase curBag;


    void Start()
    {
        isHaveCus = false;
        cusMoving = false;
        readyCheckout = false;
        haveOutFit = false;
    }
    void Update()
    {
        CheckStatus();
    }
    public override void AddCus(Customer customer)
    {
        curCus = customer;
        closet.listCurCus.Add(curCus);
        isHaveCus = true;
        cusMoving = false;
        curCus.placeToBuyBag = this;
        curCus.bagType = type;
        curCus.transBag = this.transform.position;
        closet.listCurCus.Add(curCus);
    }

    public void AddOutfit(BagBase bag)
    {
        curBag = bag;
    }

    public void CheckStatus()
    {
        if (isHaveCus)
        {
            if (!cusMoving && !curCus.onBagPos)
            {
                cusMoving = true;
                if (curCus.isLeader)
                {
                    curCus.UpdateState(BaseCustomer.MOVE_TO_BAG_STATE);
                }
                else
                {
                    curCus.UpdateState(BaseCustomer.FOLLOW_LEADER_STATE);
                }
            }
            if (curCus.onBagPos)
            {
                if (!haveOutFit)
                {
                    //OutfitBase o = (closet as Closet).GetAvailableOutfit();
                    //if (o != null)
                    //{
                    //    haveOutFit = true;
                    //    //AddOutfit(o);
                    //    //o.AddPlace(this as PlaceToBuyBag);
                    //    curOutfit.transform.DOMove(curCus.transform.position, 0.25f).OnComplete(() =>
                    //    {
                    //        Buy();
                    //    });

                    //}
                }
            }
            if (readyCheckout)
            {
                Checkout c = closet.levelManager.checkOutManager.GetEmtyCheckout();
                if (c != null)
                {
                    readyCheckout = false;
                    isHaveCus = false;
                    c.AddCus(curCus);
                    closet.levelManager.checkOutManager.listCusCheckout.Add(curCus);
                }
            }
        }
    }
    public void SetCloset(BagCloset closet)
    {
        this.closet = closet;
        this.type = closet.type;
    }
    public void Buy()
    {
        curBag.bagPos.haveOutfit = false;
        (closet as BagCloset).listBags.Remove(curBag);
        AllPoolContainer.Instance.Release(curBag);
        curCus.ChangeOutfit(this.type);
        haveOutFit = false;
        curCus.onPlacePos = false;
        readyCheckout = true;
    }
}
