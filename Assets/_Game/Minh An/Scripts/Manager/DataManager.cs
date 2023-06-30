using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : GenerticSingleton<DataManager>
{
    [SerializeField] private DataMapController dataMapController;
    [SerializeField] private DataPirceObjectController dataPirceObjectController;
    [SerializeField] private DataProcessInMapController dataProcessInMapController;
    [SerializeField] private DataMoneyController dataMoneyController;
    [SerializeField] private DataCustomizeController dataCustomizeController;
    [SerializeField] private DataUIController dataUIController;
    public void Start()
    {
        //dataMapController.SelectDataMap(1);
        //dataMapController.GetDataMap().SetLevel_Capacity_Boss(2);
        //dataMapController.GetDataMap().SetLevel_Capacity_Staff(2);
        //dataMapController.GetDataMap().SetLevel_Hire_Staff(2);
        //InfoPirceObject infoPirceObject = dataPirceObjectController.GetPirceObject(Status_All_Level_Object.NameObject_This.ChickenHabitat, 1, IngredientType.CHICKEN);
        //foreach(InfoBuy infoBuy in infoPirceObject.infoBuys)
        //{
        //    Debug.Log(infoBuy.typeCost);
        //    Debug.Log(infoBuy.value);
        //}
        //Debug.Log(dataProcessInMapController.GetApparatusProcess_Current().name);

       // Debug.Log(GetDataMap().GetDataMap().GetData_Map().GetData_InCheckout().GetData_Checkout(NameObject_This.CheckOutTable).isActiveStaff);
      //  GetDataMap().GetDataMap().SetData_ActiveStaff_Checkout(NameObject_This.CheckOutTable, true);
      //  Debug.Log(GetDataMap().GetDataMap().GetData_Map().GetData_InCheckout().GetData_Checkout(NameObject_This.CheckOutTable).isActiveStaff);
    }


    public DataMapController GetDataMap()
    {
        return dataMapController;
    }
    public DataPirceObjectController GetDataPirceObjectController()
    {
        return dataPirceObjectController;
    }
    public DataMoneyController GetDataMoneyController()
    {
        return dataMoneyController;
    }
    public DataCustomizeController GetDataCustomizeController()
    {
        return dataCustomizeController;
    }
    public DataUIController GetDataUIController()
    {
        return dataUIController;
    }
}
