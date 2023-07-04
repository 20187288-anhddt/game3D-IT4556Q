using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;
using Utilities.Components;

public class CheckUnlock : MonoBehaviour
{
    public Transform myTransform;
    public bool isHaveUnlock;
    [SerializeField]
    private DOTweenAnimation effect;
    private float t;
    private float tmp;
    private ILock normal;
    [SerializeField]
    private Coin coinPrefab;
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private GameObject bg;
    [SerializeField] private PirceObject pirceObject_UI;
    [SerializeField] private DataStatusObject dataStatusObject;
    private bool isPlayAnim = false;

    private bool isUnlockAds;
    private void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
        if(pirceObject_UI == null) { pirceObject_UI = GetComponent<PirceObject>(); }
        if(dataStatusObject == null) { dataStatusObject = GetComponentInParent<DataStatusObject>(); }
    }
    void Start()
    {
        normal = GetComponentInParent<ILock>();
    }
    public void UpdateUI()
    {
        pirceObject_UI.ReLoadUI(); 
    }
    private void OnTriggerStay(Collider other)
    {
        if (!(normal as BaseBuild).unlockAds)
        {
            if (!normal.IsLock)
            {
                return;
            }
            //IUnlock unlock = other.GetComponent<IUnlock>();
            IUnlock unlock = Cache.getIUnlock(other);
            //if (unlock != null)
            //{
                
                t += Time.deltaTime;
                if (t > 0.75f)
                {
                    unlockBuild(unlock.CoinValue);
                    //if (normal.CurrentCoin > 0)
                    //{
                      
                    //    //if (t < 1.5f)
                    //    //{
                    //    //    if (!(unlock as Player).canCatch)
                    //    //        return;
                    //    //    (unlock as Player).canCatch = false;
                    //    //    unlockBuild(unlock.CoinValue);
                    //    //    //unlock.UnlockMap(1);
                    //    //    (unlock as Player).DelayCatch(0f);
                    //    //}
                    //    //else
                    //    //{
                    //    //    if ((unlock as Player).canCatch)
                    //    //    {
                    //    //        unlockQuick(unlock);
                    //    //    }
                    //    //    else
                    //    //        (unlock as Player).DelayCatch(1);
                    //    //}
                    //}
                    ////else
                    ////{
                    ////    normal.UnLock(true, true);
                    ////}
                }
            //}
        }
        else
        {
            if (!normal.IsLock) return;
            //IUnlock unlock = other.GetComponent<IUnlock>();
            IUnlock unlock = Cache.getIUnlock(other);
            if (unlock == null)
                return;
            if (!isUnlockAds)
            {
                tmp = 2.5f;
                isUnlockAds = true;
            }
            if (isUnlockAds)
            {
                tmp -= Time.deltaTime;
                if (tmp < 1.5f)
                {
                    //bound[1].DOFillAmount(1, 1.5f).OnComplete(() => { bound[1].fillAmount = 0; });
                    if (tmp < 0)
                    {
                        //if (!MaxManager.Ins.isStartWaitting_Reward)
                        //{
                        //    MaxManager.Ins.ShowRewardedAd("unlock_build", () => { normal.UnLock(); });
                        //}
                        isUnlockAds = false;
                    }
                }
            }

        }

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    t = 0;
    //    var player = other.GetComponent<Player>();
    //    //if (player != null)
    //    //{
    //        player.canCatch = true;

    //    //}
    //}
    private void OnTriggerExit(Collider other)
    {
        //IUnlock unlock = other.GetComponent<IUnlock>();
        //IUnlock unlock = Cache.getIUnlock(other);
        //if (unlock != null)
        //{
        AudioManager.Instance.StopSFX(AudioCollection.Instance.sfxClips[3]);
        //if (isUnlockAds)
        //{
        //    //bound[1].DOKill();
        //    //bound[1].fillAmount = 0;
        //    tmp = 2.5f;
        //}
        t = 0;
        //}
    }
    private void unlockQuick(IUnlock player)
    {
        if (normal.CurrentCoin <= 0 && normal.IsLock)
        {
            normal.UnLock(true, true);
            return;
        }
        if (player.CoinValue > 0 && normal.CurrentCoin > 0)
        {
            if (player.CoinValue < normal.CurrentCoin)
            {
                Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
                parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
                parameters[1] = new Firebase.Analytics.Parameter("value", player.CoinValue);
                parameters[2] = new Firebase.Analytics.Parameter("source", "UnLock_" + dataStatusObject.GetStatus_All_Level_Object().nameObject_This);
                SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("spend_virtual_currency", parameters);
                text.DOTextInt((int)normal.CurrentCoin, (int)(normal.CurrentCoin - player.CoinValue), 0.75f);
                normal.CurrentCoin -= player.CoinValue;
                dataStatusObject.AddAmountPaid((int)player.CoinValue);
                //bound[0].DOFillAmount(bound[0].fillAmount + (float)player.CoinValue / normal.DefaultCoin, 0.75f).OnComplete(() =>
                //{
                //    UpdateUI();
                //});
                (player as Player).coinValue = 0;
                DataManager.Instance.GetDataMoneyController().SetMoney(Money.TypeMoney.USD, 0);

               

            }
            else
            {
                text.DOTextInt((int)normal.CurrentCoin, 0, 0.75f).OnComplete(() => 
                {
                    (player as Player).coinValue -= normal.CurrentCoin;
                    DataManager.Instance.GetDataMoneyController().RemoveMoney(Money.TypeMoney.USD, (int)normal.CurrentCoin);

                    Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
                    parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
                    parameters[1] = new Firebase.Analytics.Parameter("value", (int)normal.CurrentCoin);
                    parameters[2] = new Firebase.Analytics.Parameter("source", "UnLock_" + dataStatusObject.GetStatus_All_Level_Object().nameObject_This);
                    SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("spend_virtual_currency", parameters);

                    dataStatusObject.AddAmountPaid((int)normal.CurrentCoin);
                    normal.CurrentCoin = 0;
                    //normal.UnLock(true, true);
                    //bound[0].DOFillAmount(1, 0.75f).OnComplete(() =>
                    //{
                    normal.UnLock(true, true);
                    //effect.DOPause();
                    //});
                });
           
            }
        }
    }
    private void unlockBuild(float index)
    {
        if (normal.CurrentCoin <= 0 && normal.IsLock)
        {
            normal.UnLock(true, true);
            return;
        }
        if (index > 0 && normal.CurrentCoin > 0)
        {
            int value = (int)normal.DefaultCoin / 100 * 2;
            if(value <= 0)
            {
                value = 1;
            }
            if(value > index)
            {
                value = (int)index;
            }
            if (normal.CurrentCoin < value)
            {
                value = (int)normal.CurrentCoin;
            }
            DataManager.Instance.GetDataMoneyController().RemoveMoney(Money.TypeMoney.USD, value);
            normal.CurrentCoin -= value;
            dataStatusObject.AddAmountPaid(value);
            Player player = Player.Instance;
            AllPool c = AllPoolContainer.Instance.Spawn(coinPrefab, player.transform.position + Vector3.up * 0.5f);
            (c as Coin).MoveToBuildLock(this.transform.position, 0.1f);
            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[3], 1, false);
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            UpdateUI();
            if (normal.CurrentCoin <= 0)
            {
                normal.UnLock(true, true);
                AudioManager.Instance.StopSFX(AudioCollection.Instance.sfxClips[3]);
                //effect.DOPause();
            }

            Firebase.Analytics.Parameter[] parameters = new Firebase.Analytics.Parameter[3];
            parameters[0] = new Firebase.Analytics.Parameter("virtual_currency_name", "Money");
            parameters[1] = new Firebase.Analytics.Parameter("value", value);
            parameters[2] = new Firebase.Analytics.Parameter("source", "UnLock_" + dataStatusObject.GetStatus_All_Level_Object().nameObject_This);
            SDK.ABIFirebaseManager.Instance.LogFirebaseEvent("spend_virtual_currency", parameters);
        }
    }
    //IEnumerator IE_AnimProcessUnLock(float Speed = 1)
    //{
    //    float m_time = bound[0].fillAmount;
    //    while (m_time <= dataStatusObject.GetAmountPaid() / normal.DefaultCoin + 0.1f)
    //    {
    //        bound[0].fillAmount = m_time;
    //        m_time += Time.deltaTime * Speed;
    //        yield return null;
    //    }
    //    isPlayAnim = false;
    //}
}
