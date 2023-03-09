using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    private bool isTimerRunning = false;
    private float remeningTime = 5;
    private float durationForEachLevel = 5;

    private void OnEnable()
    {
        GameManager.Instance.OnGamePlayStart += SetTimerDurtion;
        GameManager.Instance.OnLevelStart += SetTimerOnStart;
        GameManager.Instance.OnLevelCleared += AddScoreOnLevelCleared;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGamePlayStart -= SetTimerDurtion;
        GameManager.Instance.OnLevelStart -= SetTimerOnStart;
        GameManager.Instance.OnLevelCleared -= AddScoreOnLevelCleared;
    }
    private void Update()
    {
        TimerRunning();
    }
    private void SetTimerDurtion(ModeData modeData)
    {
        if (modeData != null)
        {
            durationForEachLevel = modeData.durationToSolve;
            remeningTime = durationForEachLevel;
        }
    }

    private void SetTimerOnStart()
    {
        remeningTime = durationForEachLevel;
        isTimerRunning = true;
        GetComponent<TMP_Text>().text = FloatToStringFormatter(remeningTime);
    }

    private void TimerRunning()
    {
        if (isTimerRunning)
        {
            GetComponent<TMP_Text>().text = FloatToStringFormatter(remeningTime);
            remeningTime -= Time.deltaTime;

            if (remeningTime <= 0)
            {
                isTimerRunning = false;
                GetComponent<TMP_Text>().text = "00:00";

                GameManager.Instance.GameData.ActivePlayer.inGameLives -= 1;
                GameManager.Instance.LevelLost();

                if (GameManager.Instance.GameData.ActivePlayer.inGameLives > 0)
                {
                    GameManager.Instance.StartNewLevel();
                }

                else
                {
                    GameManager.Instance.EndGamePlay();
                }
            }
        }
    }

    private void AddScoreOnLevelCleared()
    {
        GameManager.Instance.GameData.ActivePlayer.inGameScore += remeningTime;
    }

    private string FloatToStringFormatter(float seconds)
    {
        int milliseconds = (int)((seconds - Mathf.Floor(seconds)) * 1000);
        return string.Format("{0:00}:{1:00}", Mathf.Floor(seconds), milliseconds / 10);
    }
}
