using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataOffUpMoney")]
public class DataOffUpMoney : ScriptableObject
{
    [System.Serializable]
    public struct SettingTime
    {
        public int hour;
        public int minute;
        public int second;
    }
    [System.Serializable]
    public struct SettingMoney
    {
        public int MaxMoney;
        public int MinMoney;
        public int MoneyUpp_Upgrade;
    }

    public SettingTime settingTimeFull;
    public SettingMoney settingMoney;
}
