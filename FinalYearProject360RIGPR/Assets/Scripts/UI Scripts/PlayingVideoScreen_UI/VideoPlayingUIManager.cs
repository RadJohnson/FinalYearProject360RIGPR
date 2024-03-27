using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Unity.Netcode;
using System.Collections.Generic;
using System;


public class VideoPlayingUIManager : MonoBehaviour
{

    // VideoPlayer
    [SerializeField] private VideoPlayer videoPlayer;

    //Draw Script
    [Space(10), Header("Annotation Components")]
    [SerializeField] public NDraw drawScript;
    [SerializeField] public NDrawSurface[] drawSurfaceScripts;

    //Book marks tab
    [Space(10), Header("Bookmark System Components")]
    [SerializeField] public GameObject BookmarksTab;

    public GameObject BookmarkIcon;
    public GameObject BookmarksGrid;

    public bool hasPlayedForFirstTime = false;

    // Input Field
    public TMP_InputField BookmarkNameInput;
    [SerializeField] public Button openbookmarksBtn;
    [SerializeField] public Button addbookmarkBtn;

    // Buttons
    [Space(10), Header("VideoPlayer UI")]
    [SerializeField] public Button exitBtn;
    [SerializeField] public Button pauseplayBtn;
    [SerializeField] public Button restartBtn;
    [SerializeField] public Button skipfwdBtn;
    [SerializeField] public Button skipbwdBtn;
    //Slider
    [SerializeField] public Slider timeLineSlider;

    //Color Buttons
    [Space(10), Header("AnnotationButtons")]
    [SerializeField] public Button yellowBtn;
    [SerializeField] public Button blueBtn;
    [SerializeField] public Button redBtn;
    [SerializeField] public Button greenBtn;
    // Erasing Annotations
    [SerializeField] public Button eraserBtn;
    [SerializeField] public Button clearAllBtn;

    [Space(10), Header("????")]
    //Save FIle
    public string saveFilePath;


    public bool isBookmarksOpen;



    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = FindAnyObjectByType<VideoPlayer>();

        // Set video player url
        videoPlayer.url = ChosenVideoScript.VideoFilePath;

        videoPlayer.prepareCompleted += OnVideoPrepared;

        BookmarkNameInput = GameObject.Find("BookmarkNameInputField").GetComponent<TMP_InputField>();

        isBookmarksOpen = false;
        BookmarksTab.SetActive(false);

        drawSurfaceScripts = FindObjectsOfType<NDrawSurface>();//needed to do this to facilitate a solution to make everything work as needed easily

        // Bind Buttons
        restartBtn.onClick.AddListener(Restart_Video);
        exitBtn.onClick.AddListener(Exit_Video); // Exit button
        pauseplayBtn.onClick.AddListener(Play_Pause); // Play pause button
        openbookmarksBtn.onClick.AddListener(OpenCloseBookmarksTab); // Open Bookmarks Tab
        addbookmarkBtn.onClick.AddListener(AddBookmark);
        skipfwdBtn.onClick.AddListener(SkipFwd);
        skipbwdBtn.onClick.AddListener(SkipBwd);

        //Colour Button Binding
        blueBtn.onClick.AddListener(ChangeColourBlue);
        redBtn.onClick.AddListener(ChangeColourRed);
        yellowBtn.onClick.AddListener(ChangeColourYellow);
        greenBtn.onClick.AddListener(ChangeColourGreen);
        eraserBtn.onClick.AddListener(Eraser);
        clearAllBtn.onClick.AddListener(() =>
        {
            foreach (var item in drawSurfaceScripts)
            {
                ClearAll(item);
            }
        });


        isBookmarksOpen = false;

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(videoPlayer.url);
        // Create a location for the bookmark file saving
        saveFilePath = Application.streamingAssetsPath + $"/{fileNameWithoutExtension}";

        if (Directory.Exists(saveFilePath))
        {
            LoadBookmarks();
        }
    }

    void Update()
    {
        timeLineSlider.value = (float)videoPlayer.time;
    }

    public void Play_Pause()
    {
        if (videoPlayer.isPlaying)
        {
            // Pause Video
            videoPlayer.Pause();
        }
        else
        {
            // Play Video

            videoPlayer.Play();

            if (!hasPlayedForFirstTime)
            {
                timeLineSlider.maxValue = videoPlayer.frameCount;
                timeLineSlider.value = videoPlayer.frame;

                hasPlayedForFirstTime = true;
            }

        }

    }

    public void Exit_Video()
    {
        SaveBookmarks();

        NetworkManager.Singleton.SceneManager.LoadScene("MainMenuNetworking", LoadSceneMode.Single);
        Debug.Log("Tried to exit");
    }

    public void Restart_Video()
    {
        if (videoPlayer.time != 0)
        {
            videoPlayer.time = 0;
        }
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
        BIcon.GetComponent<BookmarkIconScript>().bookmarkData.BookmarkNameData = BookmarkNameInput.text;
        BIcon.GetComponent<BookmarkIconScript>().bookmarkData.BookmarkTimeData = videoPlayer.time;



        Debug.Log(BookmarkNameInput.text);
        BookmarkNameInput.text = "Unnamed";
    }

    private void SaveBookmarks()
    {

        GameObject[] bookmarks = GameObject.FindGameObjectsWithTag("bookmark");
        List<BookmarkIconScript> bookmarkdata = new();
        foreach (var bookmark in bookmarks)
        {
            bookmarkdata.Add(bookmark.GetComponent<BookmarkIconScript>());
        }

        Directory.CreateDirectory(saveFilePath);

        for (int i = 0; i < bookmarkdata.Count; i++)
        {
            string bookmarkName = JsonUtility.ToJson(bookmarkdata[i].bookmarkData, true);

            string filePath = Path.Combine(saveFilePath, $"BookMark{i}.json");
            File.WriteAllText(filePath, bookmarkName);
        }
    }

    public void LoadBookmarks()
    {
        string[] fileEntries = Directory.GetFiles(saveFilePath, "*.json");
        foreach (string fileName in fileEntries)
        {
            string fileContents = File.ReadAllText(fileName);
            BookmarkIconScript bookmark = new BookmarkIconScript();

            bookmark.bookmarkData = JsonUtility.FromJson<BookMarkData>(fileContents);

            GameObject BIcon;
            BIcon = Instantiate(BookmarkIcon);
            BIcon.transform.SetParent(BookmarksGrid.transform);
            BIcon.GetComponent<BookmarkIconScript>().bookmarkData = bookmark.bookmarkData;
        }
    }


    private void SkipFwd()
    {
        videoPlayer.time += 5;
    }

    private void SkipBwd()
    {
        videoPlayer.time -= 5;
    }


    private void ChangeColourRed()
    {
        drawScript.ChangeBrushColour(Color.red);
    }

    private void ChangeColourYellow()
    {
        drawScript.ChangeBrushColour(Color.yellow);
    }

    private void ChangeColourBlue()
    {
        drawScript.ChangeBrushColour(Color.blue);
    }

    private void ChangeColourGreen()
    {
        drawScript.ChangeBrushColour(Color.green);
    }

    private void Eraser()
    {
        drawScript.ChangeToEraser();
    }

    private void ClearAll(NDrawSurface _drawSurface)
    {
        _drawSurface.Start();
    }

    void OnVideoPrepared(VideoPlayer source)
    {
        timeLineSlider.minValue = 0;
        timeLineSlider.maxValue = (float)videoPlayer.length;
    }
}
