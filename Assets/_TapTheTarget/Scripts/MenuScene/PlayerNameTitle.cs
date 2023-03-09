using System;
using TMPro;
using UnityEngine;

public class PlayerNameTitle : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnActivePlayerChange += SetPlayerText;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnActivePlayerChange -= SetPlayerText;
    }

    private void Start()
    {
        SetPlayerText(GameManager.Instance.GameData.ActivePlayer);
    }

    private void SetPlayerText(PlayerData player)
    {
        try
        {
            GetComponent<TMP_Text>().text = player.userName;
        }
        catch (Exception ex)
        {
            Debug.LogError("Error setting player text: " + ex.Message);
        }
    }
}
