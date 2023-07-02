using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Order : UI_Canvas
{
    private static Canvas_Order instane;
    public static Canvas_Order Instane
    {
        get
        {
            if(instane == null)
            {
                instane = FindObjectOfType<Canvas_Order>();
            }
            return instane;
        }
    }

    [SerializeField] private List<Process_Item> process_Items;
    [SerializeField] private Process_Item process_Item_Prefab;
    [SerializeField] private Transform transformParent_Process_Item;
    [Header("Text")]
    [SerializeField] private Text txt_Time;
    [SerializeField] private Text txt_Money;
    [Header("Sprite_Image")]
    [SerializeField] private Image img_BG_Btn_Collect;
    [SerializeField] private Image img_BG_Btn_Collect_WatchVideo;
    [SerializeField] private Sprite spr_BG_CollectWatchVideo_On;
    [SerializeField] private Sprite spr_BG_Collect_On;
    [SerializeField] private Sprite spr_BG_Off;
    [Header("Button")]
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_CloseAll;
    [SerializeField] private Button btn_Collect;
    [SerializeField] private Button btn_WatchVideo_CollectX3;
    private bool isCompleteAllMission = false;
    private int MoneyCurrent = 0;
    private System.Action actionCollect;
    private void Awake()
    {
        InItData();
    }
    private void InItData()
    {
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(NameUI.Canvas_Order); });
        btn_CloseAll.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(NameUI.Canvas_Order); });
        btn_Collect.onClick.AddListener(Collect);
        btn_WatchVideo_CollectX3.onClick.AddListener(WatchVideo_CollectX3);
    }
  
    //private void OnDisable()
    //{
    //    CloseAllItem();
    //}
    private void Update()
    {
        if (!isCompleteAllMission)
        {
            img_BG_Btn_Collect.sprite = spr_BG_Off;
            img_BG_Btn_Collect_WatchVideo.sprite = spr_BG_Off;
        }
        else
        {
            img_BG_Btn_Collect.sprite = spr_BG_Collect_On;
            img_BG_Btn_Collect_WatchVideo.sprite = spr_BG_CollectWatchVideo_On;
        }
        if (Canvas_Bonus.Instance != null)
        {
            if (Canvas_Bonus.Instance.gameObject.activeInHierarchy)
            {
                Canvas_Bonus.Instance?.gameObject.SetActive(false);
            }
         
        }
        
    }
    public void ShowItem(IngredientType ingredientType, int valueCurrent)
    {
        foreach (Process_Item process_Item in process_Items)
        {
            if (process_Item.GetIngredientType() == ingredientType)
            {
                process_Item.LoadProcess(valueCurrent);
            }
        }
    }
    public void SetCompleteAllMission(bool value)
    {
        isCompleteAllMission = value;
    }
    public void InItData(int valueMax, IngredientType ingredientType)
    {
        Process_Item process_Item_ = null;
        foreach (Process_Item process_Item in process_Items)
        {
            if (process_Item.IsClosed())
            {
                process_Item.Open();
                process_Item_ = process_Item;
                break;
            }
        }
        if (process_Item_ == null)
        {
            process_Item_ = Instantiate(process_Item_Prefab);
            process_Item_.myTransform.SetParent(transformParent_Process_Item);
            process_Items.Add(process_Item_);
        }
        process_Item_.InItData(valueMax, ingredientType);
    }
    public void CloseAllItem()
    {
        foreach (Process_Item process_Item in process_Items)
        {
            process_Item.Close();
        }
    }
    public void Collect()
    {
        if (isCompleteAllMission)
        {
            Debug.Log("Collect");
            DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
                 DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) + MoneyCurrent);
            isCompleteAllMission = false;
            UI_Manager.Instance.CloseUI(NameUI.Canvas_Order);
            actionCollect?.Invoke();
        }
    }
    public void WatchVideo_CollectX3()
    {
        if (isCompleteAllMission)
        {
            Debug.Log("Collect WatchVideo");
            DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
                 DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) + MoneyCurrent * 3);
            isCompleteAllMission = false;
            UI_Manager.Instance.CloseUI(NameUI.Canvas_Order);
            actionCollect?.Invoke();
        }
    }
    public void SetActionCollect(System.Action action)
    {
        actionCollect = action;
    }
    public void LoadTextMoneyCurrent()
    {
        if (MoneyCurrent >= 1000)
        {
            float x = MoneyCurrent / 1000;
            txt_Money.text = (x + ((MoneyCurrent - 1000 * x) / 1000)).ToString() + "K";
            txt_Money.text = txt_Money.text.Replace(",", ".");
        }
        else if (MoneyCurrent > 100)
            txt_Money.text = string.Format("{000}", MoneyCurrent);
        else
            txt_Money.text = string.Format("{00}", MoneyCurrent);
    }
    public void SetMoneyCurrent(int value)
    {
        MoneyCurrent = value;
     //   MoneyCurrent = 109008;
        LoadTextMoneyCurrent();
    }
    public void LoadTime(int valueSecond)
    {
        int minute = valueSecond / 60;
        int second = valueSecond - minute * 60;
        txt_Time.text = minute.ToString() + "m " + second.ToString() + "s";
    }
    public int GetValueMax_Mission(IngredientType ingredientType)
    {
        foreach (Process_Item process_Item in process_Items)
        {
            if (process_Item.GetIngredientType() == ingredientType)
            {
                return process_Item.GetValueMax();
            }
        }
        return 0;
    }
    public override void Close()
    {
        // UI_Manager.Instance.OpenUI(NameUI.Canvas_Home);
        Canvas_Home.Instance?.gameObject.SetActive(true);
        Canvas_Bonus.Instance?.gameObject.SetActive(true);
        base.Close();
        UI_Manager.Instance?.ReMoveUI_To_Stack_UI_Open();
        CameraController.Instance?.ResetFollowPlayer(false);
    }
    public override void Open()
    {
        base.Open();
        Close();
        base.Open();
       // UI_Manager.Instance.CloseUI(NameUI.Canvas_Home);
        UI_Manager.Instance?.AddUI_To_Stack_UI_Open(this);
        Canvas_Home.Instance?.gameObject.SetActive(false);
        BuildIngredientController buildIngredientController = BuildController.Instance.GetBuildIngredientController(IngredientType.CAR);
        CameraController.Instance?.SetFollowAndLookAt(buildIngredientController.GetBaseBuild(NameObject_This.Car).myTransform, buildIngredientController.GetBaseBuild(NameObject_This.Car).myTransform,
        0, 2.5f, 2.5f, 2.5f);
    }
  
}
