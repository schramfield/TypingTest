using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// PURPOSE:
// This script sends the debug log into a text file
// It is a very simple way to collect data
// INSTRUCTIONS:
// Attach this script to a game object like CodeBucket
// It will automatically log any PUBLIC debug log statements
// PRIVATE debug log statements (in private variables, classes, etc) will not appear
// tag debug log statments as needed to make them easy to analyze with a python script later
//

public class CollectDataToLog : MonoBehaviour
{
    // there needs to be a temporary name here or errors spring, idk
    //
    string filename = "temp.txt";
    //public string SubjectNumber = "#";
    //public string TrialNumber = "# + vr/hap/irl";


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
        // Put any filename you desire in place of DataCollected.txt
        //
        filename = Application.dataPath + "/DataCollected/subject" + TrialInformation.subject_number + "_trial" + TrialInformation.trial_number + TrialInformation.TrialType + ".txt";

        // This will write a new file or append to an existing file of the given filename
        //
        TextWriter tw = new StreamWriter(filename, true);

        // Adding some line breaks so start of data is easy to identify
        // including time logged with every datapoint
        //
        tw.WriteLine("\n\n[" + System.DateTime.Now + "] Begin Data Collection for subject "+ TrialInformation.subject_number + " trial " + TrialInformation.trial_number +":\n");
        tw.Close();
    }

    // This is the code for getting the log up and running
    //
    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("[" + System.DateTime.Now + "]" + logString);
        tw.Close();
    }

    // no Update required
    //
}
// End of Script
//
