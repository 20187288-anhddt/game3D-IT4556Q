using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPos : PosBase
{
    public Transform myTransform;
    public BagBase curOutfit;
    [SerializeField]
    private BagBase outFitPrefab;
    public int IDPos;
    public void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public override void StartInGame()
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
    public void AddOutfit(BagBase outfit)
    {
        curOutfit = outfit;
        haveOutfit = true;
    }
    public void SetCloset(BagCloset closet)
    {
        this.closet = closet;
    }
    public override void SpawnOnStart()
    {
        var curBag = AllPoolContainer.Instance.Spawn(outFitPrefab, myTransform.position, myTransform.rotation);
        (curBag as BagBase).ResetOutfit();
        if (!(closet as BagCloset).listBags.Contains(curBag as BagBase))
        {
            AddOutfit(curBag as BagBase);
            (curBag as BagBase).AddPos(this);
            (closet as BagCloset).listBags.Add(curBag as BagBase);
        }
        (curBag as BagBase).myTransform.parent = myTransform;
        (curBag as BagBase).myTransform.position = myTransform.position;
        (curBag as BagBase).myTransform.localRotation = Quaternion.identity;
    }
}
