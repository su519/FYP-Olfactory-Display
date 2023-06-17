using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;


public class ScentTrigger : MonoBehaviour
{
    public Arduinocommunication arduinoComm;

    private bool pinkInside = false;
    private bool lilacInside = false;
    private bool orangeInside = false;
    private bool purpleInside = false;
    private bool yellowInside = false;
    private bool blueInside = false;

    private int pinkIndex;
    private int lilacIndex;
    private int orangeIndex;
    private int purpleIndex;
    private int yellowIndex;
    private int blueIndex;

    public static bool held = false;

    string on = "i";
    string off = "o";
    public float dutyCycle;
    private float frequency = 1.0f;

    private Coroutine pinkCoroutine;
    private Coroutine lilacCoroutine;
    private Coroutine orangeCoroutine;
    private Coroutine purpleCoroutine;
    private Coroutine yellowCoroutine;
    private Coroutine blueCoroutine;

    //111 101 111 100 010 000

    

    private void Start()
    {
        pinkIndex = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
        lilacIndex = Arduinocommunication.binaryCodes.IndexOf("010") + 2;
        orangeIndex = Arduinocommunication.binaryCodes.IndexOf("001") + 2;
        purpleIndex = Arduinocommunication.binaryCodes.IndexOf("011") + 2;
        yellowIndex = Arduinocommunication.binaryCodes.IndexOf("100") + 2;
        blueIndex = Arduinocommunication.binaryCodes.IndexOf("101") + 2;

    }


    private void OnTriggerEnter(Collider other)
    {
        held = true;
        Debug.Log(dutyCycle);
        if(dutyCycle == 0.8f)
        {
            if (pinkInside == true)
            {
                StopCoroutine(pinkCoroutine);
            }

            if(lilacInside == true)
            {
                StopCoroutine(lilacCoroutine);
            }

            if (orangeInside == true)
            {
                StopCoroutine(orangeCoroutine);
            }

            if (purpleInside == true)
            {
                StopCoroutine(purpleCoroutine);
            }

            if (yellowInside == true)
            {
                StopCoroutine(yellowCoroutine);
            }

            if (blueInside == true)
            {
                StopCoroutine(blueCoroutine);
            }
        }
        if (other.name == "pink" && !pinkInside)
        {

            Debug.Log("enter");

            pinkInside = true;

            pinkCoroutine = StartCoroutine(SendPwmMessageCoroutine(pinkIndex, dutyCycle, pinkInside));
            Debug.Log("pink Ball entered HMD collider");
        }
        if (other.name == "lilac" && !lilacInside)
        {
            lilacInside = true;

            lilacCoroutine = StartCoroutine(SendPwmMessageCoroutine(lilacIndex, dutyCycle, lilacInside));
            Debug.Log("lilac Ball entered HMD collider");
        }
        if (other.name == "orange" && !orangeInside)
        {

            orangeInside = true;

            orangeCoroutine = StartCoroutine(SendPwmMessageCoroutine(orangeIndex, dutyCycle, orangeInside));
            Debug.Log("orange Ball entered HMD collider");
        }
        if (other.name == "purple" && !purpleInside)
        {

            purpleInside = true;

            purpleCoroutine = StartCoroutine(SendPwmMessageCoroutine(purpleIndex, dutyCycle, purpleInside));
            Debug.Log("purple Ball entered HMD collider");
        }
        if (other.name == "yellow" && !yellowInside)
        {

            yellowInside = true;

            yellowCoroutine = StartCoroutine(SendPwmMessageCoroutine(yellowIndex, dutyCycle, yellowInside));
            Debug.Log("yellow Ball entered HMD collider");
        }
        if (other.name == "blue" && !blueInside)
        {

            blueInside = true;

            blueCoroutine = StartCoroutine(SendPwmMessageCoroutine(blueIndex, dutyCycle, blueInside));
            Debug.Log("blue Ball entered HMD collider");
        }
    }

    public IEnumerator SendPwmMessageCoroutine(int index, float dutyCycle, bool isInside)
    {
        float onTime = dutyCycle * (1.0f / frequency);
        float offTime = (1.0f - dutyCycle) * (1.0f / frequency);
        Debug.Log(onTime);
        Debug.Log(offTime);

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


    private void OnTriggerExit(Collider other)
    {
        if (dutyCycle == 0.8f)
        {
            dutyCycle = 0.3f;
        }

            if (other.name == "pink" && pinkInside)
        {
            
            Debug.Log("pink Ball exited HMD collider");
            pinkInside = false;

            // Stop the coroutine for sending PWM messages
            StopCoroutine(pinkCoroutine);
            string message = $"{pinkIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
        if (other.name == "lilac" && lilacInside)
        {
            
            Debug.Log("lilac Ball exited HMD collider");
            lilacInside = false;
            // Stop the coroutine for sending PWM messages
            StopCoroutine(lilacCoroutine);
            string message = $"{lilacIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
        if (other.name == "orange" && orangeInside)
        {

            Debug.Log("orange Ball exited HMD collider");
            orangeInside = false;
            // Stop the coroutine for sending PWM messages
            StopCoroutine(orangeCoroutine);
            string message = $"{orangeIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
        if (other.name == "purple" && purpleInside)
        {

            Debug.Log("purple Ball exited HMD collider");
            purpleInside = false;
            // Stop the coroutine for sending PWM messages
            StopCoroutine(purpleCoroutine);
            string message = $"{purpleIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
        if (other.name == "yellow" && yellowInside)
        {

            Debug.Log("yellow Ball exited HMD collider");
            yellowInside = false;
            // Stop the coroutine for sending PWM messages
            StopCoroutine(yellowCoroutine);
            string message = $"{yellowIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
        if (other.name == "blue" && blueInside)
        {

            Debug.Log("blue Ball exited HMD collider");
            blueInside = false;
            // Stop the coroutine for sending PWM messages
            StopCoroutine(blueCoroutine);
            string message = $"{blueIndex}{off}";
            arduinoComm.SendMessageToArduino(message);
        }
        
    }

}
