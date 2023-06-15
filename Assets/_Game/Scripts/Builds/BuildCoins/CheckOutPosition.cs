using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutPosition : MonoBehaviour
{
    public bool isHaveCus;
    public bool isMoving;
    public Customer curCus;
    public Checkout checkOut;

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
        customer.checkOutPos = this;
        curCus.transCheckOut = this.transform.position;
        isHaveCus = true;
        isMoving = false;
    }

    public void CheckStatus()
    {
        if (isHaveCus)
        {
            if (!isMoving && !curCus.onCheckoutPos)
            {
                curCus.UpdateState(BaseCustomer.MOVE_CHECKOUT_STATE); 
                isMoving = true;
            }
            if (curCus.onCheckoutPos)
            {
                checkOut.levelManager.customerManager.customerList.Remove(curCus);
                AllPoolContainer.Instance.Release(curCus);
                isHaveCus = false;
                isMoving = false;
            }
        }
    }
}
