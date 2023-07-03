using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Habitat : BuildObj, ILock
{
    public List<AnimalBase> allAnimals;
    public List<AnimalBase> animalsIsReady;
    public List<Shit> listShit;
    public IngredientBase ingredientPrefabs;
    public Shit shitPrefabs;
    [SerializeField]
    private GameObject unlockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    public CheckCollect checkCollect;
    public Transform[] defaultAnimalPos;
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public Transform staffPos;
    public Transform animalPos;
    public bool isHaveStaff;
    public bool isReadyShit;
    [SerializeField]
    private float delayShit;
    [SerializeField]
    private float consDelayShit;
    [SerializeField]
    private int maxShit;
    private Vector3 randomShitPos;
    public int numShitSave;
    [SerializeField]
    private AnimalBase animalPrefabs;
    public int numAnimalSave;
    public bool isBuff_NoShit;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
        if (!IsLock)
        {
            return;
        }
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
       
        p.isUnlock = true;
      //  EnventManager.TriggerEvent(EventName.StopJoyStick.ToString());
        unlockModel.SetActive(true);
        unlockFx.SetActive(true);
      
        //lockModel.SetActive(false);
        //switch (habitatType)
        //{
        //    case IngredientType.BEAR:
        //        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) > 2f)
        //        {
        //            p.transform.position = checkUnlock.transform.position - Vector3.forward * 8;
        //        }
        //        break;
        //    case IngredientType.COW:
        //        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 2f)
        //        {
        //            p.transform.position = checkUnlock.transform.position - Vector3.forward * 8;
        //        }
        //        break;
        //    case IngredientType.CHICKEN:
        //        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 2f)
        //        {
        //            p.transform.position = checkUnlock.transform.position - Vector3.forward * 8;
        //        }
        //        break;
        //    case IngredientType.SHEEP:
        //        if (Vector3.Distance(new Vector3(unlockModel.transform.position.x, 0, unlockModel.transform.position.z), new Vector3(p.transform.position.x, 0, p.transform.position.z)) < 2f)
        //        {
        //            p.transform.position = checkUnlock.transform.position - Vector3.forward * 8;
        //        }
        //        break;
        //}
        // numAnimalSave = 3;
        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.isUnlock = false;
                        unlockFx.SetActive(false);
                        //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        if (!levelManager.isDoneMachineTUT)
                        {
                            checkCollect.gameObject.SetActive(false);
                        }
                        else
                        {
                            checkCollect.gameObject.SetActive(true);
                        } 
                        CounterHelper.Instance.QueueAction(consDelayShit, () =>
                        {
                            isReadyShit = true;
                        },1);
                        SpawnAnimalOnStart(numAnimalSave);
                        if (numShitSave > 0)
                        {
                            TakeAShitOnStart(numShitSave);
                        }
                        
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
          //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
            checkCollect.gameObject.SetActive(true);
            CounterHelper.Instance.QueueAction(consDelayShit, () =>
            {
                isReadyShit = true;
            },1);
        }
       
        checkUnlock.gameObject.SetActive(false);
        if (!levelManager.habitatManager.allActiveHabitats.Contains(this))
        {
            levelManager.habitatManager.allActiveHabitats.Add(this);
        }
         
        //GetComponent<BoxCollider>().enabled = true;
        switch (ingredientType)
        {
            case IngredientType.BEAR:
                if (!levelManager.habitatManager.listBearHabitatsActive.Contains(this))
                    levelManager.habitatManager.listBearHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.BearHabitat_Complete.ToString());
                }
               
                break;
            case IngredientType.COW:
                if (!levelManager.habitatManager.listCowHabitatsActive.Contains(this))
                    levelManager.habitatManager.listCowHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.CowHabitat_Complete.ToString());
                }
               
                break;
            case IngredientType.CHICKEN:
                if (!levelManager.habitatManager.listChickenHabitatsActive.Contains(this))
                    levelManager.habitatManager.listChickenHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.ChickenHabitat_Complete.ToString());
                }
               
                break;
            case IngredientType.SHEEP:
                if (!levelManager.habitatManager.listSheepHabitatsActive.Contains(this))
                    levelManager.habitatManager.listSheepHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.SheepHabitat_Complete.ToString());
                }
               
                break;
            //case IngredientType.LION:
            //    if (!levelManager.habitatManager.listLionHabitatsActive.Contains(this))
            //        levelManager.habitatManager.listLionHabitatsActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        EnventManager.TriggerEvent(EventName.LionHabitat_Complete.ToString());
            //    }

            //    break;
            //case IngredientType.CROC:
            //    if (!levelManager.habitatManager.listCrocHabitatsActive.Contains(this))
            //        levelManager.habitatManager.listCrocHabitatsActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        EnventManager.TriggerEvent(EventName.CrocHabitat_Complete.ToString());
            //    }

            //    break;
            //case IngredientType.ELE:
            //    if (!levelManager.habitatManager.listEleHabitatsActive.Contains(this))
            //        levelManager.habitatManager.listEleHabitatsActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        EnventManager.TriggerEvent(EventName.EleHabitat_Complete.ToString());
            //    }

            //    break;
            //case IngredientType.ZEBRA:
            //    if (!levelManager.habitatManager.listZebraHabitatsActive.Contains(this))
            //        levelManager.habitatManager.listZebraHabitatsActive.Add(this);
            //    if (isPushEvent)
            //    {
            //        EnventManager.TriggerEvent(EventName.ZebraHabitat_Complete.ToString());
            //    }

            //    break;
        }
    }
 
    public override void StartInGame()
    {
        base.StartInGame();
        LoadData();
        EnventManager.AddListener(EventName.QuitGame.ToString(), SaveData_OnQuitGame);
        CurrentCoin = pirceObject.Get_Pirce();
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
            dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        isHaveStaff = false;
        isReadyShit = false;
        delayShit = consDelayShit;
        randomShitPos = Vector3.zero;
        //for(int i = 0; i < allAnimals.Count; i++)
        //{
        //    allAnimals[i].SetHabitat(this);
        //    allAnimals[i].transform.position = new Vector3(defaultAnimalPos[i].transform.position.x,
        //        allAnimals[i].transform.position.y, defaultAnimalPos[i].transform.position.z);
        //    allAnimals[i].StartInGame();
        //}
        if (isLock)
        {
            //GetComponent<BoxCollider>().enabled = false;
            unlockModel.SetActive(false);
            unlockFx.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            checkCollect.gameObject.SetActive(false);
            if (CurrentCoin <= 0)
            {
                UnLock(true, true);
            }
        }
        //if (!isLock)
        //{
        //    UnLock();
        //}
        checkUnlock.UpdateUI();
        EnventManager.AddListener(EventName.NoShit_Play.ToString(), OnNoShit);
        EnventManager.AddListener(EventName.NoShit_Stop.ToString(), OffNoShit);
    }
    void Update()
    {
        if (!isLock)
        {
            RandomTakeAShit();
        }  
    }
    public void RandomTakeAShit()
    {
        if (isBuff_NoShit)
        {
            return;
        }
        if (levelManager.habitatManager.allActiveHabitats.Count > 1)
        {
            if (isReadyShit && listShit.Count < maxShit)
            {
                isReadyShit = false;
                if (CheckTakeAShit())
                {
                    TakeAShit(true);
                }
            }
            if (!isReadyShit)
            {
                delayShit -= Time.deltaTime;
            }
            if (delayShit < 0)
            {
                randomShitPos = Vector3.zero;
                delayShit = consDelayShit;
                isReadyShit = true;
            }
        } 
    }
    public bool CheckTakeAShit()
    {
        bool isTakeAShit = false;
        randomShitPos = RandomPosition();
        if(randomShitPos != Vector3.zero)
        {
            isTakeAShit = true;
        }
        else
        {
            isTakeAShit = false;
        }
        return isTakeAShit;
    }
    public void TakeAShit(bool isStart)
    {
        int r = Random.Range(0, allAnimals.Count);
        AnimalBase animal = allAnimals[r];
        var curShit = AllPoolContainer.Instance.Spawn(shitPrefabs, animal.myTransform.position, myTransform.rotation);
        curShit.transform.DOJump(new Vector3(randomShitPos.x,0.5f,randomShitPos.z), 2.5f, 1, 0.5f).OnComplete(()=> 
        {
            listShit.Add(curShit as Shit);
            if (isStart)
            {
                numShitSave++;
            }
        }); 
    }
    private void OnNoShit()
    {
        isBuff_NoShit = true;
        ClearShit();
    }
    private void ClearShit()
    {
        foreach (Shit shit in listShit)
        {
            AllPoolContainer.Instance.Release(shit);
        }
        listShit.Clear();
        numShitSave = 0;
    }
    private void OffNoShit()
    {
        isBuff_NoShit = false;
    }
    public Vector3 RandomPosition()
    {
        Vector3 randomPos = Vector3.zero;
        float radius = 0;
        switch (this.ingredientType)
        {
            case IngredientType.CHICKEN:
                radius = 4.5f;
                break;
            case IngredientType.COW:
                radius = 7f;
                break;
            case IngredientType.BEAR:
                radius = 8.5f;
                break;

        }
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += this.transform.position;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            randomPos = new Vector3(hit.position.x, myTransform.position.y, hit.position.z);
        }
        return randomPos;
    }
    public void TakeAShitOnStart(int n)
    {
        while (n > 0)
        {
            if (CheckTakeAShit())
            {
                TakeAShit(false);
                n--;
            }
        }
    }
    public void SpawnAnimal(bool isHired , Vector3 hiredPos)
    {
        Vector3 randomPos = Vector3.zero;
        while(Vector3.Distance(randomPos,Vector3.zero) == 0)
        {
            randomPos = RandomPosition();
            if(Vector3.Distance(randomPos, Vector3.zero) != 0)
            {
                if (!isHired)
                {
                    hiredPos = randomPos;
                }
                var curAnimal = AllPoolContainer.Instance.Spawn(animalPrefabs, hiredPos, myTransform.rotation);
                curAnimal.transform.Rotate(0f, 180f, 0.0f, Space.Self);
                allAnimals.Add(curAnimal as AnimalBase);
                (curAnimal as AnimalBase).SetHabitat(this);
                (curAnimal as AnimalBase).StartInGame();
                if (isHired == true)
                {
                    numAnimalSave++;
                    (curAnimal as AnimalBase).nextDes = randomPos;
                    (curAnimal as AnimalBase).UpdateState(AnimalBase.RUN_STATE);
                }
            }
        }
    }
    public void SpawnAnimalOnStart(int n)
    {
        for(int i = 0; i < n; i ++)
        {
            SpawnAnimal(false,Vector3.zero);
        }
    }
    public void SaveData_OnQuitGame()
    {
        (dataStatusObject as HabitatDataStatusObject).SetCountShit(numShitSave);
        (dataStatusObject as HabitatDataStatusObject).SetCountAnimal(numAnimalSave);
    }
    public void LoadData()
    {
        numShitSave = (dataStatusObject as HabitatDataStatusObject).GetCountShit();
        numAnimalSave = (dataStatusObject as HabitatDataStatusObject).GetCountAnimal();
       // Debug.Log(numAnimalSave);
    }
}

