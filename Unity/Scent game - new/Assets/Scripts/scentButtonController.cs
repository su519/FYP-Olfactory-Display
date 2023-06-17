using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scentButtonController : MonoBehaviour
{
    public Arduinocommunication arduinoComm;
    public ScentTrigger scentTrig;
    public PanelController PanelCont;

    //public Button Rose;
    //public Button Lavender;
    //public Button Ylang;
    //public Button Jasmine;
    //public Button Orange;
    //public Button Honeysuckle;

    private Coroutine BackgroundScentCoroutine;


    private int LemonIndex = Arduinocommunication.binaryCodes.IndexOf("101") + 2;
    private int LavenderIndex = Arduinocommunication.binaryCodes.IndexOf("100") + 2;
    private int OrangeIndex = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
    private int IrisIndex = Arduinocommunication.binaryCodes.IndexOf("001") + 2;
    private int LemongrassIndex = Arduinocommunication.binaryCodes.IndexOf("010") + 2;
    private int SakuraIndex = Arduinocommunication.binaryCodes.IndexOf("110") + 2;

    public void ScentButtonPressed()
    {
        PanelController.scentButtonPressed = true;
    }

    public void LemonButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{LemonIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void IrisButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{IrisIndex}";
        arduinoComm.SendMessageToArduino(message);
    }
    public void LavenderButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{LavenderIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void OrangeButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{OrangeIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void LemongrassButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{LemongrassIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    public void SakuraButtonPressed()
    {
        string message = $"{0}{PanelCont.activeScent}{SakuraIndex}";
        arduinoComm.SendMessageToArduino(message);
    }

    //public void RoseButtonPressed()
    //{
    //    string message = $"{0}{PanelCont.activeScent}{RoseIndex}";
    //    arduinoComm.SendMessageToArduino(message);
    //}

    //public void JasmineButtonPressed()
    //{
    //    string message = $"{0}{PanelCont.activeScent}{JasmineIndex}";
    //    arduinoComm.SendMessageToArduino(message);
    //}

    //public void LavenderButtonPressed()
    //{
    //    string message = $"{0}{PanelCont.activeScent}{LavenderIndex}";
    //    arduinoComm.SendMessageToArduino(message);
    //}

    //public void YlangButtonPressed()
    //{
    //    string message = $"{0}{PanelCont.activeScent}{YlangIndex}";
    //    arduinoComm.SendMessageToArduino(message);
    //}

    //public void OrangeButtonPressed()
    //{
    //    string message = $"{0}{PanelCont.activeScent}{OrangeIndex}";
    //    arduinoComm.SendMessageToArduino(message);
    //}

    //public void HoneysuckleButtonPressed()
    //{
    //    string message = $"{0}{PanelCont.activeScent}{HoneysuckleIndex}";
    //    arduinoComm.SendMessageToArduino(message);
    //}

    public void BackgroundScentOn()
    {
        BackgroundScentCoroutine = StartCoroutine(scentTrig.SendPwmMessageCoroutine(7, 0.1f, true));
    }

    public void BackgroundScentOff()
    {
        StopCoroutine(BackgroundScentCoroutine);
        string message = "7o";
        arduinoComm.SendMessageToArduino(message);
    }
}
