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

    //Book marks tab
    [SerializeField] public GameObject BookmarksTab;

    public GameObject BookmarkIcon;

    public GameObject BookmarksGrid;

    // Input Field
    public InputField BookmarkNameInput;

    // Buttons
    [SerializeField] public Button exitBtn;
    [SerializeField] public Button pauseplayBtn;
    [SerializeField] public Button restartBtn;
    [SerializeField] public Button skipfwdBtn;
    [SerializeField] public Button skipbwdBtn;
    [SerializeField] public Button openbookmarksBtn;
    [SerializeField] public Button addbookmarkBtn;



    public bool isBookmarksOpen;



    // Start is called before the first frame update
    void Start()
    {
        isBookmarksOpen = false;
        BookmarksTab.SetActive(false);

        exitBtn.onClick.AddListener(Exit_Video); // Exit button
        pauseplayBtn.onClick.AddListener(Play_Pause); // Play pause button
        openbookmarksBtn.onClick.AddListener(OpenCloseBookmarksTab); // Open Bookmarks Tab
        addbookmarkBtn.onClick.AddListener(AddBookmark);


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
        Debug.Log("Tried to exit");
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
            BookmarksTab.SetActive(true);
            isBookmarksOpen = true;
        }
        else
        {
            // Close tab
            BookmarksTab.SetActive(false);
            isBookmarksOpen = false;

        }

    }

    public void AddBookmark()
    {

        // Instantiate Prefab

        // Add into ui grid
        GameObject BIcon;
        BIcon = Instantiate(BookmarkIcon);
        BIcon.transform.SetParent(BookmarksGrid.transform);
        BIcon.GetComponent<BookmarkIconScript>().BookmarkName = BookmarkNameInput.text.ToString();
        BookmarkNameInput.text = "";
        // BIcon.GetComponent<BookmarkIconScript>().BookmarkTime = CurrentVideoTime
      



    }

    public void skipFwd()
    {

    }

    public void skipBwd()
    {

    }

  
 
}
