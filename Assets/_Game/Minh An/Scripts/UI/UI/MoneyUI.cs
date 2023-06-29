using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text text_Money;

    public void Start()
    {
        OnInIt();
        EnventManager.AddListener(EventName.ReLoadMoney.ToString(), LoadTextGemInData);
    }
    public void OnInIt()
    {
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
    }
}
