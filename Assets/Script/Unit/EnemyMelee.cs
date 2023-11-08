using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public EnemyMelee(GameObject unitObj, Unit Azhai, EnemyConfig enemyConfig) : base(unitObj, Azhai, enemyConfig)
    {

    }

    //Hold,NormalMove，MeleeAttack
    public override void Init()
    {
        EnemyHold enemyHoldState = new EnemyHold(this);
        fsm = new FSM(enemyHoldState);
        EnemyNormalMove enemyMoveState = new EnemyNormalMove(this);
        EnemyMeleeAttack enemyAttackState = new EnemyMeleeAttack(this);
        fsm.AddState(enemyMoveState);
        fsm.AddState(enemyAttackState);

        //Hold to Move的转换条件
        FSMTransition EnemyHoldToMove = new FSMTransition(State.EnemyHold, State.EnemyNormalMove);
        enemyHoldState.AddTransition(EnemyHoldToMove);
        FSMConditionBool enemyHoldToMoveCondition1 = new FSMConditionBool("Walk", FSMConditionBool.BoolCondition.True);
        EnemyHoldToMove.AddCondition(enemyHoldToMoveCondition1);

        //Move to Hold的转换条件
        FSMTransition EnemyMoveToHold = new FSMTransition(State.EnemyNormalMove, State.EnemyHold);
        enemyMoveState.AddTransition(EnemyMoveToHold);
        FSMConditionBool enemyMoveToHoldCondition = new FSMConditionBool("Walk", FSMConditionBool.BoolCondition.Fasle);
        EnemyMoveToHold.AddCondition(enemyMoveToHoldCondition);

        //Move to Attack
        FSMTransition MoveToAttack = new FSMTransition(State.EnemyNormalMove, State.EnemyMeleeAttack);
        enemyMoveState.AddTransition(MoveToAttack);
        FSMConditionTrigger enemyAttackCondition = new FSMConditionTrigger("Attack");
        MoveToAttack.AddCondition(enemyAttackCondition);
        //Attack to Hold
        FSMTransition AttackToHold = new FSMTransition(State.EnemyMeleeAttack, State.EnemyHold);
        enemyAttackState.AddTransition(AttackToHold);
        FSMConditionBool attackToHoldCondition1 = new FSMConditionBool("Hold", FSMConditionBool.BoolCondition.True);
        AttackToHold.AddCondition(attackToHoldCondition1);


        base.Init();
    }
}
