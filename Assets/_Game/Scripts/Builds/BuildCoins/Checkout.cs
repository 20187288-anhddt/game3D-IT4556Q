using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Checkout : BuildCoins,ILock
{
    public bool isHaveCus;
    public bool isMoving;
    public List<GroupCustomer> listGrCusCheckout;
    public List<Customer> listCusCheckout;
    public Transform[] listCheckoutPos;
    [SerializeField]
    public CoinSpawn coinSpawn;
    [SerializeField]
    private float maxMoney;
    public List<Coin> coins = new List<Coin>();
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public GameObject unlockModel;
    public Transform transExit;
    public GameObject checkPlayer;
    public bool isHaveStaff;
    public override void Start()
    {
        base.Start();
    }
    public override void StartInGame()
    {
        base.StartInGame();
        isHaveCus = false;
        isMoving = false;
        isHaveStaff = false;
        listGrCusCheckout.Clear();
        listCusCheckout.Clear();
    }
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        //Debug.Log("Unlock");
        //Player p = Player.Instance;
        if (!IsLock)
        {
            return;
        }
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);

       //p.isUnlock = true;
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        //p.isUnlock = false;
                    });
                }); ;
            });
        }
        //checkUnlock.gameObject.SetActive(false);
        coinSpawn.gameObject.SetActive(true);
        //checkPush.gameObject.SetActive(true);
        //GetComponent<BoxCollider>().enabled = true;
        if (!levelManager.checkOutManager.listCheckout.Contains(this))
            levelManager.checkOutManager.listCheckout.Add(this);
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
                listCusCheckout.Add(grCustomer.listCus[i]);
                AddCus(grCustomer.listCus[i],listCusCheckout.Count);
            }
        }
    }
    public void AddCus(Customer customer, int n)
    {
        customer.checkOut = this;
        customer.transCheckOut = listCheckoutPos[n].position;
        customer.transExit = transExit.position;
        if (customer.isLeader)
        {
            customer.UpdateState(BaseCustomer.MOVE_CHECKOUT_STATE);
        }
        else
        {
            customer.UpdateState(BaseCustomer.FOLLOW_LEADER_STATE);
        }

    }
    public void CheckStatus()
    {
        if(listGrCusCheckout.Count > 0 && isHaveStaff) { }
        //if (isHaveCus)
        //{
        //    if (curCus.isLeader)
        //    {
        //        if (!isMoving && !curCus.onCheckoutPos)
        //        {
        //            isMoving = true;
        //            curCus.UpdateState(BaseCustomer.MOVE_CHECKOUT_STATE);
        //            curCus.grCus.TeamFollowLeader();
        //        }
        //        if (curCus.onCheckoutPos)
        //        {
        //            curCus.onCheckoutPos = false;
        //            curCus.UpdateState(BaseCustomer.EXIT_STATE);
        //            SpawnMoney(curCus.grCus.grNum,curCus.grCus.typeOutfit, curCus.grCus.typeBag);
        //            levelManager.customerManager.customerList.Remove(curCus);
        //            for(int i = 0; i < curCus.grCus.teammates.Count; i++)
        //            {
        //                levelManager.customerManager.customerList.Remove(curCus.grCus.teammates[i]);
        //            }
        //            //levelManager.customerManager.customerList.Remove(curCus);
        //            isHaveCus = false;
        //        }
        //    }
        //}
    }
    public void SpawnMoney(int n, IngredientType typeOutfit, IngredientType typeBag, Transform cusPos)
    {
        //TODO
        StartCoroutine(Spawn(timeDelay, () =>
        {
            indexMoney += n*2;
            if (indexMoney > maxMoneyPrefabs)
            {
               indexMoney = maxMoneyPrefabs;
            }
            if (coins.Count <= 40)
            {
                for (int i = 0; i < n*2; i++)
                {
                    var g = AllPoolContainer.Instance.Spawn(coinPrefab, cusPos.position, Quaternion.identity);
                    Vector3 cur = coinSpawn.SpawnObjectOnComplete(coins.Count);
                    g.transform.DOLocalJump(cur, 0.75f, 1, 0.25f).OnComplete(() =>
                    {
                        float r = UnityEngine.Random.Range(-5, 5);
                        g.transform.DORotate(new Vector3(0, r, 0), 0f);
                        coins.Add(g as Coin);
                    });
                }
            }
        }));
    }
    IEnumerator Spawn(float time, Action spawnMoney)
    {
        yield return new WaitForSeconds(time);
        spawnMoney();
    }
}
