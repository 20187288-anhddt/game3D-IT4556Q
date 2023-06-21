using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Components;

public class Canvas_Iap : UI_Canvas
{
    private static Canvas_Iap instance;
    public static Canvas_Iap Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<Canvas_Iap>();
            }
            return instance;
        }
    }
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_BuySuperPack;
    [SerializeField] private Button btn_BuyMoneyOffer;
    [SerializeField] private Button btn_BuySimpleOffer;

    [Header("Sprite")]
    [SerializeField] private Image img_BuySuperPack;
    [SerializeField] private Image img_BuyMoneyOffer;
    [SerializeField] private Image img_BuySimpleOffer;
    [SerializeField] Sprite spr_On;
    [SerializeField] Sprite spr_Off;
    //private void Start()
    //{
    //    //Dictionary<string, string> pairs_ = new Dictionary<string, string>();
    //    //pairs_.Add("af_revenue", "12.97");
    //    //pairs_.Add("af_currency", "USD");
    //    //pairs_.Add("af_quantity", "So_luong_mat_hang_da_mua");
    //    //pairs_.Add("af_content_id", "ten mat hang mua");
    //    //SDK.ABIAppsflyerManager.SendEvent("af_purchase", pairs_);
    //}
    public void LoadIap()
    {
        if (IapManager.Instance.isPurchase_SuperPack_Bought())
        {
            btn_BuySuperPack.enabled = false;
            img_BuySuperPack.sprite = spr_Off;
        }
        else
        {
            img_BuySuperPack.sprite = spr_On;
            btn_BuySuperPack.enabled = true;
        }
        if (IapManager.Instance.isPurchase_MoneyOffer_Bought())
        {
            btn_BuyMoneyOffer.enabled = false;
            img_BuyMoneyOffer.sprite = spr_Off;
        }
        else
        {
            img_BuyMoneyOffer.sprite = spr_On;
            btn_BuyMoneyOffer.enabled = true;
        }
        if (IapManager.Instance.isPurchase_SimpleOffer_Bought())
        {
            btn_BuySimpleOffer.enabled = false;
            img_BuySimpleOffer.sprite = spr_Off;
        }
        else
        {
            img_BuySimpleOffer.sprite = spr_On;
            btn_BuySimpleOffer.enabled = true;
        }
    }
    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => {
            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[20], 1, false);
            UI_Manager.Instance.CloseUI(nameUI); });
        //btn_BuySuperPack.onClick.AddListener(() => { BuySuperPack(); });
        //btn_BuyMoneyOffer.onClick.AddListener(() => { BuyMoneyOffer(); });
        //btn_BuySimpleOffer.onClick.AddListener(() => { BuySimpleOffer(); });
        LoadIap();
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
        (EventBounsController.Instance?.GetUIBonus(TypeBonus.DoubleSpeed_Player) as UI_Bonus_DoubleSpeed_Player).Reward(1800, false);
        DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
             DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) + 20000);

        Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
        parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
        parameters[1] = new Firebase.Analytics.Parameter("value", 20000);
        parameters[2] = new Firebase.Analytics.Parameter("source", "Iap_SuperPack");
        SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("earn_virtual_currency", parameters);


    }
    public void BuyMoneyOffer()
    {
        DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD,
            DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD) + 10000);

        Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
        parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
        parameters[1] = new Firebase.Analytics.Parameter("value", 10000);
        parameters[2] = new Firebase.Analytics.Parameter("source", "Iap_MoneyOffer");
        SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("earn_virtual_currency", parameters);
    }
    public void BuySimpleOffer()
    {

    }
}
