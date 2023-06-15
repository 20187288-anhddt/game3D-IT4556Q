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
            isMoving = true;
            if (curCus.isLeader)
            {
                curCus.UpdateState(BaseCustomer.MOVE_CHECKOUT_STATE);
                curCus.grCus.TeamFollowLeader();
            }
            if (curCus.onCheckoutPos)
            {
                curCus.UpdateState(BaseCustomer.EXIT_STATE);
                levelManager.customerManager.customerList.Remove(curCus);
                isHaveCus = false;
                isMoving = false;
            }
        }
    }
}
