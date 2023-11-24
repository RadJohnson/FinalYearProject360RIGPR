using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class OpenFile : MonoBehaviour
{
    public RawImage rawImage;
    
    // Video stuff
    public VideoPlayer videoPlayer;
    public string videoURL;


    public void OpenFileBrowser()
    {
    
       var bp = new BrowserProperties();
       string path;
       bp.filter = "Video files | *.mp4; *.AVI";
       //bp.filter = "Image files | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
       bp.filterIndex = 0;

       new FileBrowser().OpenFileBrowser(bp, path =>
       {
        StartCoroutine(LoadImage(path)); 
       });
    

    IEnumerator LoadImage(string path)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path) )
        {

            yield return uwr.SendWebRequest();

            if(uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                //var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                //rawImage.texture = uwrTexture;
                VideoHandling(path);
            }

        }

    }
    }

        public void VideoHandling(string path)
    {
        videoPlayer.url = path;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack (0, true);
        videoPlayer.Prepare ();
    }

  


}








