using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus_DoubleSpeed_Player : UI_Bonus
{
    [SerializeField] private Text txt_Time;
    public override void Awake()
    {
        base.Awake();
        Close();
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
    public override void Reward()
    {
        SDK.AdsManager.Instance.ShowRewardVideo("Bonus_Buff_Double_Speed_All_Player", () =>
        {
            base.Reward();
            Set_OnBonus(false);
            EnventManager.TriggerEvent(EventName.Player_Double_Speed_Play.ToString());
            UI_GroupInfoBuffController.Instance.SpawnInfoBuff(UI_GroupInfoBuffController.NameBonusSpawn.Player_Speed, timeBuff,
                StopReward);
        });
       
    }
    public void Reward(float timeBuff, bool IsAds = true)
    {
        if (IsAds)
        {
            SDK.AdsManager.Instance.ShowRewardVideo("Bonus_Buff_Double_Speed_All_Player", () =>
            {
                Set_OnBonus(false);
                EnventManager.TriggerEvent(EventName.Player_Double_Speed_Play.ToString());
                UI_GroupInfoBuffController.Instance.SpawnInfoBuff(UI_GroupInfoBuffController.NameBonusSpawn.Player_Speed, timeBuff,
                    StopReward);
            });

        }
        else
        {
            Set_OnBonus(false);
            EnventManager.TriggerEvent(EventName.Player_Double_Speed_Play.ToString());
            UI_GroupInfoBuffController.Instance.SpawnInfoBuff(UI_GroupInfoBuffController.NameBonusSpawn.Player_Speed, timeBuff,
                StopReward);
        }
    }
    public override void StopReward()
    {
        Debug.Log("Stop Player_Double_Speed_Stop");
        base.StopReward();
        EnventManager.TriggerEvent(EventName.Player_Double_Speed_Stop.ToString());
    }
    public override void Close()
    {
        base.Close();
        isInItTime = false;
    }
}
