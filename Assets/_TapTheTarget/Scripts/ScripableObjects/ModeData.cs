using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModeData")]
[Serializable]
public class ModeData : ScriptableObject
{
    public List<TargetData> targetDataList = new List<TargetData>();

    public GameMode modeName = GameMode.Easy;
    public int lives = 3;
    public float durationToSolve = 5;
    public int numberOfTargetsOnScreen = 5;
    public Vector2 targetSize = new(100, 100);
    public List<Vector2> targetPositions = new()
    {
        new Vector2(-300, -400),
        new Vector2(0, -400),
        new Vector2(300, -400),
        new Vector2(-300, -100),
        new Vector2(0, -100),
        new Vector2(300, -100),
        new Vector2(-300, 200),
        new Vector2(0, 200),
        new Vector2(300, 200),
        new Vector2(-300, 500),
        new Vector2(0, 500),
        new Vector2(300, 500),
    };
}
