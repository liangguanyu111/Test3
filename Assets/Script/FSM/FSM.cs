using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class FSM
{
    public Dictionary<State, FSMState> allStatesDic;
    public Dictionary<string, List<IFSMCondition>> allConditionsDic;
    public FSMState initState;
    private FSMState currentState;


    public FSM(FSMState InitState)
    {
        allStatesDic = new Dictionary<State, FSMState>();
        allConditionsDic = new Dictionary<string, List<IFSMCondition>>();
        this.initState = InitState;
        currentState = this.initState;
        AddState(InitState);

    }
    public void AddState(FSMState stateAdd)
    {
        if(!allStatesDic.ContainsKey(stateAdd.m_State))
        {
            allStatesDic.Add(stateAdd.m_State, stateAdd);
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
                    if (allConditionsDic.ContainsKey(condition.ConditionName)&&allConditionsDic[condition.ConditionName].Count>0)
                    {
                        allConditionsDic[condition.ConditionName].Add(condition);
                    }
                    else
                    {
                        allConditionsDic.Add(condition.ConditionName, new List<IFSMCondition>() {condition});
                    }
                }
            }
        }
    }
   
    public void SwitchToState(State FromState, State ToState)
    {
        if (allStatesDic.ContainsKey(FromState)&&allStatesDic.ContainsKey(ToState) && currentState.m_State == FromState)
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
            foreach (var condition in allConditionsDic[conditionName])
            {
                FSMConditionBool boolCondition = condition as FSMConditionBool;
                boolCondition.SetTargetValue(value);
            }
        }
    }

    public void SetFloat(string conditionName,float value)
    {
        if (allConditionsDic.ContainsKey(conditionName))
        {
            foreach (var condition in allConditionsDic[conditionName])
            {
               FSMConditionFloat floatCondition = condition as FSMConditionFloat;
               floatCondition.SetTargetValue(value);
            }
        }
    }

    public void SetTrigger(string conditionName)
    {
        if (allConditionsDic.ContainsKey(conditionName))
        {
            foreach (var condition in allConditionsDic[conditionName])
            {
                FSMConditionTrigger triggerCondition = condition as FSMConditionTrigger;
                triggerCondition.SetTargetValue(true);
            }
        }
    }


    #endregion 
}
