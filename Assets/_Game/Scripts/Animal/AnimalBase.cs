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
    public bool onIdlePos;
    public float timeDelayFur;
    public float timeDelayCollect;
    public int maxFurNum;
    public int curFurNum;
    public GameObject clothes;
    [SerializeField]
    private Habitat habitat;
    [SerializeField]
    private float timeLive;
    public float consTimeLive;
    public Vector3 nextDes;
    [SerializeField]
    private HabitatManager habitatManager;
    public Transform defaultPos;

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
        if (isInside)
        {
            if (!habitat.isLock)
            {
                if (onIdlePos && STATE_ANIMAL != RUN_STATE)
                {
                    onIdlePos = false;
                    if (habitatManager.allActiveHabitats.Count > 1)
                    {
                        int r = Random.Range(1, 10);
                        if (r == 11)
                        {
                            isInside = false;
                            UpdateState(HAVE_FUN_STATE);
                        }
                        else
                        {
                            float r1 = Random.Range(0f, 1f);
                            if (r1 < 0.5f)
                            {
                                UpdateState(RUN_STATE);
                            }
                        }
                    }
                    else
                    {
                        float r = Random.Range(0f, 1f);
                        if (r < 0.5f)
                        {
                            UpdateState(RUN_STATE);
                        }
                    }    
                }
                if (!onIdlePos && isInside)
                {
                    timeLive -= Time.deltaTime;
                }
                if (timeLive < 0)
                {
                    onIdlePos = true;
                    timeLive = consTimeLive;
                }
            }
        }
        else
        {
            //if (onIdlePos && STATE_ANIMAL != HAVE_FUN_STATE)
            //{
            //    float r1 = Random.Range(0f, 1f);
            //    if (r1 < 0.5f)
            //    {
            //        UpdateState(HAVE_FUN_STATE);
            //    }
            //}   
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
        if (RandomPosition(isInside))
        {
            navMeshAgent.SetDestination(nextDes);
            navMeshAgent.stoppingDistance = 0;
        }
    }
    public virtual void CheckRunToPos()
    {
        if (Vector3.Distance(nextDes, myTransform.position) < 0.1f)
        {
            if (isInside)
            {
                GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                GetComponent<CapsuleCollider>().enabled = true;
            }
            //myTransform.DORotate(Vector3.zero, 0f);
            //myTransform.LookAt(placeToBuy.closet.myTransform.position);
            UpdateState(IDLE_STATE);
        }
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
                return true;
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
                UpdateState(IDLE_STATE);
                return false;
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
                    return true;
                }
                else
                {
                    isInside = true;
                    UpdateState(IDLE_STATE);
                }
            }
        }
        return false;
    }
    public void ResetAnimal()
    {
        ResetWool();
        GetComponent<CapsuleCollider>().enabled = false;
        isInside = true;
        nextDes = Vector3.zero;
        timeLive = consTimeLive;
        defaultPos = habitat.animalPos;
        onIdlePos = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            this.transform.position = new Vector3(defaultPos.position.x, myTransform.position.y, defaultPos.position.z);
            nextDes = Vector3.zero;
            timeLive = consTimeLive;
            UpdateState(IDLE_STATE);
            //ResetAnimal();
            if (!habitat.allAnimals.Contains(this))
            {
                habitat.allAnimals.Add(this);
            }
            ResetWool();
            GetComponent<CapsuleCollider>().enabled = false;
            isInside = true;
            onIdlePos = true;
        }
    }
}
