using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MovingSim
{

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;

        Rigidbody[] rigidbodies;
        [SerializeField] private string[] sceneLevels;
        [SerializeField] private string mainMenuScene;
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
            if (rigidbodies != null)
            {
                for (int i = 0; i < rigidbodies.Length; i++)
                {
                    Rigidbody rb = rigidbodies[i];
                    if (rb == null) continue;
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
            int previousScene = currentScene - 1;
            if (previousScene >= 0)
            {
                yield return new WaitForSeconds(3);
                AsyncOperation unloadScene = SceneManager.UnloadSceneAsync(sceneLevels[previousScene]);
                while (!unloadScene.isDone)
                {
                    yield return null;
                }
            }

            if (currentScene < sceneLevels.Length)
            {
                string level = sceneLevels[currentScene];
                AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
                sceneOperation.completed += SceneLoaded;
                while (!sceneOperation.isDone)
                {
                    yield return null;
                }
            }
            else
            {
                string level = mainMenuScene;
                AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
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
}