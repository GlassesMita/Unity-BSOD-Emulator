using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 添加对 UI 的支持
using System.Collections;

public class DelaySceneLoader : MonoBehaviour
{
    public string targetSceneName = "BSODScene";
    public float minDelay = 5f;
    public float maxDelay = 15f;

    void Start()
    {
        float delay = Random.Range(minDelay, maxDelay);
        Debug.Log($"[DelaySceneLoader] Boot delay: {delay:F2} seconds");
        Invoke("ShowUefiAndLoadTargetScene", delay);
    }

    public string hyperVText = "Microsoft Hyper-V UEFI Release v4.0.0";
    public Text hyperVTextComponent; // Inspector 拖入显示 Hyper-V 文本的 Text
    public float hyperVDisplayTime = 2.5f; // Hyper-V 文本显示时长

    void ShowUefiAndLoadTargetScene()
    {
        if (hyperVTextComponent != null)
        {
            hyperVTextComponent.text = hyperVText;
            hyperVTextComponent.gameObject.SetActive(true);
            Invoke("LoadTargetScene", hyperVDisplayTime);
        }
        else
        {
            LoadTargetScene();
        }
    }

    void LoadTargetScene()
    {
        // 模拟黑屏一秒
        StartCoroutine(BlackScreenAndLoad());
    }

    private IEnumerator BlackScreenAndLoad()
    {
        // 创建一个全屏黑色UI（如有可用Canvas，否则可自行实现）
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

        // 等待一秒
        yield return new WaitForSeconds(1f);

        // 加载目标场景
        SceneManager.LoadScene(targetSceneName);
    }
}
