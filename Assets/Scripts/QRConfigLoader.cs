using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;

public class QRConfigLoader : MonoBehaviour
{
    [System.Serializable]
    public class QRConfigEntry
    {
        public string key;      // 例如 "rickroll"
        public Sprite sprite;   // 对应的二维码图片
        public Image qrImage;   // 对应的 Image 组件
    }

    public List<QRConfigEntry> qrConfigs = new List<QRConfigEntry>();

    private string configFileName = "QRConfig";

    void Start()
    {
        string configPath = Path.Combine(Application.dataPath, configFileName);
        if (File.Exists(configPath))
        {
            string content = File.ReadAllText(configPath).Trim().ToLower();
            foreach (var entry in qrConfigs)
            {
                if (entry.qrImage != null)
                {
                    if (entry.key.ToLower() == content && entry.sprite != null)
                    {
                        entry.qrImage.sprite = entry.sprite;
                    }
                    entry.qrImage.enabled = true; // 无论是否匹配都强制显示
                }
            }
        }
        // 如果没有配置文件，则不对原有 Sprite 做任何修改
    }
}