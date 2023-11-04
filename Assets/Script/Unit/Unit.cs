using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
public abstract class Unit : MonoBehaviour
{

    [Header("属性")]
    public int startAttack;
    public int startHp;
    public float startHeight;
    public float startWidth;
    public float attackRange;

    //状态机
    protected FSM fsm;
    protected SkeletonGraphic sgp;
    private void Awake()
    {
        //组件获取
        sgp = this.GetComponent<SkeletonGraphic>();
        Init();
    }
    public virtual void Init()
    {
        
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
