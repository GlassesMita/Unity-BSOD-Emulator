using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingLogo : MonoBehaviour
{
    public Text logoText; // Windows 8.x、 Windows 10 和 Windows 11 使用 Segoe Boot Semilight 字体 作为·加载中·的动画
    private float frameRate = 1f / 30f; // 30FPS
    private float nextFrameTime;
    private int currentCharIndex;
    private const int startChar = 0xE052;
    private const int endChar = 0xE0CB;

    // Start is called before the first frame update
    void Start()
    {
        nextFrameTime = Time.time + frameRate;
        currentCharIndex = startChar;
        UpdateCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextFrameTime)
        {
            nextFrameTime += frameRate;
            currentCharIndex++;
            if (currentCharIndex > endChar)
            {
                currentCharIndex = startChar;
            }
            UpdateCharacter();
        }
    }

    void UpdateCharacter()
    {
        logoText.text = char.ConvertFromUtf32(currentCharIndex);
    }
}
