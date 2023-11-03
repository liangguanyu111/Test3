using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
public class Unit : MonoBehaviour
{
    //状态机
   private FSM fsm;
   public SkeletonGraphic sgp;

    private void Awake()
    {
        //组件获取
        sgp = this.GetComponent<SkeletonGraphic>();

        EnemyHold enemyHoldState = new EnemyHold();
        fsm = new FSM(this.gameObject,State.EnemyHold,enemyHoldState);

        EnemyMove enemyMoveState = new EnemyMove();
        fsm.AddState(State.EnemyMove,enemyMoveState);

        FSMTransition EnemyHoldToMove = new FSMTransition(State.EnemyHold, State.EnemyMove);
        enemyHoldState.AddTransition(EnemyHoldToMove);
        
        FSMConditionBool enemyHoldToMoveCondition1 = new FSMConditionBool("Walk",FSMConditionBool.BoolCondition.True);
        EnemyHoldToMove.AddCondition(enemyHoldToMoveCondition1);
        fsm.allConditionsDic.Add(enemyHoldToMoveCondition1.ConditionName, enemyHoldToMoveCondition1);
    }
    private void Start()
    {
        fsm.Init();
    }

    private void Update()
    {
        fsm.Update();
    }

    public void PlayAnimation(string animName,bool loop =true)
    {
        if(sgp!=null)
        {
            sgp.AnimationState.SetAnimation(0, animName, loop);
        }
    }
}
