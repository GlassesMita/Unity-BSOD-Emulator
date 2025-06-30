using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PercentSceneLoader : MonoBehaviour
{
    public LocalizationManager localizationManager;
    public string percentKey = "Percent";
    public Text percentText;
    public string nextSceneName = "BootScene";
    public float waitAfterFull = 5f;

    public int currentPercent;
    public float percentUpdateInterval = 0.05f;
    public int percentStep = 1;
    private Coroutine percentCoroutine;

    private bool loadingStarted = false;

    void Start()
    {
        currentPercent = localizationManager != null ? localizationManager.i : 0;
        if (percentText != null)
        {
            percentCoroutine = StartCoroutine(PercentIncreaseCoroutine());
        }
    }

    void Update()
    {
        if (!loadingStarted && percentText != null && percentText.text.Contains("100"))
        {
            loadingStarted = true;
            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    private IEnumerator PercentIncreaseCoroutine()
    {
        for (currentPercent = 0; currentPercent <= 100; currentPercent += percentStep)
        {
            if (localizationManager != null)
            {
                localizationManager.SetText(percentKey, percentText, currentPercent);
            }
            else
            {
                localizationManager.SetText(percentKey, percentText, currentPercent);
            }
            yield return new WaitForSeconds(percentUpdateInterval);
        }
    }

    IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(waitAfterFull);
        SceneManager.LoadScene(nextSceneName);
    }
}
