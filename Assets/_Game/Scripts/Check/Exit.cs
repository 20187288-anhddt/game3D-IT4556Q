using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : BaseBuild
{

    private void OnTriggerEnter(Collider other)
    {
        //var curCus = other.GetComponent<Customer>();
        var curCus = Cache.getCustomer(other);
        //if (curCus != null)
        //{
            if (curCus.isLeader)
            {
                AllPoolContainer.Instance.Release(curCus);
                //levelManager.customerManager.customerList.Remove(curCus);
                for (int i = 0; i < curCus.grCus.teammates.Count; i++)
                {
                    AllPoolContainer.Instance.Release(curCus.grCus.teammates[i]);
                    //levelManager.customerManager.customerList.Remove(curCus.grCus.teammates[i]);

                }
                levelManager.customerManager.listGroups.Remove(curCus.grCus);
                AllPoolContainer.Instance.Release(curCus.grCus);
            } 
        //}
    }

}
