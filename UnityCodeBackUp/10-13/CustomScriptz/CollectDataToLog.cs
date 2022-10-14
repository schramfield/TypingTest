using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CollectDataToLog : MonoBehaviour
{
    string filename = "temp.txt";

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/DataCollected.txt";

        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("\n\n[" + System.DateTime.Now + "] Begin Data Collection :\n");
        tw.Close();
    }
    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("[" + System.DateTime.Now + "]" + logString);
        tw.Close();
    }
}
