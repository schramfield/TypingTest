using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;



public class LuisSerialPort : MonoBehaviour
{
    private string com = "COM21";
    private int brate = 9600;
    private int timeOut = 500;
    private SerialPort stream;


    //private string getCOMPort()
    //{
    //    string[] ports = SerialPort.GetPortNames();
    //    Debug.Log("The following serial ports were found:");

    //    // display each port name to the console...
    //    foreach (string port in ports)
    //    {
    //        Debug.Log(port.ToString()); // there's also a version with UnityEngine first...?
    //    }
    //    if (ports.Length > 1)
    //    {
    //        Debug.Log("Look out! Multiple ports");
    //    }
    //    return ports[0].ToString();
    //}

    private string assertCOMport()
    {
        string[] ports = SerialPort.GetPortNames();
        int i = 1;

        try
        {
            stream = new SerialPort(com, brate);
            stream.ReadTimeout = timeOut;
            stream.Open();
            Debug.Log("Port 21 successfully opened.");
            return "COM21";
        }
        catch
        {
            for (i = 1; i < ports.Length; i++)
            {
                Debug.Log("Trying port");
                Debug.Log(com);
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
        Debug.Log("Error: No viable ports.");

        return ports[0].ToString();

    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("CodeBucket sequence initated...");
        ////com = getCOMPort();
        //stream = new SerialPort(com, brate);
        //stream.ReadTimeout = timeOut;
        //stream.Open();
        com = assertCOMport();

    }

    public void WriteToArduino()
    {
        //this is all bad
        //com = getCOMPort();
        //stream = new SerialPort(com, brate);
        //stream.ReadTimeout = timeOut;
        //stream.Open();


        //begin good
        if (TouchIsActive.LeftTouch == true && TouchIsActive.RightTouch == false)
        {
            stream.Write("1");
            Debug.Log("Left Stim triggered");
            stream.BaseStream.Flush();
        }
        else if (TouchIsActive.RightTouch == true && TouchIsActive.LeftTouch == false)
        {
            stream.Write("2");
            Debug.Log("Right Stim Triggered");
            stream.BaseStream.Flush();
        }
        else if (TouchIsActive.RightTouch == true && TouchIsActive.LeftTouch == true)
        {
            stream.Write("3");
            Debug.Log("Both Stim Triggered");
            stream.BaseStream.Flush();
        }
        else
        {
            stream.Write("0");
            Debug.Log("No Stim Triggered");
            stream.BaseStream.Flush();
        }
    }

}
//    //public void WriteLeft(string message)
//    //{

//    //}

//    //public void WriteRight(string message)
//    //{
//    //    stream.Write("2");
//    //    Debug.Log(message);
//    //    stream.BaseStream.Flush();
//    //}
//    //public void WriteBoth(string message)
//    //{
//    //    stream.Write("3");
//    //    Debug.Log(message);
//    //    stream.BaseStream.Flush();
//    //}

//    //public void LvsR(bool LEFT)
//    //{
//    //    // LEFT = 1, RIGHT = 2, LEFT & RIGHT = 3, nothing = 0
//    //    if(LEFT == true)
//        {
//            //stream.Write("1");
//            //Debug.Log("left touch");
//            //stream.BaseStream.Flush();
//        }
//        //if (RIGHT == true && LEFT == false)
//        //{
//        //    stream.Write("2");
//        //    Debug.Log("right touch");
//        //    stream.BaseStream.Flush();
//        //}
//        //if (LEFT == true && RIGHT == true)
//        //{
//        //    stream.Write("3");
//        //    Debug.Log("both touch");
//        //    stream.BaseStream.Flush();
//        //}
//    }
//}
