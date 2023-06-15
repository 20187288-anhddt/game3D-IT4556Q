using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutManager : MonoBehaviour
{
    public List<Customer> listCusCheckout;
    public List<CheckOutPosition> listCheckoutPos;
    public int maxCusCheckout;
    public Checkout checkout;


    public bool CheckoutStatus()
    {
        if (checkout.checkoutPos.isHaveCus)
        {
            return false;
        }
        return true;
    }

}
