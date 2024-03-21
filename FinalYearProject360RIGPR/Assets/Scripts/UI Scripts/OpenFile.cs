using AnotherFileBrowser.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class OpenFile : MonoBehaviour
{
    public RawImage rawImage;
    private string VideoFolderpath = Application.streamingAssetsPath; /* @"D:\TestVideoFolder"; // Video Folder Path */
    public string path;
    public GameObject VideoUIIcon;
    public GameObject VideoGrid;

    public string VideoFileURL; // Video File URL
    public string VideoFileName;
    private Button button;


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
            VIcon.GetComponent<VideoIconScript>().videoNameTxt.text = f.Name.ToString();
       

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
            ChosenVideoScript.VideoFilePath = "file://" + path;
            SceneManager.LoadScene("WaitingRoomNetworking");
        });
    }
}