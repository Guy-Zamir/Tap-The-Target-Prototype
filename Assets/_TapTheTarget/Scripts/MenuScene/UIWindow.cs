using UnityEngine;

public class UIWindow : MonoBehaviour
{
    private readonly float WINDOW_ANIMATION_DURATION = 0.15f;
    private GameObject closingWindowButton;
    private GameObject windowUI;

    void Awake()
    {
        try{
            closingWindowButton = transform.GetChild(0).gameObject; }
        catch {
            Debug.Log("No closing button was found"); }
        try {
            windowUI = transform.GetChild(1).gameObject; }
        catch {
            Debug.Log("No UI window was found"); }
    }

    public virtual void Open()
    {
        closingWindowButton.SetActive(true);
        Animation.Scale(windowUI, Vector3.zero, Vector3.one, WINDOW_ANIMATION_DURATION);
    }

    public virtual void Close()
    {
        closingWindowButton.SetActive(false);
        Animation.Scale(windowUI, Vector3.one, Vector3.zero, WINDOW_ANIMATION_DURATION);
    }
}
