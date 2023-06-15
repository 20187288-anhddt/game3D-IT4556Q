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
    public List<Customer> customerOnCheckOut;
    public Customer customerPrefab;
    public List<Transform> transformList;
    public int maxCus;
    [SerializeField]
    private PlaceManager placeManager;
    public bool isReadySpawn;
    public bool isDelaySpawn;
    public float delayTime;
    public float consDelayTime;

    void Start()
    {
        isReadySpawn = true;
        isDelaySpawn = false;
        delayTime = consDelayTime;
    }
    void Update()
    {
        if(customerList.Count > maxCus)
        {
            isReadySpawn = false;
        }
        else
        {
            if (isReadySpawn)
            {
                PlaceToBuy curPlace = placeManager.CheckPlaceEmpty();
                if (curPlace != null)
                {
                    isReadySpawn = false;
                    isDelaySpawn = true;
                    Customer curCus = SpawnCus();
                    if (curCus != null)
                    {
                        curPlace.AddCus(curCus);
                        placeManager.ResetPlace();
                    }
                }
            }
            if (isDelaySpawn)
            {
                delayTime -= Time.deltaTime;
            }
            if (delayTime < 0)
            {
                isReadySpawn = true;
                delayTime = consDelayTime;
            }
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
        }
        else
            curCus = null;
        return curCus as Customer;
    }
}
