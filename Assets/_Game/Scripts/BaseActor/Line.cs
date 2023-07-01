using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
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
    void Start()
    {
        levelManager = GameManager.Instance.listLevelManagers[0];
        player = Player.Instance;
        line = GetComponent<LineRenderer>();
        line.widthMultiplier = 0.2f;
        line.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.isTUT)
        {
            //if(levelManager.habitatManager.allActiveHabitats.Count <= 0)
            //{
            //    line.SetPosition(0, player.transform.position);
            //    line.SetPosition(1, new Vector3(habitat.transform.position.x, 0, habitat.transform.position.z));
            //}
            //else if(levelManager.machineManager.allActiveClothMachine.Count <= 0)
            //{
            //    line.SetPosition(0, player.transform.position);
            //    line.SetPosition(1, new Vector3(clothMachine.transform.position.x, 0, clothMachine.transform.position.z));
            //}
            //else if(levelManager.machineManager.allActiveBagMachine.Count <= 0)
            //{

            //}
            //else if(player.objHave > 0)
            //{
            //    line.SetPosition(0, player.transform.position);
            //    line.SetPosition(1, new Vector3(clothMachine.inIngredientPos.position.x, 0, clothMachine.inIngredientPos.position.z));
            //}
            //else if(player.objHave <=0 )
            //{
            //    line.SetPosition(0, player.transform.position);
            //    line.SetPosition(1, new Vector3(clothMachine.outIngredientPos.position.x, 0, clothMachine.outIngredientPos.position.z));
            //}
        }
    }
}
