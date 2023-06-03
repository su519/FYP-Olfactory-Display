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
    public GameObject returnButton;
    public TMP_Text scentText;
    public GameObject backButton;
    public GameObject detectedButton;
    public static bool detectedButtonPressed = false;
    public TMP_Text nextButtonText;
    public GameObject Numbers;
    private TMP_Text numberButtonText;

    string numberValue;

    private int[] scentPins;

    private int currentScent = 1;

    private bool start = false;

    private bool again = false;

    private int prev_index = 0;
    int Index;
    int currentIndex;

    string on = "i";
    string off = "o";
    int IntensityCount = 0;
    string fan = "14";

    public int activeScent;
    private float[] ScentDutyCycle;
    private Coroutine scentPWMCoroutine;
    bool numberButtonPressed = false;
    bool scentPWMactive = false;

    string message;

    void Start()
    {
        scentPins = new int[9];
        ScentDutyCycle = new float[5];

        scentPins[0] = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
        scentPins[1] = Arduinocommunication.binaryCodes.IndexOf("001") + 2;
        scentPins[2] = Arduinocommunication.binaryCodes.IndexOf("010") + 2;
        scentPins[3] = Arduinocommunication.binaryCodes.IndexOf("011") + 2;
        scentPins[4] = Arduinocommunication.binaryCodes.IndexOf("100") + 2;
        scentPins[5] = Arduinocommunication.binaryCodes.IndexOf("101") + 2;

        ScentDutyCycle[0] = 0.1f;
        ScentDutyCycle[1] = 0.8f;
        ScentDutyCycle[2] = 0.2f;
        ScentDutyCycle[3] = 1;
        ScentDutyCycle[4] = 0.3f;
        ScentDutyCycle[5] = 0.6f;
        ScentDutyCycle[6] = 0.7f;
        ScentDutyCycle[7] = 0.4f;
        ScentDutyCycle[8] = 0.9f;
        ScentDutyCycle[9] = 0.5f;

        Numbers.SetActive(false);
        nextButton.SetActive(false);
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
        numberButtonPressed = false;
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(true);
        nextButtonText.text = "Next Intensity";

        Debug.Log("Current Scent: " + currentScent);
        Debug.Log("Intensity: " + IntensityCount);

        if (currentScent == 6)
        {

            scentText.text = "End of trial";
            returnButton.SetActive(true);

        }
        else
        {
            Debug.Log("else");
            if (IntensityCount == 10)
            {

                IntensityCount = 0;
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
        scentPWMCoroutine = StartCoroutine(scentTrig.SendPwmMessageCoroutine(scentPins[currentScent - 1], ScentDutyCycle[IntensityCount], true));
        scentPWMactive = true;

        while (elapsedTime < 5f)
        {
            elapsedTime = Time.time - startTime;
            yield return null;
            if (detectedButtonPressed)
            {
                scentPWMactive = false;
                StopCoroutine(scentPWMCoroutine);
                message = $"{0}{0}{scentPins[currentScent - 1]}{ScentDutyCycle[IntensityCount]}{elapsedTime}";
                arduinoComm.SendMessageToArduino(message);
                Numbers.SetActive(true);
                yield return new WaitUntil(() => numberButtonPressed);
                break;
            }
        }

        if (elapsedTime > 5f && !detectedButtonPressed)
        {
            message = $"{0}{0}{scentPins[currentScent - 1]}{ScentDutyCycle[IntensityCount]}{0}";
            arduinoComm.SendMessageToArduino(message);
        }

        detectedButton.SetActive(false);

        if (scentPWMactive)
        {
            StopCoroutine(scentPWMCoroutine);
            scentPWMactive = false;
        }

        message = $"{scentPins[Index]}{off}";
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

        IntensityCount++;
        Debug.Log("Intensity: " + IntensityCount);

        nextButton.SetActive(true);

        if (IntensityCount == 5)
        {
            nextButtonText.text = "Next Scent";
        }

    }

    public void DetectedButtonPressed()
    {
        detectedButtonPressed = true;
    }

    public void NumberButtonPressed()
    {
        numberButtonPressed = true;
        numberButtonText = GetComponentInChildren<TMP_Text>();
        numberValue = numberButtonText.text;
        Debug.Log("Button Text: " + numberValue);
    }
}
