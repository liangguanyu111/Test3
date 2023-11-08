using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMConditionBool : FSMCondition<bool>
{
    public enum BoolCondition
    {
        True,
        Fasle
    }
 
    BoolCondition boolCondition;

    public FSMConditionBool(string conditionName, BoolCondition boolCondition)
    {
        this.ConditionName = conditionName;
        this.boolCondition = boolCondition;
    }

    public override void OnParametersChange(bool value)
    {
        switch(boolCondition)
        {
            case BoolCondition.True:
                meetCondition = value;
                break;
            case BoolCondition.Fasle:
                meetCondition = !value;
                break;
        }
        base.OnParametersChange(value);
    }
}
