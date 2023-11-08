using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移动状态确认攻击目标
public class HeroMove : FSMState
{
    Hero aZhai;
    public Vector2 velocity;
    public HeroMove(Hero azhai)
    {
        this.m_State = State.HeroMove;
        this.aZhai = azhai;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        velocity = new Vector2(0,0);
        float moveDirection = fsm.GetFloat("direction");
        Contact target;
        if(AttackOrHide(moveDirection,out target))
        {
            if((moveDirection==-1&&aZhai.isRight)|| moveDirection == 1 && !aZhai.isRight)
            {
                aZhai.Flip();
            } 
            if(target==null)
            {
                int moveFinish = GameManager._instance.timerManager.AddTimer(MoveDone, 0.2f, 0.2f);
            }
            else
            {
                fsm.SetTrigger("Attack");
               
            }
        }
        else
        {
            if ((moveDirection == 1 && aZhai.isRight) || moveDirection == -1 && !aZhai.isRight)
            {
                aZhai.Flip();
            }
            aZhai.PlayAnimation("back", false);
            int backFinish = GameManager._instance.timerManager.AddTimer(MoveDone,0.2f,0.2f);
        }
    }

    public override void Update()
    {
        base.Update();
        aZhai.unitObj.transform.position = GameManager._instance.room.ReturnPosInRoom(aZhai.unitObj.transform.position);
    }

    public override void OnExit()
    {
        
        aZhai.unitObj.transform.position = GameManager._instance.room.ReturnPosInRoom(aZhai.unitObj.transform.position);
        base.OnExit();

    }
    public void MoveDone()
    {
        aZhai.unitObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        fsm.SetBool("Hold", true);
    }

    //判断阿宅这一次的移动逻辑
    public bool AttackOrHide(float direction,out Contact target)
    {
        RaycastHit2D[] RaycastHits = Physics2D.BoxCastAll(new Vector2(-404, 0), new Vector2(10, 1000), 0, new Vector2(1, 0), 1000.0f, LayerMask.GetMask("Enemy", "Hero"));
        if (RaycastHits.Length <= 1)
        {
            target = null;
            aZhai.PlayAttackAnimation(0);
            //只有主角每秒10的速度移动0.2s
            aZhai.SetVelocity(aZhai.heroCfg.normalSpeed, new Vector2(direction, 0));
            return true;
        }
        else
        {
            for (int i = 0; i < RaycastHits.Length; i++)
            {
                if (RaycastHits[i].collider.gameObject == aZhai.unitObj)
                {
                    //阿宅在最左边按左键，在最右边按右键的时候就是后撤
                    if ((i == 0 && direction == -1) || (i == RaycastHits.Length - 1 && direction == 1))
                    {
                        target = null;
                        aZhai.SetVelocity(aZhai.heroCfg.attackSpeed, new Vector2(direction, 0));
                        return false;
                    }
                    else if (direction == -1)
                    {
                        target = RaycastHits[i-1].collider.gameObject.GetComponent<Contact>();
                        aZhai.SetVelocity(aZhai.heroCfg.attackSpeed, (target.gameObject.transform.position - aZhai.unitObj.transform.position).normalized);
                        return true;
                    }
                    else if(direction == 1)
                    {
                        target = RaycastHits[i+1].collider.gameObject.GetComponent<Contact>();
                        aZhai.SetVelocity(aZhai.heroCfg.attackSpeed, (target.gameObject.transform.position - aZhai.unitObj.transform.position).normalized);
                        return true;
                    }
                }
            }
            target = null;
            return false;
        }
    }


}
