using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public CustomerManager customerManager;
    public CheckOutManager checkOutManager;
    public List<Closet> listBearClosetActive;
    public List<Closet> listCowClosetActive;
    public List<Closet> listSheepClosetActive;
    public List<Closet> listChickenClosetActive;
    public List<BagCloset> listBearBagClosetActive;
    public List<BagCloset> listCowBagClosetActive;
    public List<BagCloset> listSheepBagClosetActive;
    public List<BagCloset> listChickenBagClosetActive;
    public List<ClothMachine> listBearClothMachineActive;
    public List<ClothMachine> listCowClothMachineActive;
    public List<ClothMachine> listSheepClothMachineActive;
    public List<ClothMachine> listChickenClothMachineActive;
    public List<BagMachine> listChickenBagMachineActive;
    public List<BagMachine> listBearBagMachineActive;
    public List<BagMachine> listCowBagMachineActive;
    public List<BagMachine> listSheepBagMachineActive;
    public List<Habitat> listSheepHabitatActive;
    public List<Habitat> listCowHabitatActive;
    public List<Habitat> listChickenHabitatActive;
    public List<Habitat> listBearHabitatActive;
    public ClosetManager closetManager;
    //public PlaceManager placeManager;
    
    public void StartInGame()
    {
        
    }
    public void ResetLevel()
    {
        
    }
}
