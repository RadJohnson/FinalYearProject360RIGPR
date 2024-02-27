using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using UnityEditor;
using Unity.VisualScripting;
using System.IO;
using System.Windows.Forms;

public class VideoPlayingUIManager : MonoBehaviour
{

    // VideoPlayer
    [SerializeField] private VideoPlayer videoPlayer;

    //Draw Script
    [SerializeField] public Draw drawScript;
    [SerializeField] public DrawSurface drawSurfaceScript;

    //Book marks tab
    [SerializeField] public GameObject BookmarksTab;

    public GameObject BookmarkIcon;
    public GameObject BookmarksGrid;

    // Input Field
    public TMP_InputField BookmarkNameInput;

    // Buttons
    [SerializeField] public Button exitBtn;
    [SerializeField] public Button pauseplayBtn;
    [SerializeField] public Button restartBtn;
    [SerializeField] public Button skipfwdBtn;
    [SerializeField] public Button skipbwdBtn;
    [SerializeField] public Button openbookmarksBtn;
    [SerializeField] public Button addbookmarkBtn;
    //Color Buttons
    [SerializeField] public Button yellowBtn;
    [SerializeField] public Button blueBtn;
    [SerializeField] public Button redBtn;
    [SerializeField] public Button greenBtn;
    // Erasing Annotations
    [SerializeField] public Button eraserBtn;
    [SerializeField] public Button clearAllBtn;
    //Slider
    [SerializeField] public Slider videoSlider;

    //Save FIle
    public string saveFilePath;


    public bool isBookmarksOpen;



    // Start is called before the first frame update
    void Start()
    {
        // Set video player url
        videoPlayer.url = ChosenVideoScript.VideoFilePath;

        BookmarkNameInput = GameObject.Find("BookmarkNameInputField").GetComponent<TMP_InputField>();

        isBookmarksOpen = false;
        BookmarksTab.SetActive(false);

        // Bind Buttons
        restartBtn.onClick.AddListener(Restart_Video);
        exitBtn.onClick.AddListener(Exit_Video); // Exit button
        pauseplayBtn.onClick.AddListener(Play_Pause); // Play pause button
        openbookmarksBtn.onClick.AddListener(OpenCloseBookmarksTab); // Open Bookmarks Tab
        addbookmarkBtn.onClick.AddListener(AddBookmark);
        skipfwdBtn.onClick.AddListener(skipFwd);
        skipbwdBtn.onClick.AddListener(skipBwd);
        //Colour Button Binding
        blueBtn.onClick.AddListener(changeColourBlue);
        redBtn.onClick.AddListener(changeColourRed);
        yellowBtn.onClick.AddListener(changeColourYellow);
        greenBtn.onClick.AddListener(changeColourGreen);
        eraserBtn.onClick.AddListener(eraser);
        clearAllBtn.onClick.AddListener(clearAll);


        isBookmarksOpen = false;

        // Coroutine timer (sets slider params)
        StartCoroutine(StartTimer());
        StopCoroutine(StartTimer());

        // Create a location for the bookmark file saving
        saveFilePath = Application.persistentDataPath + "/BoomarkData.json";

    }



    // Update is called once per frame
    void Update()
    {
        videoSlider.value = videoPlayer.frame;
    }

    public void Play_Pause()
    {
        if (videoPlayer.isPlaying) // If video is playing then...
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
        videoPlayer.frame = 0;

    }

    public void OpenCloseBookmarksTab()
    {
        if (!isBookmarksOpen)
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
        BIcon.GetComponent<BookmarkIconScript>().BookmarkName = BookmarkNameInput.text;
        BIcon.GetComponent<BookmarkIconScript>().BookmarkTime = videoPlayer.frame;



        Debug.Log(BookmarkNameInput.text);
        BookmarkNameInput.text = "Unnamed";

        // BIcon.GetComponent<BookmarkIconScript>().BookmarkTime = CurrentVideoTime

    }


    public void SaveBookmarks()
    {

        var bookmarks = GameObject.FindGameObjectsWithTag("bookmark");
        string numberOfBookmarks = bookmarks.Length.ToString();
        File.WriteAllText(saveFilePath, numberOfBookmarks);

        foreach (var bookmark in bookmarks)
        {
            string bookmarkName = JsonUtility.ToJson(bookmark.GetComponent<BookmarkIconScript>().BookmarkName);
            string bookmarkTime = JsonUtility.ToJson(bookmark.GetComponent<BookmarkIconScript>().BookmarkTime.ToString());
           
            File.WriteAllText(saveFilePath, bookmarkName + bookmarkTime);
            

        }
    }

    public void LoadBookmarks()
    {

        // For the number of bookmarks
        foreach (var bookmark in bookmarks)
        {
          
            //Instantiate a bookmark object
            // Set the name and set the time
            

        }
    }

    public void skipFwd()
    {
        videoPlayer.frame += 60 * 5;

    }

    public void skipBwd()
    {
        videoPlayer.frame -= 60 * 5;
    }


    public void changeColourRed()
    {
        drawScript.ChangeColour(Color.red);
    }

    public void changeColourYellow()
    {
        drawScript.ChangeColour(Color.yellow);
    }

    public void changeColourBlue()
    {
        drawScript.ChangeColour(Color.blue);
    }

    public void changeColourGreen()
    {
        drawScript.ChangeColour(Color.green);
    }

    public void eraser()
    {
        drawScript.ChangeToEraser();

    }

    public void clearAll()
    {
        drawSurfaceScript.Start();
    }


    IEnumerator StartTimer(float countTime = 1f)
    {

        yield return new WaitForSeconds(countTime);


        videoSlider.maxValue = videoPlayer.frameCount;
        videoSlider.value = videoPlayer.frame;
    }


}
