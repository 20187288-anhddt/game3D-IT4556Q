using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_NextMap : UI_Child
{
    [SerializeField] private Button Btn_No;
    [SerializeField] private Button Btn_Yes;
    [SerializeField] private Button Btn_Close;
    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        AddEvent();
    }
    private void AddEvent()
    {
        Btn_No.onClick.AddListener(ClickNo);
        Btn_Yes.onClick.AddListener(ClickYes);
        Btn_Close.onClick.AddListener(Close);
    }
    public void ClickYes()
    {
        Close();
        EnventManager.TriggerEvent(EventName.On_NextMap.ToString());
    }
    public void ClickNo()
    {
        Close();
    }
}
