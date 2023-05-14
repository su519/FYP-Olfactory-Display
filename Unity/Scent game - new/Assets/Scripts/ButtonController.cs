using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Arduinocommunication arduinoComm; // reference to the Arduinocommunication script
    public Button scentLoadingButton; // reference to the scent loading button
    public Button startButton; // reference to the start button

    private bool binaryCode1Received = false; // flag to track if binary code 1 has been received
    private bool binaryCode2Received = false; // flag to track if binary code 2 has been received
    private Dictionary<string, string> ballCodeDict = new Dictionary<string, string>();

    private void Start()
    {
        // Disable the start button initially
        startButton.interactable = false;
        ballCodeDict.Add("Lemon", "0001");
        ballCodeDict.Add("Lavender", "0010");
    }

    public void OnScentLoadingButtonClick()
    {
        // Send the message to the Arduino to request binary codes
        arduinoComm.SendMessageToArduino("l");

        // Enable the scent loading button and disable the start button
        scentLoadingButton.interactable = false;
        startButton.interactable = false;

        // Clear the binary codes list in case there are any old codes left over
        arduinoComm.binaryCodes.Clear();

        // Start a coroutine to check for binary codes
        StartCoroutine(CheckForBinaryCodes());
    }

    private IEnumerator CheckForBinaryCodes()
    {
        // Wait for a brief moment to give the Arduino time to send the binary codes
        yield return new WaitForSeconds(1f);

        bool allBinaryCodesReceived = true;

        // Loop through the binary codes in the dictionary and check if they have been received
        foreach (KeyValuePair<string, string> entry in ballCodeDict)
        {
            if (!arduinoComm.binaryCodes.Contains(entry.Value))
            {
                allBinaryCodesReceived = false;
                break;
            }
        }

        // Enable the start button if all binary codes have been received
        if (allBinaryCodesReceived)
        {
            startButton.interactable = true;
        }
    }


    //public void OnStartButtonClick()
    //{
    //    // Do something when the start button is clicked
    //    Debug.Log("Start button clicked");
    //}
}
