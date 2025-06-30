using UnityEngine;

public class SystemControl : MonoBehaviour
{
    public bool showCursor = false;
    public bool runInBackground = true;

    void Awake()
    {
        Cursor.visible = showCursor;
        Application.runInBackground = runInBackground;
    }

    void Update()
    {
        // 禁用 Alt+F4（仅限部分平台，Unity 无法完全拦截系统快捷键）
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.F4))
        {
            // 什么都不做，阻止默认行为（仅限部分平台有效）
        }
        // Control+Escape 退出
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.F4))
        {
            Application.Quit();
        }
    }
}
