using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverTab : MonoBehaviour{

    [SerializeField, BoxGroup("Buttons")] private Button _replayButton;
    [SerializeField, BoxGroup("Buttons")] private Button _quitButton;
    [SerializeField, BoxGroup("Text")] private TextMeshProUGUI _scoreText;
    [SerializeField, BoxGroup("Canvas Group")] private CanvasGroup _canvasGroup;

    private GameUIMediator _gameUIMediator;

    public void Configure(GameUIMediator _gameUIMediator){
        this._gameUIMediator = _gameUIMediator;
    }

    private void Awake() {
        _replayButton.onClick.AddListener(() => ServiceLocator.Instance.GetService<GameManager>().OnGameReset?.Invoke());
        _quitButton.onClick.AddListener(() => _gameUIMediator.GoToMainMenu());
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    public void Show(){
        _canvasGroup.DOFade(1f, 0.5f);
        _scoreText.text = ServiceLocator.Instance.GetService<GameManager>().playerPoints.ToString();
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }

    public void Hide(){
        _canvasGroup.DOFade(0f, 0.5f);
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }
}