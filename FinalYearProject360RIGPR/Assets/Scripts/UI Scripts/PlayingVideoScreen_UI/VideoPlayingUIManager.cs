using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class VideoPlayingUIManager : MonoBehaviour
{

    [SerializeField] private Button exitBtn;



    // Start is called before the first frame update
    void Start()
    {
        exitBtn.onClick.AddListener(Exit_Video);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play_Pause()
    {

    }

    public void Exit_Video()
    {

    }

}
