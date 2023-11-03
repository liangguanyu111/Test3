using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;

public class EnemyMove : FSMState
{
    public EnemyMove(Action onInit = null, Action onEnter = null, Action onExit = null) : base(onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyMove;
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("EnemyMove Enter");

    }
}
