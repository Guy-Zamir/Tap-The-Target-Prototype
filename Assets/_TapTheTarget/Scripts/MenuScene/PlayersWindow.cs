using UnityEngine;

public class PlayersWindow : PanelsWindow
{
    [SerializeField] GameObject addNewPlayerPanel;

    public override void Open()
    {
        base.Open();
        addNewPlayerPanel.transform.transform.SetAsLastSibling();
    }

    public void AddNewPlayer()
    {
        PlayerData newPlayer = ScriptableObject.CreateInstance<PlayerData>();
        int playerIndex = 1;
        foreach (PlayerData player in GameManager.Instance.GameData.PlayersList)
        {
            playerIndex += 1;
        }
        string playerName = "Player" + playerIndex.ToString();
        newPlayer.userName = playerName;
        newPlayer.name = playerName;
        GameManager.Instance.GameData.PlayersList.Add(newPlayer);

        GameManager.Instance.ChangeActivePlayer(newPlayer);
        base.Close();
    }
}
