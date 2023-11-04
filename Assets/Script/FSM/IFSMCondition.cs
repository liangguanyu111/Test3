using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IFSMCondition 
{
    string ConditionName { get; set; }
    bool CheckCondition();
    void AddConditionAction(Action check);
}
