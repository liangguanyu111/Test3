using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public override void Init()
    {
        EnemyHold enemyHoldState = new EnemyHold();
        fsm = new FSM(this.gameObject, State.EnemyHold, enemyHoldState);

        EnemyMove enemyMoveState = new EnemyMove();
        fsm.AddState(State.EnemyMove, enemyMoveState);

        FSMTransition EnemyHoldToMove = new FSMTransition(State.EnemyHold, State.EnemyMove);
        enemyHoldState.AddTransition(EnemyHoldToMove);

        FSMConditionBool enemyHoldToMoveCondition1 = new FSMConditionBool("Walk", FSMConditionBool.BoolCondition.True, false);
        EnemyHoldToMove.AddCondition(enemyHoldToMoveCondition1);
    }

}
