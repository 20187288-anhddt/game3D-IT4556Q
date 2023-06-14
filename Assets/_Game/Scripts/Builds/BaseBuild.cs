using System.Collections.Generic;
using UnityEngine;
public class BaseBuild : MonoBehaviour 
{
    public bool isLock;
    public bool isKey = false;
    public int IDUnlock;
    public bool unlockAds;
    public float timeDelay;
    public float defaultCoin;
    public virtual void UnLock()
    {
    }
    public virtual void Effect()
    {
    }
}