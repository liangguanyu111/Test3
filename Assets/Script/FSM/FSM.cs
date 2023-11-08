using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FSM
{
    public Dictionary<State, FSMState> allStatesDic;

    public Dictionary<string, FSMParameters<Boolean>> boolParameters;
    public Dictionary<string, FSMParameters<float>> floatParamerters;
    private FSMState currentState;

    public FSM(FSMState InitState)
    {
        allStatesDic = new Dictionary<State, FSMState>();

        boolParameters = new Dictionary<string, FSMParameters<bool>>();
        floatParamerters = new Dictionary<string, FSMParameters<float>>();
        currentState = InitState;
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
        //添加所有的条件--优化
        foreach (var state in allStatesDic)
        {
            foreach (var transition in state.Value.fsmTransitions)
            {
                foreach (var condition in transition.conditions)
                {                     
                    if(condition is FSMConditionFloat)
                    {
                        if(!floatParamerters.ContainsKey(condition.ConditionName))
                        {
                            floatParamerters.Add(condition.ConditionName, new FSMParameters<float>());
                        }
                        floatParamerters[condition.ConditionName].OnParametersChange += (condition as FSMConditionFloat).OnParametersChange;
                    }
                    else
                    {
                        if (!boolParameters.ContainsKey(condition.ConditionName))
                        {
                            boolParameters.Add(condition.ConditionName, new FSMParameters<bool>());
                        }
                        if(condition is FSMConditionBool)
                        boolParameters[condition.ConditionName].OnParametersChange += (condition as FSMConditionBool).OnParametersChange;
                        if(condition is FSMConditionTrigger)
                        boolParameters[condition.ConditionName].OnParametersChange += (condition as FSMConditionTrigger).OnParametersChange;
                    }
                }
            }
        }

        if (currentState != null)
        {
            currentState.OnInit();
            currentState.OnEnter();
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
        if(boolParameters.ContainsKey(conditionName))
        {
            boolParameters[conditionName].SetParametersValue(value);
        }
    }

    public void SetFloat(string conditionName,float value)
    {
        if (floatParamerters.ContainsKey(conditionName))
        {
            floatParamerters[conditionName].SetParametersValue(value);
        }
    }

    public void SetTrigger(string conditionName)
    {
        if (boolParameters.ContainsKey(conditionName))
        {
            boolParameters[conditionName].SetParametersValue(true);
        }
    }

    public float GetFloat(string conditionName)
    {
        if(floatParamerters.ContainsKey(conditionName))
        {
            return floatParamerters[conditionName].value;
        }
        return 0.0f;
    }
    #endregion 
}
