using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LevelManager;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject TargetsCanvas;
    [SerializeField] private List<GameObject> LevelGenerators;
    private IGenerateLevel<ModeData> ILevelGenerator;
    private ModeData modeDataAssigned;
    public interface IGenerateLevel<Data>
    {
        void GenerateLevel(Data modeData);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGamePlayStart += StartGamePlay;
        GameManager.Instance.OnTargetSelected += OnTargetSelected;
        GameManager.Instance.OnLevelStart += GenerateNewLevel;
        GameManager.Instance.OnGamePlayEnd += AssignScoreToThePlayer;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGamePlayStart -= StartGamePlay;
        GameManager.Instance.OnTargetSelected -= OnTargetSelected;
        GameManager.Instance.OnLevelStart -= GenerateNewLevel;
        GameManager.Instance.OnGamePlayEnd -= AssignScoreToThePlayer;
    }

    private void Start()
    {
        GameManager.Instance.StartGamePlay();
    }

    private void StartGamePlay(ModeData modeData)
    {
        modeDataAssigned = modeData;
        GameObject levelGeneratorPreFab = GetLevelGeneratorByMode(modeData);
        GameObject InstantiatedLevelGenerator = Instantiate(levelGeneratorPreFab, Vector3.zero, Quaternion.identity, TargetsCanvas.transform);
        ILevelGenerator = InstantiatedLevelGenerator.GetComponent<IGenerateLevel<ModeData>>();

        GameManager.Instance.StartNewLevel();
    }

    private void GenerateNewLevel()
    {
        ILevelGenerator.GenerateLevel(modeDataAssigned);
    }

    private GameObject GetLevelGeneratorByMode(ModeData modeData)
    {
        foreach (GameMode mode in Enum.GetValues(typeof(GameMode)))
        {
            if (Equals(modeData.modeName, mode))
            {
                return LevelGenerators[(int)mode];
            }

        }
        return LevelGenerators[0];
    }

    private void OnTargetSelected(bool isTarget)
    {
        PlayerData player = GameManager.Instance.GameData.ActivePlayer;
        if (isTarget)
        {
            GameManager.Instance.LevelCleared();
            GameManager.Instance.StartNewLevel();
        }

        else
        {
            player.inGameLives -= 1;
            GameManager.Instance.LevelLost();

            if (player.inGameLives == 0)
            {
                GameManager.Instance.EndGamePlay();
                return;
            }
            GameManager.Instance.StartNewLevel();
        }
    }

    private void AssignScoreToThePlayer()
    {
        PlayerData player = GameManager.Instance.GameData.ActivePlayer;
        if (player.inGameScore > player.scoresDictionary[GameManager.Instance.GameData.ActiveMode])
        {
            player.scoresDictionary[GameManager.Instance.GameData.ActiveMode] = GameManager.Instance.GameData.ActivePlayer.inGameScore;
        }
    }
}
