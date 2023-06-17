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
        text_Money.text = DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD).ToString();
    }
}
