using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Static : UI_Canvas
{
    private static Canvas_Static instance;
    public static Canvas_Static Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Canvas_Static>();
            }
            return instance;
        }
    }
    [SerializeField] private MoneyUI moneyUI;//sau thay the bang 1 list 
    [SerializeField] private UI_Notion notion;
    private float timeClose_NoInterNet = 1;
    private float m_time;
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
    private void Update()
    {
        if (notion.IsOpenNotion())
        {
            if (m_time < timeClose_NoInterNet)
            {
                m_time += Time.deltaTime;
            }
            else
            {
                m_time = 0;
                notion.CloseNotion();
            }
        }
    }     
    public UI_Notion GetNotion()
    {
        return notion;
    }
}
