using TMPro;
using UnityEngine;
using static PanelsWindow;

public class ScorePanel : MonoBehaviour, IAssignData<PlayerData>
{
    private TMP_Text playerName;

    private TMP_Text playerScore;
    private PlayerData playerData;

    void Awake()
    {
        playerName = transform.GetChild(0).GetComponent<TMP_Text>();
        playerScore = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void AssignData(PlayerData playerToAssign)
    {
        playerData = playerToAssign;
        SetPlayerName(playerData.userName);
        SetPlayerScore(playerData.scoresDictionary[GameManager.Instance.GameData.ActiveMode]);
    }

    private void SetPlayerName(string name)
    {
        playerName.text = name;
    }

    private void SetPlayerScore(float score)
    {
        playerScore.text = "Score: " + score.ToString("0.00");
    }
}
