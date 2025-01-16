using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController controller;
    
    public void Configure(PlayerStateMachine stateMachine, PlayerController controller){
        this.stateMachine = stateMachine;
        this.controller = controller;
    }

    public abstract void StateInputs();
    public abstract void StateEnter();
    public abstract void StateStep();
    public abstract void StatePhysicsStep();
    public abstract void StateLateStep();
    public abstract void StateExit();
}
