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
    public Vector3 transBag;
    public Vector3 transExit;
    public Vector3 transCheckOut;
    public PlaceToBuy placeToBuy;
    public PlaceToBuyBag placeToBuyBag;
    public Checkout checkOut;
    public bool onPlacePos;
    public bool onBagPos;
    public bool onCheckoutPos;
    public IngredientType outfitType;
    public IngredientType bagType;
    [SerializeField]
    private GameObject mainModel;
    [SerializeField]
    private GameObject[] outfitModel;
    [SerializeField]
    private GameObject[] bagModel;
    public bool isLeader;
    public bool gotOutfit;
    public bool gotBag;
    public Customer leader;
    public GroupCustomer grCus;

    protected void Awake()
    {
        fsm.init(7);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(MOVE_TO_CLOSET_STATE, StartMoveToCloset, OnMoveToClosetState));
        fsm.add(new FsmState(MOVE_TO_BAG_STATE, StartMoveToBag, OnMoveToBagState));
        fsm.add(new FsmState(MOVE_CHECKOUT_STATE, StartMoveToCheckOut, OnMoveToCheckOutState));
        fsm.add(new FsmState(FOLLOW_LEADER_STATE, null, OnFollowLeaderState));
        fsm.add(new FsmState(EXIT_STATE, StartExit, null));
        fsm.add(new FsmState(VIP_STATE, null, OnVipState));
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
    private void StartMoveToBag(FsmSystem _fsm)
    {
        MoveToBag();
    }
    private void StartMoveToCheckOut(FsmSystem _fsm)
    {
        MoveToCheckOut();
    }
    private void StartFollowLeader(FsmSystem _fsm)
    {
        FollowLeader();
    }
    private void StartExit(FsmSystem _fsm)
    {
        MoveToExit();
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
    private FsmSystem.ACTION OnMoveToBagState(FsmSystem _fsm)
    {
        CheckMoveToBag();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnMoveToCheckOutState(FsmSystem _fsm)
    {
        CheckMoveToCheckOut();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnFollowLeaderState(FsmSystem _fsm)
    {
        CheckFollowLeader();
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
      //  Debug.Log("Idle");
    }
    public virtual void MoveToCloset()
    { 
        navMeshAgent.SetDestination(transCloset);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckMoveToCloset()
    {
        if (Vector3.Distance(transCloset,this.transform.position) < 0.1f)
        {
            this.transform.DORotate(Vector3.zero, 0f);
            this.onPlacePos = true;
            UpdateState(IDLE_STATE);
        }
       // Debug.Log("Closet");
    }
    public virtual void MoveToBag()
    {
        navMeshAgent.SetDestination(transBag);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckMoveToBag()
    {
        if (Vector3.Distance(transBag, this.transform.position) < 0.1f)
        {
            this.transform.DORotate(Vector3.zero, 0f);
            this.onBagPos = true;
            UpdateState(IDLE_STATE);
        }
        Debug.Log("B");
    }
    public virtual void MoveToCheckOut()
    {
        navMeshAgent.SetDestination(transCheckOut);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckMoveToCheckOut()
    {
        if (Vector3.Distance(transCheckOut, this.transform.position) < 0.1f)
        {
            this.transform.DORotate(Vector3.zero, 0f);
            //navMeshAgent.transform.LookAt(transCheckOut);
            this.onCheckoutPos = true;
            UpdateState(IDLE_STATE);
        }
        Debug.Log("a");
    }
    public virtual void FollowLeader()
    {
        if(!isLeader && leader != null)
        {
            navMeshAgent.SetDestination(leader.transform.position);
            navMeshAgent.stoppingDistance = 0;
        }
    }
    public virtual void CheckFollowLeader()
    {
        navMeshAgent.SetDestination(leader.transform.position);
        navMeshAgent.stoppingDistance = 0;
        if (!leader.gotOutfit && !leader.gotBag)
        {
            if (Vector3.Distance(transCloset, this.transform.position) < 6f || leader.onPlacePos)
            {
                UpdateState(MOVE_TO_CLOSET_STATE);
            }
        }
        else if (leader.gotOutfit && !leader.gotBag)
        {
            if (Vector3.Distance(transBag, this.transform.position) < 6f || leader.onBagPos)
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
    }
    
    public virtual void Exit()
    {

    }
    public virtual void VipState()
    {

    }
    public void ResetStatus()
    {
        transCloset = Vector3.zero;
        transBag = Vector3.zero;
        transExit = Vector3.zero;
        transCheckOut = Vector3.zero;
        onPlacePos = false;
        onBagPos = false;
        onCheckoutPos = false;
        gotOutfit = false;
        gotBag = false;
        placeToBuy = null;
        placeToBuyBag = null;
        checkOut = null;
        grCus = null;
        leader = null;
        isLeader = false;
        mainModel.SetActive(true);
        outfitType = IngredientType.NONE;
        bagType = IngredientType.NONE;
        for (int i = 0; i < outfitModel.Length; i++)
        {
            outfitModel[i].SetActive(false);
        }
        for (int i = 0; i < bagModel.Length; i++)
        {
            bagModel[i].SetActive(false);
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
    public void ChangeBag(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.COW:
                bagModel[0].SetActive(true);
                break;
            case IngredientType.SHEEP:
                bagModel[1].SetActive(true);
                break;
            case IngredientType.CHICKEN:
                bagModel[2].SetActive(true);
                break;
            case IngredientType.BEAR:
                bagModel[3].SetActive(true);
                break;
        }
    }
}
