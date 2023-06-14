using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : BaseActor,ICollect,IUnlock
{
    public static Player Instance;
    public bool isUnlock;
    public float coinValue;
    public float CoinValue { get => coinValue; set => coinValue = value; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    protected void Start()
    {
        fsm.init(2);
        fsm.add(new FsmState(IDLE_STATE, null, OnIdleState));
        fsm.add(new FsmState(RUN_STATE, null, OnRunState));
        UpdateState(IDLE_STATE);
        fsm.execute();
    }
    protected void Update()
    {
        fsm.execute();
        var rig = GetComponent<Rigidbody>();
        animSpd = rig.velocity.magnitude;
        if (Config(GameManager.Instance.joystick.Direction) != Vector2.zero && !isUnlock)
        {
            UpdateState(RUN_STATE);
        }
        else UpdateState(IDLE_STATE);
    }
    public Vector2 Config(Vector2 input)
    {
        if (input.magnitude >= 0.09)
        {
            return input;
        }
        return Vector2.zero;
    }
    private FsmSystem.ACTION OnIdleState(FsmSystem _fsm)
    {
        Idle();
        return FsmSystem.ACTION.END;
    }
    private FsmSystem.ACTION OnRunState(FsmSystem _fsm)
    {
        UpdateMove(speed);
        return FsmSystem.ACTION.END;
    }
    public virtual void UpdateMove(float speed)
    {
        Joystick joystick = GameManager.Instance.joystick;
        Vector2 inputAxist = joystick.Direction;
        //Vector3 direction = new Vector3(joystick.Vertical, 0f, -joystick.Horizontal);
        var rig = GetComponent<Rigidbody>();
        rig.velocity = new Vector3(joystick.Horizontal * speed, rig.velocity.y, joystick.Vertical * speed);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 moveDir = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            transform.rotation = Quaternion.LookRotation(moveDir).normalized;
        }
    }
    public virtual void Idle()
    {
    }
    public void Collect()
    {
    }
    public void UnlockMap(float coin)
    {
        if (coinValue <= 0)
            return;
        if (coinValue > coin)
        {
            coinValue -= coin;
        }
        else
        {
            coinValue = 0;
        }
    }
}