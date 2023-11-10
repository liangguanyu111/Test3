using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHold : FSMState
{
    Hero aZhai;

    public HeroHold(Hero aZhai)
    {
        this.m_State = State.HeroHold;
        this.aZhai = aZhai;
    }
    public override void OnInit()
    {
        base.OnInit();
        aZhai.SetHpBar(true);
        OnEnter();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if(aZhai!=null)
        {
            aZhai.spineAniamtionHelper.PlayAnimation("hold", true);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        fsm.SetBool("Hold", false);
    }
}
