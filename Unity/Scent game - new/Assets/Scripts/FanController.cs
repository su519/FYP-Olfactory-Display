using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    public Arduinocommunication arduinoComm;
    private Coroutine fanCoroutine;

    int fan = 16;
    string on = "i";
    string off = "o";

    public IEnumerator FanCoroutine()
    {
        string message = $"{fan}{on}";
        arduinoComm.SendMessageToArduino(message);
        yield return new WaitForSeconds(3);
        message = $"{fan}{off}";
        arduinoComm.SendMessageToArduino(message);
        StopFanCoroutine();

    }

    public void StopFanCoroutine()
    {
        StopCoroutine(fanCoroutine);

    }


    private void OnTriggerExit(Collider other)
    {
        if(ScentTrigger.held == true)
        {
            fanCoroutine = StartCoroutine(FanCoroutine());
            ScentTrigger.held = false;

        }
        
    }
}