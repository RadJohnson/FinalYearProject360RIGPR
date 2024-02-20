using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenVideoController : MonoBehaviour
{

    public string VideoFilePath;

    // Start is called before the first frame update
    void Start()
    {
        VideoFilePath = ChosenVideoScript.VideoFilePath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
