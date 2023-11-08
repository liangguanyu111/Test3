using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using System;
using static Spine.AnimationState;
using Unity.VisualScripting;

public abstract class Unit 
{

    [Header("属性")]
    public float currentHp;


    //状态机
    protected FSM fsm;
    public SkeletonAnimation sani;
    public GameObject unitObj;
    public bool isRight; //面朝方向是否向右
    public event Action<Unit> OnUnitDead;

    private List<System.Action> mUnRegisterEventActions = new List<System.Action>();
    public Unit() { }
    public Unit(GameObject unitObj)
    {
        Debug.Log("Unit Init");
        sani = unitObj.GetComponent<SkeletonAnimation>();
        unitObj.GetComponent<Contact>().OnGetDamage += GetDamage;
    }

    public virtual void Init()
    {
        fsm.Init();
    }

    public void Update()
    {
        fsm.Update();
    }

    public void Flip()
    {
        isRight = !isRight;
        float angle = 180 * (isRight ? 0 : 1);
        unitObj.GetComponent<Transform>().rotation = Quaternion.Euler(0, angle, 0);
    }

    public virtual void GetDamage(float damage)
    {
        currentHp = currentHp - damage >= 0 ? currentHp - damage : 0;
        if(currentHp<=0)
        {
            OnUnitDead?.Invoke(this);
        }
    }

    #region 动画事件
    public void PlayAnimation(string animName,bool loop =false,Action callBack =null)
    {
        if(sani!=null)
        {
            sani.AnimationState.SetAnimation(0, animName, loop);
            if (callBack != null)
            {
                TrackEntryDelegate ac = null;
                ac = delegate
                {
                    callBack();
                    //删除回调，否则会堆积
                    sani.AnimationState.Complete -= ac;
                };
                sani.AnimationState.Complete += ac;
            }
        }
    }
    public Spine.Animation GetAnimation(string animName)
    {
        if (sani != null)
        {
            return sani.skeletonDataAsset.GetAnimationStateData().skeletonData.FindAnimation(animName);
        }
        return null;
    }

    #endregion


    public void SetVelocity(float Speed, Vector2 velocity)
    {
        unitObj.GetComponent<Rigidbody2D>().velocity = Speed * velocity;
    }

}
