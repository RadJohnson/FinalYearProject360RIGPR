using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class BookmarkIconScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string BookmarkName;
    public TMP_Text BookmarkNameTxtObj;
    public float BookmarkTime;

    [SerializeField] private Button JumpToTimeBtn;
    [SerializeField] private Button DeleteBookmarkBtn;


    void Start()
    {

        JumpToTimeBtn.onClick.AddListener(JumpToTime); //Jump to the bookmarks time
        DeleteBookmarkBtn.onClick.AddListener(DeleteBookmark); // Delete bookmark

        BookmarkNameTxtObj.text = BookmarkName;


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void JumpToTime()
    {
        // Set current video time to bookmarkTime
    }

    public void DeleteBookmark()
    {
        Destroy(this.gameObject);
    }



}
