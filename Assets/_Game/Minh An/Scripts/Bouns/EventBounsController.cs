using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBounsController : Singleton<EventBounsController>
{
    [SerializeField] private List<InfoBouns> infoBouns;
    public List<float> timeShows;
    [Header("Loop")]
    public bool isLoop;
    public float timeLoop;
    private DataBonus dataBonus = new DataBonus();
    private InfoBouns infoBouns_Taget;
    [SerializeField] float m_TimeCheck = 0;
    private int indexTaget = 0;
    [SerializeField] float m_timeLoop;
    [SerializeField] float m_timeShow;

    public override void Awake()
    {
        base.Awake();
        OnInIt();
    }
    private void OnInIt()
    {
        dataBonus.InItData();
        m_TimeCheck = 0;
        indexTaget = 0;
        m_timeLoop = timeLoop;
        m_timeShow = timeShows[indexTaget];
        //dataBonus.Set_OnShowBouns(true);
    }
    private void Update()
    {
        if (dataBonus.Get_OnShowBouns())
        {
            m_TimeCheck += Time.deltaTime;
            if (isLoop)
            {
                if (m_TimeCheck >= m_timeLoop)
                {
                    reload_Bonus:
                    infoBouns_Taget = infoBouns[Random.Range(0, infoBouns.Count)];
                    if (!infoBouns_Taget.uI_Bonus.Get_OnBonus())
                    {
                        goto reload_Bonus;
                    }
                    m_timeLoop = timeLoop + infoBouns_Taget.timeEnd_Show_Bonus;
                    ShowBonus(infoBouns_Taget);
                    m_TimeCheck = 0;
                }
            }
            else
            {
                if (m_TimeCheck >= m_timeShow)
                {
                    reload_Bonus:
                    infoBouns_Taget = infoBouns[Random.Range(0, infoBouns.Count)];
                    if (!infoBouns_Taget.uI_Bonus.Get_OnBonus())
                    {
                        goto reload_Bonus;
                    }
                    if (timeShows.Count > indexTaget + 1)
                    {
                        indexTaget++;
                    }
                    else
                    {
                        indexTaget = 0;
                    }
                    m_timeShow = timeShows[indexTaget] + infoBouns_Taget.timeEnd_Show_Bonus;
                    ShowBonus(infoBouns_Taget);
                    m_TimeCheck = 0;
                }
            }
        }
       
    }
  
    //public void ShowBonus(int ID)
    //{
    //    for(int i = 0; i < infoBouns.Count; i++)
    //    {
    //        if(ID == i)
    //        {
    //            infoBouns[ID].uI_Bonus.Active(true);
    //        }
    //        else
    //        {
    //            infoBouns[ID].uI_Bonus.Active(false);
    //        }
    //    }
     
    //}
    public void ShowBonus(InfoBouns infoBonus)
    {
        for (int i = 0; i < infoBouns.Count; i++)
        {
            if (infoBonus != infoBouns[i])
            {
                infoBouns[i].uI_Bonus.Active(false);
            } 
        }
        infoBonus.uI_Bonus.Active(true);
        infoBonus.uI_Bonus.InItTimeSecond(infoBonus.timeEnd_Show_Bonus);
        infoBonus.uI_Bonus.SetTimeBuff(infoBonus.timeEnd_Bonus);
    }
    IEnumerator IE_DelayAction(float m_timeDelay, System.Action action)
    {
        yield return new WaitForSeconds(m_timeDelay);
        action?.Invoke();
    
    }
    public void RewardTrigger(UI_Bonus uI_Bonus)
    {
        foreach(InfoBouns infoBouns in infoBouns)
        {
            if(infoBouns.uI_Bonus.typeBonus == uI_Bonus.typeBonus)
            {
                if (infoBouns.timeEnd_Show_Bonus > 0)
                {
                    Debug.Log(infoBouns.timeEnd_Bonus);
                    StartCoroutine(IE_DelayAction(infoBouns.timeEnd_Bonus, () =>
                    {
                        infoBouns.uI_Bonus.Set_OnBonus(true);
                        infoBouns.uI_Bonus.StopReward();
                    }));
                }
                return;
            }
        }
    }
    public DataBonus GetDataBonus()
    {
        return dataBonus;
    }
} 
[System.Serializable]
public class DataBonus : DataBase
{
    private static string nameData_OnShowBouns = "OnShowBonus";
    [SerializeField] private bool OnShowBouns = false;
    public void InItData()
    {
        SetFileName(nameof(DataBonus));
        LoadData();
    }
    public override void LoadData()
    {
        base.LoadData();
        OnShowBouns = PlayerPrefs.GetInt(nameData_OnShowBouns) == 1 ? true : false;
    }
    public override void SaveData()
    {
        base.SaveData();
        PlayerPrefs.SetInt(nameData_OnShowBouns, OnShowBouns ? 1 : -1);
    }
    public override void ResetData()
    {
        base.ResetData();
    }
    public void Set_OnShowBouns(bool value)
    {
        OnShowBouns = value;
        SaveData();
        LoadData();
    }
    public bool Get_OnShowBouns()
    {
        return OnShowBouns;
    }
}
[System.Serializable]
public class InfoBouns
{
    public UI_Bonus uI_Bonus;
    public float timeEnd_Show_Bonus;
    public float timeEnd_Bonus;
}
