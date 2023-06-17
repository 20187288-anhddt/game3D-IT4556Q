using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseActor,ICollect,IUnlock
{
    public static Player Instance;
    public GameObject gun;
    [Header("-----Status-----")]
    public bool isUnlock;
    public float coinValue;
    public int maxCollectObj { get => MaxCollectObj; set => MaxCollectObj = value; }
    public int objHave { get => ObjHave; set => ObjHave = value; }
    public float timeDelayCatch { get => TimeDelayCatch; set => TimeDelayCatch = value; }
    public float CoinValue { get => coinValue; set => coinValue = value; }
    public bool canCatch { get => CanCatch; set => CanCatch = value; }
    //public bool isTiming { get => IsTiming; set => IsTiming = value; }
    public Transform handPos { get => HandPos; set => HandPos = value; }
    public Transform backPos { get => BackPos; set => BackPos = value; }
    public Transform carryPos { get => CarryPos; set => CarryPos = value; }
    public List<IngredientBase> allIngredients { get => AllIngredients; set => AllIngredients = value; }
    public List<Fleece> fleeces { get => Fleeces; set => Fleeces = value; }
    public List<SheepCloth> sheepCloths { get => SheepCloths; set => SheepCloths = value; }
    public List<CowFur> cowFurs { get => CowFurs; set => CowFurs = value; }
    public List<CowCloth> cowCloths { get => CowCloths; set => CowCloths = value; }
    public List<ChickenFur> chickenFurs { get => ChickenFurs; set => ChickenFurs = value; }
    public List<ChickenCloth> chickenCloths { get => ChickenCloths; set => ChickenCloths = value; }
    public List<BearFur> bearFurs { get => BearFurs; set => BearFurs = value; }
    public List<BearCloth> bearCloths { get => BearCloths; set => BearCloths = value; }
    public List<SheepBag> sheepBags { get => SheepBags; set => SheepBags = value; }
    public List<CowBag> cowBags { get => CowBags; set => CowBags = value; }
    public List<ChickenBag> chickenBags { get => ChickenBags; set => ChickenBags = value; }
    public List<BearBag> bearBags { get => BearBags; set => BearBags = value; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    protected void Start()
    {
        fsm.init(2);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(RUN_STATE, null, OnRunState));
        UpdateState(IDLE_STATE);
        fsm.execute();
        if(gun.activeSelf)
            gun.SetActive(false);

        EnventManager.AddListener(EventName.PlayJoystick.ToString(), OnMove);
        EnventManager.AddListener(EventName.StopJoyStick.ToString(), StopMove);
    }
    protected void OnMove()
    {
        if (!isUnlock)
        {
            UpdateState(RUN_STATE);
        }
        else UpdateState(IDLE_STATE);
    }
    protected void StopMove()
    {
        UpdateState(IDLE_STATE);
    }


    protected void Update()
    {
        fsm.execute();
        var rig = GetComponent<Rigidbody>();
        animSpd = rig.velocity.magnitude;
        //if (Config(GameManager.Instance.joystick.Direction) != Vector2.zero && !isUnlock)
        //{
        //    UpdateState(RUN_STATE);
        //}
        //else UpdateState(IDLE_STATE);
    }
    public Vector2 Config(Vector2 input)
    {
        if (input.magnitude >= 0.09)
        {
            return input;
        }
        return Vector2.zero;
    }
    private FsmSystem.ACTION OnIdleState(FsmSystem _fsm)
    {
        Idle();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnRunState(FsmSystem _fsm)
    {
        UpdateMove(speed);
        return FsmSystem.ACTION.END;
    }
    public virtual void UpdateMove(float speed)
    {
        Canvas_Joystick joystick = Canvas_Joystick.Instance;
        Vector3 inputAxist = joystick.Get_Diraction();
        //Vector3 direction = new Vector3(joystick.Vertical, 0f, -joystick.Horizontal);
       
        if (inputAxist.x != 0 || inputAxist.z != 0)
        {
            var rig = GetComponent<Rigidbody>();
            //rig.velocity = new Vector3(inputAxist.x * speed, rig.velocity.y, inputAxist.z * speed);
            rig.MovePosition(transform.position + transform.forward.normalized * speed * 0.02f);
            Vector3 moveDir = new Vector3(inputAxist.x, 0, inputAxist.z);
            transform.rotation = Quaternion.LookRotation(moveDir).normalized;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
           // transform.Translate(Vector3.forward * speed * 0.02f);
        }
    }
    public virtual void Idle()
    {
        var rig = GetComponent<Rigidbody>();
        rig.velocity = Vector3.zero;
    }
    public void Collect()
    {
        gun.SetActive(true);
        ChangeCarryPos(backPos);

        //TODO//
        //Hien sung, chuyen back ra dan truoc//
    }

    public void CollectDone()
    {
        gun.SetActive(false);
        ChangeCarryPos(handPos);
        //TODO//
        //Deactive sung, chuyen back ve sau lung//
    }
    public void ChangeCarryPos(Transform pos)
    {
        carryPos.parent = pos;
        carryPos.position = pos.position;
    }
    public void AddIngredient(IngredientBase ingredient)
    {
        if(!allIngredients.Contains(ingredient))
            allIngredients.Add(ingredient);
        switch (ingredient.ingredientType)
        {
            case IngredientType.SHEEP:
                if (!fleeces.Contains(ingredient as Fleece))
                    fleeces.Add(ingredient as Fleece);
                break;
            case IngredientType.SHEEP_CLOTH:
                if (!sheepCloths.Contains(ingredient as SheepCloth))
                    sheepCloths.Add(ingredient as SheepCloth);
                break;
            case IngredientType.COW:
                if (!cowFurs.Contains(ingredient as CowFur))
                    cowFurs.Add(ingredient as CowFur);
                break;
            case IngredientType.COW_CLOTH:
                if (!cowCloths.Contains(ingredient as CowCloth))
                    cowCloths.Add(ingredient as CowCloth);
                break;
            case IngredientType.CHICKEN:
                if (!chickenFurs.Contains(ingredient as ChickenFur))
                    chickenFurs.Add(ingredient as ChickenFur);
                break;
            case IngredientType.CHICKEN_CLOTH:
                if (!chickenCloths.Contains(ingredient as ChickenCloth))
                    chickenCloths.Add(ingredient as ChickenCloth);
                break;
            case IngredientType.BEAR:
                if (!bearFurs.Contains(ingredient as BearFur))
                    bearFurs.Add(ingredient as BearFur);
                break;
            case IngredientType.BEAR_CLOTH:
                if (!bearCloths.Contains(ingredient as BearCloth))
                    bearCloths.Add(ingredient as BearCloth);
                break;
            case IngredientType.SHEEP_BAG:
                if (!sheepBags.Contains(ingredient as SheepBag))
                    sheepBags.Add(ingredient as SheepBag);
                break;
            case IngredientType.COW_BAG:
                if (!cowBags.Contains(ingredient as CowBag))
                    cowBags.Add(ingredient as CowBag);
                break;
            case IngredientType.CHICKEN_BAG:
                if (!chickenBags.Contains(ingredient as ChickenBag))
                    chickenBags.Add(ingredient as ChickenBag);
                break;
            case IngredientType.BEAR_BAG:
                if (!bearBags.Contains(ingredient as BearBag))
                    bearBags.Add(ingredient as BearBag);
                break;
        }
    }
    public void RemoveIngredient(IngredientBase ingredient)
    {
        int n = allIngredients.IndexOf(ingredient);
        if (allIngredients.Contains(ingredient))
        {
            ingredient.AddYOffset(-ingredient.ingreScale);
            allIngredients.Remove(ingredient);
        }

        switch (ingredient.ingredientType)
        {
            case IngredientType.SHEEP:
                if (fleeces.Contains(ingredient as Fleece))
                    fleeces.Remove(ingredient as Fleece);
                break;
            case IngredientType.SHEEP_CLOTH:
                if (sheepCloths.Contains(ingredient as SheepCloth))
                    sheepCloths.Remove(ingredient as SheepCloth);
                break;
            case IngredientType.COW:
                if (cowFurs.Contains(ingredient as CowFur))
                    cowFurs.Remove(ingredient as CowFur);
                break;
            case IngredientType.COW_CLOTH:
                if (cowCloths.Contains(ingredient as CowCloth))
                    cowCloths.Remove(ingredient as CowCloth);
                break;
            case IngredientType.CHICKEN:
                if (chickenFurs.Contains(ingredient as ChickenFur))
                    chickenFurs.Remove(ingredient as ChickenFur);
                break;
            case IngredientType.CHICKEN_CLOTH:
                if (chickenCloths.Contains(ingredient as ChickenCloth))
                    chickenCloths.Remove(ingredient as ChickenCloth);
                break;
            case IngredientType.BEAR:
                if (bearFurs.Contains(ingredient as BearFur))
                    bearFurs.Remove(ingredient as BearFur);
                break;
            case IngredientType.BEAR_CLOTH:
                if (bearCloths.Contains(ingredient as BearCloth))
                    bearCloths.Remove(ingredient as BearCloth);
                break;
            case IngredientType.SHEEP_BAG:
                if (sheepBags.Contains(ingredient as SheepBag))
                    sheepBags.Remove(ingredient as SheepBag);
                break;
            case IngredientType.COW_BAG:
                if (cowBags.Contains(ingredient as CowBag))
                    cowBags.Remove(ingredient as CowBag);
                break;
            case IngredientType.CHICKEN_BAG:
                if (chickenBags.Contains(ingredient as ChickenBag))
                    chickenBags.Remove(ingredient as ChickenBag);
                break;
            case IngredientType.BEAR_BAG:
                if (bearBags.Contains(ingredient as BearBag))
                    bearBags.Remove(ingredient as BearBag);
                break;
        }

        ShortObj(ingredient, n);
    }
    public override void ShortObj(IngredientBase ingredient, int indexIngredientInList)
    {
        bool isAdd = false;
        for (int i = allIngredients.Count - 1; i >= 0; i--)
        {
            if (i >= indexIngredientInList)
            {
                if (!isAdd)
                {
                    isAdd = true;
                    ingredient.AddYOffset(+ingredient.ingreScale);
                }
                ingredient.AddYOffset(-ingredient.ingreScale);
                allIngredients[i].transform.localPosition = Vector3.up * ingredient.GetYOffset();
            }
        }
        for (int i = allIngredients.Count - 1; i >= 0; i--)
        {
            if (i > indexIngredientInList && isAdd)
            {
                ingredient.AddYOffset(ingredient.ingreScale);
            }
        }
    }
    public void UnlockMap(float coin)
    {
        if (coinValue <= 0)
            return;
        if (coinValue > coin)
        {
            coinValue -= coin;
        }
        else
        {
            coinValue = 0;
        }
    }
    
    public void ResetPlayer()
    {
        objHave = 0;
        foreach(IngredientBase i in allIngredients)
        {
            AllPoolContainer.Instance.Release(i);     
        }
        allIngredients.Clear();
        fleeces.Clear();
        cowFurs.Clear();
        chickenFurs.Clear();
        bearFurs.Clear();
        sheepCloths.Clear();
        cowCloths.Clear();
        chickenCloths.Clear();
        bearCloths.Clear();
    }
    
}
