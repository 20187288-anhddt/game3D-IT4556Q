using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkout : BuildCoins
{
    public bool isHaveCus;
    public bool isMoving;
    public Customer curCus;
    public Transform checkOutPos;

    void Start()
    {
        isHaveCus = false;
        isMoving = false;
    }
    void Update()
    {
        CheckStatus();
    }
    public void AddCus(Customer customer)
    {
        curCus = customer;
        customer.checkOut = this;
        curCus.transCheckOut = checkOutPos.position;
        isHaveCus = true;
        isMoving = false;
    }

    public void CheckStatus()
    {
        if (isHaveCus)
        {
            if (curCus.isLeader)
            {
                if (!isMoving && !curCus.onCheckoutPos)
                {
                    isMoving = true;
                    curCus.UpdateState(BaseCustomer.MOVE_CHECKOUT_STATE);
                    curCus.grCus.TeamFollowLeader();
                }
                if (curCus.onCheckoutPos)
                {
                    curCus.onCheckoutPos = false;
                    curCus.UpdateState(BaseCustomer.EXIT_STATE);
                    levelManager.customerManager.customerList.Remove(curCus);
                    for(int i = 0; i < curCus.grCus.teammates.Count; i++)
                    {
                        levelManager.customerManager.customerList.Remove(curCus.grCus.teammates[i]);
                    }
                    levelManager.customerManager.customerList.Remove(curCus);
                    isHaveCus = false;
                }
            }
        }
    }
    public void SpawnMoney(int n, IngredientType typeOutfit, IngredientType typeBag)
    {
        //TODO
        //Spawn tien
    }
}
