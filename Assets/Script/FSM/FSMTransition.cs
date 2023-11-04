using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public  class FSMTransition 
{
    public State fromFsmState;
    public State toFsmState;

    public event Action<State,State> OnTrans;

    public int weight; //权重顺序
    public List<IFSMCondition> conditions;


    public FSMTransition(State fromFsmState, State toFsmState,int weight =0)
    {
        this.fromFsmState = fromFsmState;
        this.toFsmState = toFsmState;
        this.weight = weight;
        conditions = new List<IFSMCondition>();
    }

    public void Update()
    {
       
    }
    
    public void CheckAllCondition()
    {
        foreach (var condition in conditions)
        {
            if (!condition.CheckCondition())
            {
                return;
            }
        }
        OnTrans?.Invoke(fromFsmState, toFsmState);
    }

    public void AddCondition(IFSMCondition condition)
    {
        conditions.Add(condition);
        condition.AddConditionAction(CheckAllCondition);
    }
}
