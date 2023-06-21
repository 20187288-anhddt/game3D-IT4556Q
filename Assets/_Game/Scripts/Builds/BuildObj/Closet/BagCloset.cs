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
        foreach (PlaceToBuyBag placeToBuyBag in listPlaceToBuyBag)
        {
            placeToBuyBag.gameObject.SetActive(true);
            //placeToBuyBag.StartInGame();
        }
      
        p.isUnlock = true;
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 3f)
        {
            p.transform.position = checkUnlock.transform.position - Vector3.forward * 4;
        }
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
<<<<<<< HEAD
<<<<<<< Updated upstream
                        unlockFx.SetActive(false);
                        //   EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        foreach (PlaceToBuyBag place in listPlaceToBuyBag)
                        {
                            place.gameObject.SetActive(true);

                        }
                        //foreach (BagPos o in listBagPos)
                        //{
                        //    o.gameObject.SetActive(true);
                        //    o.StartInGame();
                        //}
                        checkPushBagCloset.gameObject.SetActive(true);
                        if (CameraController.Instance.IsCameraFollowPlayer())
                        {
                            p.PlayerContinueMove();
                        }
=======
                        p.isUnlock = false;
                        checkPushBagCloset.gameObject.SetActive(true);
>>>>>>> Stashed changes
=======
                        p.isUnlock = false;
>>>>>>> main
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
<<<<<<< HEAD
<<<<<<< Updated upstream
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

=======
>>>>>>> Stashed changes
            checkPushBagCloset.gameObject.SetActive(true);
=======
>>>>>>> main
        }
     
        checkUnlock.gameObject.SetActive(false);
        checkPushBagCloset.gameObject.SetActive(true);
        //GetComponent<BoxCollider>().enabled = true;
       //levelManager.closetManager.listAllActiveClosets.Add(this);
       if(!levelManager.closetManager.listBagClosets.Contains(this))
            levelManager.closetManager.listBagClosets.Add(this);
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
    }
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    public void SpawnOutfit()
    {
        BagPos o = GetEmtyPos();
        var curBag = AllPoolContainer.Instance.Spawn(outFitPrefab, o.transform.position, transform.rotation);
        (curBag as BagBase).ResetOutfit();
        if (!listBags.Contains(curBag as BagBase))
        {
            o.AddOutfit(curBag as BagBase);
            (curBag as BagBase).AddPos(o);
            listBags.Add(curBag as BagBase);
        }
        curBag.transform.parent = o.transform;
        curBag.transform.position = o.transform.position;
        curBag.transform.localRotation = Quaternion.identity;
    }
    public override void StartInGame()
    {
        base.StartInGame();
        foreach (PlaceToBuyBag p in listPlaceToBuyBag)
        {  
            p.SetCloset(this);
            p.StartInGame();
            p.gameObject.SetActive(false);
        }
        foreach (BagPos o in listBagPos)
        {
            o.SetCloset(this);
            o.StartInGame();
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
}
