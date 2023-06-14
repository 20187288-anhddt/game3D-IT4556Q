using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManager : MonoBehaviour
{
    public List<PlaceToBuy> listPlaceToBuy;
    public List<PlaceToBuy> listAvailablePlace;

    public void CheckPlaceOutfitEmpty()
    {
        if (listPlaceToBuy.Count == 0)
            return;
        for (int i = 0; i < listPlaceToBuy.Count; i++)
        {
            if (!listPlaceToBuy[i].isHaveCus)
            {
                if (!listAvailablePlace.Contains(listPlaceToBuy[i]))
                {
                    listAvailablePlace.Add(listPlaceToBuy[i]);
                }
            }
        }
    }
}
