using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Utilities.Components;
using MoreMountains.NiceVibrations;

public class DataSettingsController : Singleton<DataSettingsController>
{
    public DataSetting dataSetting;
    public void Start()
    {
        OnInIt();
    }

    public void OnInIt()
    {
        dataSetting.InItData();
        CheckDataSetting();
    }
    private void CheckDataSetting()
    {
        foreach(DataDictInfoSetting dataDictInfoSetting in dataSetting.GetDataDictInfoSettings())
        {
            if (dataDictInfoSetting.isOn)
            {
                switch (dataDictInfoSetting.name_SettingChild)
                {
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_JoyStick:
                        UI_Manager.Instance.ActiveLook_Joystick();
                        break;
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_Vibration:
                        MMVibrationManager.SetHapticsActive(true);
                        break;
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_Sound:
                        AudioManager.Instance.EnableSFX(true);
                        break;
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_Music:
                        AudioManager.Instance.EnableMusic(true);
                        break;
                }
            }
            else
            {
                switch (dataDictInfoSetting.name_SettingChild)
                {
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_JoyStick:
                        UI_Manager.Instance.DeactiveLook_Joystick();
                        break;
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_Vibration:
                        MMVibrationManager.SetHapticsActive(false);
                        break;
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_Sound:
                        AudioManager.Instance.EnableSFX(false);
                        break;
                    case UI_InfoGroupSettingChild.Name_SettingChild.Setting_Music:
                        AudioManager.Instance.EnableMusic(false);
                        break;
                }
            }

        }
      
    }
}
[System.Serializable]
public class DataSetting
{
    public DataInfoSetting dataInfoSetting;
    private bool isInItData = false;
    private string fileName = " ";
    public void InItData()
    {
        SetFileName(nameof(DataSetting));
        LoadData();
        isInItData = true;
    }
    public void SetFileName(string fileName)
    {
        this.fileName = fileName;
    }
    public string GetFileName()
    {
        return fileName;
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(dataInfoSetting);
        File.WriteAllText(Application.persistentDataPath + GetFileName(), json);
    }
    public void LoadData()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + GetFileName()))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/" + GetFileName(), FileMode.Create);
            ResetData();
            file.Dispose();
            SaveData();
        }
        string json = File.ReadAllText(Application.persistentDataPath + GetFileName());
        dataInfoSetting = JsonUtility.FromJson<DataInfoSetting>(json);
    }
    public void ResetData()
    {
        dataInfoSetting.ResetData();
    }
    private void CheckInItData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public List<DataDictInfoSetting> GetDataDictInfoSettings()
    {
        CheckInItData();
        return dataInfoSetting.dataDictInfoSettings;
    }
    public void SetDataInfoSettings(UI_InfoGroupSettingChild.Name_SettingChild name_SettingChild, bool isOn)
    {
        CheckInItData();
        foreach (DataDictInfoSetting dataDictInfoSetting in dataInfoSetting.dataDictInfoSettings)
        {
            if (dataDictInfoSetting.name_SettingChild == name_SettingChild)
            {
                dataDictInfoSetting.isOn = isOn;
            }
        }
        SaveData();
    }
    public DataDictInfoSetting GetDataInfoSettings(UI_InfoGroupSettingChild.Name_SettingChild name_SettingChild)
    {
        CheckInItData();
        foreach (DataDictInfoSetting dataDictInfoSetting in dataInfoSetting.dataDictInfoSettings)
        {
            if (dataDictInfoSetting.name_SettingChild == name_SettingChild)
            {
                return dataDictInfoSetting;
            }
        }
        return null;
    }
}
[System.Serializable]
public class DataInfoSetting
{
    public List<DataDictInfoSetting> dataDictInfoSettings;
    public void ResetData()
    {
        foreach (DataDictInfoSetting dataDictInfoSetting in dataDictInfoSettings)
        {
            dataDictInfoSetting.isOn = true;
        }
    }
}
[System.Serializable]
public class DataDictInfoSetting
{
    public bool isOn;
    public UI_InfoGroupSettingChild.Name_SettingChild name_SettingChild;
}
