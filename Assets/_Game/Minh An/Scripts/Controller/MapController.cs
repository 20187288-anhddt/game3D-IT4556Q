using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : Singleton<MapController>
{
    [SerializeField] private GameObject Level1;
    [SerializeField] private GameObject Level2;
    [SerializeField] private GameObject Level3;
    private void Start()
    {
        OpenMap(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent());
    }

    public void OpenMap(int value)
    {
        switch (value)
        {
            case 1:
                Level1.gameObject.SetActive(true);
                Level2.gameObject.SetActive(false);
                Level3.gameObject.SetActive(false);
                break;
            case 2:
                Level1.gameObject.SetActive(false);
                Level2.gameObject.SetActive(true);
                Level3.gameObject.SetActive(false);
                break;
            case 3:
                Level1.gameObject.SetActive(false);
                Level2.gameObject.SetActive(false);
                Level3.gameObject.SetActive(true);
                break;
        }
        if(DataManager.Instance.GetDataMap().GetMapCurrent().GetDataMapCurrent().GetLevelInMapCurrent() !=
            value)
        {
            DataManager.Instance.GetDataMap().GetMapCurrent().SetLevelInMapCurrent(value);
        }
    }
    
}
