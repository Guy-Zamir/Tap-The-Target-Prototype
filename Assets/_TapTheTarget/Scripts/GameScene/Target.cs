using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public void AssignData(TargetData targetData)
    {
        SetSize(targetData.size);
        SetPosition(targetData.position);
        SetSprite(targetData.sprite);
        AddTargetListener(targetData.isTarget);
    }

    private void SetSize(Vector2 size)
    {
        GetComponent<RectTransform>().sizeDelta = size;
    }

    private void SetPosition(Vector2 position)
    {
        GetComponent<RectTransform>().localPosition = position;
    }

    private void SetSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    private void AddTargetListener(bool isTarget)
    {
        GetComponent<Button>().onClick.AddListener(() => OnTargetClick(isTarget));
    }

    private void OnTargetClick(bool isTarget)
    {
        GameManager.Instance.SelectTarget(isTarget);
    }
}
