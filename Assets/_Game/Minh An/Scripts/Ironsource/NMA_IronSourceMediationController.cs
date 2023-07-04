using SDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NMA_IronSourceMediationController : AdsMediationController
{
    public string Android_Key;
    public string IOS_Key;

    private string m_ApplicationKey;
    private bool m_IsWatchSuccess = false;
    public void Start()
    {

#if UNITY_ANDROID
        m_ApplicationKey = Android_Key;
#elif UNITY_IPHONE
       m_ApplicationKey = IOS_Key;
#else
       m_ApplicationKey = "unexpected_platform";
#endif


        //Dynamic config example
        IronSourceConfig.Instance.setClientSideCallbacks(true);

        string id = IronSource.Agent.getAdvertiserId();
        Debug.Log("unity-script: IronSource.Agent.getAdvertiserId : " + id);

        Debug.Log("unity-script: IronSource.Agent.validateIntegration");
        IronSource.Agent.validateIntegration();

        Debug.Log("unity-script: unity version" + IronSource.unityVersion());

        // SDK init
        Debug.Log("unity-script: IronSource.Agent.init");
        IronSource.Agent.init(m_ApplicationKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        string uniqueUserID = SystemInfo.deviceUniqueIdentifier;
        IronSource.Agent.setUserId(uniqueUserID);
        IronSource.Agent.setConsent(true);

    }

    void OnEnable()
    {

        //Add Init Event
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        #region Rewarded Video Events
        //Add Rewarded Video Events
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
        #endregion
        #region Rewarded Video DemandOnly
        ////Add Rewarded Video DemandOnly Events
        //IronSourceEvents.onRewardedVideoAdOpenedDemandOnlyEvent += RewardedVideoAdOpenedDemandOnlyEvent;
        //IronSourceEvents.onRewardedVideoAdClosedDemandOnlyEvent += RewardedVideoAdClosedDemandOnlyEvent;
        //IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += RewardedVideoAdLoadedDemandOnlyEvent;
        //IronSourceEvents.onRewardedVideoAdRewardedDemandOnlyEvent += RewardedVideoAdRewardedDemandOnlyEvent;
        //IronSourceEvents.onRewardedVideoAdShowFailedDemandOnlyEvent += RewardedVideoAdShowFailedDemandOnlyEvent;
        //IronSourceEvents.onRewardedVideoAdClickedDemandOnlyEvent += RewardedVideoAdClickedDemandOnlyEvent;
        //IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += RewardedVideoAdLoadFailedDemandOnlyEvent;
        #endregion
        #region Offerwall Events
        //// Add Offerwall Events
        //IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
        //IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
        //IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
        //IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
        //IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
        //IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;
        #endregion
        #region Interstitial Events
        // Add Interstitial Events
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        #endregion
        #region Interstitial DemandOnly Events
        //// Add Interstitial DemandOnly Events
        //IronSourceEvents.onInterstitialAdReadyDemandOnlyEvent += InterstitialAdReadyDemandOnlyEvent;
        //IronSourceEvents.onInterstitialAdLoadFailedDemandOnlyEvent += InterstitialAdLoadFailedDemandOnlyEvent;
        //IronSourceEvents.onInterstitialAdShowFailedDemandOnlyEvent += InterstitialAdShowFailedDemandOnlyEvent;
        //IronSourceEvents.onInterstitialAdClickedDemandOnlyEvent += InterstitialAdClickedDemandOnlyEvent;
        //IronSourceEvents.onInterstitialAdOpenedDemandOnlyEvent += InterstitialAdOpenedDemandOnlyEvent;
        //IronSourceEvents.onInterstitialAdClosedDemandOnlyEvent += InterstitialAdClosedDemandOnlyEvent;
        #endregion
        #region Banner Events
        // Add Banner Events
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;
        #endregion
        #region ImpressionSuccess Event
        ////Add ImpressionSuccess Event
        //IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;
        //IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;
        #endregion
        #region Rewarded Video Events
        ////Add AdInfo Rewarded Video Events
        //IronSourceRewardedVideoEvents.onAdOpenedEvent += ReardedVideoOnAdOpenedEvent;
        //IronSourceRewardedVideoEvents.onAdClosedEvent += ReardedVideoOnAdClosedEvent;
        //IronSourceRewardedVideoEvents.onAdAvailableEvent += ReardedVideoOnAdAvailable;
        //IronSourceRewardedVideoEvents.onAdUnavailableEvent += ReardedVideoOnAdUnavailable;
        //IronSourceRewardedVideoEvents.onAdShowFailedEvent += ReardedVideoOnAdShowFailedEvent;
        //IronSourceRewardedVideoEvents.onAdRewardedEvent += ReardedVideoOnAdRewardedEvent;
        //IronSourceRewardedVideoEvents.onAdClickedEvent += ReardedVideoOnAdClickedEvent;
        #endregion
        #region AdInfo Interstitial Events
        ////Add AdInfo Interstitial Events
        //IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        //IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        //IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        //IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        //IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        //IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        //IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;
        #endregion
        #region AdInfo Banner Events
        ////Add AdInfo Banner Events
        //IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        //IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        //IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        //IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        //IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        //IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
        #endregion
        IronSourceEvents.onImpressionSuccessEvent += IronSourceEvents_onImpressionSuccessEvent;

    }

    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }
    #region OnGUI
    //public void OnGUI()
    //{

    //    GUI.backgroundColor = Color.blue;
    //    GUI.skin.button.fontSize = (int)(0.035f * Screen.width);





    //    Rect showRewardedVideoButton = new Rect(0.10f * Screen.width, 0.15f * Screen.height, 0.80f * Screen.width, 0.08f * Screen.height);
    //    if (GUI.Button(showRewardedVideoButton, "Show Rewarded Video"))
    //    {
    //        Debug.Log("unity-script: ShowRewardedVideoButtonClicked");
    //        if (IronSource.Agent.isRewardedVideoAvailable())
    //        {
    //            IronSource.Agent.showRewardedVideo();
    //        }
    //        else
    //        {
    //            Debug.Log("unity-script: IronSource.Agent.isRewardedVideoAvailable - False");
    //        }
    //    }



    //    Rect showOfferwallButton = new Rect(0.10f * Screen.width, 0.25f * Screen.height, 0.80f * Screen.width, 0.08f * Screen.height);
    //    if (GUI.Button(showOfferwallButton, "Show Offerwall"))
    //    {
    //        if (IronSource.Agent.isOfferwallAvailable())
    //        {
    //            IronSource.Agent.showOfferwall();
    //        }
    //        else
    //        {
    //            Debug.Log("IronSource.Agent.isOfferwallAvailable - False");
    //        }
    //    }

    //    Rect loadInterstitialButton = new Rect(0.10f * Screen.width, 0.35f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
    //    if (GUI.Button(loadInterstitialButton, "Load Interstitial"))
    //    {
    //        Debug.Log("unity-script: LoadInterstitialButtonClicked");
    //        IronSource.Agent.loadInterstitial();
    //    }

    //    Rect showInterstitialButton = new Rect(0.55f * Screen.width, 0.35f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
    //    if (GUI.Button(showInterstitialButton, "Show Interstitial"))
    //    {
    //        Debug.Log("unity-script: ShowInterstitialButtonClicked");
    //        if (IronSource.Agent.isInterstitialReady())
    //        {
    //            IronSource.Agent.showInterstitial();
    //        }
    //        else
    //        {
    //            Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
    //        }
    //    }

    //    Rect loadBannerButton = new Rect(0.10f * Screen.width, 0.45f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
    //    if (GUI.Button(loadBannerButton, "Load Banner"))
    //    {
    //        Debug.Log("unity-script: loadBannerButtonClicked");
    //        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    //    }

    //    Rect destroyBannerButton = new Rect(0.55f * Screen.width, 0.45f * Screen.height, 0.35f * Screen.width, 0.08f * Screen.height);
    //    if (GUI.Button(destroyBannerButton, "Destroy Banner"))
    //    {
    //        Debug.Log("unity-script: loadBannerButtonClicked");
    //        IronSource.Agent.destroyBanner();
    //    }




    //}
    #endregion
    public override void InitBannerAds()
    {
        base.InitBannerAds();
        IronSource.Agent.init(m_ApplicationKey, IronSourceAdUnits.BANNER);
        Debug.Log("Init banner");
    }
    public override void ShowBannerAds()
    {
        base.ShowBannerAds();
        IronSource.Agent.destroyBanner();
        BannerAdLoadedEvent();
        IronSource.Agent.displayBanner();
        Debug.Log("Show banner");
    }
    private void IronSourceEvents_onImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {
        if (impressionData != null)
        {
            double revenue = (double)impressionData.revenue;
            ImpressionData impression = new ImpressionData
            {
                ad_platform = "ironSource",
                ad_source = impressionData.adNetwork,
                ad_unit_name = impressionData.instanceName,
                ad_format = impressionData.adUnit,
                ad_revenue = revenue,
                ad_currency = "USD"
            };
            ABIAnalyticsManager.Instance.TrackAdImpression(impression);
        }

    }
    public override void InitRewardVideoAd(UnityAction videoClosed, UnityAction videoLoadSuccess, UnityAction videoLoadFailed, UnityAction videoStart)
    {
        base.InitRewardVideoAd(videoClosed, videoLoadSuccess, videoLoadFailed, videoStart);
        IronSource.Agent.init(m_ApplicationKey, IronSourceAdUnits.REWARDED_VIDEO);
        Debug.Log("Init ironsource video");

    }
    public override void RequestRewardVideoAd()
    {
        base.RequestRewardVideoAd();
        Debug.Log("Request ironsource Video");
        IronSource.Agent.loadRewardedVideo();
        IronSource.Agent.shouldTrackNetworkState(true);
    }
    public override void ShowRewardVideoAd(UnityAction successCallback, UnityAction failedCallback)
    {
        base.ShowRewardVideoAd(successCallback, failedCallback);
#if !UNITY_EDITOR
        m_IsWatchSuccess = false;
        IronSource.Agent.showRewardedVideo();
#else
        m_IsWatchSuccess = false;
        RewardedVideoAdRewardedEvent(null);
        // Debug.Log("Show");
#endif
    }
    public override bool IsRewardVideoLoaded()
    {
#if !UNITY_EDITOR
        return IronSource.Agent.isRewardedVideoAvailable();
#else
        return true;
#endif
    }
    public override void InitInterstitialAd(UnityAction adClosedCallback, UnityAction adLoadSuccessCallback, UnityAction adLoadFailedCallback, UnityAction adShowSuccessCallback, UnityAction adShowFailCallback)
    {
        base.InitInterstitialAd(adClosedCallback, adLoadSuccessCallback, adLoadFailedCallback, adShowSuccessCallback, adShowFailCallback);
        Debug.Log("Init ironsource Interstitial");

        IronSource.Agent.init(m_ApplicationKey, IronSourceAdUnits.INTERSTITIAL);
    }
    public override void RequestInterstitialAd()
    {
        base.RequestInterstitialAd();
        Debug.Log("Request ironsource Interstitial");
        IronSource.Agent.loadInterstitial();
    }
    public override void ShowInterstitialAd()
    {
        base.ShowInterstitialAd();
        Debug.Log("Show Iron source interstitial");
        IronSource.Agent.showInterstitial();
    }
    public override bool IsInterstitialLoaded()
    {
        // Debug.Log(IronSource.Agent.isInterstitialReady());
#if !UNITY_EDITOR
         return IronSource.Agent.isInterstitialReady();
#else
        return false;
#endif
    }

    #region Init callback handlers

    void SdkInitializationCompletedEvent()
    {
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent");
    
        //BannerAdLoadedEvent();
    }

    #endregion

    #region AdInfo Rewarded Video
    void ReardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdOpenedEvent With AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdClosedEvent With AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdAvailable With AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdUnavailable()
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdUnavailable");
    }
    void ReardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent With Error" + ironSourceError.ToString() + "And AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement.ToString() + "And AdInfo " + adInfo.ToString());
    }
    void ReardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got ReardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement.ToString() + "And AdInfo " + adInfo.ToString());
    }

    #endregion

    #region RewardedAd callback handlers

    void RewardedVideoAvailabilityChangedEvent(bool isAvailable)
    {
        Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + isAvailable);
        if (isAvailable)
        {
            Debug.Log("RewardedVideoAd Iron Loaded Success");
            if (m_RewardedVideoLoadSuccessCallback != null)
            {
                m_RewardedVideoLoadSuccessCallback();
            }
        }
        else
        {
            Debug.Log("RewardedVideoAd Iron Loaded Fail");
            if (m_RewardedVideoLoadFailedCallback != null)
            {
                m_RewardedVideoLoadFailedCallback();
            }
        }
    }

    void RewardedVideoAdOpenedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
#if !UNITY_EDITOR
        Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getPlacementName() + " name = " + ssp.getRewardName()); 
#endif
        m_IsWatchSuccess = true;
#if UNITY_ANDROID
        if (m_RewardedVideoEarnSuccessCallback != null)
        {
            Debug.Log("Watch video Success Callback!");
            EventManager.AddEventNextFrame(m_RewardedVideoEarnSuccessCallback);
            m_RewardedVideoEarnSuccessCallback = null;
        }
#elif UNITY_IOS
        if (m_RewardedVideoEarnSuccessCallback != null) {
                Debug.Log("Watch video Success Callback!");
                EventManager.AddEventNextFrame(m_RewardedVideoEarnSuccessCallback);
                m_RewardedVideoEarnSuccessCallback = null;
          }
#endif
    }

    void RewardedVideoAdClosedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
        if (m_RewardedVideoEarnSuccessCallback != null && m_IsWatchSuccess)
        {
            Debug.Log("Do Callback Success");
            EventManager.AddEventNextFrame(m_RewardedVideoEarnSuccessCallback);
            m_RewardedVideoEarnSuccessCallback = null;
        }
        else
        {
            Debug.Log("Don't have any callback");
        }
        if (m_RewardedVideoCloseCallback != null)
        {
            m_RewardedVideoCloseCallback();
        }
    }

    void RewardedVideoAdStartedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
        if (m_RewardedVideoShowStartCallback != null)
        {
            m_RewardedVideoShowStartCallback();
        }
    }

    void RewardedVideoAdEndedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
        m_IsWatchSuccess = true;
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
        if (m_RewardedVideoLoadFailedCallback != null)
        {
            m_RewardedVideoLoadFailedCallback();
        }
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
    }
    #region RewardedVideo DemandOnly Delegates
    /************* RewardedVideo DemandOnly Delegates *************/

    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    {

        Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {

        Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
    }

    #endregion
    #endregion

    #region AdInfo Interstitial

    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdReadyEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadFailed With Error " + ironSourceError.ToString());
    }

    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdOpenedEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdShowSucceededEvent With AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdShowFailedEvent With Error " + ironSourceError.ToString() + " And AdInfo " + adInfo.ToString());
    }

    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo.ToString());
    }


    #endregion

    #region Interstitial callback handlers

    void InterstitialAdReadyEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdReadyEvent");
        if (m_InterstitialAdLoadSuccessCallback != null)
        {
            m_InterstitialAdLoadSuccessCallback();
        }
    }

    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        if (m_InterstitialAdLoadFailCallback != null)
        {
            m_InterstitialAdLoadFailCallback();
        }
    }

    void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
        if (m_InterstitialAdShowSuccessCallback != null)
        {
            m_InterstitialAdShowSuccessCallback();
        }
    }

    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
        if (m_InterstitialAdShowFailCallback != null)
        {
            m_InterstitialAdShowFailCallback();
        }
    }

    void InterstitialAdClickedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClickedEvent");
    }

    void InterstitialAdOpenedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
    }

    void InterstitialAdClosedEvent()
    {
        Debug.Log("unity-script: I got InterstitialAdClosedEvent");
        if (m_InterstitialAdCloseCallback != null)
        {
            m_InterstitialAdCloseCallback();
        }
    }

    /************* Interstitial DemandOnly Delegates *************/

    void InterstitialAdReadyDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdReadyDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", error code: " + error.getCode() + ",error description : " + error.getDescription());
    }

    void InterstitialAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got InterstitialAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", error code :  " + error.getCode() + ",error description : " + error.getDescription());
    }

    void InterstitialAdClickedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdClickedDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdOpenedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdOpenedDemandOnlyEvent for instance: " + instanceId);
    }

    void InterstitialAdClosedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got InterstitialAdClosedDemandOnlyEvent for instance: " + instanceId);
    }




    #endregion

    #region Banner AdInfo

    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdLoadedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
    {
        Debug.Log("unity-script: I got BannerOnAdLoadFailedEvent With Error " + ironSourceError.ToString());
    }

    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdClickedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdScreenPresentedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdScreenDismissedEvent With AdInfo " + adInfo.ToString());
    }

    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got BannerOnAdLeftApplicationEvent With AdInfo " + adInfo.ToString());
    }

    #endregion

    #region Banner callback handlers

    void BannerAdLoadedEvent()
    {
        Debug.Log("unity-script: I got BannerAdLoadedEvent");
        IronSourceBannerSize size = IronSourceBannerSize.BANNER;
        size.SetAdaptive(true);
        IronSource.Agent.loadBanner(size, IronSourceBannerPosition.BOTTOM);

    }

    void BannerAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
        ShowBannerAds();
    }

    void BannerAdClickedEvent()
    {
        Debug.Log("unity-script: I got BannerAdClickedEvent");
    }

    void BannerAdScreenPresentedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
        IronSource.Agent.displayBanner();
    }

    void BannerAdScreenDismissedEvent()
    {
        Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
        IronSource.Agent.destroyBanner();
    }

    void BannerAdLeftApplicationEvent()
    {
        Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
    }

    #endregion


    #region Offerwall callback handlers

    void OfferwallOpenedEvent()
    {
        Debug.Log("I got OfferwallOpenedEvent");
    }

    void OfferwallClosedEvent()
    {
        Debug.Log("I got OfferwallClosedEvent");
    }

    void OfferwallShowFailedEvent(IronSourceError error)
    {
        Debug.Log("I got OfferwallShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void OfferwallAdCreditedEvent(Dictionary<string, object> dict)
    {
        Debug.Log("I got OfferwallAdCreditedEvent, current credits = " + dict["credits"] + " totalCredits = " + dict["totalCredits"]);

    }

    void GetOfferwallCreditsFailedEvent(IronSourceError error)
    {
        Debug.Log("I got GetOfferwallCreditsFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void OfferwallAvailableEvent(bool canShowOfferwal)
    {
        Debug.Log("I got OfferwallAvailableEvent, value = " + canShowOfferwal);

    }

    #endregion

    #region ImpressionSuccess callback handler

    void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionSuccessEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionSuccessEvent allData: " + impressionData.allData);
    }

    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    {
        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
        Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
    }
    public override AdsMediationType GetAdsMediationType()
    {
        return AdsMediationType.IRONSOURCE;
    }
    #endregion

}
