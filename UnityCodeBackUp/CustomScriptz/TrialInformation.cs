using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//PURPOSE:
// This script allows you to enter trial information up front and make sure your participants get the right sequence of trial events.
//
//INSTRUCTIONS:
// Attach this script to CodeBucket
// Before running a trial, make sure you have the correct subject number, trial number, and trial type entered in the inspection bar
// 

public class TrialInformation : MonoBehaviour
{
    // This establishes all the empty boxes to put info in
    // you should get an error if you forget
    // The public static info can be (and is) pulled by other scripts in your scene
    //
    public string SubjectNumber = "#";
    public static string subject_number;
    public string TrialNumber = "#";
    public static string trial_number;
    public static string TrialType = "trial_type_undeclared";
    public bool HapticsEnabled = false;
    public bool VROnly = false;
    public bool IRL = false;
    public static int subject_int = 0;
    public static int trial_int = 0;
    public static bool begin = false;

    // Start is called before the first frame update
    void Start()
    {
        // This delays until the start for Unity reasons
        // This sets some details that are required for all the other scripts to play nice
        // also makes the file names all pretty!
        //
        subject_number = SubjectNumber;
        trial_number = TrialNumber;
        if (HapticsEnabled == true)
        {
            TrialType = "_hap";
        }
        else if (VROnly == true)
        {
            TrialType = "_vr";
        }
        else if (IRL == true)
        {
            TrialType = "_irl";
        }

        subject_int = Int32.Parse(subject_number) - 1;
        trial_int = Int32.Parse(trial_number) - 1;
        begin = true;
    }

//update not required
//

}

// end of script
//
