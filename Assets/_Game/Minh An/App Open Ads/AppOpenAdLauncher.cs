using GoogleMobileAds.Api;
using UnityEngine;
//using NVTT;

public class AppOpenAdLauncher : Singleton<AppOpenAdLauncher>
{
    [Header("Settings")]
    public bool enableAds = false;
    public bool isTestAds = false;

    public override void Awake()
    {
        AppOpenAdManager.AOAReadyTime = 0;
        base.Awake();
        //if (PlayerPrefs.GetInt(IapManager.NameData_isNoAds) == 1)
        //{
        //    return;
        //}
        SDK.EventManager.StartListening("UpdateRemoteConfigs", () =>
        {
            enableAds = SDK.ABIFirebaseManager.Instance.GetConfigValue(ABI.Keys.key_remote_aoa_active).BooleanValue;
            AppOpenAdManager.AOA_Cooldown = SDK.ABIFirebaseManager.Instance.GetConfigValue(ABI.Keys.key_remote_aoa_time_between_step).DoubleValue;
            // AppOpenAdManager.AOAReadyTime = SDK.ABIFirebaseManager.Instance.GetConfigValue(ABI.Keys.key_remote_aoa_show_first_time_active).DoubleValue; 
            Debug.Log("AOA_Cooldown---------------------" + AppOpenAdManager.AOA_Cooldown);
        });
        if (enableAds)
        {
            AppOpenAdManager.IsTestAds = isTestAds;
            MobileAds.Initialize(status => { /*AppOpenAdManager.Instance.LoadAd();*/ });
            
            // By default, AOA will show immediately whenever an interstitial or reward ad was dismissed from screen (OnApplicationPause)
            // We have to track if the game was unpaused from ads.
            //EventManager.StartListening("show_interstitial_ads", SetResumeFromAdsToTrue);
            //EventManager.StartListening("show_reward_ads", SetResumeFromAdsToTrue);


            //EventManager.StartListening("hidden_interstitial_ads", SetResumeFromAdsToFalse);
            //EventManager.StartListening("hidden_reward_ads", SetResumeFromAdsToFalse);
        }
    }


    private void OnApplicationPause(bool pause)
    {
        //if (PlayerPrefs.GetInt(IapManager.NameData_isNoAds) == 1)
        //{
        //    return;
        //}
        if (!pause)
        {
            StartCoroutine(DelayAction(() =>
            {
#if UNITY_EDITOR
                Debug.Log("On Pause");
#endif
                if (AppOpenAdManager.ResumeFromAds)
                {
#if UNITY_EDITOR
                    Debug.Log("Return");
#endif
                    SetResumeFromAdsToTrue();
                    return;
                }

#if UNITY_ANDROID
                Debug.Log("AppOpenAdManager.AOAReadyTime============" + AppOpenAdManager.AOAReadyTime);
                if (AppOpenAdManager.ConfigResumeApp && enableAds && AppOpenAdManager.AOAReadyTime <= Time.time)
                {
                    AppOpenAdManager.Instance.ShowAdIfAvailable();
                }

                //Reset after when the app is foregrounded
                //if (!pause)
                //    AppOpenAdManager.ResumeFromAds = false;
#elif UNITY_IOS
                    if (!pause && AppOpenAdManager.ConfigResumeApp && !AppOpenAdManager.ResumeFromAds && enableAds)
                    {
                        AppOpenAdManager.Instance.ShowAdIfAvailable();
                    }
#else
                    //AD_UNIT_ID = "unexpected_platform";
                    Debug.Log("This is a Comment");
#endif
            }, 0.2f));
        }

    }
    System.Collections.IEnumerator DelayAction(System.Action action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action?.Invoke();
    }

    public void SetResumeFromAdsToTrue() => AppOpenAdManager.ResumeFromAds = true;
    public void SetResumeFromAdsToFalse() => AppOpenAdManager.ResumeFromAds = false;

    private bool didShowAoALoadingTime = false;
    private float lastCheck = 0;
    public void ShowAOAIfLoadedInLoadingTime()
	{
        if (didShowAoALoadingTime || lastCheck  + 0.2f > Time.time)
            return;

        lastCheck = Time.time;
        didShowAoALoadingTime = AppOpenAdManager.Instance.ShowAdIfAvailable();
	}
    public void ShowAOA()
    {
        // AppOpenAdManager.Instance.ShowAdIfAvailable();
        if (!AppOpenAdManager.Instance.isAoANotShowFistOneShot())
        {
            AppOpenAdManager.Instance.LoadAd();
        }
        //AppOpenAdManager.Instance.LoadAd();
    }

}