using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public float runSpeed;
    public float normalMoveSpeed;
    public HeroConfig heroCfg;

    public Hero(GameObject unitObj,HeroConfig heroCfg) : base(unitObj)
    {
        this.unitObj = unitObj;
        isRight = true;
        this.heroCfg = heroCfg;
        currentHp = heroCfg.initHp;
        Init();
    }

    public override void Init()
    {
        HeroHold heroHoldState = new HeroHold(this);
        fsm = new FSM(heroHoldState);

        //从Hold到Move  
        FSMTransition HoldToMove = new FSMTransition(State.HeroHold, State.HeroMove);
        FSMConditionTrigger HoldToMoveCondition = new FSMConditionTrigger("Move");
        FSMConditionFloat MoveDirection = new FSMConditionFloat("direction",FSMConditionFloat.FloatCondition.NotEqual,0);
        HoldToMove.AddCondition(HoldToMoveCondition);
        HoldToMove.AddCondition(MoveDirection);
        heroHoldState.AddTransition(HoldToMove);

        //从Move到Hold和Attack
        HeroMove heroMoveState = new HeroMove(this);
        fsm.AddState(heroMoveState);
        FSMTransition MoveToHold = new FSMTransition(State.HeroMove, State.HeroHold);
        FSMTransition MoveToAttack = new FSMTransition(State.HeroMove, State.HeroAttack);
        heroMoveState.AddTransition(MoveToHold);
        heroMoveState.AddTransition(MoveToAttack);
        FSMConditionBool MoveToMHoldCondition = new FSMConditionBool("Hold",FSMConditionBool.BoolCondition.True);
        FSMConditionTrigger MoveToAttackCondition = new FSMConditionTrigger("Attack");
        MoveToAttack.AddCondition(MoveToAttackCondition);
        MoveToHold.AddCondition(MoveToMHoldCondition);


        //Attack到Hold
        HeroAttack heroAttackState = new HeroAttack(this);
        fsm.AddState(heroAttackState);
        FSMTransition AttackToHold = new FSMTransition(State.HeroAttack, State.HeroHold);
        heroAttackState.AddTransition(AttackToHold);
        FSMConditionBool AttackToHoldCondition = new FSMConditionBool("Hold", FSMConditionBool.BoolCondition.True);
        AttackToHold.AddCondition(AttackToHoldCondition);

        base.Init();
    }
    public void LeftAction()
    {
        fsm.SetFloat("direction", -1);
        fsm.SetTrigger("Move");
    }
   
    public void RightAction()
    {
        fsm.SetFloat("direction", 1);
        fsm.SetTrigger("Move");
    }

    public void PlayAttackAnimation(int attackCount)
    {
        int index = (attackCount % 6) + 1;
        string animationName = "atk" + index.ToString();
       PlayAnimation(animationName, false, () => fsm.SetBool("Hold", true));
    }
}
