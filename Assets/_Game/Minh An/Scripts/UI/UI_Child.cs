using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Child : MonoBehaviour
{
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
