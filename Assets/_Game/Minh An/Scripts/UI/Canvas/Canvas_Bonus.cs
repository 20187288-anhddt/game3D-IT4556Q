using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Bonus : UI_Canvas
{
    private static Canvas_Bonus instance;
    public static Canvas_Bonus Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Canvas_Bonus>();
            }
            return instance;
        }
    }
    private DataBonusNoShit dataBonusNoShit = new DataBonusNoShit();

    private void Awake()
    {
        OnInIt();
    }
    private void Start()
    {
        //dataBonusNoShit.AddEvent();
        EnventManager.AddListener(EventName.ClearData.ToString(), () =>
        {
            dataBonusNoShit.ClearData();
        });
    }
    public override void OnInIt()
    {
        base.OnInIt();
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
    public DataBonusNoShit GetDataBonusNoShit()
    {
        return dataBonusNoShit;
    }
}
