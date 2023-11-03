using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来派生float，bool，trigger类条件
public abstract class FSMCondition<T> : IFSMCondition
{
    private string conditionName;
    public T targetValue;
    public T currentValue;
    public string ConditionName { get => conditionName; set => conditionName = value; }

    public FSMCondition() { }
    public FSMCondition(string conditionName,T targetValue)
    {
        this.conditionName = conditionName;
        this.targetValue = targetValue;
    }
    
    public virtual void SetTargetValue(T targetValue)
    {
        this.targetValue = targetValue;
    }

    public virtual bool CheckCondition()
    {
        return false;
    }
}
