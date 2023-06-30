using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InfoBuff : UI_Child
{
    public Transform myTransform;
    [SerializeField] private Text txt_Time;
    [SerializeField] private Image iconBuff;
    private float m_TimeBuff;
    private bool isInItInfo = false;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    private void OnEnable()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        isInItInfo = false;
    }
    public void InItInfo(float timeBuff)
    {
        isInItInfo = true;
        m_TimeBuff = timeBuff;
    }
    private void LoadUI()
    {
        txt_Time.text = "Time out: " + (int)m_TimeBuff + "s";
    }
    private void Update()
    {
        if (isInItInfo)
        {
            if(m_TimeBuff <= 0)
            {
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
}
