using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScreenGetPrompt : MonoBehaviour
{
    public TMP_Text promptDisplay;
    private int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            promptDisplay.text = TypingPrompts.listPrompts[TypingPrompts.Subject1[i]];
            i++;
        }
    }
}

// no start function required
// end of script
//
