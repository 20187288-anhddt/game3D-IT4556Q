using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCloset : DataStatusObject
{
    public int CountID_In_ListPos;
    public Dictionary<int, bool> keyValue_DataInListPos = new Dictionary<int, bool>();
    public override void SaveData()
    {
        SaveDataInListPos();
        base.SaveData();
    }
    public override void LoadData()
    {
        base.LoadData();
        LoadDataInListPos();
    }
    public override void ResetData()
    {
        base.ResetData();
        ResetDataInListPos();
    }
    private void ResetDataInListPos()
    {
        for (int i = 0; i < CountID_In_ListPos; i++)
        {
            keyValue_DataInListPos.Add(i, false);
        }
    }
    private void SaveDataInListPos()
    {
        for (int i = 0; i < CountID_In_ListPos; i++)
        {
            PlayerPrefs.SetInt(i.ToString() + GetFileName(), keyValue_DataInListPos[i] ? 1 : 0);
        }
    }
    private void LoadDataInListPos()
    {
        for (int i = 0; i < CountID_In_ListPos; i++)
        {
            keyValue_DataInListPos[i] = PlayerPrefs.GetInt(i.ToString() + GetFileName()) == 0 ? false : true;
        }
    }
    public bool Get_IsHaveAObj_In_Pos(int ID)
    {
        LoadData();
        return keyValue_DataInListPos[ID];
    }
    public void Set_IsHaveAObj_In_Pos(int ID, bool value)
    {
        keyValue_DataInListPos[ID] = value;
        SaveData();
        LoadData();
    }
}
