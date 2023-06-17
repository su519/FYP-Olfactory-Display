using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BackgroundIdentificationController : MonoBehaviour
{
    public Arduinocommunication arduinoComm;
    public ScentTrigger scentTrig;

    public GameObject choice;
    public GameObject startButton;
    public GameObject nextButton;
    public GameObject returnButton;
    public TMP_Text scentText;
    public GameObject backButton;
    public static bool choiceButtonPressed = false;
    public TMP_Text nextButtonText;

    private int[] scentPins;
    string numberValue;

    private int currentScent = 1;

    private bool start = false;

    private bool again = false;
    int prevScent1;
    int prevScent2;

    private int prev_index = 0;
    int Index;
    int backgroundPin;
    int currentIndex;

    string on = "i";
    string off = "o";
    int backgroundCount = 0;
    string fan = "16";

    private float[] DutyCycle;
    private Coroutine scent1PWMCoroutine;
    private Coroutine scent2PWMCoroutine;
    bool scent1PWMactive = false;
    bool scent2PWMactive = false;

    string message;

    void Start()
    {
        scentPins = new int[9];
        DutyCycle = new float[9];

        scentPins[0] = 3;
        scentPins[1] = 5;
        scentPins[2] = 2;
        scentPins[3] = 6;
        scentPins[4] = 4;
        scentPins[5] = 7;
        scentPins[6] = UnityEngine.Random.Range(2, 7);
        scentPins[7] = UnityEngine.Random.Range(2, 7);
        scentPins[8] = UnityEngine.Random.Range(2, 7);

        DutyCycle[0] = 0.1f;
        DutyCycle[1] = 0.2f;
        DutyCycle[2] = 0.3f;
        DutyCycle[3] = 0.4f;
        DutyCycle[4] = 0.5f;
        DutyCycle[5] = 0.6f;
        DutyCycle[6] = 0.7f;
        DutyCycle[7] = 0.8f;
        DutyCycle[8] = 0.9f;

   

        choice.SetActive(false);
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(false);
        returnButton.SetActive(false);
        startButton.SetActive(true);
        backButton.SetActive(true);

        nextButtonText.text = "Next";
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

        nextButton.SetActive(false);
        scentText.gameObject.SetActive(true);
        nextButtonText.text = "Next";

        Debug.Log("Current Scent: " + currentScent);
        Debug.Log("Background: " + backgroundCount);

        if (currentScent == 9)
        {
            
            scentText.text = "End of trial";
            returnButton.SetActive(true);

        }
        else
        {
            currentScent++;
            StartCoroutine(ReleaseScent());
           
        }
    }

    IEnumerator ReleaseScent()
    {
        choice.SetActive(false);
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(true);
        scentText.text = "Releasing scent 1";
        message = $"{scentPins[currentScent - 1]}{on}";
        arduinoComm.SendMessageToArduino(message);
        yield return new WaitForSeconds(5f);
        scentText.text = "Clearing scent 1";

        message = $"{fan}{on}";
        arduinoComm.SendMessageToArduino(message);
        yield return new WaitForSeconds(5f);
        message = $"{fan}{off}";
        arduinoComm.SendMessageToArduino(message);

        scentText.text = "Releasing scent 2";
        int randomPin1 = Random.Range(0, 8);
        int randomPin2 = Random.Range(0, 8);

        while (randomPin1 == randomPin2)
        {
            randomPin2 = Random.Range(0, 8);
            while(randomPin1 == prevScent1)
            {
                randomPin1 = Random.Range(0, 8);
            }
            while(randomPin2 == prevScent2)
            {
                randomPin2 = Random.Range(0, 8);
            }
        }
    

        message = $"{scentPins[randomPin1]}{on}";
        arduinoComm.SendMessageToArduino(message);
        yield return new WaitForSeconds(5f);
        scentText.text = "Clearing scent 2";
        message = $"{fan}{on}";
        arduinoComm.SendMessageToArduino(message);
        

        yield return new WaitForSeconds(5f);

        message = $"{fan}{off}";
        arduinoComm.SendMessageToArduino(message);

        scentText.text = "Releasing together";

        scent1PWMCoroutine = StartCoroutine(scentTrig.SendPwmMessageCoroutine(scentPins[currentScent - 1], DutyCycle[randomPin1], true));
        scent2PWMCoroutine = StartCoroutine(scentTrig.SendPwmMessageCoroutine(scentPins[randomPin1], DutyCycle[randomPin2], true));

        yield return new WaitForSeconds(5f);

        StopCoroutine(scent1PWMCoroutine);
        StopCoroutine(scent2PWMCoroutine);
        message = $"{scentPins[currentScent - 1]}{off}";
        arduinoComm.SendMessageToArduino(message);
        message = $"{scentPins[randomPin2]}{off}";
        arduinoComm.SendMessageToArduino(message);
        choice.SetActive(true);

        scentText.text = "Which scent did you percieve as the background scent?";
        yield return new WaitUntil(() => choiceButtonPressed);
        choice.SetActive(false);
        choiceButtonPressed = false;
        message = $"{0}{0}{scentPins[currentScent - 1]}{DutyCycle[randomPin1]}{scentPins[randomPin1]}{DutyCycle[randomPin2]}{numberValue}";
        arduinoComm.SendMessageToArduino(message);

        scentText.text = "Clearing scents";
        message = $"{fan}{on}";
        arduinoComm.SendMessageToArduino(message);
        yield return new WaitForSeconds(5f);

        message = $"{fan}{off}";
        arduinoComm.SendMessageToArduino(message);

        yield return new WaitForSeconds(5f);

        scentText.gameObject.SetActive(false);

        prevScent1 = currentScent - 1;
        prevScent2 = randomPin1;

        nextButton.SetActive(true);

    }

    public void ChoiceButtonPressed()
    {
        choiceButtonPressed = true;
        Button button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        if (button != null)
        {
            numberValue = button.GetComponentInChildren<TMP_Text>().text;
            Debug.Log("Button Text: " + numberValue);
        }
    }

}
