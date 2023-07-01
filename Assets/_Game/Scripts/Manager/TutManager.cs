using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutManager : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField]
    private Habitat habitat;
    [SerializeField]
    private ClothMachine clothMachine;
    [SerializeField]
    private BagMachine bagMachine;
    [SerializeField]
    private Closet closet;
    [SerializeField]
    private BagCloset bagCloset;
    [SerializeField]
    private Checkout checkOut;
    [SerializeField]
    private Vector3 checkOutPos;
    [SerializeField]
    private Vector3 pushClothPos;
    [SerializeField]
    private Vector3 collectClothPos;
    private Player player;
    private LevelManager levelManager;
    [SerializeField]
    private GameObject fxTUT;
    
    void Start()
    {
        levelManager = GameManager.Instance.listLevelManagers[0]; 
        player = Player.Instance;
        line = GetComponentInChildren<LineRenderer>();
        line.widthMultiplier = 0.2f;
        line.positionCount = 2;
        fxTUT.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelManager.isDoneMachineTUT)
        {
            if (levelManager.habitatManager.allActiveHabitats.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(habitat.transform.position.x, 8f, habitat.transform.position.z);
                fxTUT.SetActive(true);
                line.SetPosition(0, player.transform.position);
                line.SetPosition(1, new Vector3(habitat.transform.position.x, 0, habitat.transform.position.z));
            }
            else if (levelManager.machineManager.allActiveClothMachine.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(clothMachine.transform.position.x, 8f, clothMachine.transform.position.z);
                fxTUT.SetActive(true);
                line.SetPosition(0, player.transform.position);
                line.SetPosition(1, new Vector3(clothMachine.transform.position.x, 0, clothMachine.transform.position.z));
            }
            else if (levelManager.closetManager.listClosets.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(closet.transform.position.x, 8f, closet.transform.position.z);
                fxTUT.SetActive(true);
                line.SetPosition(0, player.transform.position);
                line.SetPosition(1, new Vector3(closet.transform.position.x, 0, closet.transform.position.z));
            }
            else if (levelManager.checkOutManager.listCheckout.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(checkOut.transform.position.x, 8f, checkOut.transform.position.z);
                fxTUT.SetActive(true);
                line.SetPosition(0, player.transform.position);
                line.SetPosition(1, new Vector3(checkOut.transform.position.x, 0, checkOut.transform.position.z));
            }
            else
            {
                //line.gameObject.SetActive(false);
                habitat.checkCollect.gameObject.SetActive(true);
                fxTUT.SetActive(false);
                levelManager.isDoneMachineTUT = true;
            }
        }
        else if (!levelManager.isDoneClosetTUT)
        {
            if (levelManager.checkOutManager.listGrCusCheckout.Count <= 0)
            {
                if(levelManager.customerManager.listGroupsHaveOutfit.Count <= 0)
                {
                    if (player.chickenCloths.Count < 1)
                    {
                        if (clothMachine.outCloths.Count < 1)
                        {
                            if (clothMachine.ingredients.Count < 1)
                            {
                                if (player.objHave < 1)
                                {
                                    line.SetPosition(0, player.transform.position);
                                    line.SetPosition(1, new Vector3(habitat.transform.position.x, 0, habitat.transform.position.z));
                                }
                                else
                                {
                                    habitat.checkCollect.gameObject.SetActive(false);
                                    line.SetPosition(0, player.transform.position);
                                    line.SetPosition(1, new Vector3(clothMachine.inIngredientPos.transform.position.x, 0, clothMachine.inIngredientPos.transform.position.z));
                                }
                            }
                            else
                            {
                                line.SetPosition(0, player.transform.position);
                                line.SetPosition(1, new Vector3(clothMachine.inIngredientPos.transform.position.x, 0, clothMachine.inIngredientPos.transform.position.z));
                            }
                        }
                        else
                        {
                            line.SetPosition(0, player.transform.position);
                            line.SetPosition(1, new Vector3(clothMachine.outIngredientPos.transform.position.x, 0, clothMachine.outIngredientPos.transform.position.z));
                        }
                    }
                    else
                    {
                        line.SetPosition(0, player.transform.position);
                        line.SetPosition(1, new Vector3(closet.transform.position.x, 0, closet.transform.position.z));
                    }
                }
                else
                {
                    line.gameObject.SetActive(false);
                }
            }
            else
            {
                if(player.coinValue <= 0)
                {
                    if(checkOut.coins.Count <= 0)
                    {
                        line.gameObject.SetActive(true);
                        line.SetPosition(0, player.transform.position);
                        line.SetPosition(1, new Vector3(checkOut.fxPos.transform.position.x, 0, checkOut.fxPos.transform.position.z));
                    }
                    else
                    {
                        line.SetPosition(0, player.transform.position);
                        line.SetPosition(1, new Vector3(checkOut.coins[0].transform.position.x, 0, checkOut.coins[0].transform.position.z));
                        fxTUT.transform.position = fxTUT.transform.position = new Vector3(checkOut.coins[0].transform.position.x, 8f, checkOut.coins[0].transform.position.z);
                        fxTUT.SetActive(true);
                    }
                }
                else
                {
                    line.gameObject.SetActive(false);
                    fxTUT.SetActive(false);
                    habitat.checkCollect.gameObject.SetActive(true);
                    levelManager.isDoneClosetTUT = true;
                }
            }
        }
        //else if (levelManager.closetManager.listAvailableClosets.Count <= 0)
        //{
        //    fxTUT.transform.position = new Vector3(closet.transform.position.x, 8f, closet.transform.position.z);
        //    fxTUT.SetActive(true);
        //    //line.SetPosition(0, player.transform.position);
        //    //line.SetPosition(1, new Vector3(closet.transform.position.x, 0, closet.transform.position.z));
        //}
        //else if (levelManager.closetManager.listAvailableBagClosets.Count <= 0)
        //{
        //    fxTUT.transform.position = new Vector3(bagCloset.transform.position.x, 8f, bagCloset.transform.position.z);
        //    fxTUT.SetActive(true);
        //    //line.SetPosition(0, player.transform.position);
        //    //line.SetPosition(1, new Vector3(closet.transform.position.x, 0, closet.transform.position.z));
        //}
        //else if (levelManager.checkOutManager.listCheckout.Count <= 0)
        //{
        //    fxTUT.transform.position = new Vector3(checkOut.transform.position.x, 8f, checkOut.transform.position.z);
        //    fxTUT.SetActive(true);
        //    //line.SetPosition(0, player.transform.position);
        //    //line.SetPosition(1, new Vector3(checkOut.outIngredientPos.position.x, 0, clothMachine.outIngredientPos.position.z));
        //}
        //else if (clothMachine.outCloths.Count <= clothMachine.maxObjOutput)
        //{
        //    if (clothMachine.ingredients.Count <= 3)
        //    {
        //        if (player.objHave <= 0)
        //        {
        //            line.SetPosition(0, player.transform.position);
        //            line.SetPosition(1, new Vector3(habitat.transform.position.x, 0, habitat.transform.position.z));
        //        }
        //        else
        //        {
        //            line.SetPosition(0, player.transform.position);
        //            line.SetPosition(1, new Vector3(clothMachine.inIngredientPos.transform.position.x, 0, clothMachine.inIngredientPos.transform.position.z));
        //        }
        //    }
        //    else
        //    {
        //        line.SetPosition(0, player.transform.position);
        //        line.SetPosition(1, new Vector3(clothMachine.outIngredientPos.transform.position.x, 0, clothMachine.outIngredientPos.transform.position.z));
        //    }
        //}
    }
}
