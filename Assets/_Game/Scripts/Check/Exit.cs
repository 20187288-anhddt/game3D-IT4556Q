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
        }
    }

}
