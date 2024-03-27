using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using System;

[Serializable]
public class BookmarkIconScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string BookmarkName;
    public TMP_Text BookmarkNameText;
    public TMP_Text BookmarkTimeText;
    [SerializeField] public long BookmarkTime;

    public VideoPlayer videoPlayer;

    [SerializeField] private Button JumpToTimeBtn;
    [SerializeField] private Button DeleteBookmarkBtn;

    [Serializable]
    public class BookmarkData
    {
        [SerializeField] public string BookmarkNameData;
        [SerializeField] public long BookmarkTimeData;
    }
    [SerializeField] public BookmarkData bookmarkData;

    void Start()
    {
        JumpToTimeBtn.onClick.AddListener(JumpToTime); //Jump to the bookmarks time
        DeleteBookmarkBtn.onClick.AddListener(DeleteBookmark); // Delete bookmark
        videoPlayer = GameObject.Find("VIDEO SPHERE").GetComponent<VideoPlayer>();
        BookmarkNameText.text = bookmarkData.BookmarkNameData;
        TimeSpan timeSpan = TimeSpan.FromSeconds(bookmarkData.BookmarkTimeData);
        BookmarkTimeText.text = timeSpan.ToString("mm':'ss");
    }

    public void JumpToTime()
    {
        // Set current video time to bookmarkTime
        videoPlayer.time = bookmarkData.BookmarkTimeData;
    }

    public void DeleteBookmark()
    {
        Destroy(this.gameObject);
    }
}
