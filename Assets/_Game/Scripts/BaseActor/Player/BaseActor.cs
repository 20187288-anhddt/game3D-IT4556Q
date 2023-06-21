using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BaseActor : AllPool
{
    public FsmSystem fsm = new FsmSystem();
    public static string IDLE_STATE = "idle_state";
    public static string RUN_STATE = "run_state";

    [Header("-----Status-----")]
    public string STATE_ACTOR;
    public float speed;
    public float animSpd;
    public int ObjHave;
    public bool CanCatch = false;

    [Header("-----Setup-----")]
    public int MaxCollectObj;
    public float TimeDelayCatch;

    [Header("-----ListCarryPos-----")]
    public Transform HandPos;
    public Transform BackPos;
    public Transform CarryPos;

    [Header("-----ListIngredients-----")]
    public List<IngredientBase> AllIngredients;
    public List<Fleece> Fleeces;
    public List<SheepCloth> SheepCloths;
    public List<CowFur> CowFurs;
    public List<CowCloth> CowCloths;
    public List<ChickenFur> ChickenFurs;
    public List<ChickenCloth> ChickenCloths;
    public List<BearFur> BearFurs;
    public List<BearCloth> BearCloths;
    public List<SheepBag> SheepBags;
    public List<CowBag> CowBags;
    public List<ChickenBag> ChickenBags;
    public List<BearBag> BearBags;

<<<<<<< HEAD
<<<<<<< Updated upstream
    public List<LionFur> LionFurs;
    public List<LionCloth> LionCloths;
    public List<LionBag> LionBags;

    public List<CrocFur> CrocFurs;
    public List<CrocCloth> CrocCloths;
    public List<CrocBag> CrocBags;

    public List<EleFur> EleFurs;
    public List<EleCloth> EleCloths;
    public List<EleBag> EleBags;

    public List<ZebraFur> ZebraFurs;
    public List<ZebraCloth> ZebraCloths;
    public List<ZebraBag> ZebraBags;

    public List<Shit> ListShits;
    public List<GameObject> ListEmojis;
=======
>>>>>>> Stashed changes
    public virtual void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
=======

>>>>>>> main
    //public bool IsTiming = false;
    public void UpdateState(string state)
    {
        fsm.setState(state);
        STATE_ACTOR = state;
    }
    public void DelayCatch(float time)
    {
        //StartCoroutine(WaitToCath(time));
        //IsTiming = true;
        CounterHelper.Instance.QueueAction(time, () =>
        {
            CanCatch = true;
        });
    }
    public virtual void ShortObj()
    {
        //int n = AllIngredients.IndexOf(ingredient);
        //Debug.Log(n);
        //for(int i = n; i < AllIngredients.Count; i++)
        //{
        //    AllIngredients[i + 1].transform.position = new Vector3(AllIngredients[i].transform.position.x, AllIngredients[i].transform.position.y, AllIngredients[i].transform.position.z);
        //}
    }
    //IEnumerator WaitToCath(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    CanCatch = true;
    //}
    //public void CancelDelay(float time)
    //{
    //    StopCoroutine(WaitToCath(time));
    //    IsTiming = false;
    //}

}
