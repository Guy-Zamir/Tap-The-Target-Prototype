using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public GameData GameData; //{ get; private set; }

    public List<ModeData> ModesDataList = new();

    public delegate void ActivePlayerChangedDelegate(PlayerData player);
    public event ActivePlayerChangedDelegate OnActivePlayerChange;

    public delegate void ActiveModeChangedDelegate(GameMode mode);
    public event ActiveModeChangedDelegate OnActiveModeChange;

    public delegate void TargetObjectiveChagedDelegate(TargetData target);
    public event TargetObjectiveChagedDelegate OnTargetObjectiveChange;

    public delegate void OnTargetSelectDelegate(bool isTarget);
    public event OnTargetSelectDelegate OnTargetSelected;

    public delegate void OnGamePlayStartDelegate(ModeData modeData);
    public event OnGamePlayStartDelegate OnGamePlayStart;

    public delegate void OnGamePlayEndDelegate();
    public event OnGamePlayEndDelegate OnGamePlayEnd;

    public delegate void OnLevelStartDelegate();
    public event OnLevelStartDelegate OnLevelStart;

    public delegate void OnLevelClearedDelegate();
    public event OnLevelClearedDelegate OnLevelCleared;

    public delegate void OnLevelLostDelegate();
    public event OnLevelLostDelegate OnLevelLost;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        SetModesData();
        GameData = SaveSystem.Load();



        Application.targetFrameRate = 60;
    }

    public void ChangeActivePlayer(PlayerData chosenPlayer)
    {
        GameData.ActivePlayer = chosenPlayer;
        OnActivePlayerChange?.Invoke(chosenPlayer);
    }

    public void ChangeActiveMode(GameMode chosenMode)
    {
        GameData.ActiveMode = chosenMode;
        OnActiveModeChange?.Invoke(chosenMode);
    }

    public void ChangeTargetObjective(TargetData chosenTarget)
    {
        GameData.ActiveTarget = chosenTarget;
        OnTargetObjectiveChange?.Invoke(chosenTarget);
    }

    public void SelectTarget(bool isTarget)
    {
        OnTargetSelected?.Invoke(isTarget);
    }

    public void StartGamePlay()
    {
        GameData.ActivePlayer.inGameScore = 0;
        ModeData mode = GetLevelModeGenerator(GameData.ActiveMode);
        OnGamePlayStart?.Invoke(mode);
    }

    public void StartNewLevel()
    {
        OnLevelStart?.Invoke();
    }

    public void EndGamePlay()
    {
        OnGamePlayEnd?.Invoke();
        SceneManager.LoadScene("Menu");
    }

    public void LevelCleared()
    {
        OnLevelCleared?.Invoke();
    }

    public void LevelLost()
    {
        OnLevelLost?.Invoke();
    }

    private void SetModesData()
    {
        foreach (GameMode mode in Enum.GetValues(typeof(GameMode)))
        {
            int index = (int)mode;
            string name = Enum.GetName(typeof(GameMode), index);
            ModesDataList.Add((ModeData)Resources.Load("ModesData/" + name + "ModeData"));
        }
    }

    private ModeData GetLevelModeGenerator(GameMode gameModeEnum)
    {
        foreach (GameMode mode in Enum.GetValues(typeof(GameMode)))
        {
            if (Equals(gameModeEnum, mode))
            {
                return ModesDataList[(int)mode];
            }

        }
        return null;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) SaveSystem.Save(GameData);
    }

    private void OnApplicationQuit()
    {
        SaveSystem.Save(GameData);
    }
}
