using UnityEngine;

public class ExitGamePlay : MonoBehaviour
{
    public void OnExitGamePlay()
    {
        GameManager.Instance.EndGamePlay();
    }
}
