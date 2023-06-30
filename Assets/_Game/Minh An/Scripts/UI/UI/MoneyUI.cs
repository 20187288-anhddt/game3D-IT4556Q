using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyUI : UI_Child
{
    [SerializeField] private Text text_Money;

    public void Start()
    {
        OnInIt();
        EnventManager.AddListener(EventName.ReLoadMoney.ToString(), LoadTextGemInData);
    }
    public  override void OnInIt()
    {
        base.OnInIt();
        LoadTextGemInData();
    }
    public void LoadTextGemInData()
    {
        int value = DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD);
        if (value > 1000)
        {
            float x = value / 1000;
            text_Money.text = "   " + (x + ((value - 1000 * x) / 1000)).ToString("F2") + "K";
            text_Money.text = text_Money.text.Replace(",", ".");
        }
        else if (value > 100)
            text_Money.text = "   " + string.Format("{000}", value);
        else
            text_Money.text = "   " + string.Format("{00}", value);
        Invoke(nameof(Open), 0.1f);
    }
    public override void Open()
    {
        base.Open();
        Close();
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }
}
