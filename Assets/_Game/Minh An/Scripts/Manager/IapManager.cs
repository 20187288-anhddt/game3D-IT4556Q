using UnityEngine;
using UnityEngine.Purchasing;

public class IapManager : GenerticSingleton<IapManager>
{
    public static string NameData_isBuy_SuperPack = "isBuy_SuperPack";
    public static string NameData_isBuy_MoneyOffer = "isBuy_MoneyOffer";
    public static string NameData_isBuy_NoAds = "isBuy_NoAds";
#if UNITY_EDITOR
    [SerializeField] private bool isReset = true;
    public override void Awake()
    {
        if (isReset)
        {
            ResetIap();
        }
    }
    public void ResetIap()
    {
        PlayerPrefs.SetInt(NameData_isBuy_SuperPack, -1);
        PlayerPrefs.SetInt(NameData_isBuy_MoneyOffer, -1);
        PlayerPrefs.SetInt(NameData_isBuy_NoAds, -1);
        Debug.Log("Reset Iap In EDITOR");
    }
#endif
    public void OnPurChaseComlete(Product product)
    {
        switch (product.definition.id)
        {
            case "superpack":
                PlayerPrefs.SetInt(NameData_isBuy_SuperPack, 1);
                PlayerPrefs.SetInt(NameData_isBuy_NoAds, 1);
                Canvas_Iap.Instance.BuySuperPack();
                Canvas_Iap.Instance.LoadIap();
                SDK.AdsManager.Instance.DestroyBannerAds();
                SDK.AdsManager.Instance.ReLoadDataAds();

                System.Collections.Generic.Dictionary<string, string> pairs_ = new System.Collections.Generic.Dictionary<string, string>();
                pairs_.Add("af_revenue", "5.99");
                pairs_.Add("af_currency", "USD");
                pairs_.Add("af_quantity", "1");
                pairs_.Add("af_content_id", "superpack");
                SDK.ABIAppsflyerManager.SendEvent("af_purchase", pairs_);

                break;
            case "moneyoffer":
                PlayerPrefs.SetInt(NameData_isBuy_MoneyOffer, 1);
                PlayerPrefs.SetInt(NameData_isBuy_NoAds, 1);
                Canvas_Iap.Instance.BuyMoneyOffer();
                Canvas_Iap.Instance.LoadIap();
                SDK.AdsManager.Instance.DestroyBannerAds();
                SDK.AdsManager.Instance.ReLoadDataAds();

                System.Collections.Generic.Dictionary<string, string> pairs__ = new System.Collections.Generic.Dictionary<string, string>();
                pairs__.Add("af_revenue", "3.99");
                pairs__.Add("af_currency", "USD");
                pairs__.Add("af_quantity", "1");
                pairs__.Add("af_content_id", "moneyoffer");
                SDK.ABIAppsflyerManager.SendEvent("af_purchase", pairs__);
              
                break;
            case "simpleoffer":
                PlayerPrefs.SetInt(NameData_isBuy_NoAds, 1);
                Canvas_Iap.Instance.BuySimpleOffer();
                Canvas_Iap.Instance.LoadIap();
                SDK.AdsManager.Instance.DestroyBannerAds();
                SDK.AdsManager.Instance.ReLoadDataAds();

                System.Collections.Generic.Dictionary<string, string> pairs___ = new System.Collections.Generic.Dictionary<string, string>();
                pairs___.Add("af_revenue", "2.99");
                pairs___.Add("af_currency", "USD");
                pairs___.Add("af_quantity", "1");
                pairs___.Add("af_content_id", "simpleoffer");
                SDK.ABIAppsflyerManager.SendEvent("af_purchase", pairs___);
                break;
        }
        
    }

    public void OnPurChaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        switch (product.definition.id)
        {
            case "superpack":
#if UNITY_EDITOR
                Debug.Log(product.definition.id + "Failded because" + failureReason);
#endif
                break;
            case "moneyoffer":
#if UNITY_EDITOR
                Debug.Log(product.definition.id + "Failded because" + failureReason);
#endif
                break;
            case "simpleoffer":
#if UNITY_EDITOR
                Debug.Log(product.definition.id + "Failded because" + failureReason);
#endif
                break;
        }

    }
    public bool isPurchase_SuperPack_Bought()
    {
        return PlayerPrefs.GetInt(NameData_isBuy_SuperPack) == 1;
    }
    public bool isPurchase_MoneyOffer_Bought()
    {
        return PlayerPrefs.GetInt(NameData_isBuy_MoneyOffer) == 1;
    }
    public bool isPurchase_SimpleOffer_Bought()
    {
        return PlayerPrefs.GetInt(NameData_isBuy_NoAds) == 1;
    }
}
