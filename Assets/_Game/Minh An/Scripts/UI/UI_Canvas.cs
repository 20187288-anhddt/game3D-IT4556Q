//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Utilities.Components;
public class UI_Canvas : MonoBehaviour
{
    public NameUI nameUI;
    public Canvas canvasThis;
    public virtual void OnInIt()
    {
        if(canvasThis == null) { canvasThis = this.GetComponent<Canvas>(); }
    }
    public virtual void Open()
    {
        gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[20], 1, false);
    }
    public virtual void Close()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[20], 1, false);
    }
    public bool IsOpend()
    {
        if (gameObject.activeSelf)
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
    Canvas_Bonus,
    Canvas_LoadAnim,
    Canvas_Tutorial,
}
