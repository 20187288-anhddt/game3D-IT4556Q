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

    public override void Start()
    {
        base.Start();
        StartInGame();
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
        if (!isLock)
        {
            if (isReadyMission && !isOnMission)
            {
                isReadyMission = false;
                RandomMission();
                CounterHelper.Instance.QueueAction(consDelayMission, () =>
                {
                    RandomCar();
                    car.transform.DOMove(idlePos.position, 3f).OnComplete(() =>
                    {       
                        checkPushCarMission.GetComponent<BoxCollider>().enabled = true;
                        isOnMission = true;
                        StartCountDown();        
                    });
                });
            }
            if (!isReadyMission && isOnMission)
            {
                CheckMission();
            }
        }
    }
    public void CheckMission()
    {
        if (carWaiting > 0)
        {
            if (CheckListMission())
            {
                MissionEnd(true);
            }
        }
        else
        {
            if (!CheckListMission())
            {
                MissionEnd(false);
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
        switch (levelManager.machineManager.allActiveMachine.Count)
        {
            case 4:
            case 5:       
                int r1 = UnityEngine.Random.Range(0, 2);
                if (r1 ==0)
                {
                    GetRandomType(2);
                    AddMission(2);
                }
                else
                {
                    GetRandomType(4);
                    AddMission(4);
                }
                break;
            case 6:
                int r2 = UnityEngine.Random.Range(0, 3);
                if (r2 == 0)
                {
                    GetRandomType(2);
                    AddMission(2);
                }
                else if(r2 == 1)
                {
                    GetRandomType(4);
                    AddMission(4);
                }
                else
                {
                    GetRandomType(6);
                    AddMission(6);
                }
                break;
        }
        foreach(IngredientType key in listMission.Keys)
        {
            int value = listMission[key];
            Debug.Log((key, value));
        }
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
                while (curClothMachine.clothPrefab.ingredientType == listCurClothMachine[i-1].clothPrefab.ingredientType)
                {
                    curClothMachine = levelManager.machineManager.GetRandomTypeClothMachine();
                    if (!listCurClothMachine.Contains(curClothMachine))
                    {
                        listCurClothMachine.Add(curClothMachine);
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
                while (curBagMachine.clothPrefab.ingredientType == listCurBagMachine[i-1].clothPrefab.ingredientType)
                {
                    curBagMachine = levelManager.machineManager.GetRandomTypeBagMachine();
                    if (!listCurBagMachine.Contains(curBagMachine))
                    {
                        listCurBagMachine.Add(curBagMachine);
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
        isOnMission = false;
        StopCoroutine(CountDownCarWait());
        checkPushCarMission.GetComponent<BoxCollider>().enabled = false;
        car.transform.DOMove(startPos.position, 3f).OnComplete(() =>
        {
            ClearMission();
            if (isWin)
            {
                Debug.Log("Win");
                EnableReward();
            }
            else
            {
                Debug.Log("Fail");
            }
        });
    }
    public void EnableReward()
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
        if(carWaiting <= 0 || !isOnMission)
        {
            return;
        }
        CounterHelper.Instance.QueueAction(1, () =>
        {
            carWaiting--;
            StartCountDown();
        });
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
        }
    }
}
