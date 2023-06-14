{
    public static string IDLE_STATE = "idle_state";
    public static string RUN_STATE = "run_state";
    public FsmSystem fsm = new FsmSystem();
    public string STATE_ACTOR;
    public float speed;
    public float animSpd;
    public float timeDelayCatch;
    public List<IngredientBase> ingredientBases = new List<IngredientBase>();
    protected bool canCatch = false;
//     public void UpdateState(string state)
//     {
//         fsm.setState(state);
//         STATE_ACTOR = state;
//     }
//     public void DelayCatch(float time)
//     {
//         CounterHelper.Instance.QueueAction(time, () =>
        
// //    cans[i].transform.DOLocalMoveY((pizzas.Count) * 0.08f + i * 0.2f, 0f);
//         //}
//     }
}