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
    string fan = "16";

    public int activeScent;
    private float[] ScentDutyCycle;
    private Coroutine scentPWMCoroutine;
    bool numberButtonPressed = false;
    bool scentPWMactive = false;

    string message;

    void Start()
    {
        scentPins = new int[6];
        ScentDutyCycle = new float[5];

        scentPins[0] = 2;
        scentPins[1] = 3;
        scentPins[2] = 4;
        scentPins[3] = 5;
        scentPins[4] = 6;
        scentPins[5] = 7;


    //    private int GrapeIndex = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
    //private int PineIndex = Arduinocommunication.binaryCodes.IndexOf("100") + 2;
    //private int LavenderIndex = Arduinocommunication.binaryCodes.IndexOf("001") + 2;
    //private int OrangeIndex = Arduinocommunication.binaryCodes.IndexOf("101") + 2;

        ScentDutyCycle[0] = 0.3f;
        ScentDutyCycle[1] = 0.7f;
        ScentDutyCycle[2] = 0.5f;
        ScentDutyCycle[3] = 0.1f;
        ScentDutyCycle[4] = 0.9f;

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

        if (currentScent == 7)
        {

            scentText.text = "End of trial";
            returnButton.SetActive(true);

        }
        else
        {
            Debug.Log("else");
            if (IntensityCount == 5)
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

        while (elapsedTime < 7f)
        {
            elapsedTime = Time.time - startTime;
            yield return null;
            if (detectedButtonPressed)
            {
                detectedButton.SetActive(false);
                scentPWMactive = false;
                StopCoroutine(scentPWMCoroutine);
                message = $"{0}{0}{scentPins[currentScent - 1]}{ScentDutyCycle[IntensityCount]}{elapsedTime}";
                arduinoComm.SendMessageToArduino(message);
                Numbers.SetActive(true);
                scentText.text = "What is the perceived distance of the scent?\n 1: Very close\n 10: Very far";
                yield return new WaitUntil(() => numberButtonPressed);
                message = $"{0}{0}{scentPins[currentScent - 1]}{ScentDutyCycle[IntensityCount]}{numberValue}";
                arduinoComm.SendMessageToArduino(message);
                Numbers.SetActive(false);
                numberButtonPressed = false;
                break;
            }
        }

        if (elapsedTime > 7f && !detectedButtonPressed)
        {
            message = $"{0}{0}{scentPins[currentScent - 1]}{ScentDutyCycle[IntensityCount]}{0}";
            arduinoComm.SendMessageToArduino(message);
            detectedButton.SetActive(false);
        }

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

        yield return new WaitForSeconds(5f);
        scentText.gameObject.SetActive(false);

        IntensityCount++;
        Debug.Log("Intensity: " + IntensityCount);

        nextButton.SetActive(true);

        if (IntensityCount == 10)
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
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if (button != null)
        {
            numberValue = button.GetComponentInChildren<TMP_Text>().text;
            Debug.Log("Button Text: " + numberValue);
        }
    }

}
