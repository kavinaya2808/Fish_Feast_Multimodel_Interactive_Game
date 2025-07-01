using System.Diagnostics;
using UnityEngine;

public class PythonRunner : MonoBehaviour
{
    Process pythonProcess1;
    Process pythonProcess2;

    void Start()
    {
        StartPythonScript1();
    }

    void StartPythonScript1()
    {
        pythonProcess2 = new Process();
        pythonProcess2.StartInfo = new ProcessStartInfo()
        {
            FileName = "/opt/anaconda3/bin/python",
            Arguments = "\"/Users/kavinayakumarchokkappan/Multimodal_Interface/Assets/Fish_game/VoiceControl.py\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        pythonProcess2.OutputDataReceived += (sender, args) => UnityEngine.Debug.Log("Python2: " + args.Data);
        pythonProcess2.ErrorDataReceived += (sender, args) => UnityEngine.Debug.LogError("Python2 ERROR: " + args.Data);

        pythonProcess2.Start();
        pythonProcess2.BeginOutputReadLine();
        pythonProcess2.BeginErrorReadLine();
    }

    void OnApplicationQuit()
    {

        if (pythonProcess2 != null && !pythonProcess2.HasExited)
            pythonProcess2.Kill();
    }
}
