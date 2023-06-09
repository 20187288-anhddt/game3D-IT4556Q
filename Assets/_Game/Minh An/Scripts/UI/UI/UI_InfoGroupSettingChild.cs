using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Components;
using MoreMountains.NiceVibrations;

public class UI_InfoGroupSettingChild : UI_Child
{
    public Name_SettingChild name_SettingChild;
    [SerializeField] private Button btn_Touch;
    [SerializeField] private Image img_BG;
    [SerializeField] private Sprite spr_BG_On;
    [SerializeField] private Sprite spr_BG_Off;
    [SerializeField] private RectTransform rectTransform_Touch;
    [SerializeField] private Vector3 posOn;
    [SerializeField] private Vector3 posOff;
    [SerializeField] private float SpeedAnim = 1;
    public bool isOn;
    private void Awake()
    {
        OnInIt();
    }
    public void InItData(bool isOn)
    {
        this.isOn = isOn;
        LoadUI();
    }
    public override void OnInIt()
    {
        btn_Touch.onClick.AddListener(ClickTouch);
        LoadUI();
    }
    public void ClickTouch()
    {
        isOn = !isOn;
        LoadUI();
    }
    public void LoadUI()
    {
        if (isOn)
        {
            TouchOn();
        }
        else
        {
            TouchOff();
        }
    }
    public void TouchOn()
    {
        img_BG.sprite = spr_BG_On;
        StartCoroutine(IE_AnimTouch(true, SpeedAnim));

        switch (name_SettingChild)
        {
            case Name_SettingChild.Setting_JoyStick:
                UI_Manager.Instance.ActiveLook_Joystick();
                break;
            case Name_SettingChild.Setting_Vibration:
                MMVibrationManager.SetHapticsActive(true);
                break;
            case Name_SettingChild.Setting_Sound:
                AudioManager.Instance.EnableSFX(true);
                break;
            case Name_SettingChild.Setting_Music:
                AudioManager.Instance.EnableMusic(true);
                break;
        }
       
    }
    public void TouchOff()
    {
        img_BG.sprite = spr_BG_Off;
        StartCoroutine(IE_AnimTouch(false, SpeedAnim));

        switch (name_SettingChild)
        {
            case Name_SettingChild.Setting_JoyStick:
                UI_Manager.Instance.DeactiveLook_Joystick();
                break;
            case Name_SettingChild.Setting_Vibration:
                MMVibrationManager.SetHapticsActive(false);
                break;
            case Name_SettingChild.Setting_Sound:
                AudioManager.Instance.EnableSFX(false);
                break;
            case Name_SettingChild.Setting_Music:
                AudioManager.Instance.EnableMusic(false);
                break;
        }
    }
    IEnumerator IE_AnimTouch(bool isOn, float speed = 1)
    {
        float m_time = 0;
        Vector3 posStart = rectTransform_Touch.anchoredPosition3D;
        Vector3 posEnd;
        if (isOn)
        {
            posEnd = posOn;
        }
        else
        {
            posEnd = posOff;
        }
        while (m_time < 1)
        {
            rectTransform_Touch.anchoredPosition3D = Vector3.Lerp(posStart, posEnd, m_time);
            m_time += Time.deltaTime * speed;
            yield return null;
        }
    }
    public enum Name_SettingChild
    {
        Setting_JoyStick,
        Setting_Vibration,
        Setting_Sound,
        Setting_Music
    }
}
