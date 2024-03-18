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
        SceneManager.LoadScene(5);
    }
    // Open UM P2
    public void OpenUserManualPageTwo()
    {
        SceneManager.LoadScene(6);
    }

    // Open UM P3
    public void OpenUserManualPageThree()
    {
        SceneManager.LoadScene(7);
    }


    public void OpenMainMenu()
    {
        SceneManager.LoadScene(2);
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
