using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuUIScript : MonoBehaviour
{

    // Exit Button Functionality
    public void QuitApplication()
    {
        Application.Quit(); // Closes Application
    }

    // Open User Manual
    public void OpenUserManual()
    {
        SceneManager.LoadScene("UI_UserManual", LoadSceneMode.Single);
    }


    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenuNetworking", LoadSceneMode.Single);
    }

    public void OpenUserManualPage2()
    {
        SceneManager.LoadScene("UI_UserManualPageTwo", LoadSceneMode.Single);
    }

    public void OpenUserManualPage3()
    {
        SceneManager.LoadScene("UI_UserManualPageThree", LoadSceneMode.Single);
    }
}
