using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomerManager : MonoBehaviour
{
    public Transform exitPos;
    public Transform startPos;
    public Transform checkOutPos;
    public List<Customer> customerList;
    public List<Customer> customerOnCloset;
    public List<GroupCustomer> listGroups;
    public List<GroupCustomer> listGroupsHaveOutfit;
    public List<GroupCustomer> listGroupsHaveBag;
    //public List<GroupCustomer> listGroupRdCheckout;
    public Customer customerPrefab;
    public GroupCustomer grCusPrefab;
    public List<Transform> transformList;
    public int maxCus;
    [SerializeField]
    private ClosetManager closetManager;
    [SerializeField]
    private CheckOutManager checkoutManager;
    private GameManager gameManager;
    private LevelManager levelManager;

    //[SerializeField]
    //private PlaceManager placeManager;
    public List<PlaceToBuy> listAvailablePlace;
    public bool isReadySpawn;
    public bool isCheckBag;
    public bool isCheckout;
    public float delayTime;
    public float consDelayTime;
    public float delayCheckBagTime;
    public float consDelayCheckBagTime;
    public float delayCheckoutTime;
    public float consDelayCheckoutTime;


    void Start()
    {
        isReadySpawn = true;
        isCheckBag = false;
        isCheckout = false;
        delayTime = consDelayTime;
        delayCheckBagTime = consDelayCheckBagTime;
        delayCheckoutTime = consDelayCheckoutTime;
        gameManager = GameManager.Instance;
        levelManager = gameManager.listLevelManagers[gameManager.curLevel];
    }
    void Update()
    {
        if (levelManager.closetManager.listClosets.Count < 2)
        {
            maxCus = 3;
        }
        else if(levelManager.closetManager.listClosets.Count == 2)
        {
            maxCus = 7;
        }
        else if (levelManager.closetManager.listClosets.Count > 2)
        {
            maxCus = 10;
        }
        if (closetManager.listClosets.Count > 0)
        {
            CheckCusToOutfit();
        }
        if(closetManager.listBagClosets.Count > 0)
            CheckCusToBag();
        if(checkoutManager.listCheckout.Count > 0)
            CheckCusCheckOut();
    }
     
    public void ResetCusOnOpen()
    {
        foreach (Customer customer in customerList)
        {
            AllPoolContainer.Instance.Release(customer);
        }
        customerList.Clear();
    }

    public Customer SpawnCus(Vector3 spawnPos)
    {
        AllPool curCus = AllPoolContainer.Instance.Spawn(customerPrefab, spawnPos, Quaternion.identity);
        curCus.transform.DORotate(new Vector3(0f, 180f, 0f), 0f);
        (curCus as Customer).ResetStatus();
        if (!customerList.Contains(curCus as Customer))
        {
            customerList.Add(curCus as Customer);
            (curCus as Customer).transExit = exitPos.position;
            (curCus as Customer).transCheckOut = checkOutPos.position;
        }
        else
            curCus = null;
        return curCus as Customer;
    }
    public GroupCustomer SpawnOneGroup(int n, Transform closetPos)
    {
        AllPool curGroup = AllPoolContainer.Instance.Spawn(grCusPrefab, startPos.position, Quaternion.identity);
        (curGroup as GroupCustomer).ResetGroup();
        (curGroup as GroupCustomer).grNum = n;
        Customer c = SpawnCus(startPos.position);
        if (!(curGroup as GroupCustomer).listCus.Contains(c))
        {
            (curGroup as GroupCustomer).listCus.Add(c);
            (curGroup as GroupCustomer).AddLeader(c);
            c.transform.parent = curGroup.transform;
            c.isLeader = true;
            c.transCloset = closetPos.position;
            for (int i = 1; i < n; i++)
            {
                Customer x = SpawnCus(startPos.position + new Vector3(0f, 0f, i * 2.5f));
                (curGroup as GroupCustomer).AddTeammate(x);
            }
            if (!listGroups.Contains((curGroup as GroupCustomer)) && (curGroup as GroupCustomer).listCus.Count > 0)
            {
                listGroups.Add((curGroup as GroupCustomer));
            }
        }
        else 
            curGroup = null;
        return curGroup as GroupCustomer;
    }
    private void CheckCusToOutfit()
    {
        if (isReadySpawn && customerList.Count < maxCus)
        {

            closetManager.CheckClosetEmpty();
            if (closetManager.listAvailableClosets.Count > 0)
            {
                isReadySpawn = false;
                int r = Random.Range(0, closetManager.listAvailableClosets.Count);
                int x;
                if (closetManager.listAvailableClosets[r].listEmtyPlaceToBuy.Count >= 3)
                {
                    x = Random.Range(1, 4);
                }
                else
                {
                    x = Random.Range(1, closetManager.listAvailableClosets[r].listEmtyPlaceToBuy.Count + 1);
                }
                PlaceToBuy curPlace = closetManager.listAvailableClosets[r].listEmtyPlaceToBuy[0];
                if (curPlace != null)
                {
                    GroupCustomer curGr = SpawnOneGroup(x, curPlace.transform);
                    curGr.AddCloset(closetManager.listAvailableClosets[r]);
                    curGr.typeOutfit = closetManager.listAvailableClosets[r].ingredientType;
                    if (curGr != null)
                    {
                        curPlace.AddCus(curGr.leader);
                        for (int i = 0; i < curGr.teammates.Count; i++)
                        {
                            PlaceToBuy nextPlace = closetManager.listAvailableClosets[r].listEmtyPlaceToBuy[i + 1];
                            curGr.teammates[i].transform.parent = curGr.transform;
                            nextPlace.AddCus(curGr.teammates[i]);
                        }
                        closetManager.listAvailableClosets[r].listEmtyPlaceToBuy.Clear();
                        closetManager.listAvailableClosets.Clear();
                    }
                }
            }
        }
        if (!isReadySpawn)
        {
            delayTime -= Time.deltaTime;
        }
        if (delayTime < 0)
        {
            isReadySpawn = true;
            delayTime = consDelayTime;
        }
    }

    private void CheckCusToBag()
    {
        if (listGroupsHaveOutfit.Count > 0 && !isCheckBag)
        {
            closetManager.CheckBagClosetEmptyWithNum(listGroupsHaveOutfit[0].grNum);
            if (closetManager.listAvailableBagClosets.Count > 0)
            {
                isCheckBag = true;
                int r = Random.Range(0, closetManager.listAvailableBagClosets.Count);
                BagCloset curBagCloset = closetManager.listAvailableBagClosets[r] ;
                GroupCustomer curGr = listGroupsHaveOutfit[0];
                PlaceToBuyBag curBagPlace = curBagCloset.listEmtyPlaceToBuyBag[0];
                if (curBagPlace != null)
                {
                    closetManager.listAvailableBagClosets.Remove(curBagCloset);
                    listGroupsHaveOutfit.Remove(curGr);
                    curGr.AddCloset(curBagCloset);
                    curGr.typeBag = curBagCloset.ingredientType;
                    for (int i = 0; i < curGr.listCus.Count; i++)
                    {
                        PlaceToBuyBag nextPlace = curBagCloset.listEmtyPlaceToBuyBag[i];
                        nextPlace.AddCus(curGr.listCus[i]);
                        curGr.listCus[i].placeToBuy.isHaveCus = false;
                        curGr.listCus[i].placeToBuy.readyGo = false;
                        curGr.listCus[i].onPlacePos = false;
                    }
                    //curBagPlace.AddCus(leader);
                    //leader.placeToBuy.isHaveCus = false;
                    //leader.onPlacePos = false;
                    //closetManager.listAvailableBagClosets[r].listEmtyPlaceToBuyBag.Clear();
                    //closetManager.listAvailableBagClosets.Clear();
                    for (int i = 0; i < curGr.listCus.Count; i++)
                    {
                        //listGroupsHaveOutfit[0].listCus[i].onPlacePos = false;
                        curGr.listCus[i].placeToBuy.closet.listCurCus.Remove(curGr.listCus[i]);
                    }
                    //leader.placeToBuy.isHaveCus = false;
                }
            }

        }
        if (isCheckBag)
        {
            delayCheckBagTime -= Time.deltaTime;
        }
        if (delayCheckBagTime < 0)
        {
            isCheckBag = false;
            delayCheckBagTime = consDelayCheckBagTime;
        }
    }
    private void CheckCusCheckOut()
    {
        if (listGroupsHaveBag.Count > 0 && !isCheckout)
        {
            Checkout c = checkoutManager.GetEmtyCheckout();
            if (c != null)
            {
                isCheckout = true;
                GroupCustomer curGr = listGroupsHaveBag[0];
                listGroupsHaveBag.Remove(listGroupsHaveBag[0]);
                c.AddCus(curGr.leader);
                //curGr.leader.placeToBuyBag.isHaveCus = false;
                checkoutManager.listGrCusCheckout.Add(curGr);
                for (int i = 0; i < curGr.listCus.Count; i++)
                {
                    curGr.listCus[i].onBagPos = false;
                    curGr.listCus[i].placeToBuyBag.isHaveCus = false;
                    curGr.listCus[i].placeToBuyBag.readyGo = false;
                    curGr.listCus[i].placeToBuyBag.closet.listCurCus.Remove(curGr.listCus[i]);
                }         
            }
        }
        if (isCheckout)
        {
            delayCheckoutTime -= Time.deltaTime;
        }
        if (delayCheckoutTime < 0)
        {
            isCheckout = false;
            delayCheckoutTime = consDelayCheckoutTime;
        }
    }
}
