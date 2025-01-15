using UnityEngine;

[CreateAssetMenu(fileName = "InputListener", menuName = "Scriptable Objects/InputListener")]
public class InputListener : ScriptableObject
{
    public enum GameModes {
        Game, UI
    }

    private PlayerInput _pInput;

    private void OnEnable() {
        if(_pInput == null){
            _pInput = new PlayerInput();

            _pInput.Player.SetCallbacks(this);
            _pInput.UI.SetCallbacks(this);
        }
    }
}
