using UnityEngine;
public interface IFSMState
{
    void OnInit();
    void OnEnter();
    void OnUpdate();
    void OnExit();

}
