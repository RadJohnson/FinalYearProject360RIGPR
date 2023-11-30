using AnotherFileBrowser.Windows;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;


public class OpenFile : MonoBehaviour
{
    public RawImage rawImage;
    private string VideoFolderpath = @"C:\TestVideoFolder"; // Video Folder Path 

    public GameObject VideoUIIcon;
    public GameObject VideoGrid;
    
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

                    //Get Video Thumbnail
            // VIcon.GetComponent<Image>().sprite = f.OpenRead().;

        }


    }


    public void OpenFileBrowser()
    {

        var bp = new BrowserProperties();
        string path;
        bp.filter = "Video files | *.mp4; *.AVI";   // FILTER TO ONLY SEE VIDEOS IN EXPLORER
        bp.filterIndex = 0;

        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            Debug.Log(path);
        });

    }






}








