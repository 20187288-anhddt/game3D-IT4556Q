//using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : GenerticSingleton<UI_Manager>
{
    public List<UI_Canvas> canvasUI;
    private Stack<UI_Canvas> stack_UIOPENs = new Stack<UI_Canvas>();
    public override void Awake()
    {
        base.Awake();
        OnInIt();
    }
    private void OnInIt()
    {
        CloseUI_FULL();
        OpenUI(NameUI.Canvas_Joystick);
        OpenUI(NameUI.Canvas_Static);
        OpenUI(NameUI.Canvas_Home);
        OpenUI(NameUI.Canvas_Bonus);
      
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.OpenUIHome.ToString(), () => { OpenUI(NameUI.Canvas_Home); });
        EnventManager.AddListener(EventName.OpenUIBonus.ToString(), () => { OpenUI(NameUI.Canvas_Bonus); });
    }
    private void Update()
    {
        CheckBack();
    }
    private void CheckBack()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
        }
    }

    public UI_Canvas OpenUI(NameUI nameUI)
    {
       // Debug.Log("Open");
        if (!DataManager.Instance.GetDataUIController().Get_IsOpenCanvasHome() && nameUI == NameUI.Canvas_Home)
        {
            return null;
        }
        if (!DataManager.Instance.GetDataUIController().Get_IsOpenCanvasBonus() && nameUI == NameUI.Canvas_Bonus)
        {
            return null;
        }
        foreach (UI_Canvas UI_canvas in canvasUI)
        {
            if(UI_canvas.nameUI == nameUI)
            {
                UI_canvas.Open();
                return UI_canvas;
            }
        }
        return null;
    }
    public void CloseUI(NameUI nameUI)
    {
        foreach (UI_Canvas UI_canvas in canvasUI)
        {
            if (UI_canvas.nameUI == nameUI)
            {
                UI_canvas.Close();
                return;
            }
        }
    }
    public bool isOpenUI(NameUI nameUI)
    {
        foreach (UI_Canvas UI_canvas in canvasUI)
        {
            if (UI_canvas.nameUI == nameUI)
            {
                return UI_canvas.IsOpend();
            }
        }
        return false;
    }
    public bool isCloseUI(NameUI nameUI)
    {
        foreach (UI_Canvas UI_canvas in canvasUI)
        {
            if (UI_canvas.nameUI == nameUI)
            {
                return UI_canvas.IsClosed();
            }
        }
        return false;
    }
  
    public void CloseUI_FULL()
    {
        foreach (UI_Canvas UI_canvas in canvasUI)
        {
            UI_canvas.Close();
        }
    }
    public void AddUI_To_Stack_UI_Open(UI_Canvas uI_Canvas)
    {
        stack_UIOPENs.Push(uI_Canvas);
    }
    public void ReMoveUI_To_Stack_UI_Open()
    {
        if(stack_UIOPENs.Count > 0)
        {
            stack_UIOPENs.Pop();
        }
       
    }
    public void CloseAll_UI_In_Stack_Open()
    {
        if(stack_UIOPENs.Count <= 0)
        {
            return;
        }
        foreach(UI_Canvas uI_Canvas in stack_UIOPENs)
        {
            uI_Canvas.Close();
            break;
        }
        CloseAll_UI_In_Stack_Open();
    }
}
