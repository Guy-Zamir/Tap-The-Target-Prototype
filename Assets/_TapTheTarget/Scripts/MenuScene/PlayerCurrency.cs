using TMPro;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    [SerializeField] private TMP_Text playerCoinsValue;
    [SerializeField] private TMP_Text playerGemsValue;

    private void OnEnable()
    {
        GameManager.Instance.OnActivePlayerChange += SetPlayerCurrenyValues;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnActivePlayerChange -= SetPlayerCurrenyValues;
    }

    private void Start()
    {
        SetPlayerCurrenyValues(GameManager.Instance.GameData.ActivePlayer);
    }

    private void SetPlayerCurrenyValues(PlayerData player)
    {
        if (player != null)
        {
            SetCoinsValue(player.coins);
            SetGemsValue(player.gems);
        }
    }

    private void SetCoinsValue(int value)
    {
        playerCoinsValue.text = value.ToString("#,##0");
    }

    private void SetGemsValue(int value)
    {
        playerGemsValue.text = value.ToString("#,##0");
    }
}
