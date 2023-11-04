using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FSMConditionBool;

public class FSMConditionFloat : FSMCondition<float>
{
    public enum FloatCondition
    {
            Greater,
            Less
    }
    new float currentValue;
    FloatCondition condition;
    public FSMConditionFloat() { }
    public FSMConditionFloat(string conditionName, FloatCondition floatCondition, float targetValue ,float currentValue =0.0f)
    {
        this.ConditionName = conditionName;
        this.condition = floatCondition;
        this.targetValue = targetValue;

        this.currentValue = currentValue;
    }

    public override void SetTargetValue(float targetValue)
    {
        this.currentValue = targetValue;
        ConditionModify();
    }

    public override bool CheckCondition()
    {
        switch (condition)
        {
            case FloatCondition.Greater:
                return currentValue>targetValue;
            case FloatCondition.Less:
                return currentValue<targetValue;
        }
        return false;
    }
}
