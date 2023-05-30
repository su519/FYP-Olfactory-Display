using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class IntensityController : MonoBehaviour
{
    public Arduinocommunication arduinoComm;
    public ScentTrigger scentTrig;

    public GameObject startButton;
    public GameObject nextButton;
    public GameObject intensityButton;
    public GameObject returnButton;
    public TMP_Text scentText;
    public GameObject backButton;
    public GameObject detectedButton;
    public static bool detectedButtonPressed = false;
    public static bool intensityButtonPressed = false;
    public TMP_Text nextButtonText;

    private int[] scentPins;

    private int currentScent = 1;

    private bool start = false;

    private bool again = false;

    private int prev_index = 0;
    int Index;
    int backgroundPin;
    int currentIndex;

    string on = "i";
    string off = "o";
    int backgroundCount = 0;
    string fan = "14";

    public int activeScent;
    private float ScentDutyCycle = 0.5f;
    private float[] BackgroundDutyCycle;
    private Coroutine scentPWMCoroutine;
    private Coroutine backgroundPWMCoroutine;
    bool scentPWMactive = false;
    bool backgroundPWMactive = false;

    string message;

    void Start()
    {
        scentPins = new int[9];
        BackgroundDutyCycle = new float[5];

        scentPins[0] = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
        scentPins[1] = Arduinocommunication.binaryCodes.IndexOf("001") + 2;
        scentPins[2] = Arduinocommunication.binaryCodes.IndexOf("010") + 2;
        scentPins[3] = Arduinocommunication.binaryCodes.IndexOf("011") + 2;
        scentPins[4] = Arduinocommunication.binaryCodes.IndexOf("100") + 2;
        scentPins[5] = Arduinocommunication.binaryCodes.IndexOf("101") + 2;
        scentPins[6] = UnityEngine.Random.Range(2, 7);
        scentPins[7] = UnityEngine.Random.Range(2, 7);
        scentPins[8] = UnityEngine.Random.Range(2, 7);

        BackgroundDutyCycle[0] = 0.1f;
        BackgroundDutyCycle[1] = 0.4f;
        BackgroundDutyCycle[2] = 0.2f;
        BackgroundDutyCycle[3] = 0.5f;
        BackgroundDutyCycle[4] = 0.3f;

        backgroundPin = 9;

        nextButton.SetActive(false);
        intensityButton.SetActive(false);
        scentText.gameObject.SetActive(false);
        returnButton.SetActive(false);
        startButton.SetActive(true);
        backButton.SetActive(true);
        detectedButton.SetActive(false);

        nextButtonText.text = "Next Intensity";
    }

    public void StartGame()
    {
        if (!start)
        {
            start = true;
            startButton.SetActive(false);
            backButton.SetActive(false);
            StartCoroutine(StartDelay());
        }
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(ReleaseScent());
    }

    public void NextScent()
    {
        //releaseButton.SetActive(false);
        detectedButtonPressed = false;
        intensityButtonPressed = false;
        nextButton.SetActive(false);
        intensityButton.SetActive(false);
        scentText.gameObject.SetActive(true);
        nextButtonText.text = "Next Intensity";

        Debug.Log("Current Scent: " + currentScent);
        Debug.Log("Background: " + backgroundCount);

        if (currentScent == 9)
        {

            scentText.text = "End of trial";
            returnButton.SetActive(true);

        }
        else
        {
            Debug.Log("else");
            if (backgroundCount == 5)
            {

                backgroundCount = 0;
                currentScent++;
            }

            scentText.text = "Releasing scent " + currentScent;
            StartCoroutine(ReleaseScent());

        }
    }

    IEnumerator ReleaseScent()
    {


        detectedButton.SetActive(true);
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(true);
        scentText.text = "Releasing scent " + currentScent;
        float StartTime = Time.time;
        //randomIndex = UnityEngine.Random.Range(0, scentPins.Length-1);
        activeScent = scentPins[currentScent - 1];

        float startTime = Time.time;
        float elapsedTime = 0f;
        scentPWMCoroutine = StartCoroutine(scentTrig.SendPwmMessageCoroutine(scentPins[currentScent - 1], ScentDutyCycle, true));
        backgroundPWMCoroutine = StartCoroutine(scentTrig.SendPwmMessageCoroutine(backgroundPin, BackgroundDutyCycle[backgroundCount], true));
        scentPWMactive = true;
        backgroundPWMactive = true;

        while (elapsedTime < 5f)
        {
            elapsedTime = Time.time - startTime;
            yield return null;
            if (detectedButtonPressed)
            {
                scentPWMactive = false;
                backgroundPWMactive = false;
                StopCoroutine(scentPWMCoroutine);
                StopCoroutine(backgroundPWMCoroutine);
                message = $"{0}{0}{scentPins[currentScent - 1]}{BackgroundDutyCycle[backgroundCount]}{elapsedTime}";
                arduinoComm.SendMessageToArduino(message);
                break;
            }
        }

        if (elapsedTime > 5f && !detectedButtonPressed)
        {
            message = $"{0}{0}{scentPins[currentScent - 1]}{BackgroundDutyCycle[backgroundCount]}{0}";
            arduinoComm.SendMessageToArduino(message);
        }

        detectedButton.SetActive(false);

        if (backgroundPWMactive)
        {
            StopCoroutine(backgroundPWMCoroutine);
            backgroundPWMactive = false;
        }

        if (scentPWMactive)
        {
            StopCoroutine(scentPWMCoroutine);
            scentPWMactive = false;
        }

        message = $"{scentPins[Index]}{off}";
        arduinoComm.SendMessageToArduino(message);
        message = $"{backgroundPin}{off}";
        arduinoComm.SendMessageToArduino(message);

        scentText.text = "";
        yield return new WaitForSeconds(1f);

        scentText.text = "Clearing scent " + currentScent;
        message = $"{fan}{on}";
        arduinoComm.SendMessageToArduino(message);
        yield return new WaitForSeconds(5f);

        message = $"{fan}{off}";
        arduinoComm.SendMessageToArduino(message);

        scentText.gameObject.SetActive(false);

        backgroundCount++;
        Debug.Log("Background: " + backgroundCount);

        nextButton.SetActive(true);

        if (backgroundCount == 5)
        {
            nextButtonText.text = "Next Scent";
        }

    }

    public void DetectedButtonPressed()
    {
        detectedButtonPressed = true;
    }

    public void IntensityButtonPressed()
    {
        intensityButtonPressed = true;
    }
}
