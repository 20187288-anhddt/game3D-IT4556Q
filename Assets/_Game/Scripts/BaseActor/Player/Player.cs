using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : BaseActor,ICollect,IUnlock,IAct
{
    public static Player Instance;
    public GameObject gun;
    [Header("-----Status-----")]
    public bool isUnlock;
    public bool isStopMove;
    public float coinValue;
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

    [SerializeField]
    private float consDelayAct;
    private float delayAct;
    private bool isAct;
    public List<GameObject> listEmojis { get => ListEmojis; set => ListEmojis = value; }
    public List<Shit> listShits { get => ListShits; set => ListShits = value; }
    public float SpeedAnim_Run_InOneSpeed = 0.3f;
    public CharacterController characterController;
    public UI_ProcessPlayer uI_ProcessPlayer_Prefabs;
    private UI_ProcessPlayer uI_ProcessPlayer;
    private Animator anim;
    private float tmpSpeed;
    private bool isBuff = false;
    public GameObject trailSmoke;
    public GameObject fxBuffSpeed;
    public GameObject fxBuffMoney;
    public GameObject emojiPanel;

    public override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        if(characterController == null) { characterController = GetComponent<CharacterController>(); }
        isStopMove = false;
        EnventManager.AddListener(EventName.ReLoadDataUpgrade.ToString(), LoadAndSetData);
        LoadAndSetData();
        ResetPlayer();
    }
    
    private void LoadAndSetData()
    {
        maxCollectObj = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoCapacityTaget().Capacity;
        speed = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoSpeedTaget().Speed;
        if (isBuff)
        {
            DoubleSpeed();
        }
    }
    private void DoubleSpeed()
    {
        isBuff = true;
        fxBuffSpeed.SetActive(true);
        speed *= 1.5f;
    }
    private void ResetSpeed()
    {
        isBuff = false;
        fxBuffSpeed.SetActive(false);
        speed = DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().GetDataPlayer().GetDataBoss().GetInfoSpeedTaget().Speed;
    }
    public void DoubleMoneyBuff()
    {
        fxBuffMoney.SetActive(true);
    }
    public void ResetMoneyBuff()
    {
        fxBuffMoney.SetActive(false);
    }
    protected void Start()
    {
        anim = GetComponentInChildren<Animator>();
        fsm.init(2);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(RUN_STATE, null, OnRunState));
        UpdateState(IDLE_STATE);
        fsm.execute();
        if(gun.activeSelf)
            gun.SetActive(false);
        ReLoadCointValue();
        EnventManager.AddListener(EventName.ReLoadMoney.ToString(), ReLoadCointValue);
        EnventManager.AddListener(EventName.PlayJoystick.ToString(), OnMove);
        EnventManager.AddListener(EventName.StopJoyStick.ToString(), StopMove);
        EnventManager.AddListener(EventName.Player_Double_Speed_Play.ToString(), DoubleSpeed);
        EnventManager.AddListener(EventName.Player_Double_Speed_Stop.ToString(), ResetSpeed);
        tmpSpeed = speed;
    }
    protected void OnMove()
    {
        if (!isUnlock && !isStopMove && EventSystem.current.currentSelectedGameObject == null)
        {
            UpdateState(RUN_STATE);
        }
        else UpdateState(IDLE_STATE);
    }
    protected void StopMove()
    {
        UpdateState(IDLE_STATE);
    }


    protected void Update()
    {
        fsm.execute();
        ChangeAnim();
        StartActing();
        //var rig = GetComponent<Rigidbody>();
        //animSpd = rig.velocity.magnitude;
        //if (Config(GameManager.Instance.joystick.Direction) != Vector2.zero && !isUnlock)
        //{
        //    UpdateState(RUN_STATE);
        //}
        //else UpdateState(IDLE_STATE);
        //if (Input.GetMouseButton(0))
        //{
        //    UpdateMove(speed);
        //}
        #region checkUI_Max
        if (uI_ProcessPlayer != null)
        {
            if (!uI_ProcessPlayer.isActive())
            {
                if (objHave >= maxCollectObj)
                {

                    uI_ProcessPlayer.SetLocalPosition((yOffset + 2) * Vector3.up);
                    uI_ProcessPlayer.Active(true);
                }
            }
            else
            {
                if (objHave < maxCollectObj)
                {
                    uI_ProcessPlayer.Active(false);
                }
            }
        }
        else
        {
            uI_ProcessPlayer = Instantiate(uI_ProcessPlayer_Prefabs);
            uI_ProcessPlayer.myTranform.SetParent(carryPos);
        }
        #endregion
    }
    public void ChangeAnim()
    {
        if (STATE_ACTOR == IDLE_STATE)
        {
            anim.speed = 1;
            if (gun.activeSelf)
            {
                anim.Play("IdleWithGun");
            }
            else
            {
                if (objHave > 0)
                {
                    anim.Play("IdleCarring");
                }
                else
                    anim.Play("IdleNormal");
            }
        }
        else if (STATE_ACTOR == RUN_STATE)
        {
            anim.speed = SpeedAnim_Run_InOneSpeed * speed;
            if (gun.activeSelf)
            {
                anim.Play("WalkWithGun");
            }
            else
            {
                if (objHave > 0)
                {
                    anim.Play("WalkCarring");
                }
                else
                {
                    anim.Play("WalkNormal");
                }
            }

        }
    }

    public Vector2 Config(Vector2 input)
    {
        if (input.magnitude >= 0.09)
        {
            return input;
        }
        return Vector2.zero;
    }
    private FsmSystem.ACTION OnIdleState(FsmSystem _fsm)
    {
        Idle();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnRunState(FsmSystem _fsm)
    {
        UpdateMove(speed);
        return FsmSystem.ACTION.END;
    }
    public virtual void UpdateMove(float speed)
    {
        Canvas_Joystick joystick = Canvas_Joystick.Instance;
        Vector3 inputAxist = joystick.Get_Diraction();
        //Vector3 direction = new Vector3(joystick.Vertical, 0f, -joystick.Horizontal);
        //rig.velocity = new Vector3(inputAxist.x * speed, rig.velocity.y, inputAxist.z * speed);
        characterController.Move(inputAxist * speed * Time.deltaTime);
        if (inputAxist.x != 0 || inputAxist.z != 0)
        {
            Vector3 moveDir = new Vector3(inputAxist.x, 0, inputAxist.z);
            myTransform.rotation = Quaternion.LookRotation(moveDir).normalized;
            myTransform.eulerAngles = new Vector3(myTransform.eulerAngles.x, myTransform.eulerAngles.y + Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
           // transform.Translate(Vector3.forward * speed * 0.02f);
        }
    }
    public virtual void Idle()
    {
        //var rig = GetComponent<Rigidbody>();
        //rig.velocity = Vector3.zero;
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
        if(!allIngredients.Contains(ingredient))
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
            case IngredientType.SHEEP_BAG:
                if (!sheepBags.Contains(ingredient as SheepBag))
                    sheepBags.Add(ingredient as SheepBag);
                break;

            case IngredientType.COW:
                if (!cowFurs.Contains(ingredient as CowFur))
                    cowFurs.Add(ingredient as CowFur);
                break;
            case IngredientType.COW_CLOTH:
                if (!cowCloths.Contains(ingredient as CowCloth))
                    cowCloths.Add(ingredient as CowCloth);
                break;
            case IngredientType.COW_BAG:
                if (!cowBags.Contains(ingredient as CowBag))
                    cowBags.Add(ingredient as CowBag);
                break;

            case IngredientType.CHICKEN:
                if (!chickenFurs.Contains(ingredient as ChickenFur))
                    chickenFurs.Add(ingredient as ChickenFur);
                break;
            case IngredientType.CHICKEN_CLOTH:
                if (!chickenCloths.Contains(ingredient as ChickenCloth))
                    chickenCloths.Add(ingredient as ChickenCloth);
                break;
            case IngredientType.CHICKEN_BAG:
                if (!chickenBags.Contains(ingredient as ChickenBag))
                    chickenBags.Add(ingredient as ChickenBag);
                break;

            case IngredientType.BEAR:
                if (!bearFurs.Contains(ingredient as BearFur))
                    bearFurs.Add(ingredient as BearFur);
                break;
            case IngredientType.BEAR_CLOTH:
                if (!bearCloths.Contains(ingredient as BearCloth))
                    bearCloths.Add(ingredient as BearCloth);
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
    public void UnlockMap(float coin)
    {
        if (coinValue <= 0)
            return;
        if (coinValue > coin)
        {
            coinValue -= coin;
        }
        else
        {
            coinValue = 0;
        }
    }
    public void ReLoadCointValue()
    {
        coinValue = DataManager.Instance.GetDataMoneyController().GetMoney(Money.TypeMoney.USD);
    }
    public void ResetPlayer()
    {
        isAct = false;
        delayAct = consDelayAct;
        objHave = 0;
        foreach(IngredientBase i in allIngredients)
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
        fxBuffSpeed.SetActive(false);
        fxBuffMoney.SetActive(false);
        emojiPanel.SetActive(false);
    }
    public void PlayerStopMove()
    {
        isStopMove = true;
        trailSmoke.SetActive(false);
        Canvas_Joystick.Instance.isStopJoysick = true;
        EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
    }
    public void PlayerContinueMove()
    {
        isStopMove = false;
        trailSmoke.SetActive(true);
        Canvas_Joystick.Instance.isStopJoysick = false;
    }
    public void StartActing()
    {
        if(listShits.Count > 0)
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
        if(n == 0)
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
                    listEmojis[i-1].SetActive(false);
                }
                else
                {
                    if (!listEmojis[i-1].activeSelf)
                    {
                        listEmojis[i-1].SetActive(true);
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
