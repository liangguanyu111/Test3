using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAttack : EnemyAttack
{
    public EnemyShootAttack(Enemy enemy, Action onInit = null, Action onEnter = null, Action onExit = null) : base(enemy, onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyShootAttack;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("ShootAttack");
        enemy.PlayAnimation("atk1", false, () => { fsm.SetBool("Hold", true); });
    }

}
