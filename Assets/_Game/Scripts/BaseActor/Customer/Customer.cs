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
    public Checkout checkOut;
    public bool onPlacePos;
    public bool onCheckoutPos;
    public IngredientType cusType;
    [SerializeField]
    private GameObject mainModel;
    [SerializeField]
    private GameObject[] outfitModel;

    protected void Start()
    {
        fsm.init(5);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(MOVE_TO_CLOSET_STATE, StartMoveToCloset, OnMoveToClosetState));
        fsm.add(new FsmState(MOVE_CHECKOUT_STATE, StartMoveToCheckOut, OnMoveToCheckOutState));
        fsm.add(new FsmState(EXIT_STATE, StartExit, null));
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
        //navMeshAgent.transform.LookAt(transCloset);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void CheckMoveToCloset()
    {
        if (Vector3.Distance(transCloset,this.transform.position) < 0.1f)
        {
            this.transform.DORotate(Vector3.zero, 0f);
            //navMeshAgent.transform.LookAt(transCloset);
            this.onPlacePos = true;
            UpdateState(IDLE_STATE);
        }
    }
    public virtual void MoveToCheckOut()
    {
        navMeshAgent.SetDestination(transCheckOut);
        navMeshAgent.stoppingDistance = 0;
    }
    public virtual void MoveToExit()
    {
        navMeshAgent.SetDestination(transExit);
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
    }
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
