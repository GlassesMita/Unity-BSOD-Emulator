using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 添加对 UI 的支持

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
        SceneManager.LoadScene(targetSceneName);
    }
}
