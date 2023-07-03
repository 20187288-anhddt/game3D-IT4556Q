using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus_NoShit : UI_Bonus
{
    [SerializeField] private Text txt_Time;
    public override void Awake()
    {
        base.Awake();
        OnInIt();
        Close();
    }
    
    public override void OnInIt()
    {
        base.OnInIt();
        Set_OnBonus(Canvas_Bonus.Instance.GetDataBonusNoShit().IsOnBonus());
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
        SDK.AdsManager.Instance.ShowRewardVideo("Bonus_Buff_NoShit", () => 
        {
            base.Reward();
            Set_OnBonus(false);
            EnventManager.TriggerEvent(EventName.NoShit_Play.ToString());
            UI_GroupInfoBuffController.Instance.SpawnInfoBuff(UI_GroupInfoBuffController.NameBonusSpawn.NoShit, timeBuff, StopReward);
        });
      
    }
    public override void StopReward()
    {
        base.StopReward();
        EnventManager.TriggerEvent(EventName.NoShit_Stop.ToString());
    }
    public override void Close()
    {
        base.Close();
        isInItTime = false;
    }
    public void SetDataOnBonus(bool value)
    {
        Debug.Log(value);
        Canvas_Bonus.Instance.GetDataBonusNoShit().SetIsOnBonus(value);
        Set_OnBonus(Canvas_Bonus.Instance.GetDataBonusNoShit().IsOnBonus());
    }
}

public class DataBonusNoShit : DataBase
{
    private static string NameDataIsOnBonus = "DataBonusNoShit";
    public bool isOnBonus = false;
    private bool isInItData = false;

    public void AddEvent()
    {
        EnventManager.AddListener(EventName.ClearData.ToString(), () =>
        {
            ClearData();
            Debug.Log("ab");
        });
    }
    public void InItData()
    {
        isInItData = true;
        SetFileName(nameof(DataBonusNoShit));
        LoadData();
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(NameDataIsOnBonus, isOnBonus ? 1 : -1);
    }
    public override void LoadData()
    {
        base.LoadData();
        isOnBonus = PlayerPrefs.GetInt(NameDataIsOnBonus) == 1 ? true : false;
    }
    public override void ResetData()
    {
        base.ResetData();
        isOnBonus = false;
    }
    private void CheckInItData()
    {
        if (!isInItData)
        {
            InItData();
        }
    }
    public void SetIsOnBonus(bool value)
    {
        CheckInItData();
        isOnBonus = value;
        SaveData();
    }
    public bool IsOnBonus()
    {
        CheckInItData();
        LoadData();
        return isOnBonus;
    }
    public void ClearData()
    {
        ResetData();
        SaveData();
        Debug.Log(isOnBonus);
    }
}
