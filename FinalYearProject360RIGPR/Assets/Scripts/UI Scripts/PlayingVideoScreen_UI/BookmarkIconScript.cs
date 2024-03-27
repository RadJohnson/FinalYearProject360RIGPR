using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using System;
using Unity.VisualScripting;

[Serializable]
public class BookmarkIconScript : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private Button JumpToTimeBtn;
    private Button DeleteBookmarkBtn;
    private TMP_Text BookmarkNameText;
    private TMP_Text BookmarkTimeText;

    [SerializeField] internal BookMarkData bookmarkData;

    internal void Start()
    {
        var buttons = GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(JumpToTime); //Jump to the bookmarks time
        buttons[1].onClick.AddListener(DeleteBookmark); // Delete bookmark

        videoPlayer = GameObject.Find("VIDEO SPHERE").GetComponent<VideoPlayer>();

        var text = GetComponentsInChildren<TMP_Text>();
        text[0].text = bookmarkData.BookmarkNameData;


        TimeSpan timeSpan = TimeSpan.FromSeconds(bookmarkData.BookmarkTimeData);
        text[1].text = timeSpan.ToString("mm':'ss");
    }

    private void JumpToTime()
    {
        // Set current video time to bookmarkTime
        if (videoPlayer.time != bookmarkData.BookmarkTimeData)
        {
            videoPlayer.time = bookmarkData.BookmarkTimeData;
        }
    }

    private void DeleteBookmark()
    {
        Destroy(this.gameObject);
    }
}
