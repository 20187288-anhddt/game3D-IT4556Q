//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class UI_Canvas : MonoBehaviour
{
    public NameUI nameUI;
  
    public virtual void OnInIt()
    {

    }
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    public bool IsOpend()
    {
        if (gameObject.activeInHierarchy)
        {
            return true;
        }
        return false;
    }
    public bool IsClosed()
    {
        return !IsOpend();
    }
  
}
public enum NameUI
{
    Canvas_Static,
    Canvas_Home,
    Canvas_Joystick,
    Canvas_Settings,
    Canvas_Upgrades,
    Canvas_Iap,
    Canvas_Customize,
    Canvas_Order,
    Canvas_Bonus
}
