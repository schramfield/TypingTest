using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PURPOSE:
// This simple script sends key press data to the Debug log
// This is a very easy way to collect data from Unity
//
// INSTRUCTIONS:
// Use this script to get data when an object is interracted with.
// Put this script on a game object such as CodeBucket 
// Apply the Unity Event Wrapper script (or similar) to the game object you want to gather data from
// Drag CodeBucket into the desired Unity Event Wrapper Trigger (probaby When Select() or When Unselect() )
// Use the drop down to select "Key Press.KeyIdentity"
// Enter the message you would like to appear in your data collection in the variable space
// (for a keyboard key, enter the key's value)
//
public class KeyPress : MonoBehaviour
{
    public void KeyIdentity(string KeyID)
    {
        Debug.Log(KeyID);
    }
    // no update or start required
    //
}
// End of Script
//