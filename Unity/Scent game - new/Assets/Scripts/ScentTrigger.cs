using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;


public class ScentTrigger : MonoBehaviour
{
    public Arduinocommunication arduinoComm; // Reference to the Arduino communication script

    private bool isLemonInside = false; // Flag to track whether the ball is inside the collider
    private bool isLavenderInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Lemon" && !isLemonInside)
        {
            arduinoComm.SendMessageToArduino("W"); // Send a message to the Arduino communication script
            Debug.Log("Lemon Ball entered HMD collider");
            isLemonInside = true;
        }
        if (other.name == "Lavender" && !isLavenderInside)
        {
            arduinoComm.SendMessageToArduino("M"); // Send a message to the Arduino communication script
            Debug.Log("Lavender Ball entered HMD collider");
            isLavenderInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Lemon" && isLemonInside)
        {
            arduinoComm.SendMessageToArduino("S"); // Send a message to the Arduino communication script
            Debug.Log("Lemon Ball exited HMD collider");
            isLemonInside = false;
        }
        if (other.name == "Lavender" && isLavenderInside)
        {
            arduinoComm.SendMessageToArduino("Z"); // Send a message to the Arduino communication script
            Debug.Log("Lavender Ball exited HMD collider");
            isLavenderInside = false;
        }
    }
}
