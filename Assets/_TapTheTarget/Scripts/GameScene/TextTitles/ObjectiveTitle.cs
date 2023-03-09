using TMPro;
using UnityEngine;

public class ObjectiveTitle : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnTargetObjectiveChange += SetText;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTargetObjectiveChange -= SetText;
    }

    private void SetText(TargetData target)
    {
        GetComponent<TMP_Text>().text = target != null ? target.tragetObjectiveString : "";
    }
}
