using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIMediator : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Components")] private TextMeshProUGUI _pointsText;
    [field: SerializeField, BoxGroup("Components")] private GameOverTab _gameOver;

    private GameManager _gameManager;

    private void Awake() {
        _gameOver.Configure(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = ServiceLocator.Instance.GetService<GameManager>();
        _gameManager.OnPointAdded += UpdatePoints;
        _gameManager.OnPlayerDeath += _gameOver.Show;
        _gameManager.OnPlayerDeath += HidePointsText;
        _gameManager.OnGameReset += ResetUI;
    }

    private void OnDisable() {
        _gameManager.OnPointAdded -= UpdatePoints;
        _gameManager.OnPlayerDeath -= _gameOver.Show;
        _gameManager.OnPlayerDeath -= HidePointsText;
        _gameManager.OnGameReset -= ResetUI;
    }

    private void UpdatePoints(int newPoints){
        _pointsText.text = newPoints.ToString();
    }

    private void HidePointsText(){
        _pointsText.gameObject.SetActive(false);
    }

    public void ResetUI(){
        _gameOver.Hide();
        _pointsText.text = "0";
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
