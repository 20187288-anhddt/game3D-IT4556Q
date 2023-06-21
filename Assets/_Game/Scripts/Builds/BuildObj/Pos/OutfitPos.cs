using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitPos : PosBase
{
    public Transform myTransform;
    public OutfitBase curOutfit;
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
    public void StartInGame()
    {
        haveOutfit = false;
    }
}
