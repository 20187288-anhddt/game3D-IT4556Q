using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

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
    public DataSetting dataSetting;
    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(nameUI); });
        dataSetting.InItData();
    }
    private void OnEnable()
    {
        LoadData();
    }
    private void OnDisable()
    {
        foreach(UI_InfoGroupSettingChild uI_InfoGroupSettingChild in uI_InfoGroupSettingChildrens)
        {
            dataSetting.SetDataInfoSettings(uI_InfoGroupSettingChild.name_SettingChild, uI_InfoGroupSettingChild.isOn);
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
            uI_InfoGroupSettingChild.InItData(dataSetting.GetDataInfoSettings(uI_InfoGroupSettingChild.name_SettingChild).isOn);
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
            if(dataDictInfoSetting.name_SettingChild == name_SettingChild)
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
