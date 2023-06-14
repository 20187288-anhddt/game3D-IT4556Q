using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckoutPos : MonoBehaviour
{
    public Transform myTransform;
    public bool isHaveCus;
    public bool cusMoving;
    public float delayTime;
    public bool readyGo;
    public Customer curCus;
    [SerializeField]
    private Checkout checkOut;
    public void Awake()
    {
        if (myTransform == null) { myTransform = this.transform; }
    }
    public void StartInGame()
    {
        isHaveCus = false;
        cusMoving = false;
        readyGo = false;
    }
    void Update()
    {
        CheckStatus();
    }
    public void SetCheckOut(Checkout checkOut)
    {
        this.checkOut = checkOut;
    }
    public void AddCus(Customer customer)
    {
        curCus = customer;
        checkOut.listCusCheckout.Add(curCus);
        isHaveCus = true;
        cusMoving = false;
        curCus.placeCheckout = this;
        curCus.transCheckOut = this.transform.position;
    }
    public void CheckStatus()
    {
        if (isHaveCus)
        {
            if (!cusMoving && !curCus.onCheckoutPos)
            {
                cusMoving = true;
                if (curCus.isLeader)
                {
                    curCus.UpdateState(BaseCustomer.MOVE_CHECKOUT_STATE);
                }
                else
                {
                    curCus.UpdateState(BaseCustomer.FOLLOW_LEADER_STATE);
                }
            }
        }
    }
}
