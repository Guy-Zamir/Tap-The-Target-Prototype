using System;
using TMPro;
using UnityEngine;

public class ModeNameTitle : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnActiveModeChange += SetModeText;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnActiveModeChange -= SetModeText;
    }
    private void Start()
    {
        SetModeText(GameManager.Instance.GameData.ActiveMode);
    }

    private void SetModeText(GameMode mode)
    {
        int index = (int)mode;
        GetComponent<TMP_Text>().text = "Mode:" + Enum.GetName(typeof(GameMode), index);
    }
}
