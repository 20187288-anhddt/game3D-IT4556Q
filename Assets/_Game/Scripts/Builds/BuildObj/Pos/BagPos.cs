using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPos : PosBase
{
    public Transform myTransform;
    public BagBase curOutfit;
    public void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public void StartInGame()
    {
        haveOutfit = false;
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
}
