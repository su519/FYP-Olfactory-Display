using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_menu : MonoBehaviour
{
    public GameObject MainSection;
    public GameObject UserStudySection;
    public static int state = 0;

    public void Start()
    {
       if(state == 0)
        {
            MainSection.SetActive(true);
            UserStudySection.SetActive(false);
        }
        else
        {
            StartUserStudy();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scent Room");
    }


    public void StartUserStudy()
    {

        MainSection.SetActive(false);
        UserStudySection.SetActive(true);

    }

    public void BackToMainMenu()
    {
        MainSection.SetActive(true);
        UserStudySection.SetActive(false);
    }
}
