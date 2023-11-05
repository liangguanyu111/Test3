using System;

[Serializable]
public class EnemyHold : FSMState
{
    Enemy enemy;
    public EnemyHold(Enemy enemy,Action onInit = null, Action onEnter = null, Action onExit = null)
    {
        this.m_State = State.EnemyHold;
        this.enemy = enemy;
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
