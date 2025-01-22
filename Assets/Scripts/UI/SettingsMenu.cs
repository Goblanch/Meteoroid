using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Buttons")] private Button _backButton;
    [field: SerializeField, BoxGroup("Canvas Group")] private CanvasGroup _canvasGroup;
    [field: SerializeField, BoxGroup("Transition")] private float fadeTime = 0.5f;

    private MenuMediator _mediator;
    
    public void Configure(MenuMediator mediator){
        _mediator = mediator;
    }

    private void Awake() {
        _backButton.onClick.AddListener(() => _mediator.BackToMainMenu());
    }

    public void Show(){
        _canvasGroup.DOFade(1f, fadeTime);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide(){
        _canvasGroup.DOFade(0f, fadeTime);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}
