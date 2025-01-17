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
        if(controller.Motor.Velocity != Vector2.zero) stateMachine.SetState(typeof(MovementState));
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
