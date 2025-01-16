using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    [SerializeField] private PlayerState[] states;
    public PlayerState CurrentState {get; private set;}
    public PlayerState LastState {get; private set;}
    public PlayerController PlayerController {get; private set;}

    public void ConfigureSMachine(PlayerController controller){
        PlayerController = controller;
    }

    public void Initialize(){
        if(states == null || states.Length <= 0){
            Debug.LogError("Player state machine empty");
            return;
        }

        foreach (PlayerState state in states) state.Configure(this, PlayerController);
        SetState(states[0].GetType());
    }

    public void Step(){
        if(CurrentState == null) return;
        CurrentState.StateInputs();
        CurrentState.StateStep();
    }

    public void PhysicsStep(){
        if(CurrentState == null) return;
        CurrentState.StatePhysicsStep();
    }

    public void LateStep(){
        if(CurrentState == null) return;
        CurrentState.StateLateStep();
    }

    public void SetState(Type nextStateType){
        PlayerState nextState = QueryStateByType(nextStateType);
        if(nextState == null || nextState == CurrentState) return;

        CurrentState?.StateExit();
        CurrentState = nextState;
        CurrentState.StateEnter();
    }

    private PlayerState QueryStateByType(Type stateType){
        for(int i = 0; i < states.Length; i++){
            if(states[i].GetType() == stateType) return states[i];
        }

        return null;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
