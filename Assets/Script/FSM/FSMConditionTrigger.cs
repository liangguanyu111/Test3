using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FSMConditionBool;

//Trigger 状态完成之前不能被重复触发
public class FSMConditionTrigger : FSMCondition<bool>
{
    bool countinouslyTrige;  //Trigger可以被重复触发吗
    public bool triggerLock = false;
    public FSMConditionTrigger() { }

    public FSMConditionTrigger(string conditionName,bool targetValue =false,bool countinouslyTrige = false)
    {
        this.ConditionName = conditionName;
        this.targetValue = targetValue;
        this.countinouslyTrige = false;
    }

    public override void SetTargetValue(bool targetValue)
    {
       base.SetTargetValue(targetValue);
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
