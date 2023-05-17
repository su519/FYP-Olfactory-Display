using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;


public class ScentTrigger : MonoBehaviour
{
    public Arduinocommunication arduinoComm; // Reference to the Arduino communication script

    private bool isLemonInside = false; // Flag to track whether the ball is inside the collider
    private bool isLavenderInside = false;
    private int LemonIndex;
    private int LavenderIndex;
    string on = "1";
    string off = "0";

    private void Start()
    {
        LemonIndex = Arduinocommunication.binaryCodes.IndexOf("0001") + 2;
        LavenderIndex = Arduinocommunication.binaryCodes.IndexOf("0010") + 2;


    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Lemon" && !isLemonInside)
        {
            Debug.Log("List count: " + Arduinocommunication.binaryCodes.Count);

            foreach (string item in Arduinocommunication.binaryCodes)
            {
                Debug.Log("List: "+item);
            }

            Debug.Log("Index: " + LemonIndex);
            string message = $"{LemonIndex}{on}";
            arduinoComm.SendMessageToArduino(message); // Send a message to the Arduino communication script
            Debug.Log("Lemon Ball entered HMD collider");
            isLemonInside = true;
        }
        if (other.name == "Lavender" && !isLavenderInside)
        {
            string message = LavenderIndex.ToString() + on;
            arduinoComm.SendMessageToArduino(message); // Send a message to the Arduino communication script
            Debug.Log("Lavender Ball entered HMD collider");
            isLavenderInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Lemon" && isLemonInside)
        {
            string message = LemonIndex.ToString() + off;
            arduinoComm.SendMessageToArduino(message); // Send a message to the Arduino communication script
            Debug.Log("Lemon Ball exited HMD collider");
            isLemonInside = false;
        }
        if (other.name == "Lavender" && isLavenderInside)
        {
            string message = LavenderIndex.ToString() + off;
            arduinoComm.SendMessageToArduino(message); // Send a message to the Arduino communication script
            Debug.Log("Lavender Ball exited HMD collider");
            isLavenderInside = false;
        }
    }
}
