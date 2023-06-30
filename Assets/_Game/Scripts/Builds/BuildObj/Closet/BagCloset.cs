using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BagCloset : ClosetBase
{
    public List<BagBase> listBags;
    public List<BagPos> listBagPos;
    [SerializeField]
    private BagBase outFitPrefab;
    public List<PlaceToBuyBag> listPlaceToBuyBag;
    public List<PlaceToBuyBag> listEmtyPlaceToBuyBag;
    [SerializeField]
    private GameObject unlockModel;
    //[SerializeField]
    //private GameObject lockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private CheckPushBagCloset checkPushBagCloset;
   

    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        p.isUnlock = true;
      //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 4f)
        {
            p.myTransform.position = checkUnlock.myTransform.position - Vector3.forward * 4;
        }
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        
                        //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        foreach (PlaceToBuyBag place in listPlaceToBuyBag)
                        {
                            place.gameObject.SetActive(true);

                        }
                        foreach (BagPos o in listBagPos)
                        {
                            o.gameObject.SetActive(true);
                            o.StartInGame(); 
                        }
                        checkPushBagCloset.gameObject.SetActive(true);
                        p.isUnlock = false;
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
            //foreach (PlaceToBuyBag place in listPlaceToBuyBag)
            //{
            //    place.gameObject.SetActive(true);

            //}
            //foreach (BagPos o in listBagPos)
            //{
            //    o.gameObject.SetActive(true);
            //    o.StartInGame();
            //}
        //    EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());

            checkPushBagCloset.gameObject.SetActive(true);
        }
        checkUnlock.gameObject.SetActive(false);
       
        //GetComponent<BoxCollider>().enabled = true;
       //levelManager.closetManager.listAllActiveClosets.Add(this);
       
        switch (ingredientType)
        {
            case IngredientType.BEAR:
                if (!levelManager.closetManager.listBearBagClosetActive.Contains(this))
                    levelManager.closetManager.listBearBagClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.BearBagCloset:
                            EnventManager.TriggerEvent(EventName.BearBagCloset_Complete.ToString());
                            break;
                    }
                }
                break;
            case IngredientType.COW:
                if (!levelManager.closetManager.listCowBagClosetActive.Contains(this))
                    levelManager.closetManager.listCowBagClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.CowBagCloset:
                            EnventManager.TriggerEvent(EventName.CowBagCloset_Complete.ToString());
                            break;
                    }
                }
                break;
            case IngredientType.CHICKEN:
                if (!levelManager.closetManager.listChickenBagClosetActive.Contains(this))
                    levelManager.closetManager.listChickenBagClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.ChickenBagCloset:
                            EnventManager.TriggerEvent(EventName.ChickenBagCloset_Complete.ToString());
                            break;
                    }
                }
                break;
            case IngredientType.SHEEP:
                if (!levelManager.closetManager.listSheepBagClosetActive.Contains(this))
                    levelManager.closetManager.listSheepBagClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.SheepBagCloset:
                            EnventManager.TriggerEvent(EventName.SheepBagCloset_Complete.ToString());
                            break;
                    }
                }
                break;
        }
        if (!levelManager.closetManager.listBagClosets.Contains(this))
            levelManager.closetManager.listBagClosets.Add(this);
    }
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    public void SpawnOutfit()
    {
        BagPos o = GetEmtyPos();
        var curBag = AllPoolContainer.Instance.Spawn(outFitPrefab, o.myTransform.position, myTransform.rotation);
        (curBag as BagBase).ResetOutfit();
        if (!listBags.Contains(curBag as BagBase))
        {
            o.AddOutfit(curBag as BagBase);
            (curBag as BagBase).AddPos(o);
            listBags.Add(curBag as BagBase);
        }
        (curBag as BagBase).myTransform.parent = o.myTransform;
        (curBag as BagBase).myTransform.position = o.myTransform.position;
        (curBag as BagBase).myTransform.localRotation = Quaternion.identity;
    }
    public override void StartInGame()
    {
        base.StartInGame();
        LoadData_IsHaveObj_In_Pos();
        EnventManager.AddListener(EventName.QuitGame.ToString(), SaveData_IsHaveObj_In_Pos);
        CurrentCoin = pirceObject.Get_Pirce();
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
           dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        foreach (PlaceToBuyBag p in listPlaceToBuyBag)
        {  
            p.SetCloset(this);
            p.StartInGame();
            //p.gameObject.SetActive(false);
        }
        foreach (BagPos o in listBagPos)
        {
            o.SetCloset(this);
            //o.gameObject.SetActive(false);
        }
        if (isLock)
        {
            checkPushBagCloset.gameObject.SetActive(false);
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
        }
        //else
        //{
        //    UnLock();
        //}
        checkUnlock.UpdateUI();
    }

    public BagBase GetAvailableOutfit()
    {
        BagBase outfit = null;
        for (int i = 0; i < listBags.Count; i++)
        {
            if (!listBags[i].isHaveCus)
            {
                outfit = listBags[i];
                break;
            }
        }
        return outfit;
    }
    public BagPos GetEmtyPos()
    {
        BagPos o = null;
        for (int i = 0; i < listBagPos.Count; i++)
        {
            if (!listBagPos[i].haveOutfit)
            {
                o = listBagPos[i];
                break;
            }
        }
        return o;
    }
    public void GetEmtyPlaceNum(int n)
    {
        listEmtyPlaceToBuyBag.Clear();
        for (int i = 0; i < listPlaceToBuyBag.Count; i++)
        {
            if (!listPlaceToBuyBag[i].isHaveCus)
            {
                if (!listEmtyPlaceToBuyBag.Contains(listPlaceToBuyBag[i]))
                {
                    listEmtyPlaceToBuyBag.Add(listPlaceToBuyBag[i]);
                }
            }
        }
        if(listEmtyPlaceToBuyBag.Count < n)
        {
            listEmtyPlaceToBuyBag.Clear();
        }
    }
    public int GetListEmptyBag()
    {
        int n = 0;
        for (int i = 0; i < listBagPos.Count; i++)
        {
            if (listBagPos[i].haveOutfit)
            {
                n++;
            }
        }
        return n;
    }
    private void LoadData_IsHaveObj_In_Pos()
    {
        foreach (BagPos bagPos in listBagPos)
        {
            bagPos.haveOutfit = (dataStatusObject as DataBagCloset).Get_IsHaveAObj_In_Pos(bagPos.IDPos);
           // Debug.Log(bagPos.haveOutfit);
        }
    }
    private void SaveData_IsHaveObj_In_Pos()
    {
        foreach(BagPos bagPos in listBagPos)
        {
           // Debug.Log(bagPos.haveOutfit);
            (dataStatusObject as DataBagCloset).Set_IsHaveAObj_In_Pos(bagPos.IDPos, bagPos.haveOutfit);
        }
    }
}
