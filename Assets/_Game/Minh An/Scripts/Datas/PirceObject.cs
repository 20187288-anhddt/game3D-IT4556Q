using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PirceObject : MonoBehaviour
{
    [SerializeField] private DataStatusObject dataStatusObject;
    [SerializeField] private TMP_Text text;
    private int value_pirce = 0;
    private NameObject_This nameObject_This;
    private int level;
    private IngredientType ingredientType;
    public void LoadPirce(NameObject_This nameObject_This, int Level, IngredientType ingredientType)
    {
        this.nameObject_This = nameObject_This;
        this.level = Level;
        this.ingredientType = ingredientType;
        if (dataStatusObject == null) { dataStatusObject = GetComponentInParent<DataStatusObject>(); }
        // Debug.Log(DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This, Level, ingredientType) == null);
        value_pirce = DataManager.Instance.GetDataPirceObjectController().GetPirceObject(nameObject_This, Level, ingredientType).infoBuys[0].value
            - dataStatusObject.GetAmountPaid();
        if (value_pirce > 1000)
        {
            float x = value_pirce / 1000;
            if (value_pirce % 1000 == 0)
            {
                text.text = "$" + (x + ((value_pirce - 1000 * x) / 1000)).ToString() + "K";
            }
            else
                text.text = "$" + (x + ((value_pirce - 1000 * x) / 1000)).ToString("F2") + "K";
        }
        else if (value_pirce > 100)
            text.text = "$" + string.Format("{000}", value_pirce);
        else
            text.text = "$" + string.Format("{00}", value_pirce);
    }
    public void ReLoadUI()
    {
        if(level == 0)
        {
            level = 1;
            BaseBuild baseBuild = GetComponentInParent<BaseBuild>();
            nameObject_This = baseBuild.nameObject_This;
            ingredientType = baseBuild.ingredientType;
        }
        LoadPirce(nameObject_This, level, ingredientType);
    }
    public int Get_Pirce()
    {
        ReLoadUI();
        return value_pirce;
    }
}
