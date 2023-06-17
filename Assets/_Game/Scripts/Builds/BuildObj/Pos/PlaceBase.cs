using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBase : MonoBehaviour
{
    public ClosetBase closet;
    public bool isHaveCus;
    public bool cusMoving;
    public float delayTime;
    public bool haveOutFit;
    public Customer curCus;
    public IngredientType type;

    public virtual void AddCus(Customer customer)
    {
        
    }
}
