using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollect
{
    bool canCatch { get; set; }
    //bool isTiming { get; set; }
    int maxCollectObj { get; set; }
    int objHave { get; set; }
    float timeDelayCatch { get; set; }
    float yOffset { get; set; }
    Transform handPos { get; set; }
    Transform backPos { get; set; }
    Transform carryPos { get; set; }

    List<IngredientBase> allIngredients { get; set; }
    List<Fleece> fleeces { get; set; }
    List<SheepCloth> sheepCloths { get; set; }
    List<CowFur> cowFurs { get; set; }
    List<CowCloth> cowCloths { get; set; }
    List<ChickenFur> chickenFurs { get; set; }
    List<ChickenCloth> chickenCloths { get; set; }
    List<BearFur> bearFurs { get; set; }
    List<BearCloth> bearCloths { get; set; }
    List<SheepBag> sheepBags { get; set; }
    List<CowBag> cowBags { get; set; }
    List<ChickenBag> chickenBags { get; set; }
    List<BearBag> bearBags { get; set; }
    void Collect();
    void CollectDone();
    void DelayCatch(float time);
    //void CancelDelay(float time);
    void ChangeCarryPos(Transform pos);
    void AddIngredient(IngredientBase ingredient);
    void RemoveIngredient(IngredientBase ingredient);
}
