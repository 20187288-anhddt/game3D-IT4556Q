using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Habitat : BuildObj, ILock
{
    public List<AnimalBase> allAnimals;
    public List<AnimalBase> animalsIsReady;
    public List<AnimalBase> listAnimalOutSide;
    public IngredientBase ingredientPrefabs;
    [SerializeField]
    private GameObject unlockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private CheckCollect checkCollect;
    public Transform[] defaultAnimalPos;
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public Transform staffPos;
    public Transform animalPos;
    public bool isHaveStaff;

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

        if (isPlayAnimUnlock) //anim
        {
            unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
                unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                    unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                    {
                        p.isUnlock = false;
                      //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
                        checkCollect.gameObject.SetActive(true);
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
          //  EnventManager.TriggerEvent(EventName.PlayJoystick.ToString());
            checkCollect.gameObject.SetActive(true);
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
        }
    }
 
    public override void StartInGame()
    {
        base.StartInGame();
        CurrentCoin = pirceObject.Get_Pirce();
        defaultCoin = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This,
            dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(), ingredientType).infoBuys[0].value;
        isHaveStaff = false;
        for(int i = 0; i < allAnimals.Count; i++)
        {
            allAnimals[i].SetHabitat(this);
            allAnimals[i].transform.position = new Vector3(defaultAnimalPos[i].transform.position.x,
                allAnimals[i].transform.position.y, defaultAnimalPos[i].transform.position.z);
            allAnimals[i].StartInGame();
        }
        if (isLock)
        {
            //GetComponent<BoxCollider>().enabled = false;
            unlockModel.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            checkCollect.gameObject.SetActive(false);
        }
        //if (!isLock)
        //{
        //    UnLock();
        //}
        checkUnlock.UpdateUI();
    }
}

