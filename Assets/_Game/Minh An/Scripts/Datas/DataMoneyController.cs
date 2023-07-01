using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataMoneyController : DataBase
{
    public MoneyData moneyData;
    private bool isDoubleAddMoney = false;
    public void Awake()
    {
        InItData();
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.OnEventDoubleMoney.ToString(), OnDoubleMoneyAdd);
        EnventManager.AddListener(EventName.OffEventDoubleMoney.ToString(), OffDoubleMoneyAdd);
    }
    public void InItData()
    {
        isDoubleAddMoney = false;
        SetFileName(nameof(DataMoneyController));
        LoadData();
    }

    public override void SaveData()
    {
        base.SaveData();
        string json = JsonUtility.ToJson(moneyData);
        File.WriteAllText(Application.persistentDataPath + "/" + GetFileName(), json);
    }
    public override void LoadData()
    {
        base.LoadData();
        string json = File.ReadAllText(Application.persistentDataPath + "/" + GetFileName());
        MoneyData dataSave = JsonUtility.FromJson<MoneyData>(json);
        moneyData = dataSave;
    }
    public override void ResetData()
    {
        base.ResetData();
        moneyData.ResetData();
    }
    public void AddMoney(Money.TypeMoney typeMoney, int value)
    {
        if (isDoubleAddMoney)
        {
            value *= 2;
        }
        moneyData.AddMoney(typeMoney, value);
        SaveData();
        LoadData();
        EnventManager.TriggerEvent(EventName.ReLoadMoney.ToString());
    }
    public void RemoveMoney(Money.TypeMoney typeMoney, int value)
    {
        moneyData.RemoveMoney(typeMoney, value);
        SaveData();
        LoadData();
        EnventManager.TriggerEvent(EventName.ReLoadMoney.ToString());
    }
    public void SetMoney(Money.TypeMoney typeMoney, int value)
    {
        moneyData.SetMoney(typeMoney, value);
        SaveData();
        LoadData();
        EnventManager.TriggerEvent(EventName.ReLoadMoney.ToString());
    }
    public int GetMoney(Money.TypeMoney typeMoney)
    {
        LoadData();
        return moneyData.GetMoney(typeMoney);
    }
    private void OnDoubleMoneyAdd()
    {
        isDoubleAddMoney = true;
        Debug.Log("ON Double Add Money");
    }
    private void OffDoubleMoneyAdd()
    {
        isDoubleAddMoney = true;
        Debug.Log("OFF Double Add Money");
    }
}
[System.Serializable]
public class MoneyData
{
    public List<Money> moneys;
    public List<Money> GetMoneys()
    {
        return moneys;
    }
    public void AddMoney(Money.TypeMoney typeMoney, int value)
    {
        foreach(Money money in moneys)
        {
            if(money.typeMoney == typeMoney)
            {
                money.AddMoney(value);
            }
        }
    }
    public void RemoveMoney(Money.TypeMoney typeMoney, int value)
    {
        foreach (Money money in moneys)
        {
            if (money.typeMoney == typeMoney)
            {
                money.RemoveMoney(value);
            }
        }
    }
    public void SetMoney(Money.TypeMoney typeMoney, int value)
    {
        foreach (Money money in moneys)
        {
            if (money.typeMoney == typeMoney)
            {
                money.SetMoney(value);
            }
        }
    }
    public int GetMoney(Money.TypeMoney typeMoney)
    {
        foreach (Money money in moneys)
        {
            if (money.typeMoney == typeMoney)
            {
                return money.GetMoney();
            }
        }
        return 0;
    }
    public void ResetData()
    {
        foreach(Money money in moneys)
        {
            money.ResetData();
        }
    }
}
[System.Serializable]
public class Money
{
    public TypeMoney typeMoney;
    public int value;
    public void AddMoney(int value)
    {
        this.value += value;
    }
    public void RemoveMoney(int value)
    {
        this.value -= value;
    }
    public void SetMoney(int value)
    {
        this.value = value;
    }
    public int GetMoney()
    {
        return this.value;
    }
    public void ResetData()
    {
        value = 200;
    }
    public enum TypeMoney
    {
        USD
    }
}
