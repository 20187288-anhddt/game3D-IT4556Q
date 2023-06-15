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
                if (!haveOutFit && !curCus.gotBag)
                {
                    BagBase o = (closet as BagCloset).GetAvailableOutfit();
                    if (o != null)
                    {
                        haveOutFit = true;
                        AddOutfit(o);
                        o.AddPlace(this);
                        curBag.transform.DOMove(curCus.transform.position, 0.25f).OnComplete(() =>
                        {
                            Buy();
                        });
                    }
                }
            }
            if (readyCheckout)
            {
                if (curCus.isLeader)
                {
                    if (curCus.grCus.CheckGotBag())
                    {
                        Checkout c = closet.levelManager.checkOutManager.GetEmtyCheckout();
                        if (c != null)
                        {
                            readyCheckout = false;             
                            c.AddCus(curCus);
                            closet.levelManager.checkOutManager.listGrCusCheckout.Add(curCus.grCus);
                            isHaveCus = false;
                        }
                    }
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
        curCus.gotBag = true;
        (closet as BagCloset).listBags.Remove(curBag);
        AllPoolContainer.Instance.Release(curBag);
        curCus.ChangeBag(this.type);
        haveOutFit = false;
        readyCheckout = true;
    }
}
