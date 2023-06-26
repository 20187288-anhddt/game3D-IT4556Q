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
    public bool isCheckout;
    [SerializeField]
    private float delayTime;
    public float consDelayCheckout;
    [SerializeField]
    private CheckUnlock checkUnlock;
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    public override void StartInGame()
    {
        base.StartInGame();
        CurrentCoin = pirceObject.Get_Pirce();
        // Debug.Log(dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis());
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
            dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        isHaveStaff = false;
        isCheckout = false;
        delayTime = consDelayCheckout;
        listGrCusCheckout.Clear();
        listCusCheckout.Clear();
        if (isLock)
        {
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            coinSpawn.gameObject.SetActive(false);
            GetComponent<BoxCollider>().enabled = false;
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
        Debug.Log("a");
      //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        p.isUnlock = true;
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(3, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.isUnlock = false;
                        GetComponent<BoxCollider>().enabled = true;
                        coinSpawn.gameObject.SetActive(true);
                    });
                }); ;
            });
        }
        checkUnlock.gameObject.SetActive(false);
        coinSpawn.gameObject.SetActive(true);
        //checkPush.gameObject.SetActive(true);
        //GetComponent<BoxCollider>().enabled = true;
        if (!levelManager.checkOutManager.listCheckout.Contains(this))
            levelManager.checkOutManager.listCheckout.Add(this);

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
    }
    void Update()
    {
        CheckStatus();
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
                    SpawnMoney(curGr.grNum, curGr.typeOutfit, curGr.typeBag,
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
    public void SpawnMoney(int n,IngredientType typeOutfit, IngredientType typeBag, Transform cusPos)
    {
        int a = 0;
        int b = 0;
        switch (typeOutfit)
        {
            case IngredientType.CHICKEN:
                a = n;
                break;
            case IngredientType.COW:
                a = n*2;
                break;
            case IngredientType.BEAR:
                a = n*3;
                break;
        }
        switch (typeBag)
        {
            case IngredientType.CHICKEN:
                b = n;
                break;
            case IngredientType.COW:
                b = n * 2;
                break;
            case IngredientType.BEAR:
                b = n * 3;
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
                g.transform.DOLocalJump(cur, 5f, 1, 0.5f).OnComplete(() =>
                {
                    float r = UnityEngine.Random.Range(-5, 5);
                    g.transform.DORotate(new Vector3(0, r, 0), 0f);
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
        GetComponent<BoxCollider>().enabled = false;
        isHaveStaff = true;
    }
}
