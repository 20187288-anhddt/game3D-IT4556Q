using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Transform exitPos;
    public Transform startPos;
    public Transform checkOutPos;
    public List<Customer> customerList;
    public List<Customer> customerOnCloset;
    public List<GroupCustomer> listGroups;
    public Customer customerPrefab;
    public GroupCustomer grCusPrefab;
    public List<Transform> transformList;
    public int maxCus;
    [SerializeField]
    private ClosetManager closetManager;

    //[SerializeField]
    //private PlaceManager placeManager;
    public List<PlaceToBuy> listAvailablePlace;
    public bool isReadySpawn;
    public float delayTime;
    public float consDelayTime;


    void Start()
    {
        isReadySpawn = true;
        delayTime = consDelayTime;
    }
    void Update()
    {
        if (isReadySpawn && customerList.Count < maxCus)
        {
            isReadySpawn = false;
            closetManager.CheckClosetEmpty();
            if (closetManager.listAvailableClosets.Count > 0)
            {
                int r = Random.Range(0, closetManager.listAvailableClosets.Count);
                int x = Random.Range(1, closetManager.listAvailableClosets[r].listEmtyPlaceToBuy.Count + 1);
                PlaceToBuy curPlace = closetManager.listAvailableClosets[r].listEmtyPlaceToBuy[0];
                if(curPlace != null)
                {
                    GroupCustomer curGr = SpawnOneGroup(x, curPlace.transform);
                    curGr.AddCloset(closetManager.listAvailableClosets[r]);
                    curGr.type = closetManager.listAvailableClosets[r].type;
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
                //for (int i = 0; i < x; i++)
                //{
                //    PlaceToBuy curPlace = closetManager.listAvailableClosets[r].listEmtyPlaceToBuy[i];
                //    if (curPlace != null)
                //    {
                //        Customer curCus = SpawnCus();
                //        if (curCus != null)
                //        {
                //            curPlace.AddCus(curCus);
                //        }
                //    }
                //    if (i == x - 1)
                //    {
                //        closetManager.listAvailableClosets[r].listEmtyPlaceToBuy.Clear();
                //        closetManager.listAvailableClosets.Clear();
                //    }
                //}
            }
            //placeManager.CheckPlaceOutfitEmpty();
            //if (placeManager.listAvailablePlace.Count > 0)
            //{
            //    int i = Random.Range(0, placeManager.listAvailablePlace.Count);
            //    PlaceToBuy curPlace = placeManager.listAvailablePlace[i];
            //    if (curPlace != null)
            //    {
            //        Customer curCus = SpawnCus();
            //        if (curCus != null)
            //        {
            //            curPlace.AddCus(curCus);
            //            placeManager.listAvailablePlace.Clear();
            //        }
            //    }
            //}
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
        if (!customerList.Contains(curCus as Customer))
        {
            customerList.Add(curCus as Customer);
            (curCus as Customer).ResetStatus();
            (curCus as Customer).transExit = exitPos.position;
            (curCus as Customer).transCheckOut = checkOutPos.position;
        }
        else
            curCus = null;
        return curCus as Customer;
    }
    public GroupCustomer SpawnOneGroup(int n, Transform closetPos)
    {
        AllPool curGroup = AllPoolContainer.Instance.Spawn(grCusPrefab, startPos.position, Quaternion.identity); ;
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
                Customer x = SpawnCus(startPos.position - new Vector3(i * 2.5f, 0f, 0f));
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
}
