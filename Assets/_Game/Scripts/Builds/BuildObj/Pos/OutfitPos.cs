using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitPos : PosBase
{
    public Transform myTransform;
    public OutfitBase curOutfit;
    [SerializeField]
    private OutfitBase outFitPrefab;
    public int IDPos;
    public void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public void AddOutfit(OutfitBase outfit)
    {
        curOutfit = outfit;
        haveOutfit = true;
    }
    public void SetCloset(ClosetBase closet)
    {
        this.closet = closet;
    }
    public override void  StartInGame()
    {
       // haveOutfit = true;
        if (!closet.isLock)
        {
            if (haveOutfit)
            {
                SpawnOnStart();
            }
        }
    }
    public override void SpawnOnStart()
    {
        haveOutfit = true;
        var curOutfit = AllPoolContainer.Instance.Spawn(outFitPrefab, myTransform.position, myTransform.rotation);
        (curOutfit as OutfitBase).ResetOutfit();
        if (!(closet as Closet).listOutfits.Contains(curOutfit as OutfitBase))
        {
            AddOutfit(curOutfit as OutfitBase);
            (curOutfit as OutfitBase).AddPos(this);
            (closet as Closet).listOutfits.Add(curOutfit as OutfitBase);
        }
        (curOutfit as OutfitBase).myTransform.parent = myTransform;
        (curOutfit as OutfitBase).myTransform.position = myTransform.position;
        (curOutfit as OutfitBase).myTransform.localRotation = Quaternion.identity;
    }
}
