using UnityEngine;
using System.Diagnostics;

public class RunPython : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))  // When 'V' is pressed
        {
            RunPythonScript();
        }
    }

    void RunPythonScript()
    {
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "python"; // Uses system Python
        psi.Arguments = "\"D:\\GameDevelopment\\MajorProject_2025-yashprogressmarch\\Assets\\ExternalApps\\quantumvisualiser.py\"";
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;  // Runs in the background
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        Process process = new Process();
        process.StartInfo = psi;
        process.Start();
    }
}
