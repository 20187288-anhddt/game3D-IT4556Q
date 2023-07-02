using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyUI : UI_Child
{
    [SerializeField] private Text text_Money;
    [SerializeField] private UI_InfoAddMoney infoAddMoney;
    private int valueMoney_ob = 0;
    private int MoneyTemp = 0;
    private float m_timeShowInfoAddMoney;
    private bool isInIt = false;
    public void Awake()
    {
        CheckInIt();
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.ReLoadMoney.ToString(), LoadTextGemInData);
    }
    public  override void OnInIt()
    {
        base.OnInIt();
        valueMoney_ob = DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD);
       // LoadTextGemInData();
        isInIt = true;
    }
    private void CheckInIt()
    {
        if (!isInIt)
        {
            OnInIt();
            LoadTextGemInData();
            isInIt = true;
        }
    }
    private void Update()
    {
        if(MoneyTemp != 0)
        {
            if (MoneyTemp < 0)
            {
                infoAddMoney.Show(-MoneyTemp, UI_InfoAddMoney.TypeShow.RemoveMoney);
                //valueMoney_ob = DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD);
            }
            else if (MoneyTemp > 0)
            {
                infoAddMoney.Show(MoneyTemp, UI_InfoAddMoney.TypeShow.AddMoney);
               
            }
            if (m_timeShowInfoAddMoney < 0.5f)
            {
                m_timeShowInfoAddMoney += Time.deltaTime;
            }
            else
            {
                MoneyTemp = 0;
                valueMoney_ob = DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD);
            }
        }
       
      
    }
    public void LoadTextGemInData()
    {
        CheckInIt();
        m_timeShowInfoAddMoney = 0;
        int value = DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD);
        MoneyTemp = value - valueMoney_ob;
        if (value > 1000)
        {
            float x = value / 1000;
            text_Money.text = "   " + (x + ((value - 1000 * x) / 1000)).ToString("F2") + "K" + " ";
            text_Money.text = text_Money.text.Replace(",", ".");
        }
        else if (value > 100)
            text_Money.text = "   " + string.Format("{000}", value) + " ";
        else if(value >= 10)
            text_Money.text = "    " + string.Format("{00}", value) + " ";
        else
        {
            text_Money.text = "     " + string.Format("{0}", value) + "  ";
        }
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
