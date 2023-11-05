using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    [Header("Manager")]
    public TimerMgr timerManager;
    public UnitManager unitMgr;
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
        unitMgr.Init();
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        timerManager.Update();

      
    }
   
}
