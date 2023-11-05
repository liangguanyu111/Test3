using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : FSMState
{
    Hero aZhai;
    public HeroMove(Hero azhai)
    {
        this.m_State = State.HeroMove;
        this.aZhai = azhai;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        if(aZhai.AttackOrHide(aZhai.lastDiretion))
        {
            aZhai.PlayAnimation("atk1", false);
            int timer = GameManager._instance.timerManager.AddTimer(MoveDone, 1f, 1f);
        }
        else
        {
            aZhai.PlayAnimation("back", false);
            int timer = GameManager._instance.timerManager.AddTimer(MoveDone, 0.2f, 0.2f);
        }
    }
    public void MoveDone()
    {
        Debug.Log("Move Done");
        fsm.SetBool("Hold", true);
    }
}
