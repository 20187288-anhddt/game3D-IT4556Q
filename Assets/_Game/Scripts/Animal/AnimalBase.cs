using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public abstract class AnimalBase : AllPool,IAct
{
    public Transform myTransform;
    public FsmSystem fsm = new FsmSystem();
    public static string IDLE_STATE = "idle_state";
    public static string EAT_STATE = "eat_state";
    public static string RUN_STATE = "run_state";
    public static string FINISH_COLLECT_STATE = "finish_collect_state";
    public static string HAVE_FUN_STATE = "have_fun_state";
    public string STATE_ANIMAL;
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    public bool isInside;
    public bool isNaked;
    public bool isReadyReset;
    public bool isReadyHaveFun;
    public bool onIdlePos;
    public bool isReadyCountDown;
    public float timeDelayFur;
    public float timeDelayCollect;
    public int maxFurNum;
    public int curFurNum;
    //public GameObject clothes;
    public Habitat habitat;
    [SerializeField]
    private float timeLive;
    [SerializeField]
    private float consTimeLive;
    [SerializeField]
    private float consTimeDelayHaveFun;
    public Vector3 nextDes;
    [SerializeField]
    private HabitatManager habitatManager;
    [SerializeField]
    private LayerMask layer;
    private Animator anim;
    public GameObject mainModel;
    public GameObject nakedModel;

    public virtual void Awake()
    {
        if (myTransform == null) { myTransform = this.transform; }
        fsm.init(2);
        fsm.add(new FsmState(IDLE_STATE, StartIdleState, OnIdleState));
        fsm.add(new FsmState(RUN_STATE, StartRunState, OnRunState));
        fsm.execute();
        UpdateState(IDLE_STATE);
    }
    public void StartInGame()
    {
        ResetAnimal();
        habitatManager = GameManager.Instance.listLevelManagers[0].habitatManager;
        mainModel.SetActive(true);
        nakedModel.SetActive(false);
    }
    void Update()
    {
        fsm.execute();
        if (habitat != null && isNaked && isReadyReset)
        {
            isReadyReset = false;
            CounterHelper.Instance.QueueAction(timeDelayFur, () => { 
                ResetWool(); },1);
        }
        if (habitat != null && !habitat.isLock)
        {
            if (isInside)
            {
                if(onIdlePos)
                {
                    onIdlePos = false;
                    RandomMoveInSide();
                    //switch (habitatManager.allActiveHabitats.Count)
                    //{
                    //    case 1:
                    //        RandomMoveInSide();
                    //        break;
                    //    case 2:
                    //    case 3:
                    //        RandomMoveOutSide();
                    //        break;
                    //}
                }
                else //if(isReadyCountDown)
                {
                    timeLive -= Time.deltaTime;
                   
                }
                if (timeLive < 0)
                {
                    //isReadyCountDown = false;
                    timeLive = consTimeLive;
                    onIdlePos = true;
                }
            }
        }
    }
    public void ChangeAnim(bool isSuprise)
    {
        //if (STATE_ANIMAL == IDLE_STATE)
        //{
        //    switch (habitat.ingredientType)
        //    {
        //        case IngredientType.BEAR:
        //            anim.SetTrigger("bearIsIdle");
        //            break;
        //        case IngredientType.COW:
        //            anim.SetTrigger("cowIsIdle");
        //            break;
        //        case IngredientType.CHICKEN:
        //            anim.SetTrigger("chickenIsIdle");
        //            break;
        //    }
        //}
        //else if (STATE_ANIMAL == RUN_STATE)
        //{
        //    switch (habitat.ingredientType)
        //    {
        //        case IngredientType.BEAR:
        //            anim.SetTrigger("bearIsRun");
        //            break;
        //        case IngredientType.COW:
        //            anim.SetTrigger("cowIsRun");
        //            break;
        //        case IngredientType.CHICKEN:
        //            anim.SetTrigger("chickenIsRun");
        //            break;
        //    }
        //}

        if (isSuprise)
        {
            if (STATE_ANIMAL == IDLE_STATE)
            {
                switch (habitat.ingredientType)
                {
                    case IngredientType.BEAR:
                        anim.SetTrigger("bearIsIdle");
                        break;
                    case IngredientType.COW:
                        anim.SetTrigger("cowIsIdle");
                        break;
                    case IngredientType.CHICKEN:
                        anim.SetTrigger("chickenIsIdle");
                        break;
                }
            }
            else if (STATE_ANIMAL == RUN_STATE)
            {
                switch (habitat.ingredientType)
                {
                    case IngredientType.BEAR:
                        anim.SetTrigger("bearIsRun");
                        break;
                    case IngredientType.COW:
                        anim.SetTrigger("cowIsRun");
                        break;
                    case IngredientType.CHICKEN:
                        anim.SetTrigger("chickenIsRun");
                        break;
                }
            }
            anim.Play("Suprise");
        }
        else
        {
            if (!isNaked)
            {
                //anim = mainModel.GetComponent<Animator>();
                if (STATE_ANIMAL == IDLE_STATE)
                {
                    int r = Random.Range(1, 5);
                    switch (r)
                    {
                        case 1:
                            anim.Play("IdleJump");
                            break;
                        case 2:
                            anim.Play("IdleDance");
                            break;
                        case 3:
                            anim.Play("IdleEat");
                            break;
                        case 4:
                            anim.Play("IdleSleep");
                            break;
                    }
                }
                else if (STATE_ANIMAL == RUN_STATE)
                {
                    anim.Play("Walk");
                }
            }
            else
            {
                //anim = nakedModel.GetComponent<Animator>();
                if (STATE_ANIMAL == IDLE_STATE)
                {
                    int r = Random.Range(1, 5);
                    switch (r)
                    {
                        case 1:
                            anim.Play("IdleJump");
                            break;
                        case 2:
                            anim.Play("IdleDance");
                            break;
                        case 3:
                            anim.Play("IdleEat");
                            break;
                        case 4:
                            anim.Play("IdleSleep");
                            break;
                    }
                }
                else if (STATE_ANIMAL == RUN_STATE)
                {
                    anim.Play("Walk");
                }
            }
        }
    }
    public void RandomMoveInSide()
    {
        //isReadyCountDown = true;
        int r = Random.Range(1, 10);
        if (r < 8)
        {
            if (RandomPosition(true))
            {
                UpdateState(RUN_STATE);
            }
            else
            {
                //CounterHelper.Instance.QueueAction(consTimeLive, () => {
                //    onIdlePos = true;
                //},1);
                UpdateState(IDLE_STATE);
            }
        }
    }
    public void RandomMoveOutSide()
    {
        int r = Random.Range(1, 10);
        if (r < 0 && isReadyHaveFun)
        {
            if (RandomPosition(false))
            {
                isReadyHaveFun = false;
                isInside = false;
                UpdateState(HAVE_FUN_STATE);
            }
            else
            {
                CounterHelper.Instance.QueueAction(consTimeLive, () => {
                    onIdlePos = true;
                },1);
                UpdateState(IDLE_STATE);
            }
        }
        else
        {
            RandomMoveInSide();
        }
    }
    public void UpdateState(string state)
    {
        fsm.setState(state);
        STATE_ANIMAL = state;
    }
    private void StartIdleState(FsmSystem _fsm)
    {
        StartIdle();
    }
    private void StartRunState(FsmSystem _fsm)
    {
        Run(true);
    }
    private void StartHaveFunState(FsmSystem _fsm)
    {
        Run(false);
    }
    private FsmSystem.ACTION OnEatState(FsmSystem _fsm)
    {
        Eat();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnIdleState(FsmSystem _fsm)
    {
        Idle();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnRunState(FsmSystem _fsm)
    {
        CheckRunToPos();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnFinishCollectState(FsmSystem _fsm)
    {
        FinishCollect();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnHaveFunState(FsmSystem _fsm)
    {
        CheckRunToPos();
        return FsmSystem.ACTION.END;
    }
    public virtual void Run(bool isInside)
    {
        ChangeAnim(false);
        navMeshAgent.isStopped = false;
        //if (isInside == false)
        //{
        //    CounterHelper.Instance.QueueAction(3f, () => { GetComponent<CapsuleCollider>().enabled = true; });
        //}
        navMeshAgent.SetDestination(nextDes);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckRunToPos()
    {
       
        if (Vector3.Distance(nextDes, myTransform.position) < 0.5f)
        {
            //myTransform.DORotate(Vector3.zero, 0f);
            //myTransform.LookAt(placeToBuy.closet.myTransform.position);
            UpdateState(IDLE_STATE);
        }
        //if (isInside)
        //{
        //    if (Vector3.Distance(nextDes, myTransform.position) < 0.1f)
        //    {
        //        //myTransform.DORotate(Vector3.zero, 0f);
        //        //myTransform.LookAt(placeToBuy.closet.myTransform.position);
        //        UpdateState(IDLE_STATE);
        //    }
        //}
        //else
        //{
        //    if (Vector3.Distance(nextDes, myTransform.position) < 3f)
        //    {
        //        GetComponent<CapsuleCollider>().enabled = true;
        //        //myTransform.DORotate(Vector3.zero, 0f);
        //        //myTransform.LookAt(placeToBuy.closet.myTransform.position);
        //        UpdateState(IDLE_STATE);
        //    }
        //}
       
    }
    public virtual void StartIdle()
    {
        ChangeAnim(false);
    }
    public virtual void Idle()
    {
        //navMeshAgent.isStopped = true;
    }

    public virtual void Eat()
    {

    }

    public virtual void FinishCollect()
    {

    }

    public void CollectWool()
    {
        isNaked = true;
        isReadyReset = true;
        //clothes.SetActive(false);
        mainModel.SetActive(false);
        nakedModel.SetActive(true);
        anim = nakedModel.GetComponent<Animator>();
        ChangeAnim(true);
        if (habitat.animalsIsReady.Contains(this))
            habitat.animalsIsReady.Remove(this);
    }
    public void ResetWool()
    {
        isNaked = false;
        //clothes.SetActive(true);
        mainModel.SetActive(true);
        nakedModel.SetActive(false);
        anim = mainModel.GetComponent<Animator>();
        CounterHelper.Instance.QueueAction(timeDelayCollect, () =>
        {
            if (!habitat.animalsIsReady.Contains(this))
                habitat.animalsIsReady.Add(this);
        },1); 
    }

    public void SetHabitat(Habitat habitat)
    {
        this.habitat = habitat;
        anim = mainModel.GetComponent<Animator>();
        UpdateState(IDLE_STATE);
    }

    public bool RandomPosition(bool tmp)
    {
        bool checkIf = false;
        nextDes = Vector3.zero;
        float radius = 0;
        switch (this.habitat.ingredientType)
        {
            case IngredientType.CHICKEN:
                radius = 4.5f;
                break;
            case IngredientType.COW:
                radius = 7f;
                break;
            case IngredientType.BEAR:
                radius = 8.5f;
                break;

        }
        if (tmp == true)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
            randomDirection += this.habitat.transform.position;
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                nextDes = new Vector3(hit.position.x, myTransform.position.y, hit.position.z);
                checkIf = true;
            }
        }
        else
        {
            Habitat curHabitat = null;
            switch (habitat.ingredientType)
            {
                case IngredientType.CHICKEN:
                    int r1 = UnityEngine.Random.Range(0, 2);
                    if (r1 == 0)
                    {
                        curHabitat = habitatManager.GetHabitatWithTypeForAnimal(IngredientType.COW);
                    }
                    else
                    {
                        curHabitat = habitatManager.GetHabitatWithTypeForAnimal(IngredientType.BEAR);
                    }
                    break;
                case IngredientType.COW:
                    int r2 = UnityEngine.Random.Range(0, 2);
                    if (r2 == 0)
                    {
                        curHabitat = habitatManager.GetHabitatWithTypeForAnimal(IngredientType.CHICKEN);
                    }
                    else
                    {
                        curHabitat = habitatManager.GetHabitatWithTypeForAnimal(IngredientType.BEAR);
                    }
                    break;
                case IngredientType.BEAR:
                    int r3 = UnityEngine.Random.Range(0, 2);
                    if (r3 == 0)
                    {
                        curHabitat = habitatManager.GetHabitatWithTypeForAnimal(IngredientType.COW);
                    }
                    else
                    {
                        curHabitat = habitatManager.GetHabitatWithTypeForAnimal(IngredientType.CHICKEN);
                    }
                    break;
            }
            if(curHabitat == null)
            {
                isInside = true;
                checkIf = false;
            }
            else
            {
                Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
                randomDirection += curHabitat.transform.position;
                UnityEngine.AI.NavMeshHit hit;
                if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
                {
                    nextDes = new Vector3(hit.position.x, myTransform.position.y, hit.position.z);
                    if (habitat.animalsIsReady.Contains(this))
                    {
                        habitat.animalsIsReady.Remove(this);
                    }
                    if (habitat.allAnimals.Contains(this))
                    {
                        habitat.allAnimals.Remove(this);
                    }
                    checkIf =  true;
                }
                else
                {
                    isInside = true;
                    checkIf = false;
                }
            }
        }
        return checkIf;
    }
    public void ResetAnimal()
    {
        ResetWool();
        //GetComponent<CapsuleCollider>().enabled = false;
        isInside = true;
        //CounterHelper.Instance.QueueAction(consTimeDelayHaveFun, () => { isReadyHaveFun = true; },1);
        nextDes = Vector3.zero;
        timeLive = consTimeLive;
        isReadyCountDown = false;
        onIdlePos = true;
        UpdateState(IDLE_STATE);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    var player = other.GetComponent<Player>();
    //    if (player != null)
    //    {
    //        GetComponent<CapsuleCollider>().enabled = false;
    //        myTransform.DORotate(new Vector3(0f,180f,0f), 0f);
    //        timeLive = consTimeLive;
    //        if (!habitat.allAnimals.Contains(this))
    //        {
    //            habitat.allAnimals.Add(this);
    //            Vector3 tmpPos = habitat.defaultAnimalPos[habitat.allAnimals.IndexOf(this)].position;
    //            this.transform.DOMove(tmpPos, 0f);
    //        }
    //        isInside = true;
    //        CounterHelper.Instance.QueueAction(consTimeDelayHaveFun, () => { 
    //            isReadyHaveFun = true;
    //            onIdlePos = true;
    //        });
    //        nextDes = Vector3.zero;
    //        UpdateState(IDLE_STATE);
    //    }
    //}
}
