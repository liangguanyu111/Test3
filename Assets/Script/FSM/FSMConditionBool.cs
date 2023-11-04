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
    public FSMConditionBool() { }

    public FSMConditionBool(string conditionName, BoolCondition boolCondition,bool targetValue =false)
    {
        this.ConditionName = conditionName;
        this.boolCondition = boolCondition;
        this.targetValue = targetValue;
    }



    public override bool CheckCondition()
    {
        switch(boolCondition)
        {
            case BoolCondition.True:
                return targetValue==true;
            case BoolCondition.Fasle:
                return targetValue == false;
        }
        return false;
    }
}
