using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCustomer : AllPool
{
    public IngredientType typeOutfit;
    public IngredientType typeBag;
    public List<Customer> listCus;
    public Customer leader;
    public List<Customer> teammates;
    public ClosetBase closetBase;
    public int grNum;

    public void AddLeader(Customer cus)
    {
        this.leader = cus;
        cus.grCus = this;
    }
    public void AddTeammate(Customer cus)
    {
        listCus.Add(cus);
        if(!cus.isLeader && !teammates.Contains(cus))
        {
            teammates.Add(cus);
            cus.grCus = this;
            cus.leader = leader;
        }
    }
    public void AddCloset(ClosetBase closet)
    {
        this.closetBase = closet;
    }
    public bool CheckGotOutfit()
    {
        for(int i = 0; i < teammates.Count; i++)
        {
            if (!teammates[i].gotOutfit)
            {
                return false;
            }
        }
        return true;
    }
    public bool CheckGotBag()
    {
        for (int i = 0; i < teammates.Count; i++)
        {
            if (!teammates[i].gotBag)
            {
                return false;
            }
        }
        return true;
    }
    public void TeamFollowLeader()
    {
        for (int i = 0; i < teammates.Count; i++)
        {
            teammates[i].UpdateState(BaseCustomer.FOLLOW_LEADER_STATE);
        }
    }
    public void TeamIdle()
    {
        for (int i = 0; i < teammates.Count; i++)
        {
            teammates[i].UpdateState(BaseCustomer.IDLE_STATE);
        }
    }
    public void ResetGroup()
    {
        listCus.Clear();
        leader = null;
        teammates.Clear();
        grNum = 0;
    }
}
