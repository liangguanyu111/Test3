using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMove : FSMState
{
    Hero aZhai;
    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Hero Move");
        if(fsm.fsmObject.TryGetComponent<Hero>(out aZhai))
        {
            if (aZhai != null)
            {
                aZhai.PlayAnimation("back", true);
                aZhai.StartCoroutine(Move());
            }
        }
    }
    IEnumerator Move()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Debug.Log("Move Done");
        fsm.SetBool("Hold", true);
    }
}
