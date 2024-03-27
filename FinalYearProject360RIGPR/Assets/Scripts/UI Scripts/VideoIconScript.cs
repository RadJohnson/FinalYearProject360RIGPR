using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Unity.Netcode;

public class VideoIconScript : MonoBehaviour
{
    // Start is called before the first frame update

    public string VideoFileURL; // Video File URL
    public string VideoFileName;
    private Button button;
    public TMP_Text videoNameTxt;


    public void ChooseVideo()
    {
        Debug.Log("PRESSED BUTTON");
        ChosenVideoScript.VideoFilePath = "file://" + VideoFileURL + "//" + VideoFileName;
        ChosenVideoScript.VideoFileName = VideoFileName;

        // Open waiting room scene
        //SceneManager.LoadScene("WaitingRoomNetworking", LoadSceneMode.Single);

        //NetworkManager.Singleton.NetworkConfig.EnableSceneManagement = true;
        //SceneManager.LoadScene("WaitingRoomNetworking", LoadSceneMode.Single);
        NetworkManager.Singleton.SceneManager.LoadScene("360VideoUINetworking", LoadSceneMode.Single);
    }
    private void Reset()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(ChooseVideo);
    }


}
