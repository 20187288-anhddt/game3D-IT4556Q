using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusObject
{
   // [SerializeField] private GameObject objModelTaget;
    public TypeStatus_IsActive typeStatus;
    public TypeStatus_IsBought typeStatus_IsBought;
    public Level levelThis;
    public enum TypeStatus_IsActive
    {
        Active,
        DeActive
    }
    public enum TypeStatus_IsBought
    {
        Buy,
        Bought,
        NotBuy
    }
    public enum Level
    {
       Level_1,
       Level_2,
       Level_3
    }
}

