using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyAttack : FSMState
{
    protected Enemy enemy;
    public EnemyAttack(Enemy enemy, Action onInit = null, Action onEnter = null, Action onExit = null)
    {
        this.m_State = State.EnemyAttack;
        this.enemy = enemy;
    }

    public override void Update()
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
}
