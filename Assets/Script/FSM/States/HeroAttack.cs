using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttack : FSMState
{
    Hero aZhai;
    public int AttackCount = 0;
    int moveFinishTimer;
    bool isStop = false;
    public HeroAttack(Hero azhai)
    {
        this.m_State = State.HeroAttack;
        this.aZhai = azhai;
    }

    public override void OnEnter()
    {
        base.OnEnter();        
        aZhai.PlayAttackAnimation(AttackCount);
        isStop = false;
        AttackCount++;
        moveFinishTimer = GameManager._instance.timerManager.AddTimer(() => { aZhai.SetVelocity(0, new Vector2(0, 0)); }, 0.4f, 0.4f);
    }
    public override void Update()
    {
        if(!isStop&&CheckTarget())
        {
            isStop = true;
            GameManager._instance.timerManager.RemoveTimer(moveFinishTimer);
            aZhai.SetVelocity(0, new Vector2(0, 0));

        }
    }


    
    public bool CheckTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(aZhai.unitObj.transform.position, aZhai.heroCfg.attackRange, LayerMask.GetMask("Enemy"));
        foreach (var collider in colliders)
        {
            collider.gameObject.GetComponent<Contact>().GetDamage(1000);
        }
        return colliders.Length > 0;
    }
}
