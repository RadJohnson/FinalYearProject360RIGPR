using AnotherFileBrowser.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;


public class OpenFile : MonoBehaviour
{
    public RawImage rawImage;
    private string VideoFolderpath = @"C:\TestVideoFolder"; // Video Folder Path 
    public string path;
    public GameObject VideoUIIcon;
    public GameObject VideoGrid;

    public string VideoFileURL; // Video File URL
    public string VideoFileName;
    private Button button;
    [SerializeField] internal VideoPlayer videoPlayer;


    private void Awake()
    {
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
    }
    void Start()
    {

        DirectoryInfo dir = new DirectoryInfo(VideoFolderpath);     // Create info for the given video folder path
        FileInfo[] info = dir.GetFiles("*.*");                // store files in folder as an array called fileinfo
        foreach (FileInfo f in info)
        {

            // Add into ui grid
            GameObject VIcon;
            VIcon = Instantiate(VideoUIIcon);
            VIcon.transform.SetParent(VideoGrid.transform);
            VIcon.GetComponent<VideoIconScript>().VideoFileURL = f.Directory.ToString();
            VIcon.GetComponent<VideoIconScript>().VideoFileName = f.Name;
            Debug.Log(VIcon.GetComponent<VideoIconScript>().VideoFileURL);

    


            //Get Video Thumbnail
            // VIcon.GetComponent<Image>().sprite = f.OpenRead().;

        }


    }


    public void OpenFileBrowser()
    {

        var bp = new BrowserProperties();
        //string path;
        bp.filter = "Video files | *.mp4; *.AVI";   // FILTER TO ONLY SEE VIDEOS IN EXPLORER
        bp.filterIndex = 0;
        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            Debug.Log(path);
            videoPlayer.url = "file://" + path;
            Debug.Log(videoPlayer.url);
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.EnableAudioTrack(0, true);
            videoPlayer.Prepare();
            videoPlayer.Play();

        });


    }






}








