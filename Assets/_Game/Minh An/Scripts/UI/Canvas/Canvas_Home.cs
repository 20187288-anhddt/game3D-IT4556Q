using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Home : UI_Canvas
{
    [SerializeField] private Button btn_Settings;
    [SerializeField] private Button btn_Shop;
    [SerializeField] private Button btn_Iap;

    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Settings.onClick.AddListener(Open_Settings);
        btn_Shop.onClick.AddListener(Open_Shop);
        btn_Iap.onClick.AddListener(Open_Iap);
    }
    public void Open_Settings()
    {
        UI_Manager.Instance.OpenUI(NameUI.Canvas_Settings);
    }
    public void Open_Shop()
    {
        //UI_Manager.Instance.OpenUI(NameUI.Ca);
    }
    public void Open_Iap()
    {
        //UI_Manager.Instance.OpenUI(NameUI.Canvas_Settings);
    }
}
