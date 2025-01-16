using UnityEngine;

public class SprintState : PlayerState
{
    public override void StateEnter()
    {
        Debug.Log("SPRINT");
        controller.Motor.StartSprint();
    }

    public override void StateExit()
    {
        controller.Motor.EndSprint();
    }

    public override void StateInputs()
    {
        if(!controller.Sprint) stateMachine.SetState(typeof(MovementState));
    }

    public override void StateLateStep()
    {
        
    }

    public override void StatePhysicsStep()
    {
        
    }

    public override void StateStep()
    {
        controller.Motor.Move(controller.PInput);
        
    }
}
