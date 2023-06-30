using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUIController : DataBase
{
    private static string name_IsOpen_CanvasHome = "Is_Open_CanvasHome";
    private static string name_IsOpen_UIUpgrade = "Is_Open_UIUpgrade";
    private bool isOpenCanvasHome = false;
    private bool isOpen_UI_Upgrade = false;
    private bool isInItData = false;

    private void Awake()
    {
        CheckInItData();
    }
    private void InItData()
    {
        SetFileName(nameof(DataUIController));
        LoadData();
        isInItData = true;
    }
    public void CheckInItData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public override void LoadData()
    {
        Debug.Log("Load Data");
        base.LoadData();
        isOpenCanvasHome = (PlayerPrefs.GetInt(name_IsOpen_CanvasHome) == -1) ? false : true;
        isOpen_UI_Upgrade = (PlayerPrefs.GetInt(name_IsOpen_UIUpgrade) == -1) ? false : true;
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(name_IsOpen_CanvasHome, isOpenCanvasHome ? 1 : -1);
        PlayerPrefs.SetInt(name_IsOpen_UIUpgrade, isOpen_UI_Upgrade ? 1 : -1);
    }
    public override void ResetData()
    {
        base.ResetData();
        isOpenCanvasHome = false;
        isOpen_UI_Upgrade = false;
    }
    public bool Get_IsOpenCanvasHome()
    {
        // LoadData();
        CheckInItData();
        return isOpenCanvasHome;
    }
    public void Set_IsOpenCanvasHome(bool value)
    {
        isOpenCanvasHome = value;
        SaveData();
        LoadData();
        if (isOpenCanvasHome)
        {
            EnventManager.TriggerEvent(EventName.OpenUIHome.ToString());
        }
    }
    public bool Get_IsOpenUIUpgrade()
    {
        CheckInItData();
        return isOpen_UI_Upgrade;
    }
    public void Set_IsOpenUIUpgrade(bool value)
    {
        isOpen_UI_Upgrade = value;
        SaveData();
        LoadData();
        if (isOpen_UI_Upgrade)
        {
            EnventManager.TriggerEvent(EventName.Show_BtnUpgrade.ToString());
        }
    }
}
