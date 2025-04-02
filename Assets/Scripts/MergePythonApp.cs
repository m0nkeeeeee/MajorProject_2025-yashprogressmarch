using UnityEngine;
using System.Diagnostics;
using System.IO;

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
        // Get Python script path dynamically
        string pythonScript = Path.Combine(Application.dataPath, "ExternalApps", "quantumvisualiser.py");

        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "python"; // Assumes Python is in the system's PATH
        psi.Arguments = $"\"{pythonScript}\"";  // Wrap in quotes to handle spaces
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;  // Runs in the background
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        Process process = new Process();
        process.StartInfo = psi;

        process.OutputDataReceived += (sender, e) => { if (e.Data != null) UnityEngine.Debug.Log(e.Data); };
        process.ErrorDataReceived += (sender, e) => { if (e.Data != null) UnityEngine.Debug.LogError(e.Data); };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
    }
}
