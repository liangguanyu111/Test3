using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHold : FSMState
{
    Hero aZhai;
    public override void OnInit()
    {
        base.OnInit();
        fsm.fsmObject.TryGetComponent<Hero>(out aZhai);

    }

    public override void OnEnter()
    {
        base.OnEnter();
        if(aZhai!=null)
        {
            aZhai.PlayAnimation("hold", true);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        fsm.SetBool("Hold", false);
    }
}
