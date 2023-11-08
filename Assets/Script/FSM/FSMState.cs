using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public enum State
{
    //Enemy
    EnemyMove =0,
    EnemyHold =1,
    EnemyAttack =2,
    EnemyNormalMove =3,
    EnemyMeleeAttack =4,
    EnemyShootHold =5,
    EnemyBlinkMove =6,
    EnemyShootAttack =7,
    //Hero
    HeroHold =8,
    HeroAttack =9,
    HeroMove = 10,

    UnitDead =11
}


[Serializable]
public abstract class FSMState :IFSMState
{
    public FSM fsm;
    public State m_State;
    private event Action m_OnInit;
    private event Action m_OnEnter;
    private event Action m_OnExit;

    public List<FSMTransition> fsmTransitions;

    //是否是第一次初始化
    public bool isInit;
    public FSMState(Action onInit =null,Action onEnter =null,Action onExit =null)
    {
        this.m_OnInit = onInit;
        this.m_OnEnter = onEnter;
        this.m_OnExit = onExit;
        this.fsmTransitions = new List<FSMTransition>();
        isInit = false;
    }

    public void SetFsm(FSM fsm)
    {
        this.fsm = fsm;
    }

    public virtual void AddTransition(FSMTransition newTranstion)
    {
        newTranstion.OnTrans +=fsm.SwitchToState;
        fsmTransitions.Add(newTranstion);
    }

    public virtual void AddTransition(State toFsmState, int weight = 0)
    {
        FSMTransition newTranstion = new FSMTransition(m_State, toFsmState, weight);
        newTranstion.OnTrans += fsm.SwitchToState;
        fsmTransitions.Add(newTranstion);
    }
    public virtual void Update()
    {
       
    }
    public virtual void Reason()
    {
       
    }

    public virtual void OnEnter()
    {
        m_OnEnter?.Invoke();
        if(isInit)
        {
            isInit = false;
            return;
        }
    }


    public virtual void OnExit()
    {
        m_OnExit?.Invoke();
    }

    public virtual void OnInit()
    {
        m_OnInit?.Invoke();
        isInit = true;
    }

    public virtual void OnUpdate()
    {
        
    }


}
