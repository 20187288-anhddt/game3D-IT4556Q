using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Customer : BaseCustomer,IAct
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
    public CheckoutPos placeCheckout;
    public bool onPlacePos;
    public bool onBagPos;
    public bool onCheckoutPos;
    public IngredientType outfitType;
    public IngredientType bagType;
    [SerializeField]
    private GameObject mainModel;
    [SerializeField]
    private GameObject[] outfitModel;
    ////[SerializeField]
    //public GameObject[] flag;
    [SerializeField]
    private GameObject[] bagModel;
    public bool isLeader;
    public bool gotOutfit;
    public bool gotBag;
    public bool isExit;
    public Customer leader;
    public GroupCustomer grCus;
    private Vector3 pointTaget = Vector3.zero;
    private Animator anim;
    [SerializeField]
    private float consDelayAct;
    private float delayAct;
    private bool isAct;
    public GameObject emojiPanel;
    private float waitingTime;
    public float consWaitingTime;
    public List<GameObject> listEmojis { get => ListEmojis; set => ListEmojis = value; }
    public override void Awake()
    {
        base.Awake();
        fsm.init(7);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(MOVE_TO_CLOSET_STATE, StartMoveToCloset, OnMoveToClosetState));
        fsm.add(new FsmState(MOVE_TO_BAG_STATE, StartMoveToBag, OnMoveToBagState));
        fsm.add(new FsmState(MOVE_CHECKOUT_STATE, StartMoveToCheckOut, OnMoveToCheckOutState));
        fsm.add(new FsmState(FOLLOW_LEADER_STATE, null, OnFollowLeaderState));
        fsm.add(new FsmState(EXIT_STATE, StartExit, null));
        fsm.add(new FsmState(VIP_STATE, null, OnVipState));
    }
    private void Start()
    {
        anim = mainModel.GetComponent<Animator>();
        EnventManager.AddListener(EventName.ReLoadNavMesh.ToString(), ReloadSetDestination);  
    }
    public void ReloadSetDestination()
    {
        if (STATE_CUSTOMER != IDLE_STATE && gameObject.activeInHierarchy)
        {
            navMeshAgent.SetDestination(pointTaget);
        }
    }
    protected void Update()
    {
        fsm.execute();
        ChangeAnim();
        StartActing();
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
        transCloset.y = myTransform.position.y;
        navMeshAgent.SetDestination(transCloset);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transCloset;
    }
    public virtual void CheckMoveToCloset()
    {
        if (Vector3.Distance(transCloset, myTransform.position) < 0.1f)
        {
            myTransform.DORotate(Vector3.zero, 0f);
            //myTransform.LookAt(placeToBuy.closet.myTransform.position);
            CountDownWatingTime();
            this.onPlacePos = true;
            UpdateState(IDLE_STATE);
        }
       // Debug.Log("Closet");
    }
    public virtual void MoveToBag()
    {
        transBag.y = myTransform.position.y;
        navMeshAgent.SetDestination(transBag);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transBag; 
    }
    public virtual void CheckMoveToBag()
    {
        if (Vector3.Distance(transBag, myTransform.position) < 0.1f)
        {
            //myTransform.DORotate(Vector3.zero, 0f);
            myTransform.LookAt(placeToBuyBag.closet.myTransform.position);
            if(waitingTime < 0)
            {
                waitingTime = consWaitingTime;
                CountDownWatingTime();
            }
            else
            {
                waitingTime = consWaitingTime;
            }
            this.onBagPos = true;
            UpdateState(IDLE_STATE);
        }
       // Debug.Log("B");
    }
    public virtual void MoveToCheckOut()
    {
        transCheckOut.y = myTransform.position.y;
        navMeshAgent.SetDestination(transCheckOut);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transCheckOut;
    }
    public virtual void CheckMoveToCheckOut()
    {
        if (Vector3.Distance(transCheckOut, myTransform.position) < 0.1f)
        {
            //myTransform.DORotate(Vector3.zero, 0f);
            navMeshAgent.transform.LookAt(checkOut.transform.position);
            if (waitingTime < 0)
            {
                waitingTime = consWaitingTime;
                CountDownWatingTime();
            }
            else
            {
                waitingTime = consWaitingTime;
            }
            this.onCheckoutPos = true;
            UpdateState(IDLE_STATE);
        }
        //Debug.Log("a");
    }
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
        if (!leader.gotOutfit && !leader.gotBag && !leader.isExit)
        {
            if (Vector3.Distance(transCloset, myTransform.position) < 6f || leader.onPlacePos)
            {
                UpdateState(MOVE_TO_CLOSET_STATE);
            }
        }
        if (leader.gotOutfit && !leader.gotBag && !leader.isExit)
        {
            if (Vector3.Distance(transBag, myTransform.position) < 6f || leader.onBagPos)
            {
                UpdateState(MOVE_TO_BAG_STATE);
            }
        }
        if (leader.gotOutfit && leader.gotBag && !leader.isExit)
        {
            if (Vector3.Distance(transCheckOut, myTransform.position) < 6f || leader.onCheckoutPos)
            {
                UpdateState(MOVE_CHECKOUT_STATE);
            }
        }
        //if( leader.gotOutfit && leader.gotBag && leader.isExit)
        //{

        //}
    }
    public virtual void MoveToExit()
    {
        transExit.y = myTransform.position.y;
        navMeshAgent.SetDestination(transExit);
        navMeshAgent.stoppingDistance = 0;
        pointTaget = transExit;
    }
    
    public virtual void Exit()
    {

    }
    public virtual void VipState()
    {

    }
    public void ResetStatus()
    {
        emojiPanel.SetActive(false);
        anim = mainModel.GetComponent<Animator>();
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
        isExit = false;
        isAct = false;
        mainModel.SetActive(true);
        outfitType = IngredientType.NONE;
        bagType = IngredientType.NONE;
        waitingTime = consWaitingTime;
        for (int i = 0; i < outfitModel.Length; i++)
        {
            outfitModel[i].SetActive(false);
        }
        for (int i = 0; i < bagModel.Length; i++)
        {
            bagModel[i].SetActive(false);
        }
        //for (int i = 0; i < flag.Length; i++)
        //{
        //    flag[i].SetActive(false);
        //}
        UpdateState(IDLE_STATE);
    }
    private static string ANIM_IDLE_NORMAL = "IdleNormal";
    private static string ANIM_IDLE_COW = "IdleCow";
    private static string ANIM_IDLE_BEAR = "IdleBear";
    private static string ANIM_IDLE_CHICKEN = "IdleChicken";
    private static string ANIM_IDLE_LION = "IdleLion";
    private static string ANIM_IDLE_CROC = "IdleCroc";
    private static string ANIM_IDLE_ELE = "IdleEle";
    private static string ANIM_IDLE_ZEBRA = "IdleZebra";

    private static string ANIM_WALK_NORMAL = "Walk";
    private static string ANIM_WALK_COW = "CowWalk";
    private static string ANIM_WALK_CHICKEN = "ChickenWalk";
    private static string ANIM_WALK_BEAR = "BearWalk";
    private static string ANIM_WALK_LION = "LionWalk";
    private static string ANIM_WALK_CROC = "CrocWalk";
    private static string ANIM_WALK_ELE = "EleWalk";
    private static string ANIM_WALK_ZEBRA = "ZebraWalk";
    public void ChangeAnim()
    {
        //anim = GetComponentInChildren<Animator>();
        //anim.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        if (navMeshAgent.velocity.magnitude < 0.1f)
        {
            if (!gotOutfit)
            {
                anim.Play(ANIM_IDLE_NORMAL);
            }
            else
            {
                switch (this.outfitType)
                {
                    case IngredientType.COW:
                        anim.Play(ANIM_IDLE_COW);
                        break;
                    case IngredientType.CHICKEN:
                        anim.Play(ANIM_IDLE_CHICKEN);
                        break;
                    case IngredientType.BEAR:
                        anim.Play(ANIM_IDLE_BEAR);
                        break;
                    case IngredientType.LION:
                        anim.Play(ANIM_IDLE_LION);
                        break;
                    case IngredientType.CROC:
                        anim.Play(ANIM_IDLE_CROC);
                        break;
                    case IngredientType.ELE:
                        anim.Play(ANIM_IDLE_ELE);
                        break;
                    case IngredientType.ZEBRA:
                        anim.Play(ANIM_IDLE_ZEBRA);
                        break;
                }
            }
        }
        else
        {
            if (!gotOutfit)
            {
                anim.Play(ANIM_WALK_NORMAL);
            }
            else
            {
                switch (this.outfitType)
                {
                    case IngredientType.COW:
                        anim.Play(ANIM_WALK_COW);
                        break;
                    case IngredientType.CHICKEN:
                        anim.Play(ANIM_WALK_CHICKEN);
                        break;
                    case IngredientType.BEAR:
                        anim.Play(ANIM_WALK_BEAR);
                        break;
                    case IngredientType.LION:
                        anim.Play(ANIM_WALK_LION);
                        break;
                    case IngredientType.CROC:
                        anim.Play(ANIM_WALK_CROC);
                        break;
                    case IngredientType.ELE:
                        anim.Play(ANIM_WALK_ELE);
                        break;
                    case IngredientType.ZEBRA:
                        anim.Play(ANIM_WALK_ZEBRA);
                        break;
                }
            }
        }
    }
    public void ChangeOutfit(IngredientType type)
    {
        mainModel.SetActive(false);
        switch (type)
        {
            case IngredientType.COW:
                outfitModel[0].SetActive(true);
                anim = outfitModel[0].GetComponent<Animator>();
                break;
            case IngredientType.SHEEP:
                outfitModel[1].SetActive(true);
                anim = outfitModel[1].GetComponent<Animator>();
                break;
            case IngredientType.CHICKEN:
                outfitModel[2].SetActive(true);
                anim = outfitModel[2].GetComponent<Animator>();
                break;
            case IngredientType.BEAR:
                outfitModel[3].SetActive(true);
                anim = outfitModel[3].GetComponent<Animator>();
                break;
            case IngredientType.LION:
                outfitModel[4].SetActive(true);
                anim = outfitModel[4].GetComponent<Animator>();
                break;
            case IngredientType.CROC:
                outfitModel[5].SetActive(true);
                anim = outfitModel[5].GetComponent<Animator>();
                break;
            case IngredientType.ELE:
                outfitModel[6].SetActive(true);
                anim = outfitModel[6].GetComponent<Animator>();
                break;
            case IngredientType.ZEBRA:
                outfitModel[7].SetActive(true);
                anim = outfitModel[7].GetComponent<Animator>();
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
            case IngredientType.LION:
                bagModel[4].SetActive(true);
                break;
            case IngredientType.CROC:
                bagModel[5].SetActive(true);
                break;
            case IngredientType.ELE:
                bagModel[6].SetActive(true);
                break;
            case IngredientType.ZEBRA:
                bagModel[7].SetActive(true);
                break;
        }
    }
    public void ChangeFlag(IngredientType type)
    {
        //switch (type)
        //{
        //    case IngredientType.COW:
        //        flag[0].SetActive(true);
        //        break;
        //    case IngredientType.SHEEP:
        //        flag[1].SetActive(true);
        //        break;
        //    case IngredientType.CHICKEN:
        //        flag[2].SetActive(true);
        //        break;
        //    case IngredientType.BEAR:
        //        flag[3].SetActive(true);
        //        break;
        //}
    }
    public void StartActing()
    {
        if (!isAct)
        {
            isAct = true;
            int r = Random.Range(4, 7);
            ChangeEmoji(r);
        }
        if (isAct)
        {
            delayAct -= Time.deltaTime;
        }
        if (delayAct < 0)
        {
            delayAct = consDelayAct;
            ChangeEmoji(0);
            isAct = false;
        }

    }
    public void ChangeEmoji(int n)
    {
        if(waitingTime <= 0)
        {
            if (!emojiPanel.activeSelf)
            {
                emojiPanel.SetActive(true);
            }
            if (!listEmojis[1].activeSelf)
            {
                for (int i = 1; i <= listEmojis.Count; i++)
                {
                    if (i != 1)
                    {
                        listEmojis[i - 1].SetActive(false);
                    }
                }
                listEmojis[1].SetActive(true);
            }      
        }
        else
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
                int r = Random.Range(0, 10);
                if (r < 5)
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
                }
                //CounterHelper.Instance.QueueAction(5f, () =>
                //{
                //    ChangeEmoji(0);
                //});
            }
        }   
    }
    public void CountDownWatingTime()
    {
        if (waitingTime < 0)
        {
            return;
        }
        CounterHelper.Instance.QueueAction(1, () =>
        {
            waitingTime--;
            CountDownWatingTime();
        }, 1);
    }
}
