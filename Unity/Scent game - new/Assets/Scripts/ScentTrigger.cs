using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;


public class ScentTrigger : MonoBehaviour
{
    public Arduinocommunication arduinoComm;

    private bool isLemonInside = false;
    private bool isLavenderInside = false;
    private int LemonIndex;
    private int LavenderIndex;
    string on = "i";
    string off = "o";
    private float dutyCycle = 0.5f;
    private float frequency = 1.0f;

    private Coroutine lemonCoroutine;
    private Coroutine lavenderCoroutine;

    //111 101 111 100 010 000


    private void Start()
    {
        LemonIndex = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
        LavenderIndex = Arduinocommunication.binaryCodes.IndexOf("010") + 2;


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Lemon" && !isLemonInside)
        {
            Debug.Log("List count: " + Arduinocommunication.binaryCodes.Count);

            foreach (string item in Arduinocommunication.binaryCodes)
            {
                Debug.Log("List: " + item);
            }

            Debug.Log("Index: " + LemonIndex);
            isLemonInside = true;

            lemonCoroutine = StartCoroutine(SendPwmMessageCoroutine(LemonIndex, dutyCycle, isLemonInside));
            Debug.Log("Lemon Ball entered HMD collider");
        }
        if (other.name == "Lavender" && !isLavenderInside)
        {
            isLavenderInside = true;

            lavenderCoroutine = StartCoroutine(SendPwmMessageCoroutine(LavenderIndex, dutyCycle, isLavenderInside));
            Debug.Log("Lavender Ball entered HMD collider");
        }
    }

    public IEnumerator SendPwmMessageCoroutine(int index, float dutyCycle, bool isInside)
    {
        float onTime = dutyCycle * (1.0f / frequency);
        float offTime = (1.0f - dutyCycle) * (1.0f / frequency);

        bool isRunning = true;

        if (isInside)
        {
            while (isRunning)
            {
                string message = $"{index}{on}";
                arduinoComm.SendMessageToArduino(message);
                yield return new WaitForSeconds(onTime);

                message = $"{index}{off}";
                arduinoComm.SendMessageToArduino(message);
                yield return new WaitForSeconds(offTime);

            }
        } else {
            isRunning = false;
        }
    }

    //public IEnumerator SendPwmMessageTimedCoroutine(int index, float dutyCycle, bool isInside)
    //{
    //    float onTime = dutyCycle * (1.0f / frequency);
    //    float offTime = (1.0f - dutyCycle) * (1.0f / frequency);

    //    bool isRunning = true;

    //    if (isInside)
    //    {
    //        while (isRunning)
    //        {
    //            string message = $"{index}{on}";
    //            arduinoComm.SendMessageToArduino(message);
    //            yield return new WaitForSeconds(onTime);

    //            message = $"{index}{off}";
    //            arduinoComm.SendMessageToArduino(message);
    //            yield return new WaitForSeconds(offTime);

    //        }
    //    }
    //    else
    //    {
    //        isRunning = false;
    //    }
    //}


    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Lemon" && isLemonInside)
        {
            
            Debug.Log("Lemon Ball exited HMD collider");
            isLemonInside = false;

            // Stop the coroutine for sending PWM messages
            StopCoroutine(lemonCoroutine);
            string message = $"{LemonIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
        if (other.name == "Lavender" && isLavenderInside)
        {
            
            Debug.Log("Lavender Ball exited HMD collider");
            isLavenderInside = false;
            // Stop the coroutine for sending PWM messages
            StopCoroutine(lemonCoroutine);
            string message = $"{LavenderIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
    }

}
