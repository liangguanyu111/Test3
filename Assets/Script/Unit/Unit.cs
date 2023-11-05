using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
public abstract class Unit 
{

    [Header("属性")]
    public int startAttack;
    public int startHp;
    public float startHeight;
    public float startWidth;
    public float attackRange;

    //状态机
    protected FSM fsm;
    public SkeletonGraphic sgp;
    public GameObject unitObj;



    public Unit(GameObject unitObj)
    {
        Debug.Log("Unit Init");
        sgp = unitObj.GetComponent<SkeletonGraphic>();
        Init();
    }

    public virtual void Init()
    {
        fsm.Init();
    }

    public void Update()
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
