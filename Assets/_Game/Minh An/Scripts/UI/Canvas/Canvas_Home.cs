using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Home : UI_Canvas
{
    private static Canvas_Home instance;
    public static Canvas_Home Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Canvas_Home>();
            }
            return instance;
        }
    }
    [SerializeField] private Button btn_Settings;
    [SerializeField] private Button btn_Customize;
    [SerializeField] private Button btn_Iap;
    [SerializeField] private Button btn_Oder;
    [SerializeField] private Text txt_Time_Order;
    private System.Action actionClickShowOder;

    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Settings.onClick.AddListener(Open_Settings);
        btn_Customize.onClick.AddListener(Open_Customize);
        btn_Iap.onClick.AddListener(Open_Iap);
        btn_Oder.onClick.AddListener(Open_UI_Oder);
        NotShow_Btn_Oder();
    }
    public void Open_Settings()
    {
        UI_Manager.Instance.OpenUI(NameUI.Canvas_Settings);
    }
    public void Open_Customize()
    {
        UI_Manager.Instance.OpenUI(NameUI.Canvas_Customize);
    }
    public void Open_Iap()
    {
        UI_Manager.Instance.OpenUI(NameUI.Canvas_Iap);
    }
    public void Open_UI_Oder()
    {
        UI_Manager.Instance.OpenUI(NameUI.Canvas_Order);
        actionClickShowOder?.Invoke();
    }
    public void Show_Btn_Oder(System.Action actionOpen_Oder)
    {
        btn_Oder.gameObject.SetActive(true);
        actionClickShowOder = actionOpen_Oder;
    }
    public void NotShow_Btn_Oder()
    {
        btn_Oder.gameObject.SetActive(false);
    }
    public void LoadTextTimeOder(int valueSecond)
    {
        int minute = valueSecond / 60;
        int second = valueSecond - minute * 60;
        txt_Time_Order.text = minute + "m " + second + "s";
    }
}
