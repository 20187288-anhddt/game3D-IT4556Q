using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Closet : ClosetBase, ILock
{
    public List<OutfitBase> listOutfits;
    public List<OutfitPos> listOutfitPos;
    [SerializeField]
    private OutfitBase outFitPrefab;
    public List<PlaceToBuy> listPlaceToBuy;
    public List<PlaceToBuy> listEmtyPlaceToBuy;
    [SerializeField]
    private GameObject unlockModel;
    //[SerializeField]
    //private GameObject lockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private CheckPushCloset checkPushCloset;

    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        if (!IsLock)
        {
            return;
        }
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        p.isUnlock = true;
      //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 3f)
        {
            p.myTransform.position = checkUnlock.myTransform.position - Vector3.forward * 4;
        }
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() =>
            {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() =>
                {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.isUnlock = false;
                        //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        foreach (PlaceToBuy place in listPlaceToBuy)
                        {
                            place.gameObject.SetActive(true);
                        }
                        foreach (OutfitPos o in listOutfitPos)
                        {
                            o.gameObject.SetActive(true);
                            o.StartInGame();
                        }
                        checkPushCloset.gameObject.SetActive(true);
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
            //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
            foreach (PlaceToBuy place in listPlaceToBuy)
            {
                place.gameObject.SetActive(true);
            }
            foreach (OutfitPos o in listOutfitPos)
            {
                o.gameObject.SetActive(true);
                o.StartInGame();
            }
            checkPushCloset.gameObject.SetActive(true);
        }
        checkUnlock.gameObject.SetActive(false);
        //GetComponent<BoxCollider>().enabled = true;
        //levelManager.closetManager.listAllActiveClosets.Add(this);
        
        switch (ingredientType)
        {
            case IngredientType.BEAR:
                if(!levelManager.closetManager.listBearClosetActive.Contains(this))  
                    levelManager.closetManager.listBearClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.BearCloset:
                            EnventManager.TriggerEvent(EventName.BearCloset_Complete.ToString());
                            break;
                        case NameObject_This.BearCloset_1:
                            EnventManager.TriggerEvent(EventName.BearCloset_1_Complete.ToString());
                            break;
                    }
                }
              
                break;
            case IngredientType.COW:
                if (!levelManager.closetManager.listCowClosetActive.Contains(this))
                    levelManager.closetManager.listCowClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.CowCloset:
                            EnventManager.TriggerEvent(EventName.CowCloset_Complete.ToString());
                            break;
                        case NameObject_This.CowCloset_1:
                            EnventManager.TriggerEvent(EventName.CowCloset_1_Complete.ToString());
                            break;
                    }
                }
              
                break;
            case IngredientType.CHICKEN:
                if (!levelManager.closetManager.listChickenClosetActive.Contains(this))
                    levelManager.closetManager.listChickenClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.ChickenCloset:
                            EnventManager.TriggerEvent(EventName.ChickenCloset_Complete.ToString());
                            break;
                        case NameObject_This.ChickenCloset_1:
                            EnventManager.TriggerEvent(EventName.ChickenCloset_1_Complete.ToString());
                            break;
                    }
                }

                break;
            case IngredientType.SHEEP:
                if (!levelManager.closetManager.listSheepClosetActive.Contains(this))
                    levelManager.closetManager.listSheepClosetActive.Add(this);
                if (isPushEvent)
                {
                    switch (nameObject_This)
                    {
                        case NameObject_This.SheepCloset:
                            EnventManager.TriggerEvent(EventName.SheepCloset_Complete.ToString());
                            break;
                        case NameObject_This.SheepCloset_1:
                            EnventManager.TriggerEvent(EventName.SheepCloset_1_Complete.ToString());
                            break;
                    }
                }
             
                break;
        }
        if (!levelManager.closetManager.listClosets.Contains(this))
        {
            levelManager.closetManager.listClosets.Add(this);
        }
    }
    public override void Start()
    {
        base.Start();
        StartInGame(); 
    }

    public void SpawnOutfit()
    {
        OutfitPos o = GetEmtyPos();
        var curOutfit = AllPoolContainer.Instance.Spawn(outFitPrefab, o.myTransform.position, myTransform.rotation);
        (curOutfit as OutfitBase).ResetOutfit();
        if (!listOutfits.Contains(curOutfit as OutfitBase))
        {
            o.AddOutfit(curOutfit as OutfitBase);
            (curOutfit as OutfitBase).AddPos(o);
            listOutfits.Add(curOutfit as OutfitBase);     
        }
        (curOutfit as OutfitBase).myTransform.parent = o.myTransform;
        (curOutfit as OutfitBase).myTransform.position = o.myTransform.position;
        (curOutfit as OutfitBase).myTransform.localRotation = Quaternion.identity;

    }
    public override void StartInGame()
    {
        base.StartInGame();
        CurrentCoin = pirceObject.Get_Pirce();
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
           dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        foreach (PlaceToBuy p in listPlaceToBuy)
        {
            p.SetCloset(this);
            p.StartInGame();
            //p.gameObject.SetActive(false);
        }
        foreach (OutfitPos o in listOutfitPos)
        {
            o.SetCloset(this);
            //o.gameObject.SetActive(false);
        }
        if (isLock)
        {
            checkPushCloset.gameObject.SetActive(false);
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
        }
        //else
        //{
        //    UnLock();
        //}
        checkUnlock.UpdateUI();
      
    }

    public OutfitBase GetAvailableOutfit()
    {
        OutfitBase outfit = null;
        for(int i = 0; i < listOutfits.Count; i++)
        {
            if (!listOutfits[i].isHaveCus)
            {
                outfit = listOutfits[i];
                break;
            }
        }
        return outfit;
    }
    public OutfitPos GetEmtyPos()
    {
        OutfitPos o = null;
        for (int i = 0; i < listOutfitPos.Count; i++)
        {
            if (!listOutfitPos[i].haveOutfit)
            {
                o = listOutfitPos[i];
                break;
            }
        }
        return o;
    }
    public void GetEmtyPlaceNum()
    {
        for(int i = 0; i < listPlaceToBuy.Count; i++)
        {
            if (!listPlaceToBuy[i].isHaveCus)
            {           
                if (!listEmtyPlaceToBuy.Contains(listPlaceToBuy[i]))
                {
                    listEmtyPlaceToBuy.Add(listPlaceToBuy[i]);
                }
            }      
        }
    }
    public int GetListEmptyOutfit()
    {
        int n = 0;
        for(int i = 0;i< listOutfitPos.Count; i++)
        {
            if (listOutfitPos[i].haveOutfit)
            {
                n++;
            }
        }
        return n;
    }
}
