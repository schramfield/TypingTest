using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;
using UnityEngine.UI;
using System.IO.Ports;

public class SerialPortInitiate : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM21", 9600);
    //Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.Write("0");
    }

    public void TouchStatus(bool pokeyPoke)
    {
        if(pokeyPoke == true) {
            Debug.Log("Poking was poked!");
            sp.Write("1");
        }

    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("You were lazy and pressed space bar!");
            sp.Write("1");
        }
        else {
            sp.Write("0");
        }
    }
    void WriteSerial(string data_to_write)
    {
        sp.Write(data_to_write);
    }
}
