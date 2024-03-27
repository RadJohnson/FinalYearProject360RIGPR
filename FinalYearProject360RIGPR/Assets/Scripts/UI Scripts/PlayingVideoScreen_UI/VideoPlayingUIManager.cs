using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using Unity.Netcode;
using System.Collections.Generic;


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
    [SerializeField] public Slider videoSlider;

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
        skipfwdBtn.onClick.AddListener(skipFwd);
        skipbwdBtn.onClick.AddListener(skipBwd);

        //Colour Button Binding
        blueBtn.onClick.AddListener(changeColourBlue);
        redBtn.onClick.AddListener(changeColourRed);
        yellowBtn.onClick.AddListener(changeColourYellow);
        greenBtn.onClick.AddListener(changeColourGreen);
        eraserBtn.onClick.AddListener(eraser);
        clearAllBtn.onClick.AddListener(() =>
        {
            foreach (var item in drawSurfaceScripts)
            {
                clearAll(item);
            }
        });


        isBookmarksOpen = false;

        // Coroutine timer (sets slider params)
        StartCoroutine(StartTimer());
        StopCoroutine(StartTimer());

        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(videoPlayer.url);

        // Create a location for the bookmark file saving
        saveFilePath = Application.streamingAssetsPath + $"/{fileNameWithoutExtension}.json";// see abvout changing this to the streaming assets folder

        //saveFilePath = Application.streamingAssetsPath + ;


        // ~Currently breaks the program so is commented for pushing to branch~ LoadBookmarks();

        //videoSlider.onValueChanged.AddListener(UpdateVideoTimeLine);
    }



    // Update is called once per frame
    void Update()
    {
        videoSlider.value = videoPlayer.frame;//this may ned to cahgne to make the slider work
    }

    void UpdateVideoTimeLine(float sliderValue)
    {
        videoPlayer.frame = (long)sliderValue;
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

        SaveBookmarks();

        NetworkManager.Singleton.SceneManager.LoadScene("MainMenuNetworking", LoadSceneMode.Single);

        //SceneManager.LoadScene("WaitingRoomNetworking");
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
        BIcon.GetComponent<BookmarkIconScript>().bookmarkData.BookmarkNameData = BookmarkNameInput.text;
        BIcon.GetComponent<BookmarkIconScript>().bookmarkData.BookmarkTimeData = videoPlayer.frame;



        Debug.Log(BookmarkNameInput.text);
        BookmarkNameInput.text = "Unnamed";

        // BIcon.GetComponent<BookmarkIconScript>().BookmarkTime = CurrentVideoTime



    }


    private void SaveBookmarks()
    {

        GameObject[] bookmarks = GameObject.FindGameObjectsWithTag("bookmark");
        List<BookmarkIconScript> bookmarkdata = new();
        foreach (var bookmark in bookmarks)
        {
            bookmarkdata.Add(bookmark.GetComponent<BookmarkIconScript>());
        }

        //string numberOfBookmarks = bookmarks.Length.ToString();
        //File.WriteAllText(saveFilePath, numberOfBookmarks);

        for (int i = 0; i < bookmarkdata.Count; i++)
        {
            string bookmarkName = JsonUtility.ToJson(bookmarkdata[i], true);

            File.WriteAllText(saveFilePath, bookmarkName);
        }



        //string baseFilePath = Path.Combine(Application.persistentDataPath, SceneManager.GetActiveScene().name);
        //if (_saveInFolder)
        //{
        //    baseFilePath = Path.Combine(baseFilePath, $"Generation_{_generationNumber}");
        //    Directory.CreateDirectory(baseFilePath);
        //}

        //for (int i = 0; i < bookmarks.Length; i++)
        //{
        //    var contentToSave = JsonUtility.ToJson(_nets[i], true);
        //    string filePath = Path.Combine(baseFilePath, $"NeuralNetwork_{i}.json");
        //    File.WriteAllText(filePath, contentToSave);
        //}

    }

    public void LoadBookmarks()
    {
        // For the number of bookmarks

        //foreach (var bookmark in bookmarks)

        string newBookmark = File.ReadAllText(saveFilePath);
        BookmarkIconScript.BookmarkData newBookmarkIconScriptData = JsonUtility.FromJson<BookmarkIconScript.BookmarkData>(newBookmark);

        if (newBookmarkIconScriptData != null)
        {
            GameObject BIcon;
            BIcon = Instantiate(BookmarkIcon);
            BIcon.transform.SetParent(BookmarksGrid.transform);
            BIcon.GetComponent<BookmarkIconScript>().bookmarkData.BookmarkNameData = newBookmarkIconScriptData.BookmarkNameData;
            BIcon.GetComponent<BookmarkIconScript>().bookmarkData.BookmarkTimeData = newBookmarkIconScriptData.BookmarkTimeData;
        }
        //}
    }

    private void skipFwd()
    {
        videoPlayer.frame += 60 * 5;
    }

    private void skipBwd()
    {
        videoPlayer.frame -= 60 * 5;
    }


    private void changeColourRed()
    {
        drawScript.ChangeBrushColour(Color.red);
    }

    private void changeColourYellow()
    {
        drawScript.ChangeBrushColour(Color.yellow);
    }

    private void changeColourBlue()
    {
        drawScript.ChangeBrushColour(Color.blue);
    }

    private void changeColourGreen()
    {
        drawScript.ChangeBrushColour(Color.green);
    }

    private void eraser()
    {
        drawScript.ChangeToEraser();

    }

    private void clearAll(NDrawSurface _drawSurface)
    {
        _drawSurface.Start();
    }


    IEnumerator StartTimer(float countTime = 2.5f)
    {

        yield return new WaitForSeconds(countTime);


        videoSlider.maxValue = videoPlayer.frameCount;
        videoSlider.value = videoPlayer.frame;

    }
}
