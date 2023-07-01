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
        line = GetComponent<LineRenderer>();
        line.widthMultiplier = 0.2f;
        line.positionCount = 2;
        fxTUT.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.isTUT)
        {
            if (levelManager.habitatManager.allActiveHabitats.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(habitat.transform.position.x, 8f, habitat.transform.position.z);
                fxTUT.SetActive(true);
                //line.SetPosition(0, player.transform.position);
                //line.SetPosition(1, new Vector3(habitat.transform.position.x, 0, habitat.transform.position.z));
            }
            else if (levelManager.machineManager.allActiveClothMachine.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(clothMachine.transform.position.x, 8f, clothMachine.transform.position.z);
                fxTUT.SetActive(true);
                //line.SetPosition(0, player.transform.position);
                //line.SetPosition(1, new Vector3(clothMachine.transform.position.x, 0, clothMachine.transform.position.z));
            }
            else if (levelManager.machineManager.allActiveBagMachine.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(bagMachine.transform.position.x, 8f, bagMachine.transform.position.z);
                fxTUT.SetActive(true);
                //line.SetPosition(0, player.transform.position);
                //line.SetPosition(1, new Vector3(bagMachine.transform.position.x, 0, bagMachine.transform.position.z));
            }
            else if (levelManager.closetManager.listAvailableClosets.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(closet.transform.position.x, 8f, closet.transform.position.z);
                fxTUT.SetActive(true);
                //line.SetPosition(0, player.transform.position);
                //line.SetPosition(1, new Vector3(closet.transform.position.x, 0, closet.transform.position.z));
            }
            else if (levelManager.closetManager.listAvailableBagClosets.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(bagCloset.transform.position.x, 8f, bagCloset.transform.position.z);
                fxTUT.SetActive(true);
                //line.SetPosition(0, player.transform.position);
                //line.SetPosition(1, new Vector3(closet.transform.position.x, 0, closet.transform.position.z));
            }
            else if (levelManager.checkOutManager.listCheckout.Count <= 0)
            {
                fxTUT.transform.position = new Vector3(checkOut.transform.position.x, 8f, checkOut.transform.position.z);
                fxTUT.SetActive(true);
                //line.SetPosition(0, player.transform.position);
                //line.SetPosition(1, new Vector3(checkOut.outIngredientPos.position.x, 0, clothMachine.outIngredientPos.position.z));
            }
            else if(clothMachine.outCloths.Count <= clothMachine.maxObjOutput)
            {
                if(clothMachine.ingredients.Count <= 3)
                {
                    if(player.objHave <= 0)
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
                    line.SetPosition(0, player.transform.position);
                    line.SetPosition(1, new Vector3(clothMachine.outIngredientPos.transform.position.x, 0, clothMachine.outIngredientPos.transform.position.z));
                }
            }
        }
    }
}
