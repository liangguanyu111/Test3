using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FSMConditionBool;

public class FSMConditionFloat : FSMCondition<float>
{
    public enum FloatCondition
    {
            Greater,
            Less,
            NotEqual
    }
    FloatCondition condition;
    private float targetValue;
    public FSMConditionFloat(string conditionName, FloatCondition floatCondition, float targetValue)
    {
        this.ConditionName = conditionName;
        this.condition = floatCondition;
        this.targetValue = targetValue;

    }
    public override void OnParametersChange(float value)
    {
        switch (condition)
        {
            case FloatCondition.Greater:
                meetCondition = value > targetValue;
                break;
            case FloatCondition.Less:
                meetCondition = value < targetValue;
                break;
            case FloatCondition.NotEqual:
                meetCondition = value != targetValue;
                break;
        }
        base.OnParametersChange(value);
    }
}
