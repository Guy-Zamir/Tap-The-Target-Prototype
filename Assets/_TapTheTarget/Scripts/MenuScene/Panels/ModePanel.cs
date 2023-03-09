using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PanelsWindow;

public class ModePanel : MonoBehaviour, IAssignData<GameMode>
{
    private TMP_Text modeName;

    private GameMode modeAssigned;
    private GameObject chooseButton;

    void Awake()
    {
        modeName = transform.GetChild(0).GetComponent<TMP_Text>();
        chooseButton = transform.GetChild(1).gameObject;
    }

    public void AssignData(GameMode modeToAssign)
    {
        modeAssigned = modeToAssign;
        SetModeName(modeToAssign);
        if (Equals(GameManager.Instance.GameData.ActiveMode, modeToAssign)) SetChooseButtonOff();
        else AddChooseButtonListener();
    }

    private void SetModeName(GameMode mode)
    {
        int index = (int)mode;
        modeName.text = Enum.GetName(typeof(GameMode), index);
    }

    private void SetChooseButtonOff()
    {
        chooseButton.GetComponent<Button>().interactable = false;
        chooseButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Acive";
    }

    private void AddChooseButtonListener()
    {
        chooseButton.GetComponent<Button>().onClick.AddListener(ChooseMode);
    }

    private void ChooseMode()
    {
        GameManager.Instance.ChangeActiveMode(modeAssigned);
        transform.GetComponentInParent<ModesWindow>().Close();
    }

}