using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class CheckUnlock : MonoBehaviour
{
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
    private Image[] bound;
    [SerializeField]
    private GameObject[] bg;
    private bool isUnlockAds;

    public void UpdateUI()
    {
        normal = GetComponentInParent<ILock>();
        if ((normal as BaseBuild).isKey)
        {
            effect.DOPlay();
        }
        else
        {
            effect.DOPause();
        }

        //if ((normal as BaseBuild).unlockAds)
        //{
        //    bg[0].SetActive(true);
        //    bg[1].SetActive(false);
        //}
        //else
        //{
        //    bg[0].SetActive(false);
        //    bg[1].SetActive(true);
        //    bound[0].fillAmount = (float)(normal.DefaultCoin - normal.CurrentCoin) / normal.DefaultCoin;
        //    if (normal.CurrentCoin > 1000)
        //    {
        //        float x = normal.CurrentCoin / 1000;
        //        if (normal.CurrentCoin % 1000 == 0)
        //        {
        //            text.text = "$" + (x + ((normal.CurrentCoin - 1000 * x) / 1000)).ToString() + "K";
        //        }
        //        else
        //            text.text = "$" + (x + ((normal.CurrentCoin - 1000 * x) / 1000)).ToString("F2") + "K";
        //    }
        //    else if (normal.CurrentCoin > 100)
        //        text.text = "$" + string.Format("{000}", normal.CurrentCoin);
        //    else
        //        text.text = "$" + string.Format("{00}", normal.CurrentCoin);
        //}

    }
    private void OnTriggerStay(Collider other)
    {
        if (!(normal as BaseBuild).unlockAds)
        {
            if (!normal.IsLock) return;
            IUnlock unlock = other.GetComponent<IUnlock>();
            if (unlock != null)
            {
                t += Time.deltaTime;
                if (t > 0.75f)
                {
                    if (normal.CurrentCoin > 0)
                    {
                        if (t < 1.5f)
                        {
                            if (!(unlock as Player).canCatch)
                                return;
                            (unlock as Player).canCatch = false;
                            normal.UnLock();
                            //unlockBuild(unlock.CoinValue);
                            //unlock.UnlockMap(1);
                            //(unlock as Player).DelayCatch(0f);
                        }
                        else
                        {
                            if ((unlock as Player).canCatch)
                            {
                                normal.UnLock();
                                //unlockQuick(unlock);
                            }                             
                            else
                                (unlock as Player).DelayCatch(1);
                        }
                    }
                    else
                    {
                        normal.UnLock();
                    }
                }
            }
        }
        else
        {
            if (!normal.IsLock) return;
            IUnlock unlock = other.GetComponent<IUnlock>();
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
                    bound[1].DOFillAmount(1, 1.5f).OnComplete(() => { bound[1].fillAmount = 0; });
                    if (tmp < 0)
                    {
                        //normal.UnLock();
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
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.canCatch = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IUnlock unlock = other.GetComponent<IUnlock>();
        if (unlock != null)
        {
            if (isUnlockAds)
            {
                bound[1].DOKill();
                bound[1].fillAmount = 0;
                tmp = 2.5f;
            }
            t = 0;
        }
    }
    private void unlockQuick(IUnlock player)
    {
        if (normal.CurrentCoin <= 0 && normal.IsLock)
        {
            normal.UnLock();
            return;
        }
        if (player.CoinValue > 0 && normal.CurrentCoin > 0)
        {
            if (player.CoinValue < normal.CurrentCoin)
            {
                text.DOTextInt((int)normal.CurrentCoin, (int)(normal.CurrentCoin - player.CoinValue), 0.75f);
                normal.CurrentCoin -= player.CoinValue;
                bound[0].DOFillAmount(bound[0].fillAmount + (float)player.CoinValue / normal.DefaultCoin, 0.75f).OnComplete(() =>
                {
                    UpdateUI();
                });
                (player as Player).coinValue = 0;
            }
            else
            {
                text.DOTextInt((int)normal.CurrentCoin, 0, 0.75f);
                (player as Player).coinValue -= normal.CurrentCoin;
                normal.CurrentCoin = 0;
                bound[0].DOFillAmount(1, 0.75f).OnComplete(() =>
                {
                    normal.UnLock();
                });
            }
        }
    }
    private void unlockBuild(float index)
    {
        if (normal.CurrentCoin <= 0 && normal.IsLock)
        {
            normal.UnLock();
            return;
        }
        if (index > 0 && normal.CurrentCoin > 0)
        {
            normal.CurrentCoin -= 1f;
            bound[0].fillAmount += (float)1 / normal.DefaultCoin;
            Player player = Player.Instance;
            AllPool c = AllPoolContainer.Instance.Spawn(coinPrefab, player.transform.position + Vector3.up * 0.5f);
            (c as Coin).MoveToBuildLock(this.transform.position, 0f);
            UpdateUI();
            if (normal.CurrentCoin <= 0)
            {
                normal.UnLock();
            }

        }
    }
}
