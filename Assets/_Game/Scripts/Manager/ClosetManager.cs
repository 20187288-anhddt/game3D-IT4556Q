using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetManager : MonoBehaviour
{
    public List<Closet> listClosets;
    public List<Closet> listAvailableClosets;

    public void CheckClosetEmpty()
    {
        if (listClosets.Count == 0)
            return;
        for (int i = 0; i < listClosets.Count; i++)
        {
            listClosets[i].GetEmtyPlaceNum();
            if (listClosets[i].listEmtyPlaceToBuy.Count > 0)
            {
                if (!listAvailableClosets.Contains(listClosets[i]))
                {
                    listAvailableClosets.Add(listClosets[i]);
                }
            }
        }
    }
}
