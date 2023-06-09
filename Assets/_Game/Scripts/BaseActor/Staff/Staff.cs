using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Staff : BaseStaff, ICollect,IAct
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
    private float waitingTime;
    public float consWaitingTime;
    private Animator anim;
    public GameObject fxBuffSpeed;

    [SerializeField]
    private float consDelayAct;
    private float delayAct;
    private bool isAct;
    public List<GameObject> listEmojis { get => ListEmojis; set => ListEmojis = value; }
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
    public Transform gunPos { get => GunPos; set => GunPos = value; }
    public List<IngredientBase> allIngredients { get => AllIngredients; set => AllIngredients = value; }

    public List<Fleece> fleeces { get => Fleeces; set => Fleeces = value; }
    public List<SheepCloth> sheepCloths { get => SheepCloths; set => SheepCloths = value; }
    public List<SheepBag> sheepBags { get => SheepBags; set => SheepBags = value; }

    public List<CowFur> cowFurs { get => CowFurs; set => CowFurs = value; }
    public List<CowCloth> cowCloths { get => CowCloths; set => CowCloths = value; }
    public List<CowBag> cowBags { get => CowBags; set => CowBags = value; }

    public List<ChickenFur> chickenFurs { get => ChickenFurs; set => ChickenFurs = value; }
    public List<ChickenCloth> chickenCloths { get => ChickenCloths; set => ChickenCloths = value; }
    public List<ChickenBag> chickenBags { get => ChickenBags; set => ChickenBags = value; }

    public List<BearFur> bearFurs { get => BearFurs; set => BearFurs = value; }
    public List<BearCloth> bearCloths { get => BearCloths; set => BearCloths = value; }
    public List<BearBag> bearBags { get => BearBags; set => BearBags = value; }

    public List<LionFur> lionFurs { get => LionFurs; set => LionFurs = value; }
    public List<LionCloth> lionCloths { get => LionCloths; set => LionCloths = value; }
    public List<LionBag> lionBags { get => LionBags; set => LionBags = value; }

    public List<CrocFur> crocFurs { get => CrocFurs; set => CrocFurs = value; }
    public List<CrocCloth> crocCloths { get => CrocCloths; set => CrocCloths = value; }
    public List<CrocBag> crocBags { get => CrocBags; set => CrocBags = value; }

    public List<EleFur> eleFurs { get => EleFurs; set => EleFurs = value; }
    public List<EleCloth> eleCloths { get => EleCloths; set => EleCloths = value; }
    public List<EleBag> eleBags { get => EleBags; set => EleBags = value; }

    public List<ZebraFur> zebraFurs { get => ZebraFurs; set => ZebraFurs = value; }
    public List<ZebraCloth> zebraCloths { get => ZebraCloths; set => ZebraCloths = value; }
    public List<ZebraBag> zebraBags { get => ZebraBags; set => ZebraBags = value; }

    public List<Shit> listShits { get => ListShits; set => ListShits = value; }
    private Vector3 pointTaget = Vector3.zero;
    public GameObject[] model;
    private bool isBuff = false;
    public GameManager gameManager;
    private bool isInItEvent = false;
    public GameObject emojiPanel;
    public override void Awake()
    {
        base.Awake();
        fsm.init(6);
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
        for (int i = 0; i < model.Length; i++)
        {
            model[i].SetActive(false);
        }
        gameManager = GameManager.Instance;
        ResetStaff();
      
        LoadAndSetData();
    }
    private void OnEnable()
    {
      
        LoadAndSetData();
        if (!isInItEvent)
        {
            isInItEvent = true;
            EnventManager.AddListener(EventName.ReLoadDataUpgrade.ToString(), LoadAndSetData);
            //anim = GetComponentInChildren<Animator>();
            EnventManager.AddListener(EventName.ReLoadNavMesh.ToString(), ReloadSetDestination);
            EnventManager.AddListener(EventName.Player_Double_Speed_Play.ToString(), DoubleSpeed);
            EnventManager.AddListener(EventName.Player_Double_Speed_Stop.ToString(), ResetSpeed);
        }
    }
    private void LoadAndSetData()
    {
        maxCollectObj = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoCapacityTaget(staffType).Capacity;
        speed = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoSpeedTaget(staffType).Speed;
        if (isBuff)
        {
            DoubleSpeed();
        }
        navMeshAgent.speed = speed;
    }
    //private void Start()
    //{
       
    //}
    private void DoubleSpeed()
    {
        isBuff = true;
        speed = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoSpeedTaget(staffType).Speed;
        speed *= 1.5f;
        fxBuffSpeed.SetActive(true);
        navMeshAgent.speed = speed;
    }
    private void ResetSpeed()
    {
        isBuff = false;
        fxBuffSpeed.SetActive(false);
        speed = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataStaff(staffType).GetInfoSpeedTaget(staffType).Speed;
        navMeshAgent.speed = speed;
    }
    public void ReloadSetDestination()
    {
        if (STATE_STAFF != IDLE_STATE && gameObject.activeInHierarchy)
        {
            navMeshAgent.SetDestination(pointTaget);
        }

    }
    void Update()
    {
        fsm.execute();
        ChangeAnim();
        StartActing();
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
        transIdle.y = myTransform.position.y;
        navMeshAgent.SetDestination(transIdle);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transIdle;
    }
    public virtual void MoveToHabitat()
    {
        transHabitat.y = myTransform.position.y;
        navMeshAgent.SetDestination(transHabitat);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transHabitat;
    }
    public virtual void MoveToMachine()
    {
        transMachine.y = myTransform.position.y;
        navMeshAgent.SetDestination(transMachine);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transMachine;
    }
    public virtual void MoveToCloset()
    {
        transCloset.y = myTransform.position.y;
        navMeshAgent.SetDestination(transCloset);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transCloset;
    }
    public virtual void MoveToGarbage()
    {
        transGarbage.y = myTransform.position.y;
        navMeshAgent.SetDestination(transGarbage);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transGarbage;
    }
    public virtual void CheckMoveToIdle()
    {
        if (Vector3.Distance(transIdle, myTransform.position) < 0.5f)
        {
            myTransform.DORotate(Vector3.zero, 0f);
            onMission = false;
            switch (staffType)
            {
                case StaffType.FARMER:
                    curMachine.isHaveInStaff = false;
                    break;
                case StaffType.WORKER:
                    curCloset.isHaveStaff = false;
                    waitingTime = consWaitingTime;
                    break;
            }
            UpdateState(IDLE_STATE);
        }
    }
    public virtual void CheckMoveToHabitat()
    {
       // Debug.Log(Vector3.Distance(transHabitat, this.transform.position));
        if (!onHabitatPos && Vector3.Distance(transHabitat, myTransform.position) < 0.2f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            myTransform.LookAt(curHabitat.myTransform.position);
            this.onHabitatPos = true;
            //UpdateState(IDLE_STATE);
        }
        if(onHabitatPos && objHave >= maxCollectObj)
        { 
            onHabitatPos = false;
            curHabitat.isHaveStaff = false;
            if (CheckFarmerHaveShit())
            {
                //curMachine.isHaveInStaff = false;
                UpdateState(MOVE_TO_GARBAGE_STATE);
            }
            else
            {
                UpdateState(MOVE_TO_MACHINE_STATE);
            }     
        }
    }
    public virtual void CheckMoveToMachine()
    {
        if (!onMachinePos && Vector3.Distance(transMachine, myTransform.position) < 0.5f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            if(this.staffType == StaffType.WORKER)
            {
                CountDownWatingTime();
            }
            myTransform.LookAt(curMachine.myTransform.position);
            this.onMachinePos = true;
            //UpdateState(IDLE_STATE);
        }
        if (onMachinePos)
        {
            switch (staffType)
            {
                case StaffType.FARMER:
                    if(objHave <= 0)
                    {
                        ingredientType = IngredientType.NONE;
                        onMachinePos = false;
                        UpdateState(MOVE_TO_IDLE_STATE);
                        
                    } 
                    else if(curMachine.ingredients.Count >= curMachine.maxObjInput || CheckFarmerHaveShit())
                    {
                        ingredientType = IngredientType.NONE;
                        onMachinePos = false;
                        UpdateState(MOVE_TO_GARBAGE_STATE);
                    }
                    break;
                case StaffType.WORKER:
                    if(objHave >= maxCollectObj)
                    {
                        onMachinePos = false;
                        curMachine.isHaveOutStaff = false;
                        UpdateState(MOVE_TO_CLOSET_STATE);
                    }
                    else
                    {
                        if(waitingTime <= 0)
                        {
                            if(objHave > 0)
                            {
                                onMachinePos = false;
                                curMachine.isHaveOutStaff = false;
                                UpdateState(MOVE_TO_CLOSET_STATE);
                            }
                            else
                            {
                                if(curMachine.ingredients.Count == 0)
                                {
                                    if(curMachine is ClothMachine && (curMachine as ClothMachine).outCloths.Count == 0)
                                    {
                                        onMachinePos = false;
                                        curMachine.isHaveOutStaff = false;
                                        UpdateState(MOVE_TO_IDLE_STATE);
                                    }
                                    else if(curMachine is BagMachine && (curMachine as BagMachine).outCloths.Count == 0)
                                    {
                                        onMachinePos = false;
                                        curMachine.isHaveOutStaff = false;
                                        UpdateState(MOVE_TO_IDLE_STATE);
                                    }
                                }
                               
                            }
                        }
                    }
                    //if(curMachine is ClothMachine)
                    //{
                    //    if (objHave >= maxCollectObj)
                    //    {
                    //        onMachinePos = false;
                    //        curMachine.isHaveOutStaff = false;
                    //        UpdateState(MOVE_TO_CLOSET_STATE);
                    //    }
                    //}     
                    //if(curMachine is BagMachine)
                    //{
                    //    if (objHave >= maxCollectObj)   
                    //    {
                    //        onMachinePos = false;
                    //        curMachine.isHaveOutStaff = false;
                    //        UpdateState(MOVE_TO_CLOSET_STATE);
                    //    }
                    //}
                    break;
            } 
        }
    }
    public virtual void CheckMoveToCloset()
    {
        if (!onClosetPos && Vector3.Distance(transCloset, myTransform.position) < 0.5f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            myTransform.LookAt(curCloset.myTransform.position);
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
        
        if (!onGarbagePos && Vector3.Distance(transGarbage, myTransform.position) < 0.5f)
        {
            //this.transform.DORotate(Vector3.zero, 0f);
            myTransform.LookAt(curGarbage.myTransform.position);
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
    private static string ANIM_IDLE_NORMAL = "IdleNormal";
    private static string ANIM_IDLE_WITH_GUN = "IdleWithGun";
    private static string ANIM_IDLE_CARRING = "IdleCarring";
    private static string ANIM_WALK_NORMAL = "WalkNormal";
    private static string ANIM_WALK_WITH_GUN = "RunWithGun";
    private static string ANIM_WALK_CARRING = "WalkCarring";
    public void ChangeAnim()
    {
        //anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        if (navMeshAgent.velocity.magnitude < 0.1f)
        {
            anim.speed = 1;
            if (gun.activeSelf)
            {
                anim.Play(ANIM_IDLE_WITH_GUN);
            }
            else
            {
                if (objHave > 0)
                {
                    anim.Play(ANIM_IDLE_CARRING);
                }
                else
                    anim.Play(ANIM_IDLE_NORMAL);
            }   
        }
        else
        {
           
            if (gun.activeSelf)
            {
                anim.speed = 1/4.0f * speed;
                anim.Play(ANIM_WALK_WITH_GUN);
            }
            else
            {
               
                if (objHave > 0)
                {
                    anim.speed = 0.3f * speed;
                    anim.Play(ANIM_WALK_CARRING);
                }
                else
                {
                    anim.speed = 0.3f * speed;
                    anim.Play(ANIM_WALK_NORMAL);
                }
                 
            }
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

            case IngredientType.LION:
                if (!lionFurs.Contains(ingredient as LionFur))
                    lionFurs.Add(ingredient as LionFur);
                break;
            case IngredientType.LION_CLOTH:
                if (!lionCloths.Contains(ingredient as LionCloth))
                    lionCloths.Add(ingredient as LionCloth);
                break;
            case IngredientType.LION_BAG:
                if (!lionBags.Contains(ingredient as LionBag))
                    lionBags.Add(ingredient as LionBag);
                break;

            case IngredientType.CROC:
                if (!crocFurs.Contains(ingredient as CrocFur))
                    crocFurs.Add(ingredient as CrocFur);
                break;
            case IngredientType.CROC_CLOTH:
                if (!crocCloths.Contains(ingredient as CrocCloth))
                    crocCloths.Add(ingredient as CrocCloth);
                break;
            case IngredientType.CROC_BAG:
                if (!crocBags.Contains(ingredient as CrocBag))
                    crocBags.Add(ingredient as CrocBag);
                break;

            case IngredientType.ELE:
                if (!eleFurs.Contains(ingredient as EleFur))
                    eleFurs.Add(ingredient as EleFur);
                break;
            case IngredientType.ELE_CLOTH:
                if (!eleCloths.Contains(ingredient as EleCloth))
                    eleCloths.Add(ingredient as EleCloth);
                break;
            case IngredientType.ELE_BAG:
                if (!eleBags.Contains(ingredient as EleBag))
                    eleBags.Add(ingredient as EleBag);
                break;

            case IngredientType.ZEBRA:
                if (!zebraFurs.Contains(ingredient as ZebraFur))
                    zebraFurs.Add(ingredient as ZebraFur);
                break;
            case IngredientType.ZEBRA_CLOTH:
                if (!zebraCloths.Contains(ingredient as ZebraCloth))
                    zebraCloths.Add(ingredient as ZebraCloth);
                break;
            case IngredientType.ZEBRA_BAG:
                if (!zebraBags.Contains(ingredient as ZebraBag))
                    zebraBags.Add(ingredient as ZebraBag);
                break;

            case IngredientType.SHIT:
                if (!listShits.Contains(ingredient as Shit))
                    listShits.Add(ingredient as Shit);
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
            case IngredientType.SHEEP_BAG:
                if (sheepBags.Contains(ingredient as SheepBag))
                    sheepBags.Remove(ingredient as SheepBag);
                break;

            case IngredientType.COW:
                if (cowFurs.Contains(ingredient as CowFur))
                    cowFurs.Remove(ingredient as CowFur);
                break;
            case IngredientType.COW_CLOTH:
                if (cowCloths.Contains(ingredient as CowCloth))
                    cowCloths.Remove(ingredient as CowCloth);
                break;
            case IngredientType.COW_BAG:
                if (cowBags.Contains(ingredient as CowBag))
                    cowBags.Remove(ingredient as CowBag);
                break;

            case IngredientType.CHICKEN:
                if (chickenFurs.Contains(ingredient as ChickenFur))
                    chickenFurs.Remove(ingredient as ChickenFur);
                break;
            case IngredientType.CHICKEN_CLOTH:
                if (chickenCloths.Contains(ingredient as ChickenCloth))
                    chickenCloths.Remove(ingredient as ChickenCloth);
                break;
            case IngredientType.CHICKEN_BAG:
                if (chickenBags.Contains(ingredient as ChickenBag))
                    chickenBags.Remove(ingredient as ChickenBag);
                break;

            case IngredientType.BEAR:
                if (bearFurs.Contains(ingredient as BearFur))
                    bearFurs.Remove(ingredient as BearFur);
                break;
            case IngredientType.BEAR_CLOTH:
                if (bearCloths.Contains(ingredient as BearCloth))
                    bearCloths.Remove(ingredient as BearCloth);
                break;
            case IngredientType.BEAR_BAG:
                if (bearBags.Contains(ingredient as BearBag))
                    bearBags.Remove(ingredient as BearBag);
                break;

            case IngredientType.LION:
                if (lionFurs.Contains(ingredient as LionFur))
                    lionFurs.Remove(ingredient as LionFur);
                break;
            case IngredientType.LION_CLOTH:
                if (lionCloths.Contains(ingredient as LionCloth))
                    lionCloths.Remove(ingredient as LionCloth);
                break;
            case IngredientType.LION_BAG:
                if (lionBags.Contains(ingredient as LionBag))
                    lionBags.Remove(ingredient as LionBag);
                break;

            case IngredientType.CROC:
                if (crocFurs.Contains(ingredient as CrocFur))
                    crocFurs.Remove(ingredient as CrocFur);
                break;
            case IngredientType.CROC_CLOTH:
                if (crocCloths.Contains(ingredient as CrocCloth))
                    crocCloths.Remove(ingredient as CrocCloth);
                break;
            case IngredientType.CROC_BAG:
                if (crocBags.Contains(ingredient as CrocBag))
                    crocBags.Remove(ingredient as CrocBag);
                break;

            case IngredientType.ELE:
                if (eleFurs.Contains(ingredient as EleFur))
                    eleFurs.Remove(ingredient as EleFur);
                break;
            case IngredientType.ELE_CLOTH:
                if (eleCloths.Contains(ingredient as EleCloth))
                    eleCloths.Remove(ingredient as EleCloth);
                break;
            case IngredientType.ELE_BAG:
                if (eleBags.Contains(ingredient as EleBag))
                    eleBags.Remove(ingredient as EleBag);
                break;

            case IngredientType.ZEBRA:
                if (zebraFurs.Contains(ingredient as ZebraFur))
                    zebraFurs.Remove(ingredient as ZebraFur);
                break;
            case IngredientType.ZEBRA_CLOTH:
                if (zebraCloths.Contains(ingredient as ZebraCloth))
                    zebraCloths.Remove(ingredient as ZebraCloth);
                break;
            case IngredientType.ZEBRA_BAG:
                if (zebraBags.Contains(ingredient as ZebraBag))
                    zebraBags.Remove(ingredient as ZebraBag);
                break;

            case IngredientType.SHIT:
                if (listShits.Contains(ingredient as Shit))
                    listShits.Remove(ingredient as Shit);
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
            allIngredients[i].myTransform.localPosition = Vector3.up * yOffset +
                Vector3.right * allIngredients[i].myTransform.localPosition.x +
                Vector3.forward * allIngredients[i].myTransform.localPosition.z;
        }
    }
    public void ResetStaff()
    {
        if (isBuff)
        {
            DoubleSpeed();
        }
        else
        {
            ResetSpeed();
        }
        onMission = false;
        objHave = 0;
        transCloset = Vector3.zero;
        transHabitat = Vector3.zero;
        transMachine = Vector3.zero;
        onClosetPos = false;
        onHabitatPos = false;
        onMachinePos = false;
        ingredientType = IngredientType.NONE;
        waitingTime = consWaitingTime;
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
        lionFurs.Clear();
        crocFurs.Clear();
        eleFurs.Clear();
        zebraFurs.Clear();
        lionCloths.Clear();
        crocCloths.Clear();
        zebraCloths.Clear();
        eleCloths.Clear();
        lionBags.Clear();
        crocBags.Clear();
        zebraBags.Clear();
        eleBags.Clear();
        emojiPanel.SetActive(false);
        isAct = false;
    }
    public void ChangeOutfit(StaffType type)
    {
        switch (type)
        {
            case StaffType.FARMER:
                model[0].SetActive(true);
                model[1].SetActive(false);
                anim = model[0].GetComponent<Animator>();
                break;
            case StaffType.WORKER:
                model[0].SetActive(false);
                model[1].SetActive(true);
                anim = model[1].GetComponent<Animator>();
                break;
        }
    }
    public bool CheckFarmerHaveShit()
    {
        bool onlyHaveShit = true;
        for(int i = 0; i < allIngredients.Count; i++)
        {
            if (allIngredients[i] is Shit)
            {
                continue;
            }
            else
            {
                onlyHaveShit = false;
                break;
            }  
        }
        return onlyHaveShit;
    }
    public void CountDownWatingTime()
    {
        if(waitingTime < 0)
        {
            return;
        }
        CounterHelper.Instance.QueueAction(1, () =>
         {
             waitingTime--;
             CountDownWatingTime();
         },1);
    }
    public void StartActing()
    {
        if (listShits.Count > 0)
        {
            if (!isAct)
            {
                isAct = true;
                int r = Random.Range(1, 4);
                ChangeEmoji(r);
            }
        }
        if (isAct)
        {
            delayAct -= Time.deltaTime;
        }
        if (delayAct < 0)
        {
            isAct = false;
            ChangeEmoji(0);
            delayAct = consDelayAct;
        }

    }
    public void ChangeEmoji(int n)
    {
        if (n == 0)
        {
            emojiPanel.SetActive(false);
            //for (int i = 0; i < listEmojis.Count; i++)
            //{
            //    listEmojis[i].SetActive(false);
            //}
        }
        else
        {
            for (int i = 1; i <= listEmojis.Count; i++)
            {
                if (i != n)
                {
                    listEmojis[i - 1].SetActive(false);
                }
                else
                {
                    if (!listEmojis[i - 1].activeSelf)
                    {
                        listEmojis[i - 1].SetActive(true);
                    }
                }
            }
            emojiPanel.SetActive(true);
            //CounterHelper.Instance.QueueAction(5f, () =>
            //{
            //    ChangeEmoji(0);
            //});
        }
    }
}
