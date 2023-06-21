using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Components;

public class Canvas_Settings : UI_Canvas
{
    private static Canvas_Settings instance;
    public static Canvas_Settings Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Canvas_Settings>();
            }
            return instance;
        }
    }
    [SerializeField] private Button btn_Close;
    [SerializeField] private List<UI_InfoGroupSettingChild> uI_InfoGroupSettingChildrens;
    private void Awake()
    {
        OnInIt();
        LoadData();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[20], 1, false);
            UI_Manager.Instance.CloseUI(nameUI); });
        DataSettingsController.Instance.dataSetting.InItData();
    }
    private void OnEnable()
    {
        LoadData();
    }
    private void OnDisable()
    {
        foreach(UI_InfoGroupSettingChild uI_InfoGroupSettingChild in uI_InfoGroupSettingChildrens)
        {
            DataSettingsController.Instance?.dataSetting.SetDataInfoSettings(uI_InfoGroupSettingChild.name_SettingChild, uI_InfoGroupSettingChild.isOn);
        }
    }
    public override void Open()
    {
        base.Open();
        UI_Manager.Instance.AddUI_To_Stack_UI_Open(this);
    }
    public override void Close()
    {
        base.Close();
        UI_Manager.Instance.ReMoveUI_To_Stack_UI_Open();
    }
    public void LoadData()
    {
        foreach(UI_InfoGroupSettingChild uI_InfoGroupSettingChild in uI_InfoGroupSettingChildrens)
        {
            uI_InfoGroupSettingChild.InItData(DataSettingsController.Instance.dataSetting.GetDataInfoSettings(uI_InfoGroupSettingChild.name_SettingChild).isOn);
        }
    }
}

