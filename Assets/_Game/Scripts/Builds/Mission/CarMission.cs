using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

public class CarMission : BaseBuild
{
    public bool IsLock { get => isLock; set => isLock = value; }
    public Transform startPos;
    public Transform idlePos;
    public GameObject[] carModel;
    public GameObject car;
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
    public Dictionary<IngredientType, int> listMission;
    public List<ClothMachine> listCurClothMachine;
    public List<BagMachine> listCurBagMachine;
    public int SpeedUpMoney = 100;
    public override void Start()
    {
        base.Start();
        StartInGame();
      //  (dataStatusObject as CarDataStatusObject).SetIsOpenOneShot(false);
    }
    public override void StartInGame()
    {
        base.StartInGame();
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
            checkPushCarMission.gameObject.SetActive(false);
            car.SetActive(false);
        }
    }
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        checkPushCarMission.gameObject.SetActive(true);
        car.SetActive(true);
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this); 
        //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
    }
    void Update()
    {
        if (!isLock && levelManager.machineManager.allActiveClothMachine.Count > 0 && levelManager.machineManager.allActiveBagMachine.Count > 0)
        {
            if (isReadyMission && !isOnMission)
            {
                isReadyMission = false;
                RandomMission();
                CounterHelper.Instance.QueueAction(consDelayMission, () =>
                {
                    RandomCar();
                    EnventManager.TriggerEvent(EventName.Camera_Follow_PosCar.ToString());
                    //if (!(dataStatusObject as CarDataStatusObject).IsOpenOneShot())
                    //{
                    //    EnventManager.TriggerEvent(EventName.Camera_Follow_PosCar.ToString());
                    //    (dataStatusObject as CarDataStatusObject).SetIsOpenOneShot(true);
                    //}
                    car.transform.DOMove(idlePos.position, 3f).OnComplete(() =>
                    {
                        isOnMission = true;
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
                                        InItDataMissionCurrent();
                                        checkPushCarMission.SetisInItDataUI(true);
                                        checkPushCarMission.OnTriggerStay(Player.Instance.GetComponent<CharacterController>());
                                    }
                                });
                            }

                        }
                    });

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
            Debug.Log((key, value));
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
        Debug.Log("End");
        isOnMission = false;
        //StopCoroutine(CountDownCarWait());
        Canvas_Home.Instance.NotShow_Btn_Oder();
        checkPushCarMission.GetComponent<BoxCollider>().enabled = false;
        UI_Manager.Instance.CloseUI(NameUI.Canvas_Order);
        car.transform.DOMove(startPos.position, 3f).OnComplete(() =>
        {
            ClearMission();
            if (isWin)
            {
                Debug.Log("Win");
                AddReward();
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    public void AddReward()
    {

    }
    public void ClearMission()
    {
        checkPushCarMission.GetComponent<BoxCollider>().enabled = false;
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
                    Canvas_Order.Instane.LoadTime((int)carWaiting);
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
            foreach (IngredientType key in listMission.Keys)
            {
                int value = listMission[key];
                Canvas_Order.Instane.ShowItem(key, value);
               // Debug.Log(key + "---" + value);
            }
            Canvas_Order.Instane.SetCompleteAllMission(CheckCompleteAll());
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
                     Canvas_Order.Instane.GetValueMax_Mission(IngredientType.BEAR_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.CHICKEN_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.chickenOutfit *
                      Canvas_Order.Instane.GetValueMax_Mission(IngredientType.CHICKEN_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.COW_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.cowOutfit *
                    Canvas_Order.Instane.GetValueMax_Mission(IngredientType.COW_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.LION_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.lionOutfit *
                     Canvas_Order.Instane.GetValueMax_Mission(IngredientType.LION_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.CROC_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.crocOutfit *
                      Canvas_Order.Instane.GetValueMax_Mission(IngredientType.CROC_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.ELE_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.eleOutfit *
                    Canvas_Order.Instane.GetValueMax_Mission(IngredientType.ELE_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.ZEBRA_CLOTH:
                    value += (GameManager.Instance.dataPrice.Data.zebraOutfit *
                    Canvas_Order.Instane.GetValueMax_Mission(IngredientType.ZEBRA_CLOTH)) * SpeedUpMoney;
                    break;
                case IngredientType.CHICKEN_BAG:
                    value += (GameManager.Instance.dataPrice.Data.chickenBag *
                       Canvas_Order.Instane.GetValueMax_Mission(IngredientType.CHICKEN_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.COW_BAG:
                    value += (GameManager.Instance.dataPrice.Data.cowBag *
                       Canvas_Order.Instane.GetValueMax_Mission(IngredientType.COW_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.BEAR_BAG:
                    value += (GameManager.Instance.dataPrice.Data.bearBag *
                      Canvas_Order.Instane.GetValueMax_Mission(IngredientType.BEAR_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.LION_BAG:
                    value += (GameManager.Instance.dataPrice.Data.lionBag *
                       Canvas_Order.Instane.GetValueMax_Mission(IngredientType.LION_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.CROC_BAG:
                    value += (GameManager.Instance.dataPrice.Data.crocBag *
                       Canvas_Order.Instane.GetValueMax_Mission(IngredientType.CROC_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.ELE_BAG:
                    value += (GameManager.Instance.dataPrice.Data.eleBag *
                      Canvas_Order.Instane.GetValueMax_Mission(IngredientType.ELE_BAG)) * SpeedUpMoney;
                    break;
                case IngredientType.ZEBRA_BAG:
                    value += (GameManager.Instance.dataPrice.Data.zebraBag *
                      Canvas_Order.Instane.GetValueMax_Mission(IngredientType.ZEBRA_BAG)) * SpeedUpMoney;
                    break;
            }
        }
        Debug.Log(value);
        return value;
    }
    public void InItDataMissionCurrent()
    {
        Debug.Log("InItCar");
        UI_Manager.Instance.OpenUI(NameUI.Canvas_Order);
        Canvas_Order.Instane.CloseAllItem();
        foreach (IngredientType key in listMission.Keys)
        {
            int value = listMission[key];
            Canvas_Order.Instane.InItData(value, key);
        }
        Canvas_Order.Instane.LoadTime((int)carWaiting);
        Canvas_Order.Instane.SetMoneyCurrent(GetRewardMoneyAllMission());
        Canvas_Order.Instane.SetActionCollect(() => { MissionEnd(true); });
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
