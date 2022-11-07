using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

// PURPOSE:
// This script controls all serial port communication
// It can be called from other game objects
//
// INSTRUCTIONS:
// Apply this script to ONLY ONE game object.
// Make sure that object is present at the start of your scene
// (My default game object for this is called "CodeBucket")
// All haptic interractables will need to use this script
// To connect haptic interactables with this script, I recommend attaching the Unity Event Wrapper script to the interactable game object
// drag the CodeBucket into the desired Unity Event Wrapper event on the interactable object's inspector (usually "On Select", and "On Unselect")
// CodeBucket will have a drop down menu, select "SerialPortInterractions"
// From there choose "Write To Arduino"
//
// SPECIAL NOTE:
// ORDER MATTERS in the Unity Event Wrapper
// make sure it calls the engage/disengage LVersusRDetect script before you trigger this script
// otherwise you'll be using the old position data
//

public class SerialPortInterractions : MonoBehaviour
{
    // COM21 is the correct port for the IMAS computer, use Arduino IDE to check port on other machines
    // You can edit the starting value in the inspector
    //
    public string com = "COM21";
    private int brate = 9600;
    private int timeOut = 50;
    private SerialPort stream;
    //public byte[] one = BitConverter.GetBytes(1);
    ////public char[] two = { '2', '0' };
    //public byte[] two = BitConverter.GetBytes(2);
    //public byte[] three = BitConverter.GetBytes(3);
    //public byte[] zero = BitConverter.GetBytes(0);


    // This function opens the com port for serial communcation
    // the function uses a try/catch protocol
    // 
    public string assertCOMport()
    {
        string[] ports = SerialPort.GetPortNames();
        int i = 0;

        // first try the designated serial port that should be correct
        //
        try
        {
            stream = new SerialPort(com, brate);
            stream.ReadTimeout = timeOut;
            stream.Open();
            Debug.Log("Port 21 successfully opened.");
            return com;
        }
        // If this fails, all ports are tried in order
        //
        catch
        {
            com = ports[i];

            // issue a warning if there are multiple open ports
            // this can be a huge headache because some computers will successfully connect random open ports
            // 
            if (ports.Length > 1)
            {
                Debug.Log("Warning: multiple open serial ports");
            }

            // list all the ports for debugging purposes
            //
            Debug.Log("The following serial ports were found:");
            foreach (string port in ports)
            {
                Debug.Log(port.ToString());
            }

            // try all the ports in order starting with 0
            // (i is used at the end so start the loop at 1)
            //
            for (i = 1; i < ports.Length; i++)
            {
                Debug.Log("Trying port...");
                Debug.Log(com);

                // if this fails the catch will get it
                // HOWEVER
                // just because it works does not mean it is connected to the correct port
                // some computers will connect to open but empty ports so beware
                //
                try
                {
                    stream = new SerialPort(com, brate);
                    stream.ReadTimeout = timeOut;
                    stream.Open();
                    Debug.Log("Port successfully opened.");
                    return ports[i].ToString();
                }
                catch (System.Exception)
                {
                    Debug.Log("Port failed.");
                    com = ports[i];
                }
            }
        }

        // if every single port fails you will get this error
        //
        Debug.Log("Error: No viable ports.");

        return ports[0].ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        // On start-up all this script needs to do is open serial port communication
        //
        Debug.Log("CodeBucket sequence initated...");
        com = assertCOMport();

    }

    public void WriteToArduino()
    {
        // Call this function whenever you want to engage haptic feedback
        // Current system with stimulation for L and R index fingers has only 4 possible states
        // Any number of states can be added here to accomodate all electrodes in use
        // This portion can be edited to send whatever message your Arduino is looking for
        // for my script: 1 = LEFT, 2 = RIGHT, 3 = BOTH, and 0 = NONE
        //
        if (TouchIsActive.LeftTouch == true && TouchIsActive.RightTouch == false)
        {
            stream.Write("1,");
            // Debugs commented out because it clutters data collection
            //
            //Debug.Log("Left Stim triggered");
            stream.BaseStream.Flush();
        }
        else if (TouchIsActive.RightTouch == true && TouchIsActive.LeftTouch == false)
        {
            stream.WriteLine("2,");
            //Debug.Log("Right Stim Triggered");
            stream.BaseStream.Flush();
        }
        else if (TouchIsActive.RightTouch == true && TouchIsActive.LeftTouch == true)
        {
            stream.WriteLine("3,");
            //Debug.Log("Both Stim Triggered");
            stream.BaseStream.Flush();
        }
        else
        {
            stream.WriteLine("0,");
            //Debug.Log("both Stim disengaged");
            stream.BaseStream.Flush();
        }
    }

    // Update not included
    //
}

// End of Script
//
