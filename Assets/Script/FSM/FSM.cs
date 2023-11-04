using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class FSM
{
    public Dictionary<State, FSMState> allStatesDic;
    public Dictionary<string, IFSMCondition> allConditionsDic;
    public FSMState initState;
    private FSMState currentState;

    public GameObject fsmObject;

    public FSM(GameObject fsmObject,State initStateEnmu,FSMState InitState)
    {
        allStatesDic = new Dictionary<State, FSMState>();
        allConditionsDic = new Dictionary<string, IFSMCondition>();
        this.initState = InitState;
        this.fsmObject = fsmObject;
        AddState(initStateEnmu, InitState);

    }

    public void AddState(State stateEnum,FSMState stateAdd)
    {
        if(!allStatesDic.ContainsKey(stateEnum))
        {
            allStatesDic.Add(stateEnum,stateAdd);
            stateAdd.SetFsm(this);
        }
    }
    
    public void Update()
    {
        currentState.Update();
        currentState.Reason();
    }

    public void Init()
    {
        if(initState!=null)
        {
            currentState = initState;
            currentState.OnInit();
            currentState.OnEnter();
        }


        //添加所有的条件--优化
        foreach (var state in allStatesDic)
        {
            foreach (var transition in state.Value.fsmTransitions)
            {
                foreach (var condition in transition.conditions)
                {
                    allConditionsDic.Add(condition.ConditionName, condition);
                }
            }
        }
    }
   
    public void SwitchToState(State FromState, State ToState)
    {
        if (allStatesDic.ContainsKey(FromState)&&allStatesDic.ContainsKey(ToState))
        {
            currentState.OnExit();
            currentState = allStatesDic[ToState];
            currentState.OnEnter();         
        }
    }
    #region 设置条件
    public void SetBool(string conditionName,bool value)
    {
        if(allConditionsDic.ContainsKey(conditionName))
        {
            if (allConditionsDic[conditionName] is FSMConditionBool)
            {
                FSMConditionBool boolCondition = allConditionsDic[conditionName] as FSMConditionBool;
                boolCondition.SetTargetValue(value);
            }
        }
    }

    public void SetFloat(string conditionName,float value)
    {
        if (allConditionsDic.ContainsKey(conditionName))
        {
            if (allConditionsDic[conditionName] is FSMConditionFloat)
            {
                FSMConditionFloat boolCondition = allConditionsDic[conditionName] as FSMConditionFloat;
                boolCondition.SetTargetValue(value);
            }
        }
    }

    public void SetTrigger(string conditionName)
    {
        if (allConditionsDic.ContainsKey(conditionName))
        {
            if (allConditionsDic[conditionName] is FSMConditionTrigger)
            {
                FSMConditionTrigger triggerCondition = allConditionsDic[conditionName] as FSMConditionTrigger;
                triggerCondition.SetTargetValue(true);
            }
        }
    }
    #endregion 
}
