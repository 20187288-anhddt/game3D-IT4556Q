using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCustomer : AllPool
{
    public IngredientType type;
    public List<Customer> listCus;
    public Customer leader;
    public List<Customer> teammates;
    public ClosetBase closetBase;
    public int grNum;

    void Start()
    {
        grNum = 0;
    }
   
    public void AddLeader(Customer cus)
    {
        this.leader = cus;
    }
    public void AddTeammate(Customer cus)
    {
        listCus.Add(cus);
        if(!cus.isLeader && !teammates.Contains(cus))
        {
            teammates.Add(cus);
            cus.leader = leader;
        }
    }
    public void AddCloset(ClosetBase closet)
    {
        this.closetBase = closet;
    }
}
