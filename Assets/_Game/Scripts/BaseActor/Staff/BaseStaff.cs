using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStaff : AllPool
{
    public FsmSystem fsm = new FsmSystem();
    public Transform myTransform;
    public static string IDLE_STATE = "idle_state";
    public static string MOVE_TO_HABITAT_STATE = "move_to_habitat_state";
    public static string MOVE_TO_MACHINE_STATE = "move_to_machine_state";
    public static string MOVE_TO_CLOSET_STATE = "move_to_closet_state";
    public static string MOVE_TO_IDLE_STATE = "move_to_idle_state";
    public static string MOVE_TO_GARBAGE_STATE = "move_to_garbage_state";

    [Header("-----Status-----")]
    public string STATE_STAFF;
    public float speed;
    public float animSpd;
    public int ObjHave;
    public bool CanCatch = false;
    public float Yoffset;

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
    public List<Shit> ListShits;

    public StaffType staffType;
    public IngredientType ingredientType;

    public virtual void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    public void UpdateState(string state)
    {
        fsm.setState(state);
        STATE_STAFF = state;
    }
    public void DelayCatch(float time)
    {
        //StartCoroutine(WaitToCath(time));
        //IsTiming = true;
        CounterHelper.Instance.QueueAction(time, () =>
        {
            CanCatch = true;
        },1);
    }
    public virtual void ShortObj(IngredientBase ingredient, int indexIngredientInList)
    {
        //int n = AllIngredients.IndexOf(ingredient);
        //Debug.Log(n);
        //for(int i = n; i < AllIngredients.Count; i++)
        //{
        //    AllIngredients[i + 1].transform.position = new Vector3(AllIngredients[i].transform.position.x, AllIngredients[i].transform.position.y, AllIngredients[i].transform.position.z);
        //}
    }
}
public enum StaffType
{
    FARMER,
    WORKER,
    CHECKOUT,
}
