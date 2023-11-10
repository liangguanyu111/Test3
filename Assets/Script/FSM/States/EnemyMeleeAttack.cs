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
        enemy.spineAniamtionHelper.AddCustomEventHandler("event.atk.trigger", MeleeBullet);
        enemy.spineAniamtionHelper.PlayAnimation("atk1", false, () => { fsm.SetBool("Hold", true); });
    }

    public void MeleeBullet()
    {
        EnemyMeleeBullet newBullet = (enemy as EnemyMelee).NewBullet() as EnemyMeleeBullet;
        //近战检测的位置应该更贴合实际
        newBullet.DamageTo(enemy.unitObj.transform.position);
    }
}
