using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialInformation : MonoBehaviour
{
    public string SubjectNumber = "0";
    public static string subject_number;
    public string TrialNumber = "0";
    public static string trial_number;
    public static string TrialType = "trial_type_undeclared";
    public bool HapticsEnabled = false;
    public bool VROnly = false;
    public bool IRL = false;

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
    }

//update not required
//

}

// end of script
//
