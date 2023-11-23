using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VideoPan360 : MonoBehaviour
{
    //Make a thing wqhere right or lecfft mouse click for dragging camerea to pan roudn 360 video
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //do rotation stuff here

            Input.GetAxis("Mouse X");
            Input.GetAxis("Mouse Y");
        }
    }
}
