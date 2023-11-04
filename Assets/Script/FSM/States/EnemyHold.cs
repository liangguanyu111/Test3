using System;

[Serializable]
public class EnemyHold : FSMState
{
    Enemy enemy;
    public EnemyHold(Action onInit = null, Action onEnter = null, Action onExit = null) : base(onInit, onEnter, onExit)
    {
        this.m_State = State.EnemyHold;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnInit()
    {
        base.OnInit();
        //fsm.SetBool("Walk", true);
    }
}
