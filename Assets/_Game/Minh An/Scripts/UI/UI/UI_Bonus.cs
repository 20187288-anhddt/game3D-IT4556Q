using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bonus : UI_Child
{
    public Transform myTransform;
    public TypeBonus typeBonus;
    [SerializeField] private Button btn_This;
    protected float timeSecond = 0;
    protected bool isInItTime = false;
    [SerializeField] protected bool On_Bonus = false;
    protected float timeBuff;
    public void InItTimeSecond(float time)
    {
        timeSecond = time;
        isInItTime = true;
    }

    public virtual void Awake()
    {
        OnInit();
    }
    private void OnInit()
    {
        if (myTransform == null) { myTransform = this.transform; }
        btn_This.onClick.AddListener(Reward);
    }
    public void Active(bool value)
    {
        gameObject.SetActive(value);
    }
    public virtual void Reward()
    {
        EventBounsController.Instance.RewardTrigger(this);
        Close();
    }
    public void Set_OnBonus(bool value)
    {
        On_Bonus = value;
    }
    public bool Get_OnBonus()
    {
        return On_Bonus;
    }
    public virtual void StopReward()
    {

    }
    public void SetTimeBuff(float value)
    {
        timeBuff = value;
    }
}
public enum TypeBonus
{
    MachineSpeed,
    Money_Double,
    Money_Buff,
    DoubleSpeed_Player
}
