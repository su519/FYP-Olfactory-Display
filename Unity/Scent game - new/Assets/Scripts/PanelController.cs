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

    private int currentScent = 1;

    void Start()
    {
        releaseButton.SetActive(false);
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(false);
        returnButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        StartCoroutine(ReleaseScent());
    }

    public void ReleaseAgain()
    {
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
        releaseButton.SetActive(false);
        nextButton.SetActive(false);
        scentText.gameObject.SetActive(true);
        scentText.text = "Releasing scent " + currentScent;
        yield return new WaitForSeconds(5f);
        scentText.text = "";
        yield return new WaitForSeconds(1f);
        scentText.text = "Clearing scent " + currentScent;
        yield return new WaitForSeconds(5f);
        scentText.gameObject.SetActive(false);
        releaseButton.SetActive(true);
        nextButton.SetActive(true);
    }
}
