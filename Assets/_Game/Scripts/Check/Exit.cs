using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : BaseBuild
{
    private void OnTriggerEnter(Collider other)
    {
        var curCus = other.GetComponent<Customer>();
        if (curCus != null)
        {
            AllPoolContainer.Instance.Release(curCus);
            if (curCus.isLeader)
            {
                for(int i = 0; i < curCus.grCus.teammates.Count; i++)
                {
                    AllPoolContainer.Instance.Release(curCus.grCus.teammates[i]);
                }
                AllPoolContainer.Instance.Release(curCus.grCus);
            } 
        }
    }

}
