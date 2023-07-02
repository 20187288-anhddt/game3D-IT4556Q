using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Iap : UI_Canvas
{
    [SerializeField] private Button btn_Close;

    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(nameUI); });
    }
    public override void Open()
    {
        base.Open();
        UI_Manager.Instance.AddUI_To_Stack_UI_Open(this);
    }
    public override void Close()
    {
        base.Close();
        UI_Manager.Instance.ReMoveUI_To_Stack_UI_Open();
    }
    public void BuySuperPack()
    {

    }
}
