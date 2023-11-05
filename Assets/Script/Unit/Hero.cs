using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Unit
{
    public float runSpeed;
    public float normalMoveSpeed;

    public int lastDiretion; //上一次移动朝向
    bool isRight; //面朝方向是否向右
    public Hero(GameObject unitObj) : base(unitObj)
    {
        this.unitObj = unitObj;
        isRight = true;
    }

    public override void Init()
    {
        HeroHold heroHoldState = new HeroHold(this);
        fsm = new FSM(heroHoldState);

        FSMTransition HoldToMove = new FSMTransition(State.HeroHold, State.HeroMove);
        FSMConditionTrigger HoldToMoveCondition = new FSMConditionTrigger("Move");
        HoldToMove.AddCondition(HoldToMoveCondition);
        heroHoldState.AddTransition(HoldToMove);


        HeroMove heroMoveState = new HeroMove(this);
        fsm.AddState(heroMoveState);
        FSMTransition MoveToHold = new FSMTransition(State.HeroMove, State.HeroHold);
        heroMoveState.AddTransition(MoveToHold);
        FSMConditionBool MoveToMHoldCondition = new FSMConditionBool("Hold",FSMConditionBool.BoolCondition.True,true);
        MoveToHold.AddCondition(MoveToMHoldCondition);

        base.Init();
    }

    public void LeftAction()
    {
        lastDiretion = -1;
        fsm.SetTrigger("Move");
    }
    
    public void RightAction()
    {
        lastDiretion = 1;
        fsm.SetTrigger("Move");
    }

    //检测输入方向的敌人 1为右 -1为左
    //阿宅的动画逻辑 运动方向有敌人都是进攻，运动方向没有敌人，且面朝方向有敌人是后退。
    public bool AttackOrHide(int direction)
    {    
        if ((direction == 1&&!isRight)||(direction==-1&&isRight))
        {
            //按键方向有没有敌人
            Vector2 boxDirection = direction == 1 ? new Vector2(1, 0) : new Vector2(-1, 0);
            RaycastHit2D[] colliderKeyFront = Physics2D.BoxCastAll(unitObj.transform.GetComponent<RectTransform>().anchoredPosition, new Vector2(10, 1000), 0, boxDirection, 1000.0f, LayerMask.GetMask("Enemy"));
            if(colliderKeyFront.Length==0)
            {
                RaycastHit2D[] colliderKeyBack = Physics2D.BoxCastAll(unitObj.transform.GetComponent<RectTransform>().anchoredPosition, new Vector2(10, 1000), 0,-boxDirection, 1000.0f, LayerMask.GetMask("Enemy"));
                if(colliderKeyBack.Length>0)
                {
                    Debug.Log("阿宅闪避");
                    return false;
                }
            }
        }
        return true;
    }
}
