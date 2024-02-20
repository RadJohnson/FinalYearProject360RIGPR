using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform platform; 
    public GameObject[] prefabsToCycle; 
    public int numberOfObjects = 3; 
    public float raiseSpeed = 2f; 
    public float pauseTime = 5f; 
    public float spinSpeed = 180f; 
    public float objectOffset = 0.5f; // Vertical offset (i.e how far above the platform) for cycled objects


    private bool isRaising = true;
    private float timeSinceLastPause = 0f;
    private int currentObjectIndex = 0;
    private List<GameObject> objectsToCycle;
    void Start()
    {
        InstantiateObjects();
        objectsToCycle[currentObjectIndex].SetActive(true);
    }

    void Update()
    {
        MovePlatform();
    }

    void InstantiateObjects()
    {
        objectsToCycle = new List<GameObject>();

        // Instantiate and add GameObjects to the list
        for (int i = 0; i < numberOfObjects; i++)
        {
            //Instantiate the current prefab in the array
            GameObject newObject = Instantiate(prefabsToCycle[i % prefabsToCycle.Length], Vector3.zero, Quaternion.identity);
            objectsToCycle.Add(newObject);
            newObject.SetActive(false); 
        }
    }

    void MovePlatform()
    {
        if (isRaising)
        {
            platform.Translate(Vector3.up * raiseSpeed * Time.deltaTime);

            if (platform.position.y >= 0f) // maximum height 
            {
                isRaising = false;
                timeSinceLastPause = Time.time;

            }
        }
        else
        {
            if (Time.time - timeSinceLastPause >= pauseTime)
            {
                platform.Translate(Vector3.down * raiseSpeed * Time.deltaTime);

                if (platform.position.y <= -10f) // starting position
                {
                    
                    SwitchToNextObject();
                    isRaising = true;
                }
            }
        }
        // Position and spin the current attached object while the platform is raising
        if (isRaising || !isRaising)
        {
            Vector3 objectPosition = platform.position + Vector3.up * objectOffset;
            objectsToCycle[currentObjectIndex].transform.position = objectPosition;
            objectsToCycle[currentObjectIndex].transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
    }

    void SwitchToNextObject()
    {
        // Increment the index to switch to the next object in the list
        currentObjectIndex = (currentObjectIndex + 1) % numberOfObjects;

        objectsToCycle[currentObjectIndex].SetActive(true);
    }
}