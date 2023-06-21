using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuild : MonoBehaviour 
{
<<<<<<< Updated upstream
=======
    public Transform myTransform;
    public NameObject_This nameObject_This;
>>>>>>> Stashed changes
    public bool isLock;
    public bool isKey = false;
    public int IDUnlock;
    public bool unlockAds;
    public float timeDelay;
    public float defaultCoin;
    public GameManager gameManager;
    public LevelManager levelManager;
    public virtual void UnLock()
    {
<<<<<<< Updated upstream

=======
        if(dataStatusObject == null) { dataStatusObject = GetComponent<DataStatusObject>(); }
        if (myTransform == null) { myTransform = this.transform; }
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
>>>>>>> Stashed changes
    }
    public virtual void Effect()
    {

    }
    public virtual void StartInGame()
    {
        gameManager = GameManager.Instance;
        levelManager = gameManager.listLevelManagers[0];
    }
}
