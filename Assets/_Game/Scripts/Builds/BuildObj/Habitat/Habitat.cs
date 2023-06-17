using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Habitat : BuildObj, ILock
{
    public IngredientType habitatType;
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

    public override void UnLock()
    {
        base.UnLock();
        //vfx.gameObject.SetActive(true);
        IsLock = false;
        //AudioManager.Instance.PlaySFX(AudioCollection.Instance.sfxClips[4], 1, false);
        //levelManager.CheckUnlockBuildID(IDUnlock, this);
        Player p = Player.Instance;
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
        
        unlockModel.transform.DOMoveY(2, 0f).OnComplete(() => {
            unlockModel.transform.DOMoveY(-0.1f, 0.5f).OnComplete(() => {
                unlockModel.transform.DOShakePosition(0.5f, new Vector3(0, 0.5f, 0), 10, 0, false).OnComplete(() =>
                {
                    p.isUnlock = false;  
                    checkCollect.gameObject.SetActive(true);
                });
            }); ;
        });
        checkUnlock.gameObject.SetActive(false);
        //GetComponent<BoxCollider>().enabled = true;
        switch (habitatType)
        {
            case IngredientType.BEAR:
                levelManager.listBearHabitatActive.Add(this);
                break;
            case IngredientType.COW:
                levelManager.listCowHabitatActive.Add(this);
                break;
            case IngredientType.CHICKEN:
                levelManager.listSheepHabitatActive.Add(this);
                break;
            case IngredientType.SHEEP:
                levelManager.listChickenHabitatActive.Add(this);
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartInGame();
    }
    public override void StartInGame()
    {
        base.StartInGame();
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

