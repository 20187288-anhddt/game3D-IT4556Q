using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Notion : MonoBehaviour
{
    [SerializeField] private GameObject obj_NoInternet;
    [SerializeField] private GameObject obj_AdsNotReady;
    [SerializeField] private GameObject obj_Noti;

    public void LoadNoti(NameNoti nameNoti)
    {
        switch (nameNoti)
        {
            case NameNoti.No_Internet:
                obj_NoInternet.SetActive(true);
                obj_AdsNotReady.SetActive(false);
                break;
            case NameNoti.Advertisement_is_not_ready:
                obj_NoInternet.SetActive(false);
                obj_AdsNotReady.SetActive(true);
                break;
        }
    }
    public bool IsOpenNotion()
    {
        return obj_Noti.activeSelf;
    }
    public bool IsCloseNotion()
    {
        return !IsOpenNotion();
    }
    public void OpenNotion(NameNoti nameNoti)
    {
        obj_Noti.SetActive(true);
        LoadNoti(nameNoti);
    }
    public void CloseNotion()
    {
        obj_Noti.SetActive(false);
    }
    public enum NameNoti
    {
        No_Internet,
        Advertisement_is_not_ready
    }
}
