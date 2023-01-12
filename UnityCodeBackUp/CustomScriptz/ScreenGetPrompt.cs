using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// PURPOSE:
// This script changes which prompt will display on the screen
// It interracts with the TypingPrompts script, TrialInformation script, and that ScreenText game object
//
// INSTRUCTIONS:
// Confusingly, you do not apply this script to the ScreenText game object
// I have it applied to an invisible intangible object called "Typing Prompts" (it also holds the TypingPrompts Script)
// After applying this script to an object, drag the desired Text Mesh Pro game object that you want to use into the "prompt display" box in the inspector
// to change the prompts mess iwth the TypingPrompts script or the TrialInformation script

public class ScreenGetPrompt : MonoBehaviour
{
    public TMP_Text promptDisplay;
    private int subj = 0;
    private int trial = 0;
    private int i = 0;

    // Update is called once per frame
    void Update()
    {

        // Wait for TrialInformation to intiate before you run, or you get blank info
        //
        if (TrialInformation.begin == true)
        {
            // pull from global variables
            //
            subj = TrialInformation.subject_int;
            trial = TrialInformation.trial_int;
        }

        // changes the display when you hit the "enter" key, before that it's whatever you put in the screen object
        // This is the version that works with an IRL keyboard only
        //
        if (Input.GetKeyDown(KeyCode.Return))
        {
            promptDisplay.text = TypingPrompts.listPrompts[TypingPrompts.subjects[subj][trial][i]];
            i++;

            // added some spaces to make the log easier to read...
            //
            Debug.Log("\n\n");
        }
    }

    // Here's the function call version that can be activated by a virtual key-press
    //
    public void EnterHit()
    {
        promptDisplay.text = TypingPrompts.listPrompts[TypingPrompts.subjects[subj][trial][i]];
        i++;
        Debug.Log("\n\n");
    }
}

// end of script
//
