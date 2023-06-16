using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusObject
{
   // [SerializeField] private GameObject objModelTaget;
    public TypeStatus_IsActive typeStatus;
    public TypeStatus_IsBought typeStatus_IsBought;
    public Level levelThis = Level.Level_1;
    public int amountPaid = 0;

    public int GetLevelThis()
    {
        int level = 0;
        switch (levelThis)
        {
            case Level.Level_1:
                level = 1;
                break;
            case Level.Level_2:
                level = 2;
                break;
            case Level.Level_3:
                level = 3;
                break;
        }
        return level;
    }
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

