using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyBlinkMove : EnemyMove
{
    public EnemyBlinkMove(Enemy enemy, Action onInit = null, Action onEnter = null, Action onExit = null) : base(enemy, onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyBlinkMove;
    }
    public override void OnEnter()
    {
        Debug.Log("Blink Enter");
        base.OnEnter();
        enemy.PlayAnimation("jump1",false, BlinkFinsh);
    }

    public void BlinkFinsh()
    {
        enemy.unitObj.transform.position = GameManager._instance.room.ReturnRandomPosAround(enemy.Azhai.unitObj.transform.position, 3, 5);
        enemy.PlayAnimation("jump2", false, () => { fsm.SetBool("Hold",true); });
    }
  
}
