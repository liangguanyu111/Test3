using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalMove : EnemyMove
{
    public EnemyNormalMove(Enemy enemy, Action onInit = null, Action onEnter = null, Action onExit = null) : base(enemy, onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyNormalMove;
    }
    public override void Update()
    {
        enemy.unitObj.GetComponent<Rigidbody2D>().velocity = enemy.moveDirection() * enemy.enemyConfig.moveSpeed;
        if ((enemy.moveDirection().x > 0 && !enemy.isRight) || (enemy.moveDirection().x < 0 && enemy.isRight))
        {
            enemy.Flip();
        }

        if(enemy.IsAzhaiInAttackRange())
        {
            enemy.unitObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            fsm.SetTrigger("Attack");
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy.PlayAnimation("walk", true);
    }
}

