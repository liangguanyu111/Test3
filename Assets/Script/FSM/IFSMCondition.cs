using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSMCondition 
{
    string ConditionName { get; set; }
    bool CheckCondition();
}
