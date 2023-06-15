using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField]
    private Transform exitPos;
    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform checkOutPos;
    public List<Customer> customerList;
    public List<Customer> customerOnCloset;
    public Customer customerPrefab;
    public List<Transform> transformList;
    public int maxCus;
    [SerializeField]
    private ClosetManager closetManager;
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
            if(closetManager.listAvailableClosets.Count > 0)
            {
                int r = Random.Range(0, closetManager.listAvailableClosets.Count);
                int x = Random.Range(1, closetManager.listAvailableClosets[r].listEmtyPlaceToBuy.Count+1);
                for(int i = 0; i < x; i++)
                {
                    PlaceToBuy curPlace = closetManager.listAvailableClosets[r].listEmtyPlaceToBuy[i];
                    if (curPlace != null)
                    {
                        Customer curCus = SpawnCus();
                        if (curCus != null)
                        {
                            curPlace.AddCus(curCus);
                        }
                    }
                    if(i == x - 1)
                    {
                        closetManager.listAvailableClosets[r].listEmtyPlaceToBuy.Clear();
                        closetManager.listAvailableClosets.Clear();
                    }
                }
            }
            //placeManager.CheckPlaceEmpty();
            //if(placeManager.listAvailablePlace.Count > 0)
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

    public Customer SpawnCus()
    {
        AllPool curCus = AllPoolContainer.Instance.Spawn(customerPrefab, startPos.position, Quaternion.identity);
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
    public void SpawnManyCus(int n)
    {

    }
}
