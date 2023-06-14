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
    public List<Closet> listLionClosetActive;
    public List<Closet> listCrocClosetActive;
    public List<Closet> listEleClosetActive;
    public List<Closet> listZebraClosetActive;

    public List<BagCloset> listBearBagClosetActive;
    public List<BagCloset> listCowBagClosetActive;
    public List<BagCloset> listSheepBagClosetActive;
    public List<BagCloset> listChickenBagClosetActive;
    public List<BagCloset> listLionBagClosetActive;
    public List<BagCloset> listCrocBagClosetActive;
    public List<BagCloset> listEleBagClosetActive;
    public List<BagCloset> listZebraBagClosetActive;

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
    public Closet GetClosetDontHaveOutfit(int n)
    {
        Closet curCloset = null;
        if (listClosets.Count <= 0)
        {
            curCloset = null;
        }
        else
        {
            int r = Random.Range(0, listClosets.Count);
            if (listClosets[r].GetListEmptyOutfit() < n)
            {
                curCloset = listClosets[r];
            }
        }
        //else
        //{
        //    bool tmp = false;
        //    do
        //    {
        //        int r = Random.Range(0, listClosets.Count);
        //        curCloset = listClosets[r];
        //        if (listClosets[r].GetListEmptyOutfit() < n)
        //        {
        //            tmp = true;
        //        }
        //    } while (!tmp); 
        //}
        return curCloset;
    }

    public BagCloset GetBagClosetDontHaveBag(int n)
    {
        BagCloset curBagCloset = null;
        if (listBagClosets.Count <= 0)
        {
            curBagCloset = null;
        }
        else
        {
            int r = Random.Range(0, listBagClosets.Count);
            if (listBagClosets[r].GetListEmptyBag() < n)
            {
                curBagCloset = listBagClosets[r]; ;
            }
            //bool tmp = false;
            //do
            //{
            //    int r = Random.Range(0, listBagClosets.Count);
            //    curBagCloset = listBagClosets[r];
            //    if (listBagClosets[r].GetListEmptyBag() < n)
            //    {
            //        tmp = true;
            //    }
            //} while (!tmp);
        }
        return curBagCloset;
        //BagCloset curCloset = null;
        //if (listBagClosets.Count == 0)
        //    curCloset = null;
        //for (int i = 0; i < listBagClosets.Count; i++)
        //{
        //    if (listBagClosets[i].GetListEmptyBag() < n)
        //    {
        //        curCloset = listBagClosets[i];
        //        break;
        //    }
           
        //}
        //return curCloset;
    }
}
