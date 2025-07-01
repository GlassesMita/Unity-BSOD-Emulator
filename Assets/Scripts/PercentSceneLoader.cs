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
        // 黑屏一秒
        GameObject blackScreen = new GameObject("BlackScreen");
        var canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            canvas = new GameObject("Canvas", typeof(Canvas)).GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        blackScreen.transform.SetParent(canvas.transform, false);
        var image = blackScreen.AddComponent<UnityEngine.UI.Image>();
        image.color = Color.black;
        image.rectTransform.anchorMin = Vector2.zero;
        image.rectTransform.anchorMax = Vector2.one;
        image.rectTransform.offsetMin = Vector2.zero;
        image.rectTransform.offsetMax = Vector2.zero;

        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(waitAfterFull);
        SceneManager.LoadScene(nextSceneName);
    }
}
