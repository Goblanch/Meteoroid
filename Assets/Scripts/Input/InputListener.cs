using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "InputListener", menuName = "Scriptable Objects/InputListener")]
public class InputListener : ScriptableObject, PlayerInput.IPlayerActions, PlayerInput.IUIActions
{
    public enum GameModes
    {
        Game, UI
    }

    public Action<Vector2> MoveEvent;
    public Action<bool> SprintEvent;
    public Action BreakEvent;
    public Action ShootEvent;
    public Action PauseEvent;
    public Action ResumeEvent;

    private PlayerInput _pInput;

    private void OnEnable()
    {
        if (_pInput == null)
        {
            _pInput = new PlayerInput();

            _pInput.Player.SetCallbacks(this);
            _pInput.UI.SetCallbacks(this);

            ChangeGameMode(GameModes.Game);
        }
    }

    private void OnDisable()
    {
        _pInput.UI.Disable();
        _pInput.Player.Disable();
    }

    public void ChangeGameMode(GameModes gMod)
    {
        switch (gMod)
        {
            case GameModes.Game:
                _pInput.Player.Enable();
                _pInput.UI.Disable();
                break;

            case GameModes.UI:
                _pInput.UI.Enable();
                _pInput.Player.Disable();
                break;
        }
    }

    #region INTERFACE IMPLEMENTATION

    public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.phase == UnityEngine.InputSystem.InputActionPhase.Performed){
            ShootEvent?.Invoke();
        }
    }

    public void OnPauseGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.phase == UnityEngine.InputSystem.InputActionPhase.Performed){
            PauseEvent?.Invoke();
        }
    }

    public void OnResumeGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.phase == UnityEngine.InputSystem.InputActionPhase.Performed){
            ResumeEvent?.Invoke();
        }
    }

    public void OnSprint(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed){
            SprintEvent?.Invoke(true);
        }

        if(context.phase == InputActionPhase.Canceled){
            SprintEvent?.Invoke(false);
        }
    }

    public void OnBreak(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed){
            BreakEvent?.Invoke();
        }

        if(context.phase == InputActionPhase.Canceled){
            BreakEvent?.Invoke();
        }
    }



    #endregion
}
