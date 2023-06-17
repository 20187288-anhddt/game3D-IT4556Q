using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Habitat : BuildObj, ILock
{
    public List<AnimalBase> allAnimals;
    public List<AnimalBase> animalsIsReady;
    public IngredientBase ingredientPrefabs;
    [SerializeField]
    private GameObject unlockModel;
    [SerializeField]
    private CheckUnlock checkUnlock;
    [SerializeField]
    private CheckCollect checkCollect;
    public bool IsLock { get => isLock; set => isLock = value; }
    public float DefaultCoin { get => defaultCoin; }
    public float CurrentCoin { get => coinUnlock; set => coinUnlock = value; }
    public Transform staffPos;
    public bool isHaveStaff;

    public override void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Player p = Player.Instance;
       
        base.UnLock(isPushEvent, isPlayAnimUnlock);
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
       
        p.isUnlock = true;
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
                    });
                }); ;
            });
        }
        else
        {
            p.isUnlock = false;
        }
        checkUnlock.gameObject.SetActive(false);
        levelManager.habitatManager.allActiveHabitats.Add(this);
        //GetComponent<BoxCollider>().enabled = true;
        switch (ingredientType)
        {
            case IngredientType.BEAR:
                levelManager.habitatManager.listBearHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.BearHabitat_Complete.ToString());
                }
               
                break;
            case IngredientType.COW:
                levelManager.habitatManager.listCowHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.CowHabitat_Complete.ToString());
                }
               
                break;
            case IngredientType.CHICKEN:
                levelManager.habitatManager.listSheepHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.ChickenHabitat_Complete.ToString());
                }
               
                break;
            case IngredientType.SHEEP:
                levelManager.habitatManager.listChickenHabitatsActive.Add(this);
                if (isPushEvent)
                {
                    EnventManager.TriggerEvent(EventName.ChickenHabitat_Complete.ToString());
                }
               
                break;
        }
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        StartInGame();
    }
    public override void StartInGame()
    {
        base.StartInGame();
        isHaveStaff = false;
        foreach(AnimalBase a in allAnimals)
        {
            a.SetHabitat(this);
        }
        if (isLock)
        {
            //GetComponent<BoxCollider>().enabled = false;
            unlockModel.SetActive(false);
            checkUnlock.gameObject.SetActive(true);
            checkCollect.gameObject.SetActive(false);
        }
        if (!isLock)
        {
            UnLock();
        }
        checkUnlock.UpdateUI();
    }
}

