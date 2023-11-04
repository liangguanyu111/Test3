using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来派生float，bool，trigger类条件
public abstract class FSMCondition<T> : IFSMCondition
{
    private string conditionName;
    public T targetValue;
    public T currentValue;

    public event Action OnConditionModify; //当条件的值发生变化，检测是否满足transition
    public string ConditionName { get => conditionName; set => conditionName = value; }

    public FSMCondition() { }
    
    public virtual void SetTargetValue(T targetValue)
    {
        this.targetValue = targetValue;
        OnConditionModify();
    }

    public virtual bool CheckCondition()
    {
        return false;
    }


    public void ConditionModify()
    {
        OnConditionModify?.Invoke();

    }

    public void AddConditionAction(Action check)
    {
        OnConditionModify += check;
    }
}
