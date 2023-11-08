using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyShootHold : FSMState
{
    Enemy enemy;
    public EnemyShootHold(Enemy enemy, Action onInit = null, Action onEnter = null, Action onExit = null)
    {
        this.m_State = State.EnemyShootHold;
        this.enemy = enemy;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        enemy.SetVelocity(0, new Vector2(0, 0));
        enemy.PlayAnimation("hold", true);
        if (enemy.IsAzhaiInAttackRange())
        {
            int holdTimer = GameManager._instance.timerManager.AddTimer(() => { fsm.SetTrigger("Attack"); }, 1.0f, 1.0f);
        }
        else
        {
            int holdTimer = GameManager._instance.timerManager.AddTimer(() => { fsm.SetTrigger("Blink"); }, UnityEngine.Random.Range(1.0f, 2.0f), 1.0f);
        }      
    }
    public override void OnInit()
    {
        base.OnInit();
        enemy.PlayAnimation("appear1", false);
    }

}
