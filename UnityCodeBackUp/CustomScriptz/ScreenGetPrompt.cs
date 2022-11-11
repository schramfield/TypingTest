using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScreenGetPrompt : MonoBehaviour
{
    public TMP_Text promptDisplay;
    private int subj = 0;
    private int trial = 0;
    private int i = 0;

    //void Start()
    //{
    //    int presubj = Int32.Parse(TrialInformation.subject_number);
    //    subj = presubj -1;
    //    int pretrial = Int32.Parse(TrialInformation.trial_number);
    //    trial = pretrial -1;
    //}

    // Update is called once per frame
    void Update()
    {
        if (TrialInformation.begin == true)
        {
            subj = TrialInformation.subject_int;
            trial = TrialInformation.trial_int;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            promptDisplay.text = TypingPrompts.listPrompts[TypingPrompts.subjects[subj][trial][i]];
            i++;
            Debug.Log("\n\n");
        }
    }
    public void EnterHit()
    {
        promptDisplay.text = TypingPrompts.listPrompts[TypingPrompts.subjects[subj][trial][i]];
        i++;
        Debug.Log("\n\n");
    }
}

// end of script
//
