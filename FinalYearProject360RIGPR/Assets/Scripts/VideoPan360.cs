using UnityEngine;

public class VideoPan360 : MonoBehaviour
{
    private Vector2 cameraRotation;
    [SerializeField, Tooltip("Y is minimum camera angle and Y is maximum camera angle")] private Vector2 cameraClampYAxis;

    void Update()//may need to clamp the mouse to the window
    {
        if (Input.GetMouseButton(0))// hold left mouse to allow fo thte inputs to work
        {
            cameraRotation += new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            cameraRotation.y = Mathf.Clamp(cameraRotation.y, cameraClampYAxis.x, cameraClampYAxis.y);
        }

        transform.localRotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0f);
    }


    private void Reset()
    {
        cameraClampYAxis = new Vector2(-90f, 90f);
    }
}
