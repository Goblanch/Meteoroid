using UnityEngine;

public class IdleState : PlayerState
{
    public override void StateEnter()
    {
        
    }

    public override void StateExit()
    {
        
    }

    public override void StateInputs()
    {
        if(controller.PInput.magnitude != 0) stateMachine.SetState(typeof(MovementState));
    }

    public override void StateLateStep()
    {
        
    }

    public override void StatePhysicsStep()
    {
        
    }

    public override void StateStep()
    {
        
    }
}
