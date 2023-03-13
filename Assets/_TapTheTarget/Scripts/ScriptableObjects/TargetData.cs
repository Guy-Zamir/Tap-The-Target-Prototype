using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetData")]
[Serializable]
public class TargetData : ScriptableObject
{
    public TargetObjective targetObjective;
    public string tragetObjectiveString;
    public Sprite sprite;
    public Vector2 size;
    public Vector2 position;
    public bool isTarget;
}
