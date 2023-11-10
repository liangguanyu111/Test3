using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using System;
using static Spine.AnimationState;
using Unity.VisualScripting;
using UnityEngine.Events;
using Spine.Unity.Modules;

public abstract class Unit 
{
    [Header("属性")]
    public float currentHp;

    //状态机
    protected FSM fsm;
    public GameObject unitObj;
    public bool isRight; //面朝方向是否向右
    public UnitSpineAnimationHelper spineAniamtionHelper;  //动画事件处理

    public event Action<Unit> OnUnitDead;
    public Unit() { }
    public Unit(GameObject unitObj)
    {
        this.unitObj = unitObj;
        spineAniamtionHelper = new UnitSpineAnimationHelper(this.unitObj);
        unitObj.GetComponent<Contact>().OnGetDamage += GetDamage;
    }

    public virtual void Init()
    {
        UnitDead deadState = new UnitDead(this);
        fsm.AddState(deadState);
        fsm.Init();
    }

    public void Update()
    {
        fsm.Update();
    }

    public virtual void GetDamage(float damage)
    {
        currentHp = currentHp - damage >= 0 ? currentHp - damage : 0;
        if(currentHp<=0)
        {
            fsm.SwitchToState(State.UnitDead);
        }
    }
    public void UnitDead()
    {
        OnUnitDead?.Invoke(this);
    }

    public void Flip()
    {
        isRight = !isRight;
        float angle = 180 * (isRight ? 0 : 1);
        unitObj.GetComponent<Transform>().rotation = Quaternion.Euler(0, angle, 0);
    }
    public void SetVelocity(float Speed, Vector2 velocity)
    {
        unitObj.GetComponent<Rigidbody2D>().velocity = Speed * velocity;
    }

    public void SetHpBar(bool status)
    {
        unitObj.transform.GetChild(0).gameObject.SetActive(status);
    }
}
