using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuild : MonoBehaviour 
{
    public Transform myTransform;
    public NameObject_This nameObject_This;
    public IngredientType ingredientType;
    public bool isLock;
    public bool isKey = false;
    public int IDUnlock;
    public bool unlockAds;
    public float timeDelay;
    public float coinUnlock;
    public float defaultCoin;
    public GameManager gameManager;
    public LevelManager levelManager;
    public DataStatusObject dataStatusObject;
    public PirceObject pirceObject;
    public virtual void Awake()
    {
        if(dataStatusObject == null) { dataStatusObject = GetComponent<DataStatusObject>(); }
        if (myTransform == null) { myTransform = this.transform; }
    }
    public void Load_LoadPirce_UI()
    {
        pirceObject?.LoadPirce(dataStatusObject.GetStatus_All_Level_Object().nameObject_This,
         dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis(),
         ingredientType);
        //Debug.Log(dataStatusObject.GetStatus_All_Level_Object().GetStatusObject_Current().GetLevelThis());
    }
    public virtual void Start()
    {
        EnventManager.AddListener(EventName.StatusData_OnLoad.ToString(), () =>
        {
            if (dataStatusObject != null)
            {
                if (dataStatusObject.isStatus_Bought())
                {
                    UnLock();
                }
                else if (dataStatusObject.isStaus_OnBuy())
                {
                    Active(true);
                }
                else
                {
                    DontUnLock();
                }
            }
        });
        StartCoroutine(IE_DelayAction(0.2f, () =>
        {
            if (dataStatusObject != null)
            {
                if (dataStatusObject.isStatus_Bought())
                {
                    UnLock(false, true);
                }
                else if (dataStatusObject.isStaus_OnBuy())
                {
                    Active(true);

                }
                else
                {
                    DontUnLock();
                }
            }
        }));
        if (pirceObject == null) { pirceObject = GetComponentInChildren<PirceObject>(); }
        Load_LoadPirce_UI();
    }
    public virtual void UnLock(bool isPushEvent = false, bool isPlayAnimUnlock = false)
    {
        Active(true);
        //GameManager.Instance.buildUnlock++;
        if (!dataStatusObject.isStatus_Bought())
        {
            OnBought();
        }
        //GameManager.Instance.CheckShowInter();
        //FirebaseManager.ins.unlock_new_build(name, GameManager.Instance.buildUnlock);
    }
    public virtual void OnBought()
    {
        dataStatusObject?.OnBought();
    }
    public virtual void DontUnLock()
    {
        Active(false);
    }
    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public virtual void Effect()
    {

    }
    public virtual void StartInGame()
    {
        gameManager = GameManager.Instance;
        levelManager = gameManager.listLevelManagers[0];
    }
    public IEnumerator IE_DelayAction(float timeDelay, System.Action action)
    {
        yield return new WaitForSeconds(timeDelay);
        action?.Invoke();
    }
}
