using UnityEngine;
using System;

[Serializable]
public class EnemyHold : FSMState
{
    Enemy enemy;
    public EnemyHold(Enemy enemy,Action onInit = null, Action onEnter = null, Action onExit = null)
    {
        this.m_State = State.EnemyHold;
        this.enemy = enemy;
    }
    public override void Update()
    {
        base.Update();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        enemy.SetVelocity(0,new Vector2(0, 0));
        enemy.PlayAnimation("hold", true);
        int holdTimer = GameManager._instance.timerManager.AddTimer(() => { fsm.SetBool("Walk", true); }, 1.0f, 1.0f);
    }
    public override void OnInit()
    {
        base.OnInit();
        enemy.PlayAnimation("appear1", false);
    }

    public void CoolDown()
    {

    }
}
