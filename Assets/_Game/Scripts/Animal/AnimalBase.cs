using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public abstract class AnimalBase : MonoBehaviour
{
    public Transform myTransform;
    public FsmSystem fsm = new FsmSystem();
    public static string IDLE_STATE = "idle_state";
    public static string EAT_STATE = "eat_state";
    public static string RUN_STATE = "run_state";
    public static string FINISH_COLLECT_STATE = "finish_collect_state";

    public string STATE_ANIMAL;

    [SerializeField]
    private NavMeshAgent navMeshAgent;
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
    public Vector3 curPos;
    [SerializeField]
    private float timeLive;
    public float consTimeLive;

    public virtual void Awake()
    {
        if (myTransform == null) { myTransform = this.transform; }
        fsm.init(4);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(EAT_STATE, null, OnEatState));
        fsm.add(new FsmState(RUN_STATE, StartRunState, OnRunState));
        fsm.add(new FsmState(FINISH_COLLECT_STATE, null, OnFinishCollectState));
        UpdateState(IDLE_STATE);
        fsm.execute();
      
    }
    void Start()
    {
        ResetAnimal();
    }
    void Update()
    {
        fsm.execute();
        if(isNaked && isReadyReset)
        {
            isReadyReset = false;
            CounterHelper.Instance.QueueAction(timeDelayFur, () => { ResetWool(); });
        }
        if (!habitat.isLock)
        {
            if (onIdlePos && STATE_ANIMAL != RUN_STATE)
            {
                onIdlePos = false;
                float r = Random.Range(0f, 1f);
                if(r<0.5f)
                {
                    UpdateState(RUN_STATE);
                } 
            }
            if (!onIdlePos)
            {
                timeLive -= Time.deltaTime;
            }
            if(timeLive < 0)
            {
                onIdlePos = true;
                timeLive = consTimeLive;
            }
        }
    }
    public void UpdateState(string state)
    {
        fsm.setState(state);
        STATE_ANIMAL = state;
    }
    private void StartRunState(FsmSystem _fsm)
    {
        Run();
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
    public virtual void Run()
    {
        curPos = RandomPosition();
        navMeshAgent.SetDestination(curPos);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckRunToPos()
    {
        if (Vector3.Distance(curPos, myTransform.position) < 0.1f)
        {
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

    public Vector3 RandomPosition()
    {
        float radius = 0;
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
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return new Vector3(finalPosition.x,myTransform.position.y,finalPosition.z);
    }
    public void ResetAnimal()
    {
        ResetWool();
        curPos = Vector3.zero;
        timeLive = consTimeLive;
        onIdlePos = true;
    }
}
