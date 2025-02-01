using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameUIMediator : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Components")] private TextMeshProUGUI _pointsText;

    private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = ServiceLocator.Instance.GetService<GameManager>();
        _gameManager.OnPointAdded += UpdatePoints;
    }

    private void OnDisable() {
        _gameManager.OnPointAdded -= UpdatePoints;
    }

    private void UpdatePoints(int newPoints){
        _pointsText.text = newPoints.ToString();
    }
}
