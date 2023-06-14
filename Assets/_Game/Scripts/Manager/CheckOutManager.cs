using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOutManager : MonoBehaviour
{
    public List<GroupCustomer> listGrCusCheckout;
    public List<Checkout> listCheckout;
    public int maxCusCheckout;
    //public Transform[] exitPos;

    public Checkout GetEmtyCheckout()
    {
        Checkout c = null;
        if (listCheckout.Count <= 0)
            c = null;
        //int r = Random.Range(0, listCheckout.Count);
        //c = listCheckout[r];
        //c.transExit = exitPos[r];
        int r = Random.Range(0, listCheckout.Count);
        if(listCheckout[r].listCusCheckout.Count < maxCusCheckout)
        {
            c = listCheckout[r];
        }
        //for (int i = 0; i < listCheckout.Count; i++)
        //{
        //    if (listCheckout[i].listCusCheckout.Count < maxCusCheckout)
        //    {
        //        c = listCheckout[i];
        //        //c.transExit = exitPos[i];
        //        break;
        //    }
        //}
        return c;
    }
}
