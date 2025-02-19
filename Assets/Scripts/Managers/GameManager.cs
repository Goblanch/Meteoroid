using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int playerPoints { get; private set; }

    public Action<int> OnPointAdded;
    public Action OnPlayerDeath;
    public Action OnGameReset;

    private void Start()
    {
        OnGameReset += HandleGameReset;
    }

    private void OnDisable()
    {
        OnGameReset -= HandleGameReset;
    }

    public void AddPoint(int pointsToAdd)
    {
        playerPoints += pointsToAdd;
        OnPointAdded?.Invoke(playerPoints);
    }

    public void PlayerHit()
    {
        Debug.Log("Player Hit");
        GameOverSound();
        OnPlayerDeath.Invoke();
    }

    private void GameOverSound()
    {
        AudioManager audio = ServiceLocator.Instance.GetService<AudioManager>();
        audio.StopSoundsByType(SoundTypes.Music);
        audio.StopSoundsByType(SoundTypes.FX);
        audio.PlaySound("GameOver");
    }

    private void HandleGameReset(){
        playerPoints = 0;
        ServiceLocator.Instance.GetService<AudioManager>().PlaySound("Game");
    }

}