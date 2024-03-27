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
                    string relativePath = GetRelativePathForStreamingAssets(videoPlayer.url);
                    SyncPlayheadClientRpc(relativePath, videoPlayer.time, videoPlayer.isPlaying);
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
            string videoFullPath = Application.streamingAssetsPath + videoUrl;

            // Only update if the URL is different to minimize loading times
            if (!string.IsNullOrEmpty(videoFullPath) && videoPlayer.url != videoFullPath)
            {
                videoPlayer.url = videoFullPath;
            }

            if (videoPlayer.time != time)
                videoPlayer.time = time;

            if (action)
                videoPlayer.Play();
            else
                videoPlayer.Pause();

        }
    }

    private string GetRelativePathForStreamingAssets(string fullPath)
    {
        // Extract the relative path from the full URL
        string keyword = "StreamingAssets";
        int index = fullPath.IndexOf(keyword, System.StringComparison.Ordinal);
        if (index >= 0)
        {
            // +1 to account for the trailing slash or backslash
            return fullPath.Substring(index + keyword.Length + 1);
        }
        return null;
    }

    void Reset()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }
}