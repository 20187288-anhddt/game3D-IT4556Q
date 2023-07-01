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
        line.widthMultiplier = 1f;
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
                bagMachine.checkUnlock.gameObject.SetActive(false);
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
                                    line.SetPosition(0, player.transform.position);
                                    line.SetPosition(1, new Vector3(clothMachine.inIngredientPos.transform.position.x, 0, clothMachine.inIngredientPos.transform.position.z));
                                }
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
                if (player.coinValue <= 100)
                {
                    if (checkOut.coins.Count <= 0)
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
                    bagMachine.checkUnlock.gameObject.SetActive(true);
                    clothMachine.checkPush.gameObject.SetActive(false);
                    clothMachine.checkCollectCloth.gameObject.SetActive(false);
                    line.gameObject.SetActive(false);
                    fxTUT.SetActive(false);
                    levelManager.isDoneClosetTUT = true;
                }
            }
        }
        else if (!levelManager.isDoneBagClosetTUT)
        {
            if (bagCloset.listBags.Count <= 0)
            {
                if (levelManager.machineManager.allActiveBagMachine.Count <= 0)
                {
                    fxTUT.transform.position = new Vector3(bagMachine.transform.position.x, 8f, bagMachine.transform.position.z);
                    fxTUT.SetActive(true);
                    line.gameObject.SetActive(true);
                    line.SetPosition(0, player.transform.position);
                    line.SetPosition(1, new Vector3(bagMachine.transform.position.x, 0, bagMachine.transform.position.z));
                }
                else if (levelManager.closetManager.listBagClosets.Count <= 0)
                {
                    fxTUT.transform.position = new Vector3(bagCloset.transform.position.x, 8f, bagCloset.transform.position.z);
                    fxTUT.SetActive(true);
                    line.SetPosition(0, player.transform.position);
                    line.SetPosition(1, new Vector3(bagCloset.transform.position.x, 0, bagCloset.transform.position.z));
                }
                else if(player.chickenBags.Count < 1)
                {
                    fxTUT.SetActive(false);
                    if (bagMachine.outCloths.Count < 1)
                    {
                        if (bagMachine.ingredients.Count < 1)
                        {
                            if (player.objHave < 1)
                            {
                                habitat.checkCollect.gameObject.SetActive(true);
                                line.SetPosition(0, player.transform.position);
                                line.SetPosition(1, new Vector3(habitat.transform.position.x, 0, habitat.transform.position.z));
                            }
                            else
                            {
                                line.SetPosition(0, player.transform.position);
                                line.SetPosition(1, new Vector3(bagMachine.inIngredientPos.transform.position.x, 0, bagMachine.inIngredientPos.transform.position.z));
                            }
                        }
                        else
                        {
                            habitat.checkCollect.gameObject.SetActive(false);
                            line.SetPosition(0, player.transform.position);
                            line.SetPosition(1, new Vector3(bagMachine.inIngredientPos.transform.position.x, 0, bagMachine.inIngredientPos.transform.position.z));
                        }
                    }
                    else
                    {
                        line.SetPosition(0, player.transform.position);
                        line.SetPosition(1, new Vector3(bagMachine.outIngredientPos.transform.position.x, 0, bagMachine.outIngredientPos.transform.position.z));
                    }       
                }
                else
                {
                    line.gameObject.SetActive(true);
                    line.SetPosition(0, player.transform.position);
                    line.SetPosition(1, new Vector3(bagCloset.transform.position.x, 0, bagCloset.transform.position.z));
                }
            }
            else
            {
                levelManager.isDoneBagClosetTUT = true;
                clothMachine.checkPush.gameObject.SetActive(true);
                clothMachine.checkCollectCloth.gameObject.SetActive(true);
                habitat.checkCollect.gameObject.SetActive(true);
                fxTUT.SetActive(false);
                line.gameObject.SetActive(false);
            }
        }
    }
}
