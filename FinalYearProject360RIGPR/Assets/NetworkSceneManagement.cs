using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkSceneManagement : MonoBehaviour
{


    private void Start()
    {
        NetworkManager.Singleton.OnClientStarted += () =>
        {
            if (NetworkManager.Singleton.IsHost)
            {
                StartCoroutine(
                SceneSwitch(1, 0, NetworkManager.Singleton.LocalClient.PlayerObject.gameObject)
                    );
            }

            //else if (NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost)
            //{
            //    StartCoroutine(
            //        SceneSwitch(2, 0, NetworkManager.Singleton.LocalClient.PlayerObject.gameObject)
            //        );
            //}
        };
    }

    public IEnumerator SceneSwitch(int loadSceneBuildIndex, int unloadSceneBuildIndex, GameObject _clientToMove)
    {
        // Load the new scene additively
        string sceneToLoadName = SceneUtility.GetScenePathByBuildIndex(loadSceneBuildIndex);
        sceneToLoadName = System.IO.Path.GetFileNameWithoutExtension(sceneToLoadName);
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Additive);
        yield return new WaitUntil(() => loadOperation.isDone);

        // Move the GameObject to the newly loaded scene
        Scene loadedScene = SceneManager.GetSceneByBuildIndex(loadSceneBuildIndex);
        SceneManager.MoveGameObjectToScene(_clientToMove, loadedScene);

        // Unload the old scene
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(unloadSceneBuildIndex);
        yield return new WaitUntil(() => unloadOperation.isDone);
    }
}
