using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : Enemy
{
    //hold,binkMove,rangeAttack
    public EnemyRange(GameObject unitObj, Unit Azhai, EnemyConfig enemyConfig) : base(unitObj, Azhai, enemyConfig)
    {

    }

    public override void Init()
    {
        EnemyShootHold enemyHoldState = new EnemyShootHold(this);
        fsm = new FSM(enemyHoldState);
        EnemyBlinkMove enemyBlinkMove = new EnemyBlinkMove(this);
        fsm.AddState(enemyBlinkMove);
        EnemyShootAttack enemyShootAttack = new EnemyShootAttack(this);
        fsm.AddState(enemyShootAttack);

        //Hold to Move的转换条件
        FSMTransition EnemyHoldToMove = new FSMTransition(State.EnemyShootHold, State.EnemyBlinkMove);
        enemyHoldState.AddTransition(EnemyHoldToMove);
        FSMConditionTrigger enemyBlinkCondition = new FSMConditionTrigger("Blink");
        EnemyHoldToMove.AddCondition(enemyBlinkCondition);
        //Move to Hold
        FSMTransition EnemyMoveToHold = new FSMTransition(State.EnemyBlinkMove,State.EnemyShootHold);
        enemyBlinkMove.AddTransition(EnemyMoveToHold);
        FSMConditionBool attackToHoldCondition1 = new FSMConditionBool("Hold", FSMConditionBool.BoolCondition.True);
        EnemyMoveToHold.AddCondition(attackToHoldCondition1);
        //Hold to Attack
        FSMTransition holdToAttack = new FSMTransition(State.EnemyShootHold, State.EnemyShootAttack);
        enemyHoldState.AddTransition(holdToAttack);
        FSMConditionTrigger enemyAttackCondition = new FSMConditionTrigger("Attack");
        holdToAttack.AddCondition(enemyAttackCondition);
        //Attack to Hold
        FSMTransition AttackToHold = new FSMTransition(State.EnemyShootAttack, State.EnemyShootHold);
        enemyShootAttack.AddTransition(AttackToHold);
        AttackToHold.AddCondition(attackToHoldCondition1);

        base.Init();
    }
}
