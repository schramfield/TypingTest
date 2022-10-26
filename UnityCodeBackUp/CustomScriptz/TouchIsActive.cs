using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PURPOSE:
// This script allows for multiple electrodes to be stimulated symultaneously
// while still only sending one signal through the serial port
// this cuts down on lag and keeps serial communication very simple
// the original script is for L and R index fingers only
// This can easily be expanded to include more electrode locations
//
// INSTRUCTIONS:
// Apply this script to ONLY ONE game object.
// Make sure that object is present at the start of your scene
// (My default game object for this is called "CodeBucket")
// This code is referenced by SerialPortInterractions
// AND LVersusRDetect
// These static variables can be accessed from any custom script
// simply call: TouchIsActive.RightTouch and/or TouchIsActive.LeftTouch
//

public class TouchIsActive : MonoBehaviour
{
    public static bool LeftTouch = false;
    public static bool RightTouch = false;

}
// no start and no update
// this is just storing static variables
//
// End of Script
//