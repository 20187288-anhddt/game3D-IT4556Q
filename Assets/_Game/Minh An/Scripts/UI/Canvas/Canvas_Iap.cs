using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Iap : UI_Canvas
{
    [SerializeField] private Button btn_Close;
    [SerializeField] private Button btn_BuySuperPack;
    [SerializeField] private Button btn_BuyMoneyOffer;
    [SerializeField] private Button btn_BuySimpleOffer;
    private void Start()
    {
        //Dictionary<string, string> pairs_ = new Dictionary<string, string>();
        //pairs_.Add("af_revenue", "12.97");
        //pairs_.Add("af_currency", "USD");
        //pairs_.Add("af_quantity", "So_luong_mat_hang_da_mua");
        //pairs_.Add("af_content_id", "ten mat hang mua");
        //SDK.ABIAppsflyerManager.SendEvent("af_purchase", pairs_);
    }
    private void Awake()
    {
        OnInIt();
    }
    public override void OnInIt()
    {
        base.OnInIt();
        btn_Close.onClick.AddListener(() => { UI_Manager.Instance.CloseUI(nameUI); });
        btn_BuySuperPack.onClick.AddListener(() => { BuySuperPack(); });
        btn_BuyMoneyOffer.onClick.AddListener(() => { BuyMoneyOffer(); });
        btn_BuySimpleOffer.onClick.AddListener(() => { BuySimpleOffer(); });
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
