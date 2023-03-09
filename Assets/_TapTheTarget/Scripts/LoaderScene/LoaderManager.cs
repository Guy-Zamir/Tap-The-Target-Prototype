using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Awake()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForEndOfFrame();
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync("Menu");
        while (!loadingOperation.isDone)
        {
            slider.value = loadingOperation.progress;
            yield return null;
        }
    }
}
