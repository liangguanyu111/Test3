using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FSMConditionBool;

public class FSMConditionTrigger : FSMCondition<bool>
{
    public FSMConditionTrigger() { }

    public FSMConditionTrigger(string conditionName,bool targetValue =false)
    {
        this.ConditionName = conditionName;
        this.targetValue = targetValue;
    }
    public override bool CheckCondition()
    {
        if(targetValue)
        {
            return true;
        }
        return false;
    }

}
