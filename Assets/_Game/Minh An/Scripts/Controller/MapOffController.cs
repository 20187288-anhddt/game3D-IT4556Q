using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapOffController : MonoBehaviour
{
    [System.Serializable]
    private struct SettingTime
    {
        public int hour;
        public int minute;
        public int second;

        public int GetAllSecondTime()
        {
            int second = 0;
            second = hour * 60 * 60;
            second += minute * 60;
            second += this.second;
            return second;
        }
    }
    [System.Serializable]
    private struct SettingMoney
    {
        public int MaxMoney;
        public int MinMoney;
        public int MoneyUpp_Upgrade;
    }
    private static string NameDataSecondCheck = "DataSecondCheck";
    [SerializeField] private SettingTime settingTimeFull;
    [SerializeField] private SettingMoney settingMoney;
    private DataOffUpMoney dataOffUpMoney;
    private DataTimeOff dataTimeOff = new DataTimeOff();
    private static string pathResouceThis = "Data_ScriptTable\\Offline\\MoneyUp\\Map "; //+ IDMap
    private int m_IDMap = 0;
    private int m_MoneyMaxCurrent;
    private int m_secondTimeFull = 0;
    private int m_SecondCheck = 0;
    private float m_TimeCheck = 0;
    private int Scond_Remaining_Full = 0;
    private bool isOnInIt = false;
    public int MoneyTest = 0;
    [SerializeField] private bool isUnlock = false;
    [SerializeField] private bool isMapCurrent = false;
    //private void Start()
    //{
    //    MoneyTest = GetMoneyCurrent();
    //}
    private void Update()
    {
        if (isUnlock)
        {
            CheckProgressFull();
        }
    }
    private void CheckProgressFull()
    {
        if(m_TimeCheck <= m_secondTimeFull)
        {
            m_TimeCheck += Time.deltaTime;
        }
        if(m_TimeCheck > m_SecondCheck + 1)
        {
            m_SecondCheck++;
        }
    }
    public float GetProgressFull()
    {
        float progress = 0;
        progress = (float)m_SecondCheck / m_secondTimeFull;
        progress = Mathf.Clamp(progress, 0, 1);
        return progress;
    }
    public int GetMoneyCurrent()
    {
        int money = 0;

        money = (int)(GetProgressFull() * m_MoneyMaxCurrent) + MapOffManager.Instance.GetCountUpgradeInMap(m_IDMap) * settingMoney.MoneyUpp_Upgrade;
        return money;
    }
    public int GetMoneyMax()
    {
        return m_MoneyMaxCurrent + MapOffManager.Instance.GetCountUpgradeInMap(m_IDMap) * settingMoney.MoneyUpp_Upgrade;
    }
    public void Load(int IDMap, bool isUnlock, bool isMapCurrent)
    {
        this.m_IDMap = IDMap;
        this.isUnlock = isUnlock;
        this.isMapCurrent = isMapCurrent;
        OnInIt();
    }
    private void OnInIt()
    {
        if (isOnInIt) { return; }
        isOnInIt = true;
        dataTimeOff.OnInIt(m_IDMap + 1);
        dataOffUpMoney = (DataOffUpMoney)Resources.Load(pathResouceThis + (m_IDMap + 1), typeof(DataOffUpMoney));
        settingTimeFull.hour = dataOffUpMoney.settingTimeFull.hour;
        settingTimeFull.minute = dataOffUpMoney.settingTimeFull.minute;
        settingTimeFull.second = dataOffUpMoney.settingTimeFull.second;

        settingMoney.MaxMoney = dataOffUpMoney.settingMoney.MaxMoney;
        settingMoney.MinMoney = dataOffUpMoney.settingMoney.MinMoney;
        settingMoney.MoneyUpp_Upgrade = dataOffUpMoney.settingMoney.MoneyUpp_Upgrade;

        m_secondTimeFull = settingTimeFull.GetAllSecondTime();
        CheckTimeInIt();
        CheckMoneyCurrent();
    }
    public void ReloadTimeCheck()
    {
        m_TimeCheck = 0;
        m_SecondCheck = 0;
        SaveDataSecondCheck();
    }
    public int GetScond_Remaining_Full()
    {
        Scond_Remaining_Full = m_secondTimeFull - m_SecondCheck;
        Scond_Remaining_Full = Mathf.Clamp(Scond_Remaining_Full, 0, m_secondTimeFull);
        return Scond_Remaining_Full;
    }
    private void CheckTimeInIt()
    {
        //timeCompleteLoad
        m_SecondCheck = GetDataSecondCheck();
        //timeoff
        m_SecondCheck += Mathf.Abs((dataTimeOff.GetDateTimeOff() - DateTime.Now).Seconds);
        m_SecondCheck = Mathf.Clamp(m_SecondCheck, 0, m_secondTimeFull);
        m_TimeCheck = m_SecondCheck;
    }
    private void CheckMoneyCurrent()
    {
        m_MoneyMaxCurrent = settingMoney.MinMoney;
    }
    private void OnApplicationQuit()
    {
        dataTimeOff.SetDateTimeOff(DateTime.Now);
        SaveDataSecondCheck();
    }
    private void OnApplicationPause()
    {
        dataTimeOff.SetDateTimeOff(DateTime.Now);
        SaveDataSecondCheck();
    }
    public void SaveDataSecondCheck()
    {
        PlayerPrefs.SetInt(NameDataSecondCheck, m_SecondCheck);
    }
    public int GetDataSecondCheck()
    {
        return PlayerPrefs.GetInt(NameDataSecondCheck, 0);
    }
    public void Collect()
    {
        ReloadTimeCheck();
        DataManager.Instance.GetDataMoneyController().AddMoney(Money.TypeMoney.USD, GetMoneyMax());

        Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
        parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
        parameters[1] = new Firebase.Analytics.Parameter("value", GetMoneyMax());
        parameters[2] = new Firebase.Analytics.Parameter("source", "Purchase_Map" + (m_IDMap + 1) +"_In_Offline");
        SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("earn_virtual_currency", parameters);
    }
    public bool IsUnlock()
    {
        return isUnlock;
    }
    public bool IsMapCurrent()
    {
        return isMapCurrent;
    }
    public int GetIDMap()
    {
        return m_IDMap;
    }
}
public class DataTimeOff : DataBase
{
    private int IDMap;
    private DateTime dateTimeStartOff;
    public void OnInIt(int IDMap)
    {
        
        SetIDMap(IDMap);
        SetFileName(nameof(DataTimeOff) + "Map " + IDMap);
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetString(GetFileName(), dateTimeStartOff.ToString());
    }
    public override void LoadData()
    {
        base.LoadData();
        dateTimeStartOff = DateTime.Parse(PlayerPrefs.GetString(GetFileName()));
    }
    public override void ResetData()
    {
        base.ResetData();
        dateTimeStartOff = DateTime.Now;
    }
    public void SetIDMap(int ID)
    {
        this.IDMap = ID;
    }
    public void SetDateTimeOff(DateTime dateTime)
    {
        dateTimeStartOff = dateTime;
        SaveData();
    }
    public DateTime GetDateTimeOff()
    {
        LoadData();
        return dateTimeStartOff;
    }
}
