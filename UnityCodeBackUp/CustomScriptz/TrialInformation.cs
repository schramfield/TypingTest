using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrialInformation : MonoBehaviour
{
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
