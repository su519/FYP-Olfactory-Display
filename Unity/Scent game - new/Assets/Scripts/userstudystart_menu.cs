using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class userstudystart_menu : MonoBehaviour
{

    public void switchToUserStudy()
    {
        start_menu.state = 1;
        SceneManager.LoadScene("Start Screen");
    }
}
