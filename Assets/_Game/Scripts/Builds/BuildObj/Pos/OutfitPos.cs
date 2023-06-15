using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitPos : MonoBehaviour
{
    public OutfitBase curOutfit;
    public bool haveOutfit;
    public Closet closet;
    void Start()
    {
        haveOutfit = false;
    }
    public void AddOutfit(OutfitBase outfit)
    {
        curOutfit = outfit;
        haveOutfit = true;
    }
    public void SetCloset(Closet closet)
    {
        this.closet = closet;
    }
}
