using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Staff : BaseStaff, ICollect
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    public GameObject gun;
    [Header("-----Status-----")]
    public bool onMission;
    public float coinValue;
    public Vector3 transIdle;
    public Vector3 transHabitat;
    public Vector3 transMachine;
    public Vector3 transCloset;
    public Vector3 transGarbage;
    public bool onHabitatPos;
    public bool onMachinePos;
    public bool onClosetPos;
    public bool onGarbagePos;
    public Habitat curHabitat;
    public MachineBase curMachine;
    public ClosetBase curCloset;
    public TrashCan curGarbage;
    
    public int maxCollectObj { get => MaxCollectObj; set => MaxCollectObj = value; }
    public int objHave { get => ObjHave; set => ObjHave = value; }
    public float timeDelayCatch { get => TimeDelayCatch; set => TimeDelayCatch = value; }
    public float CoinValue { get => coinValue; set => coinValue = value; }
    public float yOffset { get => Yoffset; set => Yoffset = value; }
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
    private Vector3 pointTaget = Vector3.zero;

    public GameManager gameManager;
    protected void Awake()
    {
        fsm.init(5);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(MOVE_TO_IDLE_STATE, StartMoveToIdle, OnMoveToIdleState));
        fsm.add(new FsmState(MOVE_TO_HABITAT_STATE, StartMoveToHabitat, OnMoveToHabitatState));
        fsm.add(new FsmState(MOVE_TO_MACHINE_STATE, StartMoveToMachine, OnMoveToMachineState));
        fsm.add(new FsmState(MOVE_TO_CLOSET_STATE, StartMoveToCloset, OnMoveToClosetState));
        fsm.add(new FsmState(MOVE_TO_GARBAGE_STATE, StartMoveToGarbage, OnMoveToGarbageState));
        UpdateState(IDLE_STATE);
        fsm.execute();
        if (gun.activeSelf)
            gun.SetActive(false);
        gameManager = GameManager.Instance;
        ResetStaff();
    }
    private void Start()
    {
        EnventManager.AddListener(EventName.ReLoadNavMesh.ToString(), ReloadSetDestination);
    }
    public void ReloadSetDestination()
    {
        if(STATE_STAFF != IDLE_STATE)
        {
            navMeshAgent.SetDestination(pointTaget);
        }
     
    }
    void Update()
    {
        fsm.execute();
        ChangeAnim();
    }
    private FsmSystem.ACTION OnIdleState(FsmSystem _fsm)
    {
        Idle();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToIdleState(FsmSystem _fsm)
    {
        CheckMoveToIdle();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToHabitatState(FsmSystem _fsm)
    {
        CheckMoveToHabitat();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToMachineState(FsmSystem _fsm)
    {
        CheckMoveToMachine();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToClosetState(FsmSystem _fsm)
    {
        CheckMoveToCloset();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToGarbageState(FsmSystem _fsm)
    {
        CheckMoveToGarbage();
        return FsmSystem.ACTION.END;
    }
    private void StartMoveToIdle(FsmSystem _fsm)
    {
        MoveToIdle();
    }
    private void StartMoveToHabitat(FsmSystem _fsm)
    {
        MoveToHabitat();
    }
    private void StartMoveToMachine(FsmSystem _fsm)
    {
        MoveToMachine();
    }
    private void StartMoveToCloset(FsmSystem _fsm)
    {
        MoveToCloset();
    }
    private void StartMoveToGarbage(FsmSystem _fsm)
    {
        MoveToGarbage();
    }
    public virtual void Idle()
    {

    }
    public virtual void MoveToIdle()
    {
        navMeshAgent.SetDestination(transIdle);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transIdle;
    }
    public virtual void MoveToHabitat()
    {
        navMeshAgent.SetDestination(transHabitat);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transHabitat;
    }
    public virtual void MoveToMachine()
    {
        navMeshAgent.SetDestination(transMachine);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transMachine;
    }
    public virtual void MoveToCloset()
    {
        navMeshAgent.SetDestination(transCloset);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transCloset;
    }
    public virtual void MoveToGarbage()
    {
        navMeshAgent.SetDestination(transGarbage);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transGarbage;
    }
    public virtual void CheckMoveToIdle()
    {
        if (Vector3.Distance(transIdle, this.transform.position) < 0.5f)
        {     
            this.transform.DORotate(Vector3.zero, 0f);
            onMission = false;
            switch (staffType)
            {
                case StaffType.FARMER:
                    curMachine.isHaveInStaff = false;
                    break;
                case StaffType.WORKER:
                    curCloset.isHaveStaff = false;
                    break;
            }
            UpdateState(IDLE_STATE);
        }
    }
    public virtual void CheckMoveToHabitat()
    {
       // Debug.Log(Vector3.Distance(transHabitat, this.transform.position));
        if (!onHabitatPos && Vector3.Distance(transHabitat, this.transform.position) < 0.2f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            this.transform.LookAt(curHabitat.transform.position);
            this.onHabitatPos = true;
            //UpdateState(IDLE_STATE);
        }
        if(onHabitatPos && objHave >= maxCollectObj)
        { 
            onHabitatPos = false;
            curHabitat.isHaveStaff = false;
            UpdateState(MOVE_TO_MACHINE_STATE);
        }
    }
    public virtual void CheckMoveToMachine()
    {
        if (!onMachinePos && Vector3.Distance(transMachine, this.transform.position) < 0.5f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            this.transform.LookAt(curMachine.transform.position);
            this.onMachinePos = true;
            //UpdateState(IDLE_STATE);
        }
        if (onMachinePos)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    if(objHave <= 0 || curMachine.ingredients.Count >= curMachine.maxObjInput)
                    {
                        ingredientType = IngredientType.NONE;
                        onMachinePos = false;
                        UpdateState(MOVE_TO_IDLE_STATE);
                        
                    } 
                    break;
                case StaffType.WORKER:
                    if(curMachine is ClothMachine)
                    {
                        if (objHave >= maxCollectObj)
                            //|| ((curMachine as ClothMachine).outCloths.Count <= 0
                            //&& curMachine.ingredients.Count <= 0))
                        {
                            onMachinePos = false;
                            curMachine.isHaveOutStaff = false;
                            UpdateState(MOVE_TO_CLOSET_STATE);
                        }
                    }     
                    if(curMachine is BagMachine)
                    {
                        if (objHave >= maxCollectObj)
                            //|| ((curMachine as BagMachine).outCloths.Count <= 0
                            //&& curMachine.ingredients.Count <= 0))
                        {
                            onMachinePos = false;
                            curMachine.isHaveOutStaff = false;
                            UpdateState(MOVE_TO_CLOSET_STATE);
                        }
                    }
                    break;
            } 
        }
    }
    public virtual void CheckMoveToCloset()
    {
        if (!onClosetPos && Vector3.Distance(transCloset, this.transform.position) < 0.5f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            this.transform.LookAt(curCloset.transform.position);
            this.onClosetPos = true;
            //UpdateState(IDLE_STATE);
        }
        if (onClosetPos)
        {
            if(curCloset is Closet)
            {
                if(objHave <= 0)
                {
                    onClosetPos = false;
                    UpdateState(MOVE_TO_IDLE_STATE);
                }
                else if((curCloset as Closet).listOutfits.Count >= (curCloset).maxObj)
                {
                    onClosetPos = false;
                    UpdateState(MOVE_TO_GARBAGE_STATE);
                }
            }  
            if(curCloset is BagCloset)
            {
                if (objHave <= 0)
                {
                    onClosetPos = false;
                    UpdateState(MOVE_TO_IDLE_STATE);
                }
                else if((curCloset as BagCloset).listBags.Count >= (curCloset).maxObj)
                {
                    onClosetPos = false;
                    UpdateState(MOVE_TO_GARBAGE_STATE);
                }
            }
        }
    }
    public virtual void CheckMoveToGarbage()
    {
        if (!onGarbagePos && Vector3.Distance(transGarbage, this.transform.position) < 0.5f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            this.transform.LookAt(curGarbage.transform.position);
            this.onGarbagePos = true;
            //UpdateState(IDLE_STATE);
        }
        if (onGarbagePos)
        {
            if(objHave <= 0)
            {
                onGarbagePos = false;
                UpdateState(MOVE_TO_IDLE_STATE);
            }
        }
    }
    public void ChangeAnim()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            //animator.Play("Running");
        }
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
        if (!allIngredients.Contains(ingredient))
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
        yOffset = 0;
        for (int i = 0; i < allIngredients.Count; i++)
        {
            yOffset += ingredient.ingreScale;
            allIngredients[i].transform.localPosition = Vector3.up * yOffset +
                Vector3.right * allIngredients[i].transform.localPosition.x +
                Vector3.forward * allIngredients[i].transform.localPosition.z;
        }
    }

    public bool CheckMoveToGarbage(IngredientBase ingredient)
    {
        bool tmp = false;
        switch (staffType)
        {
            case StaffType.FARMER:
                if (onMission)
                {
                    if (onMachinePos)
                    {

                    }
                }
                break;
            case StaffType.WORKER:
                break;
        }
        return tmp;
    }
    public void ResetStaff()
    {
        onMission = false;
        objHave = 0;
        transCloset = Vector3.zero;
        transHabitat = Vector3.zero;
        transMachine = Vector3.zero;
        onClosetPos = false;
        onHabitatPos = false;
        onMachinePos = false;
        ingredientType = IngredientType.NONE;
        yOffset = 0;
        foreach (IngredientBase i in allIngredients)
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
