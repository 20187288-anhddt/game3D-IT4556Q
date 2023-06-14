using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILock 
{
    bool IsLock { get; set; }
    void UnLock(bool isPushEvent, bool isPlayAnimUnlock);
    float DefaultCoin { get;  }
    float CurrentCoin { get; set; }
}
