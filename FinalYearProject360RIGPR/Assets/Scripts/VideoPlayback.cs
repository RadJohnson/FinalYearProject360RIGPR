using UnityEngine;
using UnityEngine.Video;
using Unity.Netcode;

public class VideoPlayback : NetworkBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private float updateRate = 2.0f;
    private float timeSinceLastUpdate = 0.0f;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        // Host sends out current playhead position
        if (IsClient)
        {
            if (IsHost)
            {
                timeSinceLastUpdate += Time.deltaTime;
                if (timeSinceLastUpdate >= updateRate)
                {
                    SyncPlayheadClientRpc(videoPlayer.url, videoPlayer.time, videoPlayer.isPlaying);
                    timeSinceLastUpdate = 0.0f;
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    videoPlayer.frame += 60*5;
        //}
        //else if (Input.GetKeyDown(KeyCode.J))
        //{
        //    videoPlayer.frame -= 60*5;
        //}
        //else if (Input.GetKeyDown(KeyCode.K))
        //{
        //    if (!videoPlayer.isPaused)
        //        videoPlayer.Pause();
        //    else 
        //        videoPlayer.Play();
        //}
    }


    [ClientRpc]
    private void SyncPlayheadClientRpc(string videoUrl, double time, bool action)
    {
        if (!IsHost && videoPlayer)
        {
            if (videoPlayer.url == null || videoPlayer.url == "")
                videoPlayer.url = videoUrl;

            if (videoPlayer.time != time)
                videoPlayer.time = time;

            if (action)
                videoPlayer.Play();
            else
                videoPlayer.Pause();

        }
    }

    void Reset()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
}