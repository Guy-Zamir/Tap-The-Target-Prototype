using System;
using TMPro;
using UnityEngine;

public class ModesWindow : PanelsWindow
{
    [SerializeField] TMP_Text activeModeText;

    protected override void SetValues()
    {
        foreach (GameMode mode in Enum.GetValues(typeof(GameMode)))
        {
            GameObject Panel = Instantiate(panelPrefab, scrollContent, false);
            IAssignData<GameMode> assignData = Panel.GetComponent<IAssignData<GameMode>>();
            assignData.AssignData(mode);
            panelsArray.Add(Panel);
        }
    }
}
