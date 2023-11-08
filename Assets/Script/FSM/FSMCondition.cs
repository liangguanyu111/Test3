using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来派生float，bool，trigger类条件
public abstract class FSMCondition<T> : IFSMCondition
{
    private string conditionName;               
    protected bool meetCondition = false; //条件是否满足
    public event Action OnConditionModify; //当条件的值发生变化，检测是否满足transition
    public string ConditionName { get => conditionName; set => conditionName = value; }
  
    public virtual void OnParametersChange(T value)
    {
        OnConditionModify?.Invoke();
    }

    public virtual bool CheckCondition()
    {
        return meetCondition;
    }
    public void AddConditionAction(Action check)
    {
        OnConditionModify += check;
    }
}
