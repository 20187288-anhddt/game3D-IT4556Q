using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus_BuffMoney : UI_Bonus
{
    [SerializeField] private Text txt_Time;
    [SerializeField] private Text txt_Money;
    private int moneyBuff;
    public override void Awake()
    {
        base.Awake();
        Close();
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.ReLoadBonusMoneyBuff.ToString(), LoadMoneyBuff);
    }
    private void OnEnable()
    {
        LoadMoneyBuff();
    }
    private void LoadMoneyBuff()
    {
        moneyBuff = DataManager.Instance.GetData_Bonus_BuffMoney().GetMoneyBuff();
        if (moneyBuff > 1000)
        {
            float x = moneyBuff / 1000;
            txt_Money.text = "+" + (x + ((moneyBuff - 1000 * x) / 1000)).ToString("F2") + "K";
            txt_Money.text = txt_Money.text.Replace(",", ".");
        }
        else if (moneyBuff > 100)
            txt_Money.text = "+" + string.Format("{000}", moneyBuff);
        else
            txt_Money.text = "+" + string.Format("{00}", moneyBuff);
    }
    public void LoadUI()
    {
        if (timeSecond >= 10 || timeSecond < 1)
        {
            txt_Time.text = ((int)timeSecond).ToString() + "s";
        }
        else
        {
            txt_Time.text = "0" + ((int)timeSecond).ToString() + "s";
        }

    }
    //private void Update()
    //{
    //    if (isInItTime)
    //    {
    //        if (timeSecond > 0)
    //        {
    //            timeSecond -= Time.deltaTime;
    //        }
    //        if (timeSecond <= 0)
    //        {
    //            timeSecond = 0;
    //            Close();
    //        }
    //        LoadUI();
    //    }

    //}
    public override void Reward()
    {
        SDK.AdsManager.Instance.ShowRewardVideo("Bonus_Buff_AddMoney", () =>
        {
            base.Reward();
            DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
                DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) +
                moneyBuff);
            Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
            parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
            parameters[1] = new Firebase.Analytics.Parameter("value", moneyBuff);
            parameters[2] = new Firebase.Analytics.Parameter("source", "Bonus_Buff_AddMoney");
            SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("earn_virtual_currency", parameters);
        });
       // Set_OnBonus(false);
    }
    public override void StopReward()
    {
        //base.StopReward();
       // Set_OnBonus(true);
    }
    public override void Close()
    {
        base.Close();
        isInItTime = false;
    }
}
