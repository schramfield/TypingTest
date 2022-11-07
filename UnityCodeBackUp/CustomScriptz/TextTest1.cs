using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextTest1 : MonoBehaviour
{
    public TMP_Text sampleText;

    // Update is called once per frame
    void Update()
    {
        sampleText.text = DateTime.Now.ToString();
    }
}
