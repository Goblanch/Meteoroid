using System;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public int playerPoints {get; private set;}

    public Action<int> OnPointAdded;

    public void AddPoint(int pointsToAdd){
        playerPoints += pointsToAdd;
        OnPointAdded?.Invoke(playerPoints);
    }
}