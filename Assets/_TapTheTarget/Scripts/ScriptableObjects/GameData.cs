using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
[Serializable]
public class GameData : ScriptableObject
{
    public List<PlayerData> PlayersList;
    public PlayerData ActivePlayer;
    public TargetData ActiveTarget;
    public GameMode ActiveMode;
}
