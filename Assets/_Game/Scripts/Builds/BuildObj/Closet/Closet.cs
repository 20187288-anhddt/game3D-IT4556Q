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
    public CheckUnlock checkUnlock;
    public CheckPushCloset checkPushCloset;
    public GameObject fxPos;

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
       
        //EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        unlockModel.SetActive(true);
        unlockFx.SetActive(true);
        //lockModel.SetActive(false);
        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 4f)
        {
            p.myTransform.position = checkUnlock.myTransform.position - Vector3.forward * 4;
        }
        if (isPlayAnimUnlock) //anim
        {
            p.PlayerStopMove();
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() =>
            {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() =>
                {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        unlockFx.SetActive(false);
                        //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        //foreach (PlaceToBuy place in listPlaceToBuy)
                        //{
                        //    place.gameObject.SetActive(true);
                        //}
                        //foreach (OutfitPos o in listOutfitPos)
                        //{
                        //    o.gameObject.SetActive(true);
                        //    o.StartInGame();
                        //}
                        fxPos.SetActive(true);
                        for (int i = 0; i < listEmtyPlaceToBuy.Count; i++)
                        {
                            listEmtyPlaceToBuy[i].gameObject.SetActive(true);
                        }
                        for (int i = 0; i < listOutfitPos.Count; i++)
                        {
                            listOutfitPos[i].gameObject.SetActive(true);
                            listOutfitPos[i].StartInGame();
                        }
                        checkPushCloset.gameObject.SetActive(true);
                        if (CameraController.Instance.IsCameraFollowPlayer())
                        {
                            p.PlayerContinueMove();
                        }
                    });
                }); ;
            });
        }
        //else
        //{
        //    p.isUnlock = false;
        //    //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
        //    //foreach (PlaceToBuy place in listPlaceToBuy)
        //    //{
        //    //    place.gameObject.SetActive(true);
        //    //}
        //    //foreach (OutfitPos o in listOutfitPos)
        //    //{
        //    //    o.gameObject.SetActive(true);
        //    //    o.StartInGame();
        //    //}
        //    //checkPushCloset.gameObject.SetActive(true);
        //}
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
            //case IngredientType.LION:
            //    if (!levelManager.closetManager.listLionClosetActive.Contains(this))
            //        levelManager.closetManager.listLionClosetActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        switch (nameObject_This)
            //        {
            //            case NameObject_This.LionCloset:
            //                EnventManager.TriggerEvent(EventName.LionCloset_Complete.ToString());
            //                break;
            //            case NameObject_This.LionCloset_1:
            //                EnventManager.TriggerEvent(EventName.LionCloset_1_Complete.ToString());
            //                break;
            //        }
            //    }
            //    break;
            //case IngredientType.CROC:
            //    if (!levelManager.closetManager.listCrocClosetActive.Contains(this))
            //        levelManager.closetManager.listCrocClosetActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        switch (nameObject_This)
            //        {
            //            case NameObject_This.CrocCloset:
            //                EnventManager.TriggerEvent(EventName.CrocCloset_Complete.ToString());
            //                break;
            //            case NameObject_This.CrocCloset_1:
            //                EnventManager.TriggerEvent(EventName.CrocCloset_1_Complete.ToString());
            //                break;
            //        }
            //    }
            //    break;
            //case IngredientType.ELE:
            //    if (!levelManager.closetManager.listEleClosetActive.Contains(this))
            //        levelManager.closetManager.listEleClosetActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        switch (nameObject_This)
            //        {
            //            case NameObject_This.EleCloset:
            //                EnventManager.TriggerEvent(EventName.EleCloset_Complete.ToString());
            //                break;
            //            case NameObject_This.EleCloset_1:
            //                EnventManager.TriggerEvent(EventName.EleCloset_1_Complete.ToString());
            //                break;
            //        }
            //    }
            //    break;
            //case IngredientType.ZEBRA:
            //    if (!levelManager.closetManager.listZebraClosetActive.Contains(this))
            //        levelManager.closetManager.listZebraClosetActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        switch (nameObject_This)
            //        {
            //            case NameObject_This.ZebraCloset:
            //                EnventManager.TriggerEvent(EventName.ZebraCloset_Complete.ToString());
            //                break;
            //            case NameObject_This.ZebraCloset_1:
            //                EnventManager.TriggerEvent(EventName.ZebraCloset_1_Complete.ToString());
            //                break;
            //        }
            //    }
            //    break;
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
        LoadData_IsHaveObj_In_Pos();
        EnventManager.AddListener(EventName.QuitGame.ToString(), SaveData_IsHaveObj_In_Pos);
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
            unlockModel.SetActive(false);
            fxPos.SetActive(false);
            unlockFx.SetActive(false);
            checkPushCloset.gameObject.SetActive(false);
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            if (CurrentCoin <= 0)
            {
                UnLock(true, true);
            }
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
    private void LoadData_IsHaveObj_In_Pos()
    {
        foreach (OutfitPos outfitPos in listOutfitPos)
        {
            outfitPos.haveOutfit = (dataStatusObject as DataCloset).Get_IsHaveAObj_In_Pos(outfitPos.IDPos);
            // Debug.Log(bagPos.haveOutfit);
        }
    }
    private void SaveData_IsHaveObj_In_Pos()
    {
        foreach (OutfitPos outfitPos in listOutfitPos)
        {
            // Debug.Log(bagPos.haveOutfit);
            (dataStatusObject as DataCloset).Set_IsHaveAObj_In_Pos(outfitPos.IDPos, outfitPos.haveOutfit);
        }
    }
}
