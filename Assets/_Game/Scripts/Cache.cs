using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider, ICollect> icollectDic = new Dictionary<Collider, ICollect>();

    public static ICollect getICollect(Collider key)
    {
        if (!icollectDic.ContainsKey(key))
        {
            icollectDic.Add(key, key.GetComponent<ICollect>());
        }
        return icollectDic[key];
    }

    private static Dictionary<Collider, IAct> iActDic = new Dictionary<Collider, IAct>();

    public static IAct getIAct(Collider key)
    {
        if (!iActDic.ContainsKey(key))
        {
            iActDic.Add(key, key.GetComponent<IAct>());
        }
        return iActDic[key];
    }

    private static Dictionary<Collider, IUnlock> iUnlockDic = new Dictionary<Collider, IUnlock>();

    public static IUnlock getIUnlock(Collider key)
    {
        if (!iUnlockDic.ContainsKey(key))
        {
            iUnlockDic.Add(key, key.GetComponent<IUnlock>());
        }
        return  iUnlockDic[key];
    }

    private static Dictionary<Collider, Customer> customerDic = new Dictionary<Collider, Customer>();

    public static Customer getCustomer(Collider key)
    {
        if (!customerDic.ContainsKey(key))
        {
            customerDic.Add(key, key.GetComponent<Customer>());
        }
        return customerDic[key];
    }
}
