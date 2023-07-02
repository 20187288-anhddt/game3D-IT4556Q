using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus_DoubleMoney : UI_Bonus
{
    [SerializeField] private Text txt_Time;
    public override void Awake()
    {
        base.Awake();
        On_Bonus = true;
        Close();
    }
   
    public void LoadUI()
    {
        if(timeSecond >= 10 || timeSecond < 1)
        {
            txt_Time.text = ((int)timeSecond).ToString() + "s";
        }
        else
        {
            txt_Time.text = "0" + ((int)timeSecond).ToString() + "s";
        }
       
    }
    private void Update()
    {
        if (isInItTime)
        {
            if (timeSecond > 0)
            {
                timeSecond -= Time.deltaTime;
            }
            if (timeSecond <= 0)
            {
                timeSecond = 0;
                Close();
            }
            LoadUI();
        }
       
    }
    public override void Close()
    {
        base.Close();
        isInItTime = false;
    }
    public override void Reward()
    {
        base.Reward();
        //DataManager.Instance.GetDataMoneyController().AddMoney(Money.TypeMoney.USD,
        //     DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD));
        UI_GroupInfoBuffController.Instance.SpawnInfoBuff(UI_GroupInfoBuffController.NameBonusSpawn.Money_Double, timeBuff,
            EventBounsController.Instance.GetUIBonus(TypeBonus.DoubleSpeed_Player).StopReward);
        Set_OnBonus(false);
        EnventManager.TriggerEvent(EventName.OnEventDoubleMoney.ToString());
    }
    public override void StopReward()
    {
        base.StopReward();
        Set_OnBonus(true);
        EnventManager.TriggerEvent(EventName.OffEventDoubleMoney.ToString());
    }
}
