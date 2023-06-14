using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : Singleton<CamFollow>
{
    public float smoothSpeed = 0.125f;
    //[SerializeField] private GameObject jt;
    [SerializeField] private GameObject player;
    [HideInInspector] public bool DelayFollow;
    public Transform myTransform;
    private Vector3 startCameraPosition;
    private Vector3 endCameraPosition;
    private Vector3 startPlayerPosition;
    private Vector3 lv1StartCameraPosition;
    private Vector3 lv1startPlayerPosition;
    private Vector3 lv2StartCameraPosition;
    private Vector3 lv2startPlayerPosition;
    private Vector3 movePos;
    private GameManager _manager;
    public override void Awake()
    {
        if(myTransform == null) { myTransform = this.transform; }
    }
    private void Start()
    {
        //Player p = Player.Instance;
        startPlayerPosition = player.transform.position;
        startCameraPosition = myTransform.position;
        endCameraPosition = (player.transform.position - startPlayerPosition);
        FollowPlayer();
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null && !DelayFollow)
        {
            FollowPlayer();
        }
    }
    private void FollowPlayer()
    {
        //transform.position = startCameraPosition + (player.transform.position - startPlayerPosition);
        endCameraPosition = (player.transform.position - startPlayerPosition);
        transform.position = Vector3.Lerp(startCameraPosition, startCameraPosition + player.transform.position, 0.8f);
    }
    public void FollowTut(GameObject g)
    {
        player = g;
        startPlayerPosition = player.transform.position;
        startCameraPosition = transform.position;
        DelayFollow = false;
    }
    public void BackToPlayer()
    {
        startPlayerPosition = player.transform.position;
        startCameraPosition = transform.position;
        DelayFollow = false;
    }
}
