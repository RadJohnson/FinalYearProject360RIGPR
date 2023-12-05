using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoIconScript : MonoBehaviour
{
    // Start is called before the first frame update

    public string VideoFileURL; // Video File URL
    public string VideoFileName;
    private Button button;
    [SerializeField] internal VideoPlayer videoPlayer;
    [SerializeField] internal VideoClip vidclip;

    private void Awake()
    {
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
    }


    public void ChooseVideo()
    {
        Debug.Log("PRESSED BUTTON");
        videoPlayer.url = "file://" + VideoFileURL + "//" + VideoFileName;
        Debug.Log(videoPlayer.url);
        videoPlayer.clip = vidclip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.Prepare();
        videoPlayer.Play();
    }
    private void Reset()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ChooseVideo);
    }
}
