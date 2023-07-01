using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Checkout : BuildCoins,ILock
{
    public List<GroupCustomer> listGrCusCheckout;
    public List<Customer> listCusCheckout;
    public Transform[] listCheckoutPos;
    [SerializeField]
    public CoinSpawn coinSpawn;
    [SerializeField]
    private int maxMoney;
    public List<Coin> coins = new List<Coin>();
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public GameObject unlockModel;
    public Transform transExit;
    public bool isHaveStaff;
    public bool isHired;
    public bool isCheckout;
    [SerializeField]
    private float delayTime;
    public float consDelayCheckout;
    [SerializeField]
    private GameObject staffModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    public int coinSave;
    public GameObject fxPos;

    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    public override void StartInGame()
    {
        base.StartInGame();
        LoadCoin_Not_Collect();
        CurrentCoin = pirceObject.Get_Pirce();
        EnventManager.AddListener(EventName.QuitGame.ToString(), SaveCoin_Not_Collect);
        // Debug.Log(dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis());
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
            dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        isHaveStaff = false;
        isCheckout = false;
        delayTime = consDelayCheckout;
        listGrCusCheckout.Clear();
        listCusCheckout.Clear();
        isHired = (dataStatusObject as DataCheckOutTable).GetData_IsHireStaff();
        if (isLock)
        {
            unlockFx.SetActive(false);
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            coinSpawn.gameObject.SetActive(false);
            staffModel.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
            if (CurrentCoin <= 0)
            {
                UnLock(true, true);
            }
            fxPos.SetActive(false);
        }
        else
        {
            if (isHired)
            {
                GetComponent<BoxCollider>().enabled = false;
                staffModel.SetActive(true);
                isHaveStaff = true;
            }
            else
            {
                GetComponent<BoxCollider>().enabled = true;
                staffModel.SetActive(false);
            }
        }
        checkUnlock.UpdateUI();

    }
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        //Debug.Log("Unlock");
        Player p = Player.Instance;
        if (!IsLock)
        {
            return;
        }
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //Debug.Log("a");
        //EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        
        unlockModel.SetActive(true);
        unlockFx.SetActive(true);
        //lockModel.SetActive(false);
        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 6f)
        {
            p.myTransform.position = checkUnlock.myTransform.position + Vector3.forward * 4;
        }
        if (isPlayAnimUnlock) //anim
        {
            p.PlayerStopMove();
            unlockModel.transform.DOMoveY(3, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        unlockFx.SetActive(false);
                        if (isHired)
                        {
                            GetComponent<BoxCollider>().enabled = false;
                            staffModel.SetActive(true);
                            isHaveStaff = true;
                            fxPos.SetActive(false);
                        }
                        else
                        {
                            GetComponent<BoxCollider>().enabled = true;
                            staffModel.SetActive(false);
                            fxPos.SetActive(true);
                        }
                        if (coinSave > 0)
                        {
                            SpawnMoney(true, coinSave, IngredientType.NONE, IngredientType.NONE, this.transform);
                        }
                        //p.isUnlock = false;
                        if (CameraController.Instance.IsCameraFollowPlayer())
                        {
                            p.PlayerContinueMove();
                        }
                      
                        //CounterHelper.Instance.QueueAction(1f, () => { p.isUnlock = false; });
                    });
                }); ;
            });
        }
        //else
        //{
        //    p.isUnlock = false;
        //    if (isHired)
        //    {
        //        GetComponent<BoxCollider>().enabled = false;
        //        staffModel.SetActive(true);
        //        isHaveStaff = true;
        //    }
        //    else
        //    {
        //        GetComponent<BoxCollider>().enabled = true;
        //        staffModel.SetActive(false);
        //    }
        //}
        checkUnlock.gameObject.SetActive(false);
        coinSpawn.gameObject.SetActive(true);
      
        //checkPush.gameObject.SetActive(true);
        //GetComponent<BoxCollider>().enabled = true;


        switch (nameObject_This)
        {
            case NameObject_This.CheckOutTable:
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.CheckOutTable_Complete.ToString());
                }
                break;
            case NameObject_This.CheckOutTable_1:
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.CheckOutTable_1_Complete.ToString());
                }
                break;
        }
        if (!levelManager.checkOutManager.listCheckout.Contains(this))
            levelManager.checkOutManager.listCheckout.Add(this);
    }
    void Update()
    {
        CheckStatus();
        if (!isLock && isHired)
        {
            if (listCusCheckout.Count > 0)
            {
                staffModel.GetComponent<Animator>().Play("Wave");
            }
            else
            {
                staffModel.GetComponent<Animator>().Play("IdleNormal");
            }
        }
    }
    public void AddGrCus(GroupCustomer grCustomer)
    {
        if (!listGrCusCheckout.Contains(grCustomer))
        {
            listGrCusCheckout.Add(grCustomer);
        }
        for(int i = 0; i < grCustomer.grNum ; i++)
        {
            if (!listCusCheckout.Contains(grCustomer.listCus[i]))
            {
                Customer curCus = grCustomer.listCus[i];
                listCusCheckout.Add(curCus);
                curCus.checkOut = this;
                curCus.transCheckOut = listCheckoutPos[listCusCheckout.Count - 1].position;
                curCus.transExit = transExit.position;
                if (curCus.isLeader)
                {
                    curCus.UpdateState(BaseCustomer.MOVE_CHECKOUT_STATE);
                }
                else
                {
                    curCus.UpdateState(BaseCustomer.FOLLOW_LEADER_STATE);
                }
            }
        }
    }
    public void CheckStatus()
    {
        if(listGrCusCheckout.Count > 0 && isHaveStaff && !isCheckout)
        {
            for(int i = 0; i < listGrCusCheckout.Count; i++)
            {
                if (listGrCusCheckout[i].CheckonCheckoutPos())
                {
                    isCheckout = true;
                    staffModel.GetComponent<Animator>().Play("Wave");
                    GroupCustomer curGr = listGrCusCheckout[i];
                    listGrCusCheckout.Remove(listGrCusCheckout[i]);
                    curGr.leader.UpdateState(BaseCustomer.EXIT_STATE);
                    curGr.leader.isExit = true;
                    listCusCheckout.Remove(curGr.leader);
                    for (int j = 0; j < curGr.teammates.Count ; j++)
                    {
                        curGr.teammates[j].UpdateState(BaseCustomer.FOLLOW_LEADER_STATE);
                        listCusCheckout.Remove(curGr.teammates[j]);
                    }
                    SpawnMoney(false, curGr.grNum, curGr.typeOutfit, curGr.typeBag,
                            curGr.leader.transform);
                    for (int x = 0; x < curGr.listCus.Count; x++)
                    {
                        levelManager.customerManager.customerList.Remove(curGr.listCus[x]);
                    }
                }
            }
        }
        if (isCheckout)
        {
            delayTime -= Time.deltaTime;
        }
        if(delayTime < 0)
        {
            isCheckout = false;
            delayTime = consDelayCheckout;
        }
    }
    public void SpawnMoney(bool isStart, int n,IngredientType typeOutfit, IngredientType typeBag, Transform cusPos)
    {
        int a = 0;
        int b = 0;
        switch (typeOutfit)
        {
            case IngredientType.CHICKEN:
                a = n * GameManager.Instance.dataPrice.Data.chickenOutfit;
                break;
            case IngredientType.COW:
                a = n * GameManager.Instance.dataPrice.Data.cowOutfit;
                break;
            case IngredientType.BEAR:
                a = n * GameManager.Instance.dataPrice.Data.bearOutfit;
                break;
            case IngredientType.LION:
                a = n * GameManager.Instance.dataPrice.Data.lionOutfit;
                break;
            case IngredientType.CROC:
                a = n * GameManager.Instance.dataPrice.Data.crocOutfit;
                break;
            case IngredientType.ELE:
                a = n * GameManager.Instance.dataPrice.Data.eleOutfit;
                break;
            case IngredientType.ZEBRA:
                a = n * GameManager.Instance.dataPrice.Data.zebraOutfit;
                break;
            case IngredientType.NONE:
                a = n / 2;
                break;
        }
        switch (typeBag)
        {
            case IngredientType.CHICKEN:
                b = n * GameManager.Instance.dataPrice.Data.chickenBag;
                break;
            case IngredientType.COW:
                b = n * GameManager.Instance.dataPrice.Data.cowBag; ;
                break;
            case IngredientType.BEAR:
                b = n * GameManager.Instance.dataPrice.Data.bearBag; ;
                break;
            case IngredientType.LION:
                b = n * GameManager.Instance.dataPrice.Data.lionBag;
                break;
            case IngredientType.CROC:
                b = n * GameManager.Instance.dataPrice.Data.crocBag; ;
                break;
            case IngredientType.ELE:
                b = n * GameManager.Instance.dataPrice.Data.eleBag; ;
                break;
            case IngredientType.ZEBRA:
                b = n * GameManager.Instance.dataPrice.Data.zebraBag; ;
                break;
            case IngredientType.NONE:
                if (isStart)
                {
                    b = n / 2;
                }
                else
                {
                    b = 0;
                }
                break;
        }
        indexMoney += (a+b);
        if (indexMoney > maxMoneyPrefabs)
        {
            indexMoney = maxMoneyPrefabs;
        }
        if (coins.Count <= maxMoney)
        {
            for (int i = 0; i < (a+b); i++)
            {
                var g = AllPoolContainer.Instance.Spawn(coinPrefab, cusPos.position, Quaternion.identity);
                Vector3 cur = coinSpawn.SpawnObjectOnComplete(coins.Count);
                coins.Add(g as Coin);
                if (typeBag != IngredientType.NONE && typeOutfit != IngredientType.NONE)
                {
                    coinSave++;
                }
                g.transform.DOLocalJump(cur, 5f, 1, 0.5f).OnComplete(() =>
                {
                    //float r = UnityEngine.Random.Range(-5, 5);
                    //g.transform.DORotate(new Vector3(0, r, 0), 0f);
                });
            }
        }
        //TODO
        //StartCoroutine(Spawn(timeDelay, () =>
        //{
        //    indexMoney += n*2;
        //    if (indexMoney > maxMoneyPrefabs)
        //    {
        //       indexMoney = maxMoneyPrefabs;
        //    }
        //    if (coins.Count <= 40)
        //    {
        //        for (int i = 0; i < 2; i++)
        //        {
        //            var g = AllPoolContainer.Instance.Spawn(coinPrefab, cusPos.position, Quaternion.identity);
        //            Vector3 cur = coinSpawn.SpawnObjectOnComplete(coins.Count);
        //            g.transform.DOLocalJump(cur, 0.75f, 1, 0.25f).OnComplete(() =>
        //            {
        //                float r = UnityEngine.Random.Range(-5, 5);
        //                g.transform.DORotate(new Vector3(0, r, 0), 0f);
        //                coins.Add(g as Coin);
        //            });
        //        }
        //        //var g = AllPoolContainer.Instance.Spawn(coinPrefab, cusPos.position, Quaternion.identity);
        //        //Vector3 cur = coinSpawn.SpawnObjectOnComplete(coins.Count);
        //        //g.transform.DOLocalJump(cur, 2f, 1, 0.25f).OnComplete(() =>
        //        //{
        //        //    float r = UnityEngine.Random.Range(-5, 5);
        //        //    g.transform.DORotate(new Vector3(0, r, 0), 0f);
        //        //    coins.Add(g as Coin);
        //        //});
        //    }
        //}));
    }
    IEnumerator Spawn(float time, Action spawnMoney)
    {
        yield return new WaitForSeconds(time);
        spawnMoney();
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            isHaveStaff = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            isHaveStaff = false;
        }
    }
    public void BuyStaff()
    {
        isHired = true;
        GetComponent<BoxCollider>().enabled = false;
        fxPos.SetActive(false);
        staffModel.SetActive(true);
        staffModel.GetComponent<Animator>().Play("IdleNormal");
        isHaveStaff = true;
        (dataStatusObject as DataCheckOutTable).SetData_IsHireStaff(isHired);
    }
    public void SaveCoin_Not_Collect()
    {
        (dataStatusObject as DataCheckOutTable).SetCount_Money_Not_Collect(coinSave);
    }
    public void LoadCoin_Not_Collect()
    {
        coinSave = (dataStatusObject as DataCheckOutTable).GetCount_Money_Not_Collect();
    }
}
