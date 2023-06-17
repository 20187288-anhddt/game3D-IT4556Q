using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetManager : MonoBehaviour
{
    public List<Closet> listClosets;
    public List<Closet> listAvailableClosets;
    public List<BagCloset> listBagClosets;
    public List<BagCloset> listAvailableBagClosets;
    public List<Closet> listBearClosetActive;
    public List<Closet> listCowClosetActive;
    public List<Closet> listSheepClosetActive;
    public List<Closet> listChickenClosetActive;
    public List<BagCloset> listBearBagClosetActive;
    public List<BagCloset> listCowBagClosetActive;
    public List<BagCloset> listSheepBagClosetActive;
    public List<BagCloset> listChickenBagClosetActive;

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

    public void CheckBagClosetEmptyWithNum(int n)
    {
        listAvailableBagClosets.Clear();
        if (listBagClosets.Count == 0)
            return;
        for (int i = 0; i < listBagClosets.Count; i++)
        {
            listBagClosets[i].GetEmtyPlaceNum(n);
            if (listBagClosets[i].listEmtyPlaceToBuyBag.Count >= n)
            {
                if (!listAvailableBagClosets.Contains(listBagClosets[i]))
                {
                    listAvailableBagClosets.Add(listBagClosets[i]);
                }
            }
        }
    }
    public Closet GetClosetDontHaveOutfit()
    {
        Closet curCloset = null;
        if (listClosets.Count == 0)
            curCloset = null;
        for (int i = 0; i < listClosets.Count; i++)
        {
            if(listClosets[i].GetListEmptyOutfit() < 3)
            {
                curCloset = listClosets[i];
            }
            break;
        }
        return curCloset;
    }

    public BagCloset GetBagClosetDontHaveBag()
    {
        BagCloset curCloset = null;
        if (listClosets.Count == 0)
            curCloset = null;
        for (int i = 0; i < listBagClosets.Count; i++)
        {
            if (listBagClosets[i].GetListEmptyBag() < 3)
            {
                curCloset = listBagClosets[i];
            }
            break;
        }
        return curCloset;
    }
}
