using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PanelController : MonoBehaviour
{
    public Arduinocommunication arduinoComm;

    public GameObject startButton;
    public GameObject releaseButton;
    public GameObject nextButton;
    public GameObject returnButton;
    public TMP_Text scentText;
    public GameObject backButton;
    public GameObject scentList;
    public static bool scentButtonPressed = false;

    private int[] scentPins;

    private int currentScent = 1;

    private bool start = false;

    private bool again = false;

    private int prev_index = 0;
    int randomIndex;

    string on = "i";
    string off = "o";

    string fan = "14";

    public int activeScent;

    string message;

    void Start()
    {
        scentPins = new int[9];

        scentPins[0] = Arduinocommunication.binaryCodes.IndexOf("000") + 2;
        scentPins[1] = Arduinocommunication.binaryCodes.IndexOf("001") + 2;
        scentPins[2] = Arduinocommunication.binaryCodes.IndexOf("010") + 2;
        scentPins[3] = Arduinocommunication.binaryCodes.IndexOf("011") + 2;
        scentPins[4] = Arduinocommunication.binaryCodes.IndexOf("100") + 2;
        scentPins[5] = Arduinocommunication.binaryCodes.IndexOf("101") + 2;
        scentPins[6] = UnityEngine.Random.Range(2, 7);
        scentPins[7] = UnityEngine.Random.Range(2, 7);
        scentPins[8] = UnityEngine.Random.Range(2, 7);

        releaseButton.SetActive(false);
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(false);
        returnButton.SetActive(false);
        startButton.SetActive(true);
        backButton.SetActive(true);
        scentList.SetActive(false);
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

    public void ReleaseAgain()
    {
        again = true;
        StartCoroutine(ReleaseScent());
    }

    public void NextScent()
    {
        releaseButton.SetActive(false);
        nextButton.SetActive(false);
        if (currentScent == 9)
        {
            scentText.gameObject.SetActive(true);
            scentText.text = "End of trial";
            returnButton.SetActive(true);

        }
        else
        {
            currentScent++;
            scentText.gameObject.SetActive(true);
            scentText.text = "Releasing scent " + currentScent;

            StartCoroutine(ReleaseScent());
        }
    }

    IEnumerator ReleaseScent()
    {
        if (again == false){
            randomIndex = UnityEngine.Random.Range(0, 8);
        }
        else
        {
            randomIndex = prev_index;
        }

        again = false;

        releaseButton.SetActive(false);
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(true);
        scentText.text = "Releasing scent " + currentScent;
        activeScent = scentPins[randomIndex];

        message = $"{scentPins[randomIndex]}{on}";
        arduinoComm.SendMessageToArduino(message);

        yield return new WaitForSeconds(5f);

        message = $"{scentPins[randomIndex]}{off}";
        arduinoComm.SendMessageToArduino(message);

        scentText.text = "";
        yield return new WaitForSeconds(1f);

        scentText.text = "Which scent are you smelling?";

        scentList.SetActive(true);
        yield return new WaitUntil(() => scentButtonPressed);
        scentList.SetActive(false);
        scentButtonPressed = false;

        scentText.text = "Clearing scent " + currentScent;
        message = $"{fan}{on}";
        arduinoComm.SendMessageToArduino(message);
        yield return new WaitForSeconds(5f);

        message = $"{fan}{off}";
        arduinoComm.SendMessageToArduino(message);

        scentText.gameObject.SetActive(false);
        releaseButton.SetActive(true);
        nextButton.SetActive(true);

        prev_index = randomIndex;
    }
}
