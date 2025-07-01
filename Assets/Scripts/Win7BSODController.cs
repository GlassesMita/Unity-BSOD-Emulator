using UnityEngine;
using UnityEngine.UI;

public class Win7BSODController : MonoBehaviour
{
    [Header("UI 组件")]
    public Text bsodText; // Inspector 拖入用于显示蓝屏文本的 Text

    [Header("错误信息设置")]
    [TextArea(5, 30)]
    public string stopMessageTemplate = "*** STOP: {0}\n"
        + "Some driver or system error has occurred.\n\n"
        + "If this is the first time you've seen this Stop error screen,\n"
        + "restart your computer. If this screen appears again, follow\n"
        + "these steps:\n\n"
        + "Check to make sure any new hardware or software is properly installed.\n"
        + "If this is a new installation, ask your hardware or software manufacturer\n"
        + "for any Windows updates you might need.\n\n"
        + "If problems continue, disable or remove any newly installed hardware\n"
        + "or software. Disable BIOS memory options such as caching or shadowing.\n"
        + "If you need to use Safe Mode to remove or disable components, restart\n"
        + "your computer, press F8 to select Advanced Startup Options, and then\n"
        + "select Safe Mode.\n\n"
        + "Technical information:\n"
        + "*** STOP: {0}\n\n"
        + "Collecting data for crash dump ...\n"
        + "Initializing disk for crash dump ...\n"
        + "Beginning dump of physical memory.\n"
        + "Dumping physical memory to disk: 100\n"
        + "Physical memory dump complete.\n"
        + "Contact your system administrator or technical support group for further assistance.";

    void Start()
    {
        if (bsodText != null)
        {
            string randomStopCode = Enums.GetRandomStopCode(true); // 指定 Win7 错误代码
            string stopMsg = string.Format(stopMessageTemplate, randomStopCode);
            bsodText.text = stopMsg;
        }
    }
}