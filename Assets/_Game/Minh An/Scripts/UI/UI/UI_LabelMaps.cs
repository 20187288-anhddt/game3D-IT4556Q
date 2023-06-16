using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LabelMaps : UI_Child
{
    [System.Serializable]
    private struct SettingLabel
    {
        public int IDMap;
        public Sprite spr_Map;
        public Sprite spr_ButtonCollect_On;
        public Sprite spr_ButtonCollect_Off;
    }
    [System.Serializable]
    private struct TimeProgress
    {
        public int second;
        public int hour;
        public int minute;
    }
    [SerializeField] private Text txt_Name;
    [Header("Settings")]
    [SerializeField] private List<SettingLabel> settingLabels;
    [Header("Image")]
    [SerializeField] private Image img_Icon;
    [SerializeField] private Image img_btnCollect;
    [Header("Button")]
    [SerializeField] private Button btn_Go;
    [SerializeField] private Button btn_Collect;
    [Header("Progress")]
    [SerializeField] private Text txt_Money;
    [SerializeField] private RectTransform rect_BackGround;
    [SerializeField] private RectTransform rect_Loading;
    [SerializeField] private Text txt_Time;
    [Header("Object")]
    [SerializeField] private GameObject obj_Go;
    [SerializeField] private GameObject obj_Current;
    [SerializeField] private GameObject obj_Lock;
    [SerializeField] private int IDMap = 0;
    private TimeProgress timeProgress = new TimeProgress();
    private int secondCheck = 0;
    private float valueprogress = 0;
    private bool isUnlock = false;
    private bool isCurrent = false;
    private void Awake()
    {
        OnInIt();
    }
    private void LateUpdate()
    {
        LoadTextTime();
        LoadProgress();
    }
    private void OnEnable()
    {
        if(MapOffManager.Instance.GetMapOffController(IDMap) != null)
        {
            isUnlock = MapOffManager.Instance.GetMapOffController(IDMap).IsUnlock();
            isCurrent = MapOffManager.Instance.GetMapOffController(IDMap).IsMapCurrent();
            LoadMoney();
        }
        if (!isUnlock)
        {
            OpenObjLock();
        }
        else
        {
            if (isCurrent)
            {
                OpenObjCurrent();
            }
            else
            {
                OpenObjGo();
            }
        }
    }
    private void OpenObjGo()
    {
        obj_Go.SetActive(true);
        obj_Current.SetActive(false);
        obj_Lock.SetActive(false);
    }
    private void OpenObjCurrent()
    {
        obj_Go.SetActive(false);
        obj_Current.SetActive(true);
        obj_Lock.SetActive(false);
    }
    private void OpenObjLock()
    {
        obj_Go.SetActive(false);
        obj_Current.SetActive(false);
        obj_Lock.SetActive(true);
    }
    private void LoadTextTime()
    {
        if (!isUnlock)
        {
            txt_Time.enabled = false;
            img_btnCollect.sprite = settingLabels[IDMap].spr_ButtonCollect_Off;
            return;
        }
        secondCheck = MapOffManager.Instance.GetMapOffController(IDMap).GetScond_Remaining_Full();
        if(secondCheck == 0)
        {
            txt_Time.enabled = false;
            img_btnCollect.sprite = settingLabels[IDMap].spr_ButtonCollect_On;
        }
        else
        {
            txt_Time.enabled = true;
            img_btnCollect.sprite = settingLabels[IDMap].spr_ButtonCollect_Off;
            timeProgress.hour = secondCheck / 3600;
            timeProgress.minute = (secondCheck - timeProgress.hour * 3600) / 60;
            timeProgress.second = (secondCheck - timeProgress.hour * 3600 - timeProgress.minute * 60);
            txt_Time.text = timeProgress.hour + "h " + timeProgress.minute + "m " + timeProgress.second + "s";
        }
    }
    private void LoadProgress()
    {
        if (!isUnlock)
        {
            valueprogress = 0;
        }
        else
        {
            valueprogress = MapOffManager.Instance.GetMapOffController(IDMap).GetProgressFull();
        }
      
        rect_Loading.sizeDelta = Vector2.right * valueprogress * rect_BackGround.sizeDelta.x + Vector2.up * rect_Loading.sizeDelta.y;
       
    }
    private void LoadMoney()
    {
        int value = MapOffManager.Instance.GetMapOffController(IDMap).GetMoneyMax();
        if (value >= 1000)
        {
            float x = value / 1000;
            txt_Money.text = "   " + (x + ((value - 1000 * x) / 1000)).ToString() + "K" + " ";
            txt_Money.text = txt_Money.text.Replace(",", ".");
        }
        else if (value > 100)
            txt_Money.text = "   " + string.Format("{000}", value) + " ";
        else if (value >= 10)
            txt_Money.text = "    " + string.Format("{00}", value) + " ";
        else
        {
            txt_Money.text = "     " + string.Format("{0}", value) + "  ";
        }
    }
    public void SetNameLabel(string name)
    {
        txt_Name.text = name;
    }
    public override void OnInIt()
    {
        base.OnInIt();
        AddEvent();
    }
    public void LoadLabel(int IDMap)
    {
        SetNameLabel("Map " + (IDMap + 1));
        SetIDMap(IDMap);
        OnEnable();
    }
    private void AddEvent()
    {
        btn_Go.onClick.AddListener(ClickGo);
        btn_Collect.onClick.AddListener(() => 
        {
            if (valueprogress >= 1)
            {
                ClickCollect();
            }
        });
    }
    public void ClickGo()
    {
        LoadMapController.Instance.LoadLevel(IDMap, LoadMapController.TypeMove.MoveToMapNext);
        MapOffManager.Instance.ReLoadMapOffMoney();
        Canvas_Home.Instance.NotShow_Btn_Oder();
        UI_Manager.Instance.CloseUI(NameUI.Canvas_Maps);
    }
    public void ClickCollect()
    {
        MapOffManager.Instance.GetMapOffController(IDMap).Collect();
    }
    public void SetIDMap(int value)
    {
        IDMap = value;
    }
}
