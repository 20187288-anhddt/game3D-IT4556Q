using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetBase : BuildObj,ILock
{
    public List<Customer> listCurCus;
    public IngredientType type;
    public int maxObj;
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
}
