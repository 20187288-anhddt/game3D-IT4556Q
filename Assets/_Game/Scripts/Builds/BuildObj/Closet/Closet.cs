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

    public override void UnLock()
    {
        base.UnLock();
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        foreach (PlaceToBuy placeToBuy in listPlaceToBuy)
        {
            placeToBuy.gameObject.SetActive(true);
            placeToBuy.StartInGame();
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
        checkPushCloset.gameObject.SetActive(true);
        //GetComponent<BoxCollider>().enabled = true;
        //levelManager.closetManager.listAllActiveClosets.Add(this);
        levelManager.closetManager.listClosets.Add(this);
        switch (type)
        {
            case IngredientType.BEAR:
                levelManager.closetManager.listBearClosetActive.Add(this);
                break;
            case IngredientType.COW:
                levelManager.closetManager.listCowClosetActive.Add(this);
                break;
            case IngredientType.CHICKEN:
                levelManager.closetManager.listSheepClosetActive.Add(this);
                break;
            case IngredientType.SHEEP:
                levelManager.closetManager.listChickenClosetActive.Add(this);
                break;
        }
    }
    void Start()
    {
        StartInGame(); 
    }

    public void SpawnOutfit()
    {
        OutfitPos o = GetEmtyPos();
        var curOutfit = AllPoolContainer.Instance.Spawn(outFitPrefab, o.transform.position, transform.rotation);
        (curOutfit as OutfitBase).ResetOutfit();
        if (!listOutfits.Contains(curOutfit as OutfitBase))
        {
            o.AddOutfit(curOutfit as OutfitBase);
            (curOutfit as OutfitBase).AddPos(o);
            listOutfits.Add(curOutfit as OutfitBase);     
        }
        curOutfit.transform.parent = o.transform;
        curOutfit.transform.position = o.transform.position;
        curOutfit.transform.localRotation = Quaternion.identity;

    }
    public override void StartInGame()
    {
        base.StartInGame();
        foreach (PlaceToBuy p in listPlaceToBuy)
        {
            p.SetCloset(this);
            p.StartInGame();
        }
        foreach (OutfitPos o in listOutfitPos)
        {
            o.SetCloset(this);
            o.StartInGame();
        }
        if (isLock)
        {
            checkPushCloset.gameObject.SetActive(false);
            unlockModel.gameObject.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
        }
        else
        {
            UnLock();
        }
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
