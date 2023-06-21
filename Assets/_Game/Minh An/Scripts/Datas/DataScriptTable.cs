using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InfoBuy
{
    public TypeCost typeCost;
    public int value;
}
public enum TypeCost
{
    WatchVideo,
    Money,
}
