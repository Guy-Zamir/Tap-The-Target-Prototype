using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
[Serializable]
public class PlayerData : ScriptableObject
{
    public string userName = "Player";
    public int coins = 0;
    public int gems = 0;
    public float inGameScore = 0;
    public float inGameLives = 3;
    public Backround ActiveBackround = Backround.Blue;
    public Dictionary<GameMode, float> scoresDictionary;


    private void OnEnable()
    {
        scoresDictionary = new Dictionary<GameMode, float>();
        foreach (GameMode value in Enum.GetValues(typeof(GameMode)))
        {
            scoresDictionary.Add(value, 0f);
        }
    }
}

