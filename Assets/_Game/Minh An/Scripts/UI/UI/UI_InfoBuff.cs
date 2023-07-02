using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfoBuff : UI_Child
{
    public Transform myTransform;
    [SerializeField] private Text txt_Time;
    [SerializeField] private Image iconBuff;
    public GameObject Icon_Machine_Speed;
    public GameObject Icon_Money_Double;
    public GameObject Icon_Player_DoubleSpeed;
    public GameObject Icon_NoShit;
    private float m_TimeBuff;
    private bool isInItInfo = false;
    private System.Action action_StopBuff;
    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        isInItInfo = false;
        if (myTransform == null) { myTransform = this.transform; }
    }
    public void InItInfo(float timeBuff)
    {
        isInItInfo = true;
        if (gameObject.activeInHierarchy)
        {
            m_TimeBuff += timeBuff;
        }
        else
        {
            m_TimeBuff = timeBuff;
        }
    }
    private void LoadUI()
    {
        int minute = (int)m_TimeBuff / 60;
        int second = (int)m_TimeBuff - minute * 60;
        txt_Time.text = "Time out: " + minute + "m " + second + "s";
    }
    private void Update()
    {
        if (isInItInfo)
        {
            if(m_TimeBuff <= 0)
            {
                action_StopBuff?.Invoke();
                DestroyThis();
            }
            else
            {
                m_TimeBuff -= Time.deltaTime;
            }
            LoadUI();
        }
    }
    public override void Close()
    {
        base.Close();
        isInItInfo = false;
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
    public void SetActionStopBuff(System.Action actionStopBuff)
    {
        action_StopBuff = actionStopBuff;
    }
}
