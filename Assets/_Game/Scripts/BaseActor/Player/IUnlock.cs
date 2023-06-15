using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IUnlock 
{
    void UnlockMap(float coin);
    float CoinValue { get; set; }
}