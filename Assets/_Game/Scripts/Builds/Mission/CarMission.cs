using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;
using Utilities.Components;
public class CarMission : BaseBuild
{
    private static CarMission instance;
    public static CarMission Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<CarMission>();
            }
            return instance;
        }
    }
    public bool IsLock { get => isLock; set => isLock = value; }
    public Transform startPos;
    public Transform idlePos;
    public GameObject[] carModel;
    public GameObject car;
    public GameObject carSmoke;
    public bool isReadyMission;
    public bool isOnMission;
    [SerializeField]
    private float carWaiting;
    [SerializeField]
    private float consCarWaiting;
    [SerializeField]
    private float consDelayMission;
    [SerializeField]
    private CheckPushCarMission checkPushCarMission;
    [SerializeField] private GameObject checkColliPlayer;
    [SerializeField] private GameObject ColliCar;
    public Dictionary<IngredientType, int> listMission;
    public List<ClothMachine> listCurClothMachine;
    public List<BagMachine> listCurBagMachine;
    private Animator anim;
    public int SpeedUpMoney = 200;
    private Canvas_Order canvas_oder;
    public override void Start()
    {
        base.Start();
        StartInGame();
      //  (dataStatusObject as CarDataStatusObject).SetIsOpenOneShot(false);
    }
    public override void StartInGame()
    {
        base.StartInGame();
        carSmoke.gameObject.SetActive(false);
        listMission = new Dictionary<IngredientType, int>();
        listCurClothMachine = new List<ClothMachine>();
        listCurBagMachine = new List<BagMachine>();
        isReadyMission = true;
        isOnMission = false;
        carWaiting = consCarWaiting;
        checkPushCarMission.GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < carModel.Length; i++)
        {
            carModel[i].SetActive(false);
        }
        if (!isLock)
        {
          //  checkPushCarMission.gameObject.SetActive(false);
            car.SetActive(false);
        }
    }
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //checkPushCarMission.GetComponent<BoxCollider>().enabled = true;
        car.SetActive(true);
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this); 
        //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
    }
    public void CallCar()
    {
        RandomCar();
        RandomMission();
        EnventManager.TriggerEvent(EventName.Camera_Follow_PosCar.ToString());
        //if (!(dataStatusObject as CarDataStatusObject).IsOpenOneShot())
        //{
        //    EnventManager.TriggerEvent(EventName.Camera_Follow_PosCar.ToString());
        //    (dataStatusObject as CarDataStatusObject).SetIsOpenOneShot(true);
        //}
        checkColliPlayer.SetActive(true);
        ColliCar.SetActive(true);
        car.transform.DOMove(idlePos.position, 3f).OnComplete(() =>
        {
            anim.Play("Open");
            AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[8], 1, false);
            carSmoke.gameObject.SetActive(true);
            isOnMission = true;
            checkColliPlayer.SetActive(false);
            StartCountDown();
            checkPushCarMission.GetComponent<BoxCollider>().enabled = true;
            if (Canvas_Home.Instance != null)
            {
                if (!Canvas_Home.Instance.IsShow_Btn_Oder())
                {
                    Canvas_Home.Instance.Show_Btn_Oder(() =>
                    {
                        if (!checkPushCarMission.GetisInItDataUI())
                        {
                            //InItDataMissionCurrent();
                            //checkPushCarMission.SetisInItDataUI(true);
                            checkPushCarMission.OnTriggerEnter(Player.Instance.GetComponent<CharacterController>());
                            UpdateMission();
                        }
                    });
                }

            }
        });
    }
    void Update()
    {
        if (!isLock && levelManager.machineManager.allActiveClothMachine.Count > 0 && levelManager.machineManager.allActiveBagMachine.Count > 0)
        {
            if (isReadyMission && !isOnMission)
            {
                isReadyMission = false;
                // RandomMission();
                Debug.Log("callcar" + consDelayMission);
                CounterHelper.Instance.QueueAction(consDelayMission, () =>
                {
                    CallCar();
                },1);
               

            }
            if (!isReadyMission && isOnMission)
            {
                CheckMission();
            }
        }
    }
    public void CheckMission()
    {
        if (carWaiting <= 0)
        {
            if (!CheckListMission())
            {
                MissionEnd(false);
            }
            else
            {
                MissionEnd(true);
            }
        }
    }
    public void RandomCar()
    {
       
        int r = UnityEngine.Random.Range(0, carModel.Length);
        for (int i = 0; i < carModel.Length; i++)
        {
            if (i == r)
            {

                carModel[i].SetActive(true);
                anim = carModel[i].GetComponent<Animator>();
            }
            else
            {
                carModel[i].SetActive(false);
            }
        }
    }
    public void RandomMission()
    {
        int r1 = UnityEngine.Random.Range(0, 2);
        if (r1 == 0)
        {
            GetRandomType(2);
            AddMission(2);
        }
        else
        {
            GetRandomType(4);
            AddMission(4);
        }
        //switch (levelManager.machineManager.allActiveMachine.Count)
        //{
        //    case 4:
        //    case 5:       
        //        int r1 = UnityEngine.Random.Range(0, 2);
        //        if (r1 ==0)
        //        {
        //            GetRandomType(2);
        //            AddMission(2);
        //        }
        //        else
        //        {
        //            GetRandomType(4);
        //            AddMission(4);
        //        }
        //        break;
        //    case 6:
        //        int r2 = UnityEngine.Random.Range(0, 3);
        //        if (r2 == 0)
        //        {
        //            GetRandomType(2);
        //            AddMission(2);
        //        }
        //        else if(r2 == 1)
        //        {
        //            GetRandomType(4);
        //            AddMission(4);
        //        }
        //        else
        //        {
        //            GetRandomType(6);
        //            AddMission(6);
        //        }
        //        break;
        //}
        foreach(IngredientType key in listMission.Keys)
        {
            int value = listMission[key];
           // Debug.Log((key, value));
        }
        checkPushCarMission.SetisInItDataUI(false);
    }
    public void GetRandomType(int n)
    {
        for(int i = 0; i < n / 2; i++)
        {
            ClothMachine curClothMachine = levelManager.machineManager.GetRandomTypeClothMachine();
            if (!listCurClothMachine.Contains(curClothMachine))
            {
                listCurClothMachine.Add(curClothMachine);
            }
            else
            {
                curClothMachine = null;
                while (curClothMachine == null)
                {
                    curClothMachine = levelManager.machineManager.GetRandomTypeClothMachine();
                    if (!listCurClothMachine.Contains(curClothMachine))
                    {
                        listCurClothMachine.Add(curClothMachine);
                    }
                    else
                    {
                        curClothMachine = null;
                    }
                }
            }
        }
        for (int i = 0; i < n / 2; i++)
        {
            BagMachine curBagMachine = levelManager.machineManager.GetRandomTypeBagMachine();
            if (!listCurBagMachine.Contains(curBagMachine))
            {
                listCurBagMachine.Add(curBagMachine);
            }
            else
            {
                curBagMachine = null;
                while (curBagMachine == null)
                {
                    curBagMachine = levelManager.machineManager.GetRandomTypeBagMachine();
                    if (!listCurBagMachine.Contains(curBagMachine))
                    {
                        listCurBagMachine.Add(curBagMachine);
                    }
                    else
                    {
                        curBagMachine = null;
                    }
                }
            }
        }
    }
    
    public void AddMission(int n)
    {
        for(int i = 0; i < n / 2; i++)
        {
            int ranNumCloth = UnityEngine.Random.Range(1, 6);
            int ranNumBag = UnityEngine.Random.Range(1, 6);
            listMission.Add(listCurClothMachine[i].clothPrefab.ingredientType, ranNumCloth);
            listMission.Add(listCurBagMachine[i].clothPrefab.ingredientType, ranNumBag);
        }
    }
    public bool CheckListMission()
    {
        bool isWin = true;
        for (int i = 0; i < listMission.Keys.Count; i++)
        {
            if(listMission.ElementAt(i).Value > 0)
            {
                isWin = false;
                break;
            }
        }
        return isWin;
    }
    public void MissionEnd(bool isWin)
    {
        anim.SetTrigger("Close");
        AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[9], 1, false);
        isOnMission = false;
        ColliCar.SetActive(false);
        //StopCoroutine(CountDownCarWait());
        Canvas_Home.Instance.NotShow_Btn_Oder();
        checkPushCarMission.GetComponent<BoxCollider>().enabled = false;
        UI_Manager.Instance.CloseUI(NameUI.Canvas_Order);
       // checkPushCarMission.GetComponent<>().SetActive(true);
        checkColliPlayer.gameObject.SetActive(false);
        car.transform.DOMove(startPos.position, 3f).OnComplete(() =>
        {
            //checkColliPlayer.SetActive(false);
            carSmoke.gameObject.SetActive(false);
            ClearMission();
            if (isWin)
            {
                AddReward();
            }
        });
    }
    public void AddReward()
    {

    }
    public void ClearMission()
    {
      //  checkPushCarMission.GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < carModel.Length; i++)
        {
            carModel[i].SetActive(false);
        }
        listMission.Clear();
        listCurClothMachine.Clear();
        listCurBagMachine.Clear();
        carWaiting = consCarWaiting;
        isReadyMission = true;
    }
    IEnumerator DelayMission(float time,Action spawnMission)
    {
        yield return new WaitForSeconds(time);
        spawnMission();
    }
    IEnumerator CountDownCarWait()
    {
        while (carWaiting > 0)
        {
            yield return new WaitForSeconds(1);
            carWaiting--;
        }
       
    }
    public void StartCountDown()
    {
        if(carWaiting < 0 || !isOnMission)
        {
            return;
        }
        else if(carWaiting >= 0 && isOnMission)
        {
            carWaiting--;
            if (Canvas_Home.Instance != null)
            {
                if (UI_Manager.Instance.isOpenUI(NameUI.Canvas_Order))
                {
                    if (canvas_oder == null)
                    {
                        canvas_oder = (UI_Manager.Instance.OpenUI(NameUI.Canvas_Order) as Canvas_Order);
                    }
                    canvas_oder.LoadTime((int)carWaiting);
                }
                Canvas_Home.Instance.LoadTextTimeOder((int)carWaiting);
            }

        }
        CounterHelper.Instance.QueueAction(1f, () =>
        {
            StartCountDown();
        },1);
    }
    public void UpdateMission()
    {
        if (UI_Manager.Instance.isOpenUI(NameUI.Canvas_Order))
        {
            if (canvas_oder == null)
            {
                 canvas_oder = (UI_Manager.Instance.OpenUI(NameUI.Canvas_Order) as Canvas_Order);
            }
            // Canvas_Order canvas_Order = UI_Manager.Instance.OpenUI(NameUI.Canvas_Order) as Canvas_Order;
            foreach (IngredientType key in listMission.Keys)
            {
                int value = listMission[key];
                canvas_oder.ShowItem(key, value);
               // Debug.Log(key + "---" + value);
            }
            canvas_oder.SetCompleteAllMission(CheckCompleteAll());
        }
      
    }
    private bool CheckCompleteAll()
    {
        bool isComplete = true;
        int value = 0;
        foreach (IngredientType key in listMission.Keys)
        {
            value = listMission[key];
            if(value != 0)
            {
                return isComplete = false;
            }
        }
        return isComplete;
    }
    public int GetRewardMoneyAllMission()
    {
        int value = 0;
       // int count = 1;
        foreach (IngredientType key in listMission.Keys)
        {
            // count = listMission[key];
            if (canvas_oder == null)
            {
                canvas_oder = (UI_Manager.Instance.GetUI(NameUI.Canvas_Order) as Canvas_Order);
            }
           // canvas_Order = UI_Manager.Instance.OpenUI(NameUI.Canvas_Order) as Canvas_Order;
            switch (key)
            {
                //case IngredientType.SHEEP_CLOTH:
                //    value += GameManager.Instance.dataPrice.Data.
                //    break;
                //case IngredientType.COW_BAG:
                //    value += GameManager.Instance.dataPrice.Data.cowBag
                //    break;
                case IngredientType.BEAR_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.bearOutfit *
                     canvas_oder.GetValueMax_Mission(IngredientType.BEAR_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.CHICKEN_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.chickenOutfit *
                      canvas_oder.GetValueMax_Mission(IngredientType.CHICKEN_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.COW_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.cowOutfit *
                    canvas_oder.GetValueMax_Mission(IngredientType.COW_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.LION_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.lionOutfit *
                    canvas_oder.GetValueMax_Mission(IngredientType.LION_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.CROC_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.crocOutfit *
                      canvas_oder.GetValueMax_Mission(IngredientType.CROC_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.ELE_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.eleOutfit *
                    canvas_oder.GetValueMax_Mission(IngredientType.ELE_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.ZEBRA_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.zebraOutfit *
                    canvas_oder.GetValueMax_Mission(IngredientType.ZEBRA_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.CHICKEN_BAG:
                    value += (GameManager.Instance.dataPrice.Data.chickenBag *
                       canvas_oder.GetValueMax_Mission(IngredientType.CHICKEN_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.COW_BAG:
                    value += (GameManager.Instance.dataPrice.Data.cowBag *
                       canvas_oder.GetValueMax_Mission(IngredientType.COW_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.BEAR_BAG:
                    value += (GameManager.Instance.dataPrice.Data.bearBag *
                     canvas_oder.GetValueMax_Mission(IngredientType.BEAR_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.LION_BAG:
                    value += (GameManager.Instance.dataPrice.Data.lionBag *
                       canvas_oder.GetValueMax_Mission(IngredientType.LION_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.CROC_BAG:
                    value += (GameManager.Instance.dataPrice.Data.crocBag *
                       canvas_oder.GetValueMax_Mission(IngredientType.CROC_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.ELE_BAG:
                    value += (GameManager.Instance.dataPrice.Data.eleBag *
                      canvas_oder.GetValueMax_Mission(IngredientType.ELE_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.ZEBRA_BAG:
                    value += (GameManager.Instance.dataPrice.Data.zebraBag *
                      canvas_oder.GetValueMax_Mission(IngredientType.ZEBRA_BAG)) * SpeedUpMoney;
                    break;
            }
        }
       // Debug.Log(value);
        return value;
    }
    public void InItDataMissionCurrent()
    {
        Debug.Log("InItCar");
        if(canvas_oder == null)
        {
            canvas_oder = (UI_Manager.Instance.OpenUI(NameUI.Canvas_Order) as Canvas_Order);
        }
       
        // Debug.Log(canvas_oder);
       // Debug.Log("Clear -------------------------------------------------------------");
        canvas_oder.CloseAllItem(); ;
       // Canvas_Order.Instane.CloseAllItem();
        foreach (IngredientType key in listMission.Keys)
        {
            int value = listMission[key];
            canvas_oder.InItData(value, key);
        }
        canvas_oder.LoadTime((int)carWaiting);
        canvas_oder.SetMoneyCurrent(GetRewardMoneyAllMission());
        canvas_oder.SetActionCollect(() => { MissionEnd(true); });
    }
    public void ReduceType(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.COW_CLOTH:
                if (listMission.ContainsKey(IngredientType.COW_CLOTH))
                {
                    if (listMission[IngredientType.COW_CLOTH] > 0)
                    {
                        listMission[IngredientType.COW_CLOTH]--;
                        //if (listMission[IngredientType.COW_CLOTH] == 0)
                        //{
                        //    listMission.Remove(IngredientType.COW_CLOTH);
                        //}
                    }
                }
                break;
            case IngredientType.CHICKEN_CLOTH:
                if (listMission.ContainsKey(IngredientType.CHICKEN_CLOTH))
                {
                    if (listMission[IngredientType.CHICKEN_CLOTH] > 0)
                    {
                        listMission[IngredientType.CHICKEN_CLOTH]--;
                        //if(listMission[IngredientType.CHICKEN_CLOTH] == 0)
                        //{
                        //    listMission.Remove(IngredientType.CHICKEN_CLOTH);
                        //}
                    }
                }
                break;
            case IngredientType.BEAR_CLOTH:
                if (listMission.ContainsKey(IngredientType.BEAR_CLOTH))
                {
                    if (listMission[IngredientType.BEAR_CLOTH] > 0)
                    {
                        listMission[IngredientType.BEAR_CLOTH]--;
                        //if (listMission[IngredientType.BEAR_CLOTH] == 0)
                        //{
                        //    listMission.Remove(IngredientType.BEAR_CLOTH);
                        //}
                    }
                }
                break;
            case IngredientType.LION_CLOTH:
                if (listMission.ContainsKey(IngredientType.LION_CLOTH))
                {
                    if (listMission[IngredientType.LION_CLOTH] > 0)
                    {
                        listMission[IngredientType.LION_CLOTH]--;
                        //if (listMission[IngredientType.COW_CLOTH] == 0)
                        //{
                        //    listMission.Remove(IngredientType.COW_CLOTH);
                        //}
                    }
                }
                break;
            case IngredientType.CROC_CLOTH:
                if (listMission.ContainsKey(IngredientType.CROC_CLOTH))
                {
                    if (listMission[IngredientType.CROC_CLOTH] > 0)
                    {
                        listMission[IngredientType.CROC_CLOTH]--;
                        //if(listMission[IngredientType.CHICKEN_CLOTH] == 0)
                        //{
                        //    listMission.Remove(IngredientType.CHICKEN_CLOTH);
                        //}
                    }
                }
                break;
            case IngredientType.ELE_CLOTH:
                if (listMission.ContainsKey(IngredientType.ELE_CLOTH))
                {
                    if (listMission[IngredientType.ELE_CLOTH] > 0)
                    {
                        listMission[IngredientType.ELE_CLOTH]--;
                        //if (listMission[IngredientType.BEAR_CLOTH] == 0)
                        //{
                        //    listMission.Remove(IngredientType.BEAR_CLOTH);
                        //}
                    }
                }
                break;
            case IngredientType.ZEBRA_CLOTH:
                if (listMission.ContainsKey(IngredientType.ZEBRA_CLOTH))
                {
                    if (listMission[IngredientType.ZEBRA_CLOTH] > 0)
                    {
                        listMission[IngredientType.ZEBRA_CLOTH]--;
                        //if (listMission[IngredientType.BEAR_CLOTH] == 0)
                        //{
                        //    listMission.Remove(IngredientType.BEAR_CLOTH);
                        //}
                    }
                }
                break;
            case IngredientType.COW_BAG:
                if (listMission.ContainsKey(IngredientType.COW_BAG))
                {
                    if (listMission[IngredientType.COW_BAG] > 0)
                    {
                        listMission[IngredientType.COW_BAG]--;
                        //if (listMission[IngredientType.COW_BAG] == 0)
                        //{
                        //    listMission.Remove(IngredientType.COW_BAG);
                        //}
                    }
                }
                break;
            case IngredientType.CHICKEN_BAG:
                if (listMission.ContainsKey(IngredientType.CHICKEN_BAG))
                {
                    if (listMission[IngredientType.CHICKEN_BAG] > 0)
                    {
                        listMission[IngredientType.CHICKEN_BAG]--;
                        //if (listMission[IngredientType.CHICKEN_BAG] == 0)
                        //{
                        //    listMission.Remove(IngredientType.CHICKEN_BAG);
                        //}
                    }
                }
                break;
            case IngredientType.BEAR_BAG:
                if (listMission.ContainsKey(IngredientType.BEAR_BAG))
                {
                    if (listMission[IngredientType.BEAR_BAG] > 0)
                    {
                        listMission[IngredientType.BEAR_BAG]--;
                        //if (listMission[IngredientType.BEAR_BAG] == 0)
                        //{
                        //    listMission.Remove(IngredientType.BEAR_BAG);
                        //}
                    }
                }
                break;
            case IngredientType.LION_BAG:
                if (listMission.ContainsKey(IngredientType.LION_BAG))
                {
                    if (listMission[IngredientType.LION_BAG] > 0)
                    {
                        listMission[IngredientType.LION_BAG]--;
                        //if (listMission[IngredientType.COW_BAG] == 0)
                        //{
                        //    listMission.Remove(IngredientType.COW_BAG);
                        //}
                    }
                }
                break;
            case IngredientType.CROC_BAG:
                if (listMission.ContainsKey(IngredientType.CROC_BAG))
                {
                    if (listMission[IngredientType.CROC_BAG] > 0)
                    {
                        listMission[IngredientType.CROC_BAG]--;
                        //if (listMission[IngredientType.CHICKEN_BAG] == 0)
                        //{
                        //    listMission.Remove(IngredientType.CHICKEN_BAG);
                        //}
                    }
                }
                break;
            case IngredientType.ELE_BAG:
                if (listMission.ContainsKey(IngredientType.ELE_BAG))
                {
                    if (listMission[IngredientType.ELE_BAG] > 0)
                    {
                        listMission[IngredientType.ELE_BAG]--;
                        //if (listMission[IngredientType.BEAR_BAG] == 0)
                        //{
                        //    listMission.Remove(IngredientType.BEAR_BAG);
                        //}
                    }
                }
                break;
            case IngredientType.ZEBRA_BAG:
                if (listMission.ContainsKey(IngredientType.ZEBRA_BAG))
                {
                    if (listMission[IngredientType.ZEBRA_BAG] > 0)
                    {
                        listMission[IngredientType.ZEBRA_BAG]--;
                        //if (listMission[IngredientType.BEAR_BAG] == 0)
                        //{
                        //    listMission.Remove(IngredientType.BEAR_BAG);
                        //}
                    }
                }
                break;
        }
        UpdateMission();
    }
}
