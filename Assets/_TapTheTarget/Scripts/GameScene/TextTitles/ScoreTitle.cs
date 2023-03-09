using TMPro;
using UnityEngine;

public class ScoreTitle : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnLevelCleared += SetTextOnLevelCleared;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelCleared -= SetTextOnLevelCleared;
    }

    private void SetTextOnLevelCleared()
    {
        string score = GameManager.Instance.GameData.ActivePlayer.inGameScore.ToString(".00");
        GetComponent<TMP_Text>().text = "Score: " + score ?? "";
    }
}
