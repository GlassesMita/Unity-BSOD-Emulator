using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LocalizationManager : MonoBehaviour
{
    [Serializable]
    public class LocalizedTextEntry
    {
        public string key; // 如 "ErrorMessage", "Percent", "Tip1", "Tip2", "StopCode"
        public Text text;
    }

    public int i = 1;

    public List<LocalizedTextEntry> localizedTextEntries; // Inspector 拖入所有需要本地化的 Text
    public string localizationFolder = Application.streamingAssetsPath + "/Localization";
    public Dictionary<string, string> localizedDict = new Dictionary<string, string>();

    public bool forceLanguage = false;
    public string forcedLanguageCode = "en-US";

    void Awake()
    {
        LoadLocalization();
    }

    public void SetText(string key, Text text, params object[] args)
    {
        if (text == null) return; // 防止空引用
        if (localizedDict != null && localizedDict.TryGetValue(key, out string value) && !string.IsNullOrEmpty(value))
        {
            text.text = string.Format(value, args);
        }
        else
        {
            text.text = key;
        }
    }

    public void SetRandomStopCode(Text text)
    {
        string stopCode = Enums.GetRandomStopCode();
        SetText("StopCode", text, stopCode);
    }

    void Start()
    {
        foreach (var entry in localizedTextEntries)
        {
            if (entry.key == "StopCode")
                SetRandomStopCode(entry.text);
            else if (entry.key == "Percent")
                StartCoroutine(UpdatePercent(entry.text));
            else
                SetText(entry.key, entry.text);
        }
    }

    private IEnumerator UpdatePercent(Text percentText)
    {
        for (i = 0; i <= 100; i++)
        {
            SetText("Percent", percentText, i);
            yield return new WaitForSeconds(2f); // 速度可调
        }
    }

    void LoadLocalization()
    {
        string langCode;
        if (forceLanguage && !string.IsNullOrEmpty(forcedLanguageCode))
        {
            langCode = forcedLanguageCode;
        }
        else
        {
            langCode = CultureInfo.CurrentCulture.Name; // zh-CN, en-US
        }
        string folderPath = localizationFolder;
        if (!folderPath.EndsWith("/")) folderPath += "/";
        string filePath = Path.Combine(folderPath, langCode + ".json");

        // 1. 优先地区+语言（如 zh-CN.json）
        // 2. 其次仅语言（如 zh.json）
        // 3. 最后英文（en-US.json 或 en.json）

        if (!File.Exists(filePath))
        {
            // 仅语言部分
            string langOnly = langCode.Split('-')[0]; // zh-CN -> zh
            filePath = Path.Combine(folderPath, langOnly + ".json");
            if (!File.Exists(filePath))
            {
                // 尝试 en-US.json
                filePath = Path.Combine(folderPath, "en-US.json");
                if (!File.Exists(filePath))
                {
                    // 尝试 en.json
                    filePath = Path.Combine(folderPath, "en.json");
                }
            }
        }

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var dict = MiniJson.Json.Deserialize(json) as Dictionary<string, object>;
            localizedDict = new Dictionary<string, string>();
            if (dict != null)
            {
                foreach (var kv in dict)
                {
                    if (kv.Value != null)
                        localizedDict[kv.Key] = kv.Value.ToString();
                }
            }
        }
        else
        {
            Debug.LogWarning($"Localization file not found: {filePath}");
        }
    }
}
