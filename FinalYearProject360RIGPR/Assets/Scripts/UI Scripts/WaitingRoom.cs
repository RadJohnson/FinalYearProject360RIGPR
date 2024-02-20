using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaitingRoom : MonoBehaviour
{

    public string VideoFilePath;
    public TMP_Text VideoName;
    // Start is called before the first frame update
    void Start()
    {
        VideoFilePath = ChosenVideoScript.VideoFilePath;
        VideoName.text = VideoFilePath;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSession()
    {
        // Open video playing scene
        SceneManager.LoadScene("VideoPlayingScene", LoadSceneMode.Single);
    }

  
}
