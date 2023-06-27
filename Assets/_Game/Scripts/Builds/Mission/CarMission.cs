using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CarMission : BaseBuild
{
    public bool IsLock { get => isLock; set => isLock = value; }
    public Transform startPos;
    public Transform idlePos;
    public GameObject[] carModel;
    public GameObject car;
    public bool isReadyMission;
    public bool isOnMission;
    public bool isDoneMission;
    [SerializeField]
    private float carWaiting;
    [SerializeField]
    private float consCarWaiting;
    [SerializeField]
    private float consDelayMission;
    [SerializeField]
    private CheckPushCarMission checkPushCarMission;
    public Dictionary<IngredientType, int> listMission;
    public List<IngredientType> listCurClothType;
    public List<IngredientType> listCurBagType;
    public int ranNumCloth;
    public int ranNumBag;
    public GameManager gameManager;
    public MachineManager machineManager;

    public override void Start()
    {
        base.Start();
        machineManager = gameManager.listLevelManagers[gameManager.curLevel].machineManager;
    }
    public override void StartInGame()
    {
        base.StartInGame();
        listMission = new Dictionary<IngredientType, int>();
        listCurClothType = new List<IngredientType>();
        listCurBagType = new List<IngredientType>();
        isReadyMission = true;
        isOnMission = false;
        isDoneMission = false;
        carWaiting = consCarWaiting;     
    }
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this); 
        //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
    }
    void Update()
    {
        if (isReadyMission && !isOnMission && !isDoneMission)
        {
            isReadyMission = false;
            CounterHelper.Instance.QueueAction(consDelayMission, () =>
            {
                RandomCar();
                car.transform.DOMove(idlePos.position, 1f).OnComplete(() =>
                {
                    isOnMission = true;
                    RandomMission();
                    checkPushCarMission.GetComponent<BoxCollider>().enabled = true;
                    StartCoroutine(CountDownCarWait());
                });
            });
            //StartCoroutine(DelayMission(consDelayMission, () =>
            //{
            //    RandomCar();
            //    car.transform.DOMove(idlePos.position, 1f).OnComplete(() =>
            //    {
            //        isOnMission = true;
            //        RandomMission();
            //        GetComponent<BoxCollider>().enabled = true;
            //    });
            //}));  
        }
        if (!isReadyMission && isOnMission && !isDoneMission)
        {
            CheckMission();
        }
    }
    public void CheckMission()
    {
        if(carWaiting > 0)
        {
            if(listMission.Count == 0)
            {
                MissionEnd(true);
            }
        }
        else
        {
            if(listMission.Count > 0)
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
        switch (machineManager.allActiveMachine.Count)
        {
            case 5:
            case 4:
                float r1 = UnityEngine.Random.Range(0, 1);
                if (r1 < 0.5f)
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
                float r2 = UnityEngine.Random.Range(0, 1);
                if (r2 < 0.3f)
                {
                    GetRandomType(2);
                    AddMission(2);
                }
                else if(0.3f < r2 && r2 < 0.6f)
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
    }
    public void GetRandomType(int n)
    {
        for(int i = 0; i < n / 2; i++)
        {
            listCurClothType[i] = machineManager.GetRandomTypeClothMachine();
            while( i>0 && listCurClothType[i] == listCurClothType[i - 1])
            {
                listCurClothType[i] = machineManager.GetRandomTypeClothMachine();
            }
        }
        for (int j = 0; j < n / 2; j++)
        {
            listCurBagType[j] = machineManager.GetRandomTypeBagMachine();
            while (j > 0 && listCurBagType[j] == listCurBagType[j - 1])
            {
                listCurBagType[j] = machineManager.GetRandomTypeBagMachine();
            }
        }
    }
    
    public void AddMission(int n)
    {
        for(int i = 0; i < n / 2; i++)
        {
            int ranNumCloth = UnityEngine.Random.Range(1, 5);
            int ranNumBag = UnityEngine.Random.Range(1, 5);
            listMission.Add(listCurClothType[i], ranNumCloth);
            listMission.Add(listCurBagType[i], ranNumBag);
        }
    }
    public void MissionEnd(bool isWin)
    {
        isOnMission = false;
        isDoneMission = true;
        StopCoroutine(CountDownCarWait());
        checkPushCarMission.GetComponent<BoxCollider>().enabled = false;
        car.transform.DOMove(startPos.position, 1f).OnComplete(() =>
        {
            ClearMission();
            if (isWin)
            {
                EnableReward();
            }    
        });
    }
    public void EnableReward()
    {

    }
    public void ClearMission()
    {
        GetComponent<BoxCollider>().enabled = false;
        listMission.Clear();
        listCurClothType.Clear();
        listCurBagType.Clear();
        carWaiting = consCarWaiting;
        isReadyMission = true;
        isDoneMission = false;
    }
    IEnumerator DelayMission(float time,Action spawnMission)
    {
        yield return new WaitForSeconds(time);
        spawnMission();
    }
    IEnumerator CountDownCarWait()
    {
        yield return new WaitForSeconds(1);
        carWaiting--;
    }
    public void ReduceType(IngredientType type)
    {
        switch (type)
        {
            case IngredientType.COW_CLOTH:
                int numCowCloth;
                if (listMission.TryGetValue(IngredientType.COW_CLOTH, out numCowCloth))
                {
                    if (numCowCloth > 0)
                    {
                        listMission[IngredientType.COW_CLOTH]--;
                        if (listMission[IngredientType.COW_CLOTH] == 0)
                        {
                            listMission.Remove(IngredientType.COW_CLOTH);
                        }
                    }
                }
                break;
            case IngredientType.CHICKEN_CLOTH:
                int numChickenCloth;
                if (listMission.TryGetValue(IngredientType.CHICKEN_CLOTH, out numChickenCloth))
                {
                    if (numChickenCloth > 0)
                    {
                        listMission[IngredientType.CHICKEN_CLOTH]--;
                        if(listMission[IngredientType.CHICKEN_CLOTH] == 0)
                        {
                            listMission.Remove(IngredientType.CHICKEN_CLOTH);
                        }
                    }
                }
                break;
            case IngredientType.BEAR_CLOTH:
                int numBearCloth;
                if (listMission.TryGetValue(IngredientType.BEAR_CLOTH, out numBearCloth))
                {
                    if (numBearCloth > 0)
                    {
                        listMission[IngredientType.BEAR_CLOTH]--;
                        if (listMission[IngredientType.BEAR_CLOTH] == 0)
                        {
                            listMission.Remove(IngredientType.BEAR_CLOTH);
                        }
                    }
                }
                break;
            case IngredientType.COW_BAG:
                int numCowBag;
                if (listMission.TryGetValue(IngredientType.COW_BAG, out numCowBag))
                {
                    if (numCowBag > 0)
                    {
                        listMission[IngredientType.COW_BAG]--;
                        if (listMission[IngredientType.COW_BAG] == 0)
                        {
                            listMission.Remove(IngredientType.COW_BAG);
                        }
                    }
                }
                break;
            case IngredientType.CHICKEN_BAG:
                int numChickenBag;
                if (listMission.TryGetValue(IngredientType.CHICKEN_BAG, out numChickenBag))
                {
                    if (numChickenBag > 0)
                    {
                        listMission[IngredientType.CHICKEN_BAG]--;
                        if (listMission[IngredientType.CHICKEN_BAG] == 0)
                        {
                            listMission.Remove(IngredientType.CHICKEN_BAG);
                        }
                    }
                }
                break;
            case IngredientType.BEAR_BAG:
                int numBearBag;
                if (listMission.TryGetValue(IngredientType.BEAR_BAG, out numBearBag))
                {
                    if (numBearBag > 0)
                    {
                        listMission[IngredientType.BEAR_BAG]--;
                        if (listMission[IngredientType.BEAR_BAG] == 0)
                        {
                            listMission.Remove(IngredientType.BEAR_BAG);
                        }
                    }
                }
                break;
        }
    }
}
