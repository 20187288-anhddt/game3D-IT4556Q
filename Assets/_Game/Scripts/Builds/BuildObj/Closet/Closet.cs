using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : BuildObj
{
    public Dictionary<Transform, PlaceToBuy> placeDic;
    public List<OutfitBase> listOutfits;
    public IngredientType outfitType;
    public int maxObj;
    [SerializeField]
    private Transform[] outfitPos;
    [SerializeField]
    private List<PlaceToBuy> listPlaceToBuy;
    [SerializeField]
    private OutfitBase outFitPrefab;

    void Start()
    {
        StartInGame(); 
    }
    public void SpawnOutfit()
    {
        var curOutfit = AllPoolContainer.Instance.Spawn(outFitPrefab, outfitPos[0].position, transform.rotation);
        if (!listOutfits.Contains(curOutfit as OutfitBase))
        {
            listOutfits.Add(curOutfit as OutfitBase);     
        }
        curOutfit.transform.parent = outfitPos[listOutfits.Count - 1];
        curOutfit.transform.position = outfitPos[listOutfits.Count - 1].position;
        curOutfit.transform.localRotation = Quaternion.identity;
        listPlaceToBuy[listOutfits.Count - 1].AddOutfit(curOutfit as OutfitBase);
    }
    public override void StartInGame()
    {
        foreach (PlaceToBuy p in listPlaceToBuy)
        {
            p.SetCloset(this);
        }
    }
}
