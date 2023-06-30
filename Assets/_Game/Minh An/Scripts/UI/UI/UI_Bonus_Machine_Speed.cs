using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Bonus_Machine_Speed : UI_Bonus
{
    
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
         
        }

    }
    public override void Reward()
    {
        base.Reward();
        Set_OnBonus(false);
        EnventManager.TriggerEvent(EventName.Machine_Double_Speed_Play.ToString());
        UI_GroupInfoBuffController.Instance.SpawnInfoBuff(timeBuff);
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
