using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus_Machine_Speed : UI_Bonus
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
        base.Reward();
        Set_OnBonus(false);
        EnventManager.TriggerEvent(EventName.Machine_Double_Speed_Play.ToString());
        UI_GroupInfoBuffController.Instance.SpawnInfoBuff(UI_GroupInfoBuffController.NameBonusSpawn.Machine_Speed, timeBuff, StopReward);
        Debug.Log("On Play Machine double Speed");
    }
    public override void StopReward()
    {
        base.StopReward();
        EnventManager.TriggerEvent(EventName.Machine_Double_Speed_Stop.ToString());
        Debug.Log("On Stop Machine double Speed");
    }
    public override void Close()
    {
        base.Close();
        isInItTime = false;
    }
}
