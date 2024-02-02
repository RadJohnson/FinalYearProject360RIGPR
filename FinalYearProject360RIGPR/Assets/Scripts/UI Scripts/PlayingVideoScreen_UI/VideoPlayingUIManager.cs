using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayingUIManager : MonoBehaviour
{

    // VideoPlayer
    [SerializeField] private VideoPlayer videoPlayer;

    // Buttons
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button pauseplayBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button skipfwdBtn;
    [SerializeField] private Button skipbwdBtn;
    [SerializeField] private Button bookmarksBtn;

    public bool isBookmarksOpen;



    // Start is called before the first frame update
    void Start()
    {

        exitBtn.onClick.AddListener(Exit_Video); // Exit button
        pauseplayBtn.onClick.AddListener(Play_Pause); // Play pause button
        bookmarksBtn.onClick.AddListener(OpenCloseBookmarksTab); // Open Bookmarks Tab

        isBookmarksOpen = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_Pause()
    {
        if(videoPlayer.isPlaying) // If video is playing then...
        {
            // Pause Video
            videoPlayer.Pause();
        }
        else
        {
            // Play Video
            videoPlayer.Play();
        }

    }

    public void Exit_Video()
    {
        SceneManager.LoadScene("UI_WaitingRoom");
    }

    public void Restart_Video()
    {
        videoPlayer.Stop(); // Stops the video and not pause?
        videoPlayer.Play(); // Then start the video again from the beginning.
    }

    public void OpenCloseBookmarksTab()
    {
        if(!isBookmarksOpen) 
        {
            // Open Tab
        }
        else
        {
            // Close tab
        }

    }

    public void skipFwd()
    {

    }

    public void skipBwd()
    {

    }

}
