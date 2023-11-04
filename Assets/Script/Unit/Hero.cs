using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public float runSpeed;
    public float normalMoveSpeed;
    public override void Init()
    {
        HeroHold heroHoldState = new HeroHold();
        fsm = new FSM(this.gameObject,State.HeroHold,heroHoldState);

        FSMTransition HoldToMove = new FSMTransition(State.HeroHold, State.HeroMove);
        FSMConditionTrigger HoldToMoveCondition = new FSMConditionTrigger("Move");
        HoldToMove.AddCondition(HoldToMoveCondition);
        heroHoldState.AddTransition(HoldToMove);


        HeroMove heroMoveState = new HeroMove();
        fsm.AddState(State.HeroMove, heroMoveState);
        FSMTransition MoveToHold = new FSMTransition(State.HeroMove, State.HeroHold);
        heroMoveState.AddTransition(MoveToHold);
        FSMConditionBool MoveToMHoldCondition = new FSMConditionBool("Hold",FSMConditionBool.BoolCondition.True,true);
        MoveToHold.AddCondition(MoveToMHoldCondition);


    }

    public void LeftAction()
    {
        fsm.SetTrigger("Move");
    }
    
    public void RightAction()
    {
        fsm.SetTrigger("Move");
    }
}
