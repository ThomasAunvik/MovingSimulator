using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    Rigidbody[] rigidbodies;
    [SerializeField] private string[] sceneLevels;
    int currentScene;

    void Start()
    {
        instance = this;

        currentScene = -1;
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        if (currentScene < sceneLevels.Length)
        {
            StartCoroutine(LoadNextLevelAsync());
        }
    }

    public void EndGame()
    {
        if(rigidbodies != null)
        {
            for(int i = 0; i < rigidbodies.Length; i++)
            {
                Rigidbody rb = rigidbodies[i];
                rb.useGravity = true;
                rb.isKinematic = false;

                float randomX = UnityEngine.Random.Range(-1, 1);
                float randomY = UnityEngine.Random.Range(-1, 1);
                float randomZ = UnityEngine.Random.Range(-1, 1);
                rb.angularVelocity = new Vector3(randomX, randomY, randomZ);
            }
        }
        StartCoroutine(LoadNextLevelAsync());
    }

    IEnumerator LoadNextLevelAsync()
    {
        currentScene++;
        string level = sceneLevels[currentScene];
        int previousScene = currentScene;
        if(previousScene > 0)
        {
            yield return new WaitForSeconds(1);
            AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneLevels[previousScene]);
            while (!unloadScene.isDone)
            {
                yield return null;
            }
        }

        if (currentScene < sceneLevels.Length - 1)
        {
            AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
            sceneOperation.completed += SceneLoaded;
            while (!sceneOperation.isDone)
            {
                yield return null;
            }
        }
    }

    private void SceneLoaded(AsyncOperation obj)
    {
        rigidbodies = FindObjectsOfType<Rigidbody>();
    }
}
