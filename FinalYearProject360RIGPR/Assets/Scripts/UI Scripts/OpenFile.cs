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
    public string VideoFolderpath = @"C:\TestVideoFolder"; // Video Folder Path 
    
    void Start()
    {


        DirectoryInfo dir = new DirectoryInfo(VideoFolderpath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            Debug.Log(f.Extension);
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








