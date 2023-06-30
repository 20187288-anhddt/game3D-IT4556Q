using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Static : UI_Canvas
{
    [SerializeField] private MoneyUI moneyUI;//sau thay the bang 1 list 
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

}
