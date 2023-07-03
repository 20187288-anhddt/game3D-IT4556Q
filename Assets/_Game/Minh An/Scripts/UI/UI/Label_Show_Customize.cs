using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label_Show_Customize : UI_Child
{
    public Transform myTransform;
    public int ID_This = 0;
    [SerializeField] private Image img_Icon;
    [Header("Money")]
    [SerializeField] private GameObject obj_Money;
    [SerializeField] private Text txt_Money;
    [Header("Video")]
    [SerializeField] private GameObject obj_Video;
    [SerializeField] private Text txt_Video;
    [Header("Button")]
    [SerializeField] private Button btn_This;
    [Header("Tick")]
    [SerializeField] private GameObject obj_Tick;
    private int Value_CostCurent = 0;
    private InfoSkinPlayer infoSkinPlayerTaget;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.CheckTickInCustomize.ToString(), CheckTickInThis);
        EnventManager.TriggerEvent(EventName.CheckTickInCustomize.ToString());
    }
    public void Load(InfoSkinPlayer infoSkinPlayer, bool isBought, int ID)
    {
        myTransform.localScale = Vector3.one;
        ID_This = ID;
        infoSkinPlayerTaget = infoSkinPlayer;
        img_Icon.sprite = infoSkinPlayer.Icon;
        if (!isBought)
        {
            switch (infoSkinPlayer.infoBuy.typeCost)
            {
                case TypeCost.WatchVideo:
                    Open_Obj_Video();
                    Value_CostCurent = infoSkinPlayer.infoBuy.value;
                    txt_Video.text = DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().GetValueWatchVideo(ID_This) + "/" + infoSkinPlayer.infoBuy.value.ToString();
                    break;
                case TypeCost.Money:
                    Open_Obj_Money();
                    int value = infoSkinPlayer.infoBuy.value;
                    if (value <= 0)
                    {
                        DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().AddID_Onboughts(ID_This);
                        SetTagetID(ID);
                        Load(infoSkinPlayer, true, ID);

                        return;
                    }
                    else
                    {
                        Value_CostCurent = value;
                        if (value >= 1000)
                        {
                            float x = value / 1000;
                            txt_Money.text = (x + ((value - 1000 * x) / 1000)).ToString() + "K";
                            txt_Money.text = txt_Money.text.Replace(",", ".");
                        }
                        else if (value > 100)
                            txt_Money.text = string.Format("{000}", value);
                        else
                            txt_Money.text = string.Format("{00}", value);
                    }
                    break;
            }
            btn_This.onClick.RemoveAllListeners();
            btn_This.onClick.AddListener(() => { Buy(infoSkinPlayer.infoBuy.typeCost); });
        }
        else
        {
            CloseAll();
            btn_This.onClick.RemoveAllListeners();
            btn_This.onClick.AddListener(() => { SetTagetID(ID_This); EnventManager.TriggerEvent(EventName.CheckTickInCustomize.ToString()); });
        }
        EnventManager.TriggerEvent(EventName.CheckTickInCustomize.ToString());
    }
    private void CheckTickInThis()
    {
        if (DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().GetID() == ID_This)
        {
            Open_Obj_Tick();
        }
        else
        {
            Close_Obj_Tick();
        }
    }
    private  void Open_Obj_Money()
    {
        obj_Money.SetActive(true);
        obj_Money.SetActive(false);
        StartCoroutine(IE_DelayAction(0.1f, () =>
        {
            obj_Money.SetActive(true);
            obj_Video.SetActive(false);
            obj_Tick.SetActive(false);
        }));
    
    }
    private void Open_Obj_Video()
    {
        obj_Money.SetActive(false);
        obj_Video.SetActive(true);
        StartCoroutine(IE_DelayAction(0.1f, () =>
        {
            obj_Video.SetActive(false);
            obj_Video.SetActive(true);
            obj_Tick.SetActive(false);
        }));
       
    }
    private void Open_Obj_Tick()
    {
        obj_Video.SetActive(false);
        obj_Video.SetActive(false);
        obj_Tick.SetActive(true);
    }
    private void Close_Obj_Tick()
    {
        obj_Tick.SetActive(false);
    }
    private void CloseAll()
    {
        obj_Money.SetActive(false);
        obj_Video.SetActive(false);
        obj_Tick.SetActive(false);
    }
    IEnumerator IE_DelayAction(float m_Time, System.Action action)
    {
        yield return new WaitForSeconds(m_Time);
        action?.Invoke();
    }

    private void Buy(TypeCost typeCost)
    {
        switch (typeCost)
        {
            case TypeCost.WatchVideo:

                SDK.AdsManager.Instance.ShowRewardVideo("Customize", () =>
                {
                    DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().AddValueWatchVideo(ID_This, 1);
                    bool isBought = false;
                    if (DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().GetValueWatchVideo(ID_This) >= Value_CostCurent)
                    {
                        isBought = true;
                        DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().AddID_Onboughts(ID_This);
                        SetTagetID(ID_This);
                    }
                    Load(infoSkinPlayerTaget, isBought, ID_This);
                });
              
                break;
            case TypeCost.Money:
             
                if (DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) >= Value_CostCurent)
                {
                    DataManager.Instance.GetDataMoneyController().RemoveMoney(Money.TypeMoney.USD, Value_CostCurent);
                    DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().AddID_Onboughts(ID_This);
                    SetTagetID(ID_This);
                    btn_This.onClick.RemoveAllListeners();
                    Load(infoSkinPlayerTaget, true, ID_This);

                    Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
                    parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
                    parameters[1] = new Firebase.Analytics.Parameter("value", Value_CostCurent);
                    parameters[2] = new Firebase.Analytics.Parameter("source", "Customize_" + infoSkinPlayerTaget.ID);
                    SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("spend_virtual_currency", parameters);
                }
                break;

        }
    }
    private void SetTagetID(int ID)
    {
        DataManager.Instance.GetDataCustomizeController().GetDataCustomize_Head().SetID(ID);
    }
}
