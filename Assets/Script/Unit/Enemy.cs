using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public Unit azhai;

    public Enemy(GameObject unitObj,Unit Azhai) : base(unitObj)
    {
        this.unitObj = unitObj;
    }

    public override void Init()
    {
        EnemyHold enemyHoldState = new EnemyHold(this);
        fsm = new FSM(enemyHoldState);

        EnemyMove enemyMoveState = new EnemyMove(this);
        fsm.AddState(enemyMoveState);
        //Hold to Move的转换条件
        FSMTransition EnemyHoldToMove = new FSMTransition(State.EnemyHold, State.EnemyMove);
        enemyHoldState.AddTransition(EnemyHoldToMove);
        FSMConditionBool enemyHoldToMoveCondition1 = new FSMConditionBool("Walk", FSMConditionBool.BoolCondition.True, false);
        EnemyHoldToMove.AddCondition(enemyHoldToMoveCondition1);

        //Move to Hold的转换条件
        FSMTransition EnemyMoveToHold = new FSMTransition(State.EnemyMove, State.EnemyHold);
        enemyMoveState.AddTransition(EnemyMoveToHold);
        FSMConditionBool enemyMoveToHoldCondition = new FSMConditionBool("Walk", FSMConditionBool.BoolCondition.Fasle);
        EnemyMoveToHold.AddCondition(enemyMoveToHoldCondition);

        base.Init();
    }


    public float ReturnDistanceBetweenZhai()
    {
        return Vector3.Distance(unitObj.transform.position, azhai.unitObj.transform.position);
    }
}
