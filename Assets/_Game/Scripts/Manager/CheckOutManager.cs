using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutManager : MonoBehaviour
{
    public List<GroupCustomer> listGrCusCheckout;
    public List<Checkout> listCheckout;
    public int maxCusCheckout;

    public Checkout GetEmtyCheckout()
    {
        Checkout c = null;
        if (listCheckout.Count <= 0)
            c = null;
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
