using TMPro;
using UnityEngine;

public class LivesTitle : MonoBehaviour
{
    private int livesForTheGameMode;

    private void OnEnable()
    {
        GameManager.Instance.OnGamePlayStart += SetLivesOnGamePlayStart;
        GameManager.Instance.OnLevelLost += SetText;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGamePlayStart -= SetLivesOnGamePlayStart;
        GameManager.Instance.OnLevelLost -= SetText;
    }

    private void SetLivesOnGamePlayStart(ModeData modeData)
    {
        livesForTheGameMode = modeData.lives;
        GameManager.Instance.GameData.ActivePlayer.inGameLives = livesForTheGameMode;
        SetText();
    }

    private void SetText()
    {
        string lives = GameManager.Instance.GameData.ActivePlayer.inGameLives.ToString();
        GetComponent<TMP_Text>().text = "Lives: " + lives ?? "";
    }
}
