using System.Collections.Generic;
using UnityEngine;

public abstract class PanelsWindow : UIWindow
{
    [SerializeField] protected GameObject panelPrefab;
    [SerializeField] protected RectTransform scrollContent;
    protected List<GameObject> panelsArray = new();

    public interface IAssignData<Data>
    {
        void AssignData(Data player);
    }

    public override void Open()
    {
        base.Open();
        SetValues();
    }

    public override void Close()
    {
        base.Close();
        ClearArrayData();
    }

    protected virtual void SetValues()
    {
        RectTransform contantRect = scrollContent.GetComponent<RectTransform>();
        contantRect.sizeDelta = new Vector2(contantRect.sizeDelta.x, 400);
        foreach (PlayerData player in GameManager.Instance.GameData.PlayersList)
        {
            GameObject Panel = Instantiate(panelPrefab, scrollContent, false);

            IAssignData<PlayerData> assignData = Panel.GetComponent<IAssignData<PlayerData>>();
            assignData.AssignData(player);

            panelsArray.Add(Panel);
            contantRect.sizeDelta = new Vector2(contantRect.sizeDelta.x, contantRect.sizeDelta.y + Panel.GetComponent<RectTransform>().sizeDelta.y);
            contantRect.position = Vector2.zero;
            if (ReferenceEquals(GameManager.Instance.GameData.ActivePlayer, player)) Panel.transform.SetAsFirstSibling();
        }
    }

    private void ClearArrayData()
    {
        foreach (GameObject itemPanel in panelsArray)
        {
            Destroy(itemPanel);
        }
        panelsArray.Clear();
    }
}
