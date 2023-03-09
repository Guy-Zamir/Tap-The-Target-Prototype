using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
