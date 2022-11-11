using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScreenGetPrompt : MonoBehaviour
{
    public TMP_Text promptDisplay;
    private int subj =0;
    private int trial =0;
    private int i = 0;

    void Start()
    {
        subj = Int32.Parse(TrialInformation.subject_number)-1;
        trial = Int32.Parse(TrialInformation.trial_number)-1;
    }

    // Update is called once per frame
    void Update()
    {
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
