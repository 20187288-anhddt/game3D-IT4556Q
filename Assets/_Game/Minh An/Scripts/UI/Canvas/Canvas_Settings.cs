using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Settings : UI_Canvas
{
    [SerializeField] private Button btn_Close;

    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(NameUI.Canvas_Settings); });
    }

}
