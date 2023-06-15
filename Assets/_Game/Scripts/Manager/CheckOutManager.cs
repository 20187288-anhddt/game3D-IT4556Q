using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutManager : MonoBehaviour
{
    public List<Customer> listCusCheckout;
    public List<Checkout> listCheckout;
    public int maxCusCheckout;

    public Checkout GetEmtyCheckout()
    {
        Checkout c = null;
        for (int i = 0; i < listCheckout.Count; i++)
        {
            if (!listCheckout[i].isHaveCus)
            {
                c = listCheckout[i];
                break;
            }
        }
        return c;
    }

}
