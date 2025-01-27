using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMediator : MonoBehaviour {
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private SettingsMenu _settingsMenu;

    private AudioManager audioManager;

    private void Awake() {
        _mainMenu.Configure(this);
        _settingsMenu.Configure(this);

        _settingsMenu.Hide();
    }

    private void Start() {
        audioManager = ServiceLocator.Instance.GetService<AudioManager>();
    }

    public void BackToMainMenu(){
        _mainMenu.Show();
        _settingsMenu.Hide();
    }

    public void StartGame(){
        SceneManager.LoadScene("Game");
    }

    public void GoToSettingsMenu(){
        _mainMenu.Hide();
        _settingsMenu.Show();
    }

    public void UpdateMusicVolume(float newVolume){
        audioManager.audioConfiguration.ChangeMusicVolume(newVolume);
    }
}