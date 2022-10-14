using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendDatapoints : MonoBehaviour
{
    public string[] strStatements;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            RandomDebug();
    }

    void RandomDebug()
    {
        Debug.Log(strStatements[Random.Range(0, strStatements.Length)]);

    }
}
