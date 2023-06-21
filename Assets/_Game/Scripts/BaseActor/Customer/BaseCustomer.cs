using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCustomer : AllPool
{
    public FsmSystem fsm = new FsmSystem();
    public static string IDLE_STATE = "idle_state";
    public static string MOVE_TO_CLOSET_STATE = "move_to_closet_state";
    public static string MOVE_TO_BAG_STATE = "move_to_bag_state";
    public static string MOVE_CHECKOUT_STATE = "move_to_checkout_state";
    public static string FOLLOW_LEADER_STATE = "follow_leader_state";
    public static string EXIT_STATE = "exit_state";
    public static string VIP_STATE = "vip_state";

    [Header("-----Status-----")]
    public string STATE_CUSTOMER;


    public void UpdateState(string state)
    {
        Debug.Log(state);
        fsm.setState(state);
        STATE_CUSTOMER = state;
    }
}
