using System.Data;
using UnityEngine;

public class MovementState : PlayerState
{
    public override void StateEnter()
    {

    }

    public override void StateExit()
    {
        
    }

    public override void StateInputs()
    {
        if(controller.Sprint) stateMachine.SetState(typeof(SprintState));
        if(controller.Motor.Velocity == Vector2.zero) stateMachine.SetState(typeof(IdleState));
    }

    public override void StateLateStep()
    {
        
    }

    public override void StatePhysicsStep()
    {
        
    }

    public override void StateStep()
    {
        //controller.Motor.Move(controller.PInput);
    }
}
