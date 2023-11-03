using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyHold : FSMState
{
    public EnemyHold(Action onInit = null, Action onEnter = null, Action onExit = null) : base(onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyHold;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnInit()
    {
        base.OnInit();
        Debug.Log("EnemyHold Init!");
        fsm.SetBool("Walk", true);
    }
}
