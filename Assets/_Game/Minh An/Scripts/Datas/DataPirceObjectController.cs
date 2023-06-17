using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPirceObjectController : MonoBehaviour
{
    public static string PathGetData;

    public InfoPirceObject GetPirceObject(NameObject_This nameObject_This, int Level, IngredientType ingredientType)
    {
       // Debug.Log(nameObject_This);
        InfoPirceObject infoPirceObjectResource = null;
        PathGetData = "Data_ScriptTable" + "\\Map " + DataManager.Instance.GetDataMap().GetDataMap().GetData_Map().LevelMap + "\\Pirce Object\\" + ingredientType.ToString().ToLower()
            + "\\" + nameObject_This.ToString() + "\\" + "Level " + Level.ToString();
       // Debug.Log(PathGetData);
        infoPirceObjectResource = (InfoPirceObject)Resources.Load(PathGetData, typeof(InfoPirceObject));
       //Debug.Log((infoPirceObjectResource == null) +  " "+ PathGetData);
        return infoPirceObjectResource;
    }
}
