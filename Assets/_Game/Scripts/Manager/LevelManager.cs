using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CustomerManager customerManager;
    public CheckOutManager checkOutManager;
    public MachineManager machineManager;
    public ClosetManager closetManager;
    public HabitatManager habitatManager;
    public StaffManager staffManager;
    public bool isDoneMachineTUT;
    public bool isDoneClosetTUT;
    public bool isDoneBagClosetTUT;
    public bool isDoneCarTUT;
    //public PlaceManager placeManager;

    public void StartInGame()
    {
        isDoneMachineTUT = false;
        isDoneClosetTUT = false;
        isDoneBagClosetTUT = false;
        isDoneCarTUT = false;
    }
    public void ResetLevel()
    {
        
    }
}
