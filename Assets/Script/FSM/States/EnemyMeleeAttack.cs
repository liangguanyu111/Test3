using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public EnemyMeleeAttack(Enemy enemy, Action onInit = null, Action onEnter = null, Action onExit = null) : base(enemy, onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyMeleeAttack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.PlayAnimation("atk1", false, () => { fsm.SetBool("Hold", true); });
    }

}
