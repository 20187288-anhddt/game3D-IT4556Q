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
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }

    public override void UnLock()
    {
        base.UnLock();
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        foreach (PlaceToBuyBag placeToBuyBag in listPlaceToBuyBag)
        {
            placeToBuyBag.gameObject.SetActive(true);
            placeToBuyBag.StartInGame();
        }
        Player p = Player.Instance;
        p.isUnlock = true;
        unlockModel.SetActive(true);
        //lockModel.SetActive(false);
        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 3f)
        {
            p.transform.position = checkUnlock.transform.position - Vector3.forward * 4;
        }
        unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
            unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                {
                    p.isUnlock = false;
                });
            }); ;
        });
        checkUnlock.gameObject.SetActive(false);
        checkPushBagCloset.gameObject.SetActive(true);
        //GetComponent<BoxCollider>().enabled = true;
        levelManager.closetManager.listBagClosets.Add(this);
        switch (type)
        {
            case IngredientType.BEAR:
                levelManager.listBearBagClosetActive.Add(this);
                break;
            case IngredientType.COW:
                levelManager.listCowBagClosetActive.Add(this);
                break;
            case IngredientType.CHICKEN:
                levelManager.listSheepBagClosetActive.Add(this);
                break;
            case IngredientType.SHEEP:
                levelManager.listChickenBagClosetActive.Add(this);
                break;
        }
    }
    void Start()
    {
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
        else
        {
            UnLock();
        }
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
}
