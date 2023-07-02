using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_Bonus_BuffMoney : DataBase
{
    private static string NameDataProcessBonusMoneyBuff = "DataProcessBonusMoneyBuff";
    public int moneyBuff = 0;
    public int level_MoneyBuff = 0;
    private bool isInItData = false;
    private void Start()
    {
        EnventManager.AddListener(EventName.ClearData.ToString(), ClearData);
    }
    public void InItData()
    {
        SetFileName(nameof(Data_Bonus_BuffMoney));
        isInItData = true;
        LoadData();
    }
    public void CheckInItData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(NameDataProcessBonusMoneyBuff, level_MoneyBuff);
    }
    public override void LoadData()
    {
        base.LoadData();
        level_MoneyBuff = PlayerPrefs.GetInt(NameDataProcessBonusMoneyBuff);
    }
    public override void ResetData()
    {
        base.ResetData();
        moneyBuff = 100;
        level_MoneyBuff = 1;
    }
    public void ClearData()
    {
        CheckInItData();
        ResetData();
        SaveData();
        LoadData();
    }
    public int GetMoneyBuff()
    {
        CheckInItData();
        LoadData();
        LoadBonus:
        InfoBonusMoneyBuff infoBonusMoneyBuff;
        infoBonusMoneyBuff = (InfoBonusMoneyBuff)Resources.Load("Data_ScriptTable\\Bonus\\MoneyBuff\\Process " + level_MoneyBuff, typeof(InfoBonusMoneyBuff));
        if(infoBonusMoneyBuff == null)
        {
            level_MoneyBuff--;
            goto LoadBonus;
        }
        moneyBuff = infoBonusMoneyBuff.value;
        return moneyBuff; 
    }
    public void NextProcessMoneyBuff()
    {
        CheckInItData();
        level_MoneyBuff++;
        SaveData();
        GetMoneyBuff();
        //LoadData();
    }
}
