using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class BookmarkIconScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string BookmarkName;
    public TMP_Text BookmarkNameTxtObj;
    public long BookmarkTime;

    public VideoPlayer videoPlayer;

    [SerializeField] private Button JumpToTimeBtn;
    [SerializeField] private Button DeleteBookmarkBtn;


    void Start()
    {

        JumpToTimeBtn.onClick.AddListener(JumpToTime); //Jump to the bookmarks time
        DeleteBookmarkBtn.onClick.AddListener(DeleteBookmark); // Delete bookmark

        videoPlayer = GameObject.Find("VIDEO SPHERE").GetComponent<VideoPlayer>();


        BookmarkNameTxtObj.text = BookmarkName;


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void JumpToTime()
    {
        // Set current video time to bookmarkTime
        videoPlayer.frame = BookmarkTime;

    }

    public void DeleteBookmark()
    {
        Destroy(this.gameObject);
    }



}
