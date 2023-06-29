using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public abstract class AnimalBase : MonoBehaviour,IAct
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
    public GameObject clothes;
    public Habitat habitat;
    [SerializeField]
    private float timeLive;
    [SerializeField]
    private float consTimeLive;
    [SerializeField]
    private float consTimeDelayHaveFun;
    [SerializeField]
    private Vector3 nextDes;
    [SerializeField]
    private HabitatManager habitatManager;

    public virtual void Awake()
    {
        if (myTransform == null) { myTransform = this.transform; }
        fsm.init(5);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(EAT_STATE, null, OnEatState));
        fsm.add(new FsmState(RUN_STATE, StartRunState, OnRunState));
        fsm.add(new FsmState(FINISH_COLLECT_STATE, null, OnFinishCollectState));
        fsm.add(new FsmState(HAVE_FUN_STATE, StartHaveFunState, OnHaveFunState));
        UpdateState(IDLE_STATE);
        fsm.execute();
    }
    public void StartInGame()
    {
        ResetAnimal();
        habitatManager = GameManager.Instance.listLevelManagers[0].habitatManager;
    }
    void Update()
    {
        fsm.execute();
        if (isNaked && isReadyReset)
        {
            isReadyReset = false;
            CounterHelper.Instance.QueueAction(timeDelayFur, () => { ResetWool(); });
        }
        if (!habitat.isLock)
        {
            if (isInside)
            {
                if(onIdlePos)
                {
                    onIdlePos = false;
                    switch (habitatManager.allActiveHabitats.Count)
                    {
                        case 1:
                            RandomMoveInSide();
                            break;
                        case 2:
                        case 3:
                            RandomMoveOutSide();
                            break;
                    }
                }
                else if(isReadyCountDown)
                {
                    timeLive -= Time.deltaTime;
                    if (timeLive < 0)
                    {
                        onIdlePos = true;
                        isReadyCountDown = false;
                        timeLive = consTimeLive;
                    }
                }
            }
        }
    }
    public void RandomMoveInSide()
    {
        isReadyCountDown = true;
        int r = Random.Range(1, 10);
        if (r < 5)
        {
            if (RandomPosition(true))
            {
                UpdateState(RUN_STATE);
            }
            else
            {
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
        if (!isInside)
        {
            CounterHelper.Instance.QueueAction(3f, () => { GetComponent<CapsuleCollider>().enabled = true; });
        }
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
    public virtual void Idle()
    {
        
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
        clothes.SetActive(false);
        if (habitat.animalsIsReady.Contains(this))
            habitat.animalsIsReady.Remove(this);
    }
    public void ResetWool()
    {
        isNaked = false;
        clothes.SetActive(true);
        CounterHelper.Instance.QueueAction(timeDelayCollect, () =>
        {
            if (!habitat.animalsIsReady.Contains(this))
                habitat.animalsIsReady.Add(this);
        }); 
    }

    public void SetHabitat(Habitat habitat)
    {
        this.habitat = habitat;
    }

    public bool RandomPosition(bool tmp)
    {
        bool checkIf = false;
        float radius = 0;
        nextDes = Vector3.zero;
        if (tmp)
        {
            switch (habitat.ingredientType)
            {
                case IngredientType.CHICKEN:
                    radius = 5f;
                    break;
                case IngredientType.COW:
                    radius = 7.5f;
                    break;
                case IngredientType.BEAR:
                    radius = 9f;
                    break;

            }
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
                        curHabitat = habitatManager.GetHabitatWithType(IngredientType.COW);
                    }
                    else
                    {
                        curHabitat = habitatManager.GetHabitatWithType(IngredientType.BEAR);
                    }
                    break;
                case IngredientType.COW:
                    int r2 = UnityEngine.Random.Range(0, 2);
                    if (r2 == 0)
                    {
                        curHabitat = habitatManager.GetHabitatWithType(IngredientType.CHICKEN);
                    }
                    else
                    {
                        curHabitat = habitatManager.GetHabitatWithType(IngredientType.BEAR);
                    }
                    break;
                case IngredientType.BEAR:
                    int r3 = UnityEngine.Random.Range(0, 2);
                    if (r3 == 0)
                    {
                        curHabitat = habitatManager.GetHabitatWithType(IngredientType.COW);
                    }
                    else
                    {
                        curHabitat = habitatManager.GetHabitatWithType(IngredientType.CHICKEN);
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
                switch (habitat.ingredientType)
                {
                    case IngredientType.CHICKEN:
                        radius = 5f;
                        break;
                    case IngredientType.COW:
                        radius = 7.5f;
                        break;
                    case IngredientType.BEAR:
                        radius = 9f;
                        break;

                }
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
        GetComponent<CapsuleCollider>().enabled = false;
        isInside = true;
        CounterHelper.Instance.QueueAction(consTimeDelayHaveFun, () => { isReadyHaveFun = true; });
        nextDes = Vector3.zero;
        timeLive = consTimeLive;
        isReadyCountDown = false;
        onIdlePos = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            GetComponent<CapsuleCollider>().enabled = false;
            navMeshAgent.isStopped = true;
            myTransform.DORotate(new Vector3(0f,180f,0f), 0f);
            timeLive = consTimeLive;
            if (!habitat.allAnimals.Contains(this))
            {
                habitat.allAnimals.Add(this);
                Vector3 tmpPos = habitat.defaultAnimalPos[habitat.allAnimals.IndexOf(this)].position;
                this.transform.DOMove(tmpPos, 0f);
            }
            ResetWool();
            isInside = true;
            CounterHelper.Instance.QueueAction(consTimeDelayHaveFun, () => { 
                isReadyHaveFun = true;
                onIdlePos = true;
                navMeshAgent.isStopped = false;
            });
            nextDes = Vector3.zero;
            UpdateState(IDLE_STATE);
        }
    }
}
