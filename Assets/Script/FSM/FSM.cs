using System;
using System.Collections;
using System.Collections.Generic;
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
            initState.OnInit();
            initState.OnEnter();
            currentState = initState;
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

    }
    #endregion 
}
