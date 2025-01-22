using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Buttons")] private Button _startGameButton;
    [field: SerializeField, BoxGroup("Buttons")] private Button _settingsButton;
    [field: SerializeField, BoxGroup("Canvas Group")] private CanvasGroup _canvasGroup;

    private MenuMediator _mediator;
    
    public void Configure(MenuMediator mediator){
        _mediator = mediator;
    }

    private void Awake() {
        _startGameButton.onClick.AddListener(() => _mediator.StartGame());
        _settingsButton.onClick.AddListener(() => _mediator.GoToSettingsMenu());
    }

    public void Show(){
        _canvasGroup.DOFade(1f, 0.5f);
    }

    public void Hide(){
        _canvasGroup.DOFade(0f, 0.5f);
    }
}
