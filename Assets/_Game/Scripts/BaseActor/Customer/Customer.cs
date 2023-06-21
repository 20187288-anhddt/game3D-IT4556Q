using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Customer : BaseCustomer
{
    [SerializeField]
    private NavMeshAgent navMeshAgent;
    public Vector3 transCloset;
    public Vector3 transExit;
    public Vector3 transCheckOut;
    public PlaceToBuy placeToBuy;
    public CheckOutPosition checkOutPos;
    public bool onPlacePos;
    public bool onCheckoutPos;
    public IngredientType cusType;
    [SerializeField]
    private GameObject mainModel;
    [SerializeField]
    private GameObject[] outfitModel;
<<<<<<< Updated upstream

    protected void Start()
    {
        fsm.init(5);
=======
    [SerializeField]
    private GameObject[] bagModel;
    public bool isLeader;
    public bool gotOutfit;
    public bool gotBag;
    public Customer leader;
    public GroupCustomer grCus;
    private Vector3 pointTaget = Vector3.zero;
    public override void Awake()
    {
        base.Awake();
        fsm.init(7);
>>>>>>> Stashed changes
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(MOVE_TO_CLOSET_STATE, StartMoveToCloset, OnMoveToClosetState));
        fsm.add(new FsmState(MOVE_CHECKOUT_STATE, StartMoveToCheckOut, OnMoveToCheckOutState));
        fsm.add(new FsmState(EXIT_STATE, null, OnExitState));
        fsm.add(new FsmState(VIP_STATE, null, OnVipState));
        onPlacePos = false;
        onCheckoutPos = false;
        cusType = IngredientType.NONE;
    }
    protected void Update()
    {
        fsm.execute();
        ChangeAnim();
    }
    private void StartMoveToCloset(FsmSystem _fsm)
    {
        MoveToCloset();
    }
    private void StartMoveToCheckOut(FsmSystem _fsm)
    {
        MoveToCheckOut();
    }
    private FsmSystem.ACTION OnIdleState(FsmSystem _fsm)
    {
        Idle();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToClosetState(FsmSystem _fsm)
    {
        CheckMoveToCloset();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToCheckOutState(FsmSystem _fsm)
    {
        CheckMoveToCheckOut();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnExitState(FsmSystem _fsm)
    {
        Exit();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnVipState(FsmSystem _fsm)
    {
        VipState();
        return FsmSystem.ACTION.END;
    }
    public virtual void Idle()
    {
    }
    public virtual void MoveToCloset()
    { 
        navMeshAgent.SetDestination(transCloset);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckMoveToCloset()
    {
        if (Vector3.Distance(transCloset, myTransform.position) < 0.1f)
        {
            myTransform.DORotate(Vector3.zero, 0f);
            this.onPlacePos = true;
            UpdateState(IDLE_STATE);
        }
<<<<<<< Updated upstream
=======
       // Debug.Log("Closet");
    }
    public virtual void MoveToBag()
    {
      
        navMeshAgent.SetDestination(transBag);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transBag; 
    }
    public virtual void CheckMoveToBag()
    {
        if (Vector3.Distance(transBag, myTransform.position) < 0.1f)
        {
            myTransform.DORotate(Vector3.zero, 0f);
            this.onBagPos = true;
            UpdateState(IDLE_STATE);
        }
       // Debug.Log("B");
>>>>>>> Stashed changes
    }
    public virtual void MoveToCheckOut()
    {
        navMeshAgent.SetDestination(transCheckOut);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckMoveToCheckOut()
    {
        if (Vector3.Distance(transCheckOut, myTransform.position) < 0.1f)
        {
<<<<<<< Updated upstream
            this.transform.DORotate(Vector3.zero, 0f);
=======
            myTransform.DORotate(Vector3.zero, 0f);
            //navMeshAgent.transform.LookAt(transCheckOut);
>>>>>>> Stashed changes
            this.onCheckoutPos = true;
            UpdateState(IDLE_STATE);
        }
    }
<<<<<<< Updated upstream
=======
    public virtual void FollowLeader()
    {
        if(!isLeader && leader != null)
        {
            navMeshAgent.SetDestination(leader.myTransform.position);
            navMeshAgent.stoppingDistance = 0;
            pointTaget = leader.myTransform.position;
        }
    }
    public virtual void CheckFollowLeader()
    {
        navMeshAgent.SetDestination(leader.myTransform.position);
        navMeshAgent.stoppingDistance = 0;
        if (!leader.gotOutfit && !leader.gotBag)
        {
            if (Vector3.Distance(transCloset, myTransform.position) < 6f || leader.onPlacePos)
            {
                UpdateState(MOVE_TO_CLOSET_STATE);
            }
        }
        else if (leader.gotOutfit && !leader.gotBag)
        {
            if (Vector3.Distance(transBag, myTransform.position) < 6f || leader.onBagPos)
            {
                UpdateState(MOVE_TO_BAG_STATE);
            }
        }
        //else if (leader.gotOutfit && leader.gotBag && leader.onCheckoutPos)
        //{
        //    if(Vector3.Distance(transCheckout, leader.transform.position) < 3f)
        //    {
        //        UpdateState(IDLE_STATE);
        //    }
        //} 
    }
    public virtual void MoveToExit()
    {
        navMeshAgent.SetDestination(transExit);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transExit;
    }
    
>>>>>>> Stashed changes
    public virtual void Exit()
    {

    }
    public virtual void VipState()
    {

    }
    public void ResetStatus()
    {
        onPlacePos = false;
        onCheckoutPos = false;
        placeToBuy = null;
        mainModel.SetActive(true);
        for(int i = 0; i < outfitModel.Length; i++)
        {
            outfitModel[i].SetActive(false);
        }
        UpdateState(IDLE_STATE);
    }
    public void ChangeAnim()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            //animator.Play("Running");
        }
    }
    public void ChangeOutfit(IngredientType type)
    {
        mainModel.SetActive(false);
        switch (type)
        {
            case IngredientType.COW:
                outfitModel[0].SetActive(true);
                break;
            case IngredientType.SHEEP:
                outfitModel[1].SetActive(true);
                break;
            case IngredientType.CHICKEN:
                outfitModel[2].SetActive(true);
                break;
            case IngredientType.BEAR:
                outfitModel[3].SetActive(true);
                break;
        }
        
    }
}
