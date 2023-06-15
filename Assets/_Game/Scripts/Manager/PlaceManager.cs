using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    public List<PlaceToBuy> listPlaceToBuy;
    public PlaceToBuy curPlaceToBuy;
    

    public PlaceToBuy CheckPlaceEmpty()
    {
        if (listPlaceToBuy.Count == 0)
            curPlaceToBuy = null;
        for (int i = 0; i < listPlaceToBuy.Count; i++)
        {
            if (!listPlaceToBuy[i].isHaveCus)
            {
                curPlaceToBuy = listPlaceToBuy[i];
                break;
            }
        }
        return curPlaceToBuy;
    }
    public void ResetPlace()
    {
        curPlaceToBuy = null;
    }
}
