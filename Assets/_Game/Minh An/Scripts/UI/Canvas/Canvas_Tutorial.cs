using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Canvas_Tutorial : UI_Canvas
{
    private static Canvas_Tutorial share_CANVASTUTORIAL;
    public static Canvas_Tutorial Share_CANVASTUTORIAL
    {
        get
        {
            if (share_CANVASTUTORIAL == null)
            {
                share_CANVASTUTORIAL = FindObjectOfType<Canvas_Tutorial>();
            }
            return share_CANVASTUTORIAL;
        }
    }
    [SerializeField] private RectTransform Rect_LEFT, Rect_RIGHT, Rect_BOTTOM, Rect_TOP, Rect_CENTER;
    [SerializeField] private RectTransform RectThisFillAllScene;

    [SerializeField] private List<StepTutorial> stepTutorials;
    [SerializeField] private RectTransform parentALL;
    private int IndexProgress = 0;
    private Button btnCurrent;
    private StepTutorial stepTutorialCurrent = new StepTutorial();

    private Action actionComplete;
    private Vector3 vt_CenterCurrent = Vector3.zero;
    //private void Start()
    //{
    //    LoadTutorial(TypeTutorial.Upgrade, null);
    //}
    public void LoadTutorial(TypeTutorial typeTutorial, Action actionComplete)
    {
        vt_CenterCurrent = Vector3.zero;
        this.actionComplete = actionComplete;
        foreach (StepTutorial stepTutoria in stepTutorials)
        {
            if (typeTutorial == stepTutoria.typeTutorial)
            {
                stepTutorialCurrent = stepTutoria;
            }
        }
        if (stepTutorialCurrent == null)
        {
            return;
        }
        if (stepTutorialCurrent.dictButtonAndRectTutorials.Count > 0)
        {
            IndexProgress = 0;
            LoadTutorialTaget(stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].btn_Current.transform.position,
                stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].vt_achoOffset);
            btnCurrent = stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].btn_Current;
        }
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null && Input.GetMouseButtonDown(0))
        {
            btnCurrent?.onClick?.Invoke();
            NextStep();
            return;
        }
        if(btnCurrent != null && Vector3.Distance(vt_CenterCurrent, Rect_CENTER.position) != 0)
        {
            LoadTutorialTaget(btnCurrent.transform.position, stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].vt_achoOffset);
        }
    }
    private void NextStep()
    {
        IndexProgress++;
        if (IndexProgress >= stepTutorialCurrent.dictButtonAndRectTutorials.Count)
        {
            actionComplete?.Invoke();
            Close();

            return;
        }
        //if (Screen.height >= 1920 && IndexProgress == 2)
        //{
        //    stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].vt_Current.y = -400;
        //}
        //LoadTutorialTaget(stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].vt_Current);
        LoadTutorialTaget(stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].btn_Current.transform.position,
            stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].vt_achoOffset);

        btnCurrent = stepTutorialCurrent.dictButtonAndRectTutorials[IndexProgress].btn_Current;
    }
    private void LoadTutorialTaget(Vector3 Pos, Vector2 vt_Offset)
    {
        Rect_CENTER.position = Pos;
        vt_CenterCurrent = Pos;
        //Debug.Log(Rect_CENTER.anchoredPosition);
        //Debug.Log(Pos);
        Rect_CENTER.anchoredPosition = Rect_CENTER.anchoredPosition + vt_Offset;
        //LEFT
        Rect_LEFT.sizeDelta = new Vector2(Mathf.Abs(Rect_CENTER.anchoredPosition.x - Rect_CENTER.rect.width / 2 + RectThisFillAllScene.rect.width / 2), RectThisFillAllScene.rect.height);
        Rect_LEFT.anchoredPosition = new Vector2(-RectThisFillAllScene.rect.width / 2 + Rect_LEFT.rect.width / 2, 0);

        //RIGHT
        Rect_RIGHT.sizeDelta = new Vector2(Mathf.Abs(-Rect_CENTER.anchoredPosition.x + RectThisFillAllScene.rect.width / 2 - Rect_CENTER.rect.width / 2), RectThisFillAllScene.rect.height);
        Rect_RIGHT.anchoredPosition = new Vector2(RectThisFillAllScene.rect.width / 2 - Rect_RIGHT.rect.width / 2, 0);

        //BOTTOM
        Rect_BOTTOM.sizeDelta = new Vector2(Rect_CENTER.rect.width, Mathf.Abs(Rect_CENTER.anchoredPosition.y - Rect_CENTER.rect.height / 2 + RectThisFillAllScene.rect.height / 2));
        Rect_BOTTOM.anchoredPosition = new Vector2(Rect_CENTER.anchoredPosition.x, -RectThisFillAllScene.rect.height / 2 + Rect_BOTTOM.rect.height / 2);

        //TOP
        Rect_TOP.sizeDelta = new Vector2(Rect_CENTER.rect.width, Mathf.Abs(-Rect_CENTER.anchoredPosition.y - Rect_CENTER.rect.height / 2 + RectThisFillAllScene.rect.height / 2));
        Rect_TOP.anchoredPosition = new Vector2(Rect_CENTER.anchoredPosition.x, RectThisFillAllScene.rect.height / 2 - Rect_TOP.rect.height / 2);

    }
    public override void Close()
    {
        base.Close();
        EnventManager.TriggerEvent(EventName.Close_Canvas_Tutorial.ToString());
    }
    public override void Open()
    {
        base.Open();
        EnventManager.TriggerEvent(EventName.Open_Canvas_Tutorial.ToString());
    }
}

[Serializable]
public class DictButtonAndRectTutorial
{
    public Button btn_Current;
    public Vector2 vt_achoOffset;
}
[Serializable]
public class StepTutorial
{
    public TypeTutorial typeTutorial;
    public List<DictButtonAndRectTutorial> dictButtonAndRectTutorials;
}
public enum TypeTutorial
{
    Upgrade,

}
