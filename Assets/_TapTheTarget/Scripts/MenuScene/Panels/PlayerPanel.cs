using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PanelsWindow;

public class PlayerPanel : MonoBehaviour, IAssignData<PlayerData>
{
    private TMP_Text playerName;

    private GameObject chooseButton;
    private PlayerData playerData;

    void Awake()
    {
        playerName = transform.GetChild(0).GetComponent<TMP_Text>();
        chooseButton = transform.GetChild(1).gameObject;
    }

    public void AssignData(PlayerData playerToAssign)
    {
        playerData = playerToAssign;
        SetPlayerName(playerData.userName);
        if (ReferenceEquals(GameManager.Instance.GameData.ActivePlayer, playerData)) SetChooseButtonOff();
        else AddButtonListener();
    }

    private void SetPlayerName(string name)
    {
        playerName.text = name;
    }

    private void SetChooseButtonOff()
    {
        chooseButton.GetComponent<Button>().interactable = false;
        chooseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Active";
    }

    private void AddButtonListener()
    {
        chooseButton.GetComponent<Button>().onClick.AddListener(ChoosePlayer);
    }

    private void ChoosePlayer()
    {
        GameManager.Instance.ChangeActivePlayer(playerData);
        transform.GetComponentInParent<PlayersWindow>().Close();
    }
}