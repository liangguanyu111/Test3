using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;

public class EnemyMove : FSMState
{
    protected Enemy enemy;
    public EnemyMove(Enemy enemy,Action onInit = null, Action onEnter = null, Action onExit = null)
    {
        this.m_State = State.EnemyMove;
        this.enemy = enemy;
    }


}
