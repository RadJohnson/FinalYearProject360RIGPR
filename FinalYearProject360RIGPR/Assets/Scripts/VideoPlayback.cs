using UnityEngine;
using UnityEngine.Video;

public class VideoPlayback : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            videoPlayer.frame += 60*5;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            videoPlayer.frame -= 60*5;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            if (!videoPlayer.isPaused)
                videoPlayer.Pause();
            else 
                videoPlayer.Play();
        }

    }
  
    void Reset()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
}