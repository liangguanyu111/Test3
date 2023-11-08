using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public CinemachineVirtualCamera vc;
    [Header("Manager")]
    public TimerMgr timerManager;
    public UnitManager unitMgr;
    public Room room;
    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        timerManager = new TimerMgr();
        unitMgr = new UnitManager();
        room = new Room(34, 6);
        unitMgr.Init();
    }

    private void Start()
    {
        vc.Follow = unitMgr.Azhai.unitObj.transform;
    }

    private void Update()
    {
        timerManager.Update();
        unitMgr.Update();
    }
   
}
