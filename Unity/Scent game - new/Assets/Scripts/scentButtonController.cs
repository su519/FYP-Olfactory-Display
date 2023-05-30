using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scentButtonController : MonoBehaviour
{
    public Arduinocommunication arduinoComm;
    public PanelController PanelCont;

    public Button Rose;
    public Button Lavender;
    public Button Ylang;
    public Button Jasmine;
    public Button Orange;
    public Button Honeysuckle;

    private int RoseIndex = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
    private int LavenderIndex = Arduinocommunication.binaryCodes.IndexOf("001") + 2;
    private int YlangIndex = Arduinocommunication.binaryCodes.IndexOf("010") + 2;
    private int JasmineIndex = Arduinocommunication.binaryCodes.IndexOf("011") + 2;
    private int OrangeIndex = Arduinocommunication.binaryCodes.IndexOf("100") + 2;
    private int HoneysuckleIndex = Arduinocommunication.binaryCodes.IndexOf("101") + 2;

    public void ScentButtonPressed()
    {
        PanelController.scentButtonPressed = true;
    }



    public void RoseButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{RoseIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void JasmineButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{JasmineIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void LavenderButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{LavenderIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void YlangButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{YlangIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void OrangeButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{OrangeIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void HoneysuckleButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{HoneysuckleIndex}";
        arduinoComm.SendMessageToArduino(message);
    }
}
