using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class UnitDead : FSMState
{
    Unit unit;
    public UnitDead(Unit unit,Action onInit = null, Action onEnter = null, Action onExit = null)
    {
        this.m_State = State.UnitDead;
        this.unit = unit;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        unit.SetVelocity(0, new Vector2(0, 0));
        unit.SetHpBar(false);
        unit.unitObj.GetComponent<BoxCollider2D>().enabled = false;
        unit.unitObj.GetComponent<Contact>().enabled = false;
        unit.spineAniamtionHelper.PlayAnimation("dead", false, () => { unit.UnitDead(); });
    }
}
