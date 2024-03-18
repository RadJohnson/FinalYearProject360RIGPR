using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuUIScript : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    
    }


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
        SceneManager.LoadScene("UI_MainMenu", LoadSceneMode.Single);
    }

    public void OpenUserManualPage2()
    {
        SceneManager.LoadScene("UI_UserManualPageTwo", LoadSceneMode.Single);
    }

    public void OpenUserManualPage3()
    {
        SceneManager.LoadScene("UI_UserManualPageThree", LoadSceneMode.Single);
    }
    
    // Loading Video Files
    


            // Choosing Video File

    //Choose Video from list
    public void ChooseVideo()
    {
        //Videoplayer URL = Video File URL

    }

    // Open Other Video File
    public void OpenOtherVideoFile()
    {

    }




}
