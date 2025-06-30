using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArgsGetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        bool withReboot = false;
        foreach (var arg in args)
        {
            if (arg == "--with-reboot") withReboot = true;
        }
        string sceneName = withReboot ? "ModernBSODWithReboot" : "ModernBSOD";
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
