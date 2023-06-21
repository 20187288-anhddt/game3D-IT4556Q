using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildIngredientController : MonoBehaviour
{
    public IngredientType ingredientType;
    [SerializeField] private List<BaseBuild> baseBuilds;
    [SerializeField] private List<DataStatusObject> dataStatusObjects;


    public bool IsBuild_Complete(NameObject_This nameObject_This)
    {
        foreach(BaseBuild baseBuild in baseBuilds)
        {
            if(baseBuild.nameObject_This == nameObject_This)
            {
                return true;
            }
        }
        return false;
    }
    public BaseBuild GetBaseBuild(NameObject_This nameObject_This)
    {
        foreach (BaseBuild baseBuild in baseBuilds)
        {
            if (baseBuild.nameObject_This == nameObject_This)
            {
                return baseBuild;
            }
        }
        return null;
    }

    public DataStatusObject GetDataStatusObject(NameObject_This nameObject_This)
    {
        foreach (DataStatusObject dataStatusObject in dataStatusObjects)
        {
            if(dataStatusObject.GetStatus_All_Level_Object().nameObject_This == nameObject_This)
            {
                return dataStatusObject;
            }
        }
        return null;
    }

    public void OnBuy(NameObject_This nameObject_This)
    {
        //neu Habitat da hoat dong thi k OnBuy
        GetDataStatusObject(nameObject_This).OnBuy();
    }
    public void OnBought(NameObject_This nameObject_This)
    {
        GetDataStatusObject(nameObject_This).OnBought();
        EnventManager.TriggerEvent(EventName.StatusData_OnLoad.ToString());
    }
}
