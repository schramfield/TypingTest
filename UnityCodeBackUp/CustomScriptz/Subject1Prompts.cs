using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Subject1Prompts : MonoBehaviour
{
    public TMP_Text promptDisplay;
    //string filler = "Good Job! \n Rest as long as you want, \n then press 'Enter' when you are ready to begin the next prompt.";
    //private string prompt1 = "Prompt #1!";
    //private string prompt2 = "Prompt #2!";
    //private string prompt3 = "Prompt #3!";
    //private string prompt4 = "Prompt #4!";
    private int i = 0;
    string[] listPrompts = { 
        "prompt1",
        "Good Job! \n Rest as long as you want, \n then press 'Enter' when you are ready to begin the next prompt.",
        "prompt3",
        "Good Job! \n Rest as long as you want, \n then press 'Enter' when you are ready to begin the next prompt.",
        "Prompt 4"}; //= prompt1, prompt2, prompt3, prompt4;
    //ListPrompts[0] = prompt1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            promptDisplay.text = listPrompts[i];
            i++;
        }

    }
}
