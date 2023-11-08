using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// FSM的参数
public class FSMParameters<T>
{
   public string parametersName;
   public T value;

   public event Action<T> OnParametersChange;
    
   public void SetParametersValue(T value)
   {
       this.value = value;
       OnParametersChange?.Invoke(value);
   }
}
