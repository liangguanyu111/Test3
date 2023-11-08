using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FSMConditionBool;

//Trigger 状态完成之前不能被重复触发
public class FSMConditionTrigger : FSMCondition<bool>
{
    public FSMConditionTrigger() { }
    public FSMConditionTrigger(string conditionName)
    {
        this.ConditionName = conditionName;
    }
    public override void OnParametersChange(bool value)
    {
        meetCondition = true;
        base.OnParametersChange(value);
        meetCondition = false;
    }
}
