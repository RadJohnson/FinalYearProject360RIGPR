using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoIconScript : MonoBehaviour
{
    // Start is called before the first frame update

    public string VideoFileURL; // Video File URL
    public string VideoFileName;
    private Button button;
    public TMP_Text videoNameTxt;
    [SerializeField] internal VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
    }


    public void ChooseVideo()
    {
        Debug.Log("PRESSED BUTTON");
        ChosenVideoScript.VideoFilePath = "file://" + VideoFileURL + "//" + VideoFileName;
        Debug.Log(videoPlayer.url);


        // Open waiting room scene
        SceneManager.LoadScene("UI_WaitingRoom", LoadSceneMode.Single);

        //videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        //videoPlayer.EnableAudioTrack(0, true);
       // videoPlayer.Prepare();
       // videoPlayer.Play();
    }
    private void Reset()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ChooseVideo);
    }


}
